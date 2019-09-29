using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Microsoft.DwayneNeed.Win32;
using Microsoft.DwayneNeed.Win32.Kernel32;

namespace Microsoft.DwayneNeed.Threading
{
    public class UIThreadPerfCounters : INotifyPropertyChanged
    {
        private readonly List<UIThreadPerfSample> _samples = new List<UIThreadPerfSample>();

        private double _fps;

        private long _idleCycleTime;
        private TimeSpan _lastRenderingTime = TimeSpan.MinValue;

        private int _numberOfProcessors;

        private long _processCycleTime;

        public UIThreadPerfCounters()
        {
            NumberOfSamples = 10;

            SYSTEM_INFO sysinfo = new SYSTEM_INFO();
            NativeMethods.GetSystemInfo(ref sysinfo);
            _numberOfProcessors = (int) sysinfo.dwNumberOfProcessors;

            DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
            bool isInDesignMode = (bool) DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement))
                .Metadata.DefaultValue;
            if (!isInDesignMode) CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        public int NumberOfProcessors
        {
            get => _numberOfProcessors;

            private set => _numberOfProcessors = value;
        }

        public int NumberOfSamples
        {
            get => _samples.Count();

            set
            {
                int numberOfSamples = value >= 1 ? value : 1;

                if (numberOfSamples != _samples.Count)
                {
                    _samples.Clear();
                    for (int i = 0; i < numberOfSamples; i++) _samples.Add(null);

                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null) handler(this, new PropertyChangedEventArgs("NumberOfSamples"));
                }
            }
        }

        public double FPS
        {
            get => _fps;
            private set
            {
                if (value != _fps)
                {
                    _fps = value;

                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null) handler(this, new PropertyChangedEventArgs("FPS"));
                }
            }
        }

        public long ProcessCycleTime
        {
            get => _processCycleTime;
            private set
            {
                if (value != _processCycleTime)
                {
                    _processCycleTime = value;

                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("ProcessCycleTime"));
                        handler(this, new PropertyChangedEventArgs("ProcessCyclePercentage"));
                        handler(this, new PropertyChangedEventArgs("IdleCyclePercentage"));
                    }
                }
            }
        }

        public long IdleCycleTime
        {
            get => _idleCycleTime;
            private set
            {
                if (value != _idleCycleTime)
                {
                    _idleCycleTime = value;

                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("IdleCycleTime"));
                        handler(this, new PropertyChangedEventArgs("ProcessCyclePercentage"));
                        handler(this, new PropertyChangedEventArgs("IdleCyclePercentage"));
                    }
                }
            }
        }

        public double ProcessCyclePercentage
        {
            get
            {
                // BIG assumption is that the only contributors to this
                // calculation are the activity of this process and the
                // activity of the idle threads.  If other processes are
                // consuming CPU, then this calculation is off.  Then we
                // would have to enumerate all running processes and
                // account for their usage too.
                long total = ProcessCycleTime + IdleCycleTime;
                return total > 0 ? ProcessCycleTime / (double) total : 0;
            }
        }

        public double IdleCyclePercentage
        {
            get
            {
                // BIG assumption is that the only contributors to this
                // calculation are the activity of this process and the
                // activity of the idle threads.  If other processes are
                // consuming CPU, then this calculation is off.  Then we
                // would have to enumerate all running processes and
                // account for their usage too.
                long total = ProcessCycleTime + IdleCycleTime;
                return total > 0 ? IdleCycleTime / (double) total : 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            RenderingEventArgs renderingEventArgs = e as RenderingEventArgs;

            if (renderingEventArgs.RenderingTime > _lastRenderingTime)
            {
                _lastRenderingTime = renderingEventArgs.RenderingTime;

                int frameCount = 1;

                long processCycleTime = 0;
                NativeMethods.QueryProcessCycleTime(new IntPtr(-1), ref processCycleTime);

                long[] idleCycleTimes = new long[_numberOfProcessors];
                int sizeIdleCycleTimes = _numberOfProcessors * Marshal.SizeOf(typeof(long));
                NativeMethods.QueryIdleProcessorCycleTime(ref sizeIdleCycleTimes, idleCycleTimes);
                long idleCycleTime = 0;
                foreach (long time in idleCycleTimes) idleCycleTime += time;

                // Remove the oldest sample.
                _samples.RemoveAt(0);

                // Add a new sample at the end.
                _samples.Add(new UIThreadPerfSample(_lastRenderingTime, frameCount, processCycleTime, idleCycleTime));

                // Update the perf counters by examining all of the samples.
                UpdatePerfCounters();
            }
        }

        private void UpdatePerfCounters()
        {
            TimeSpan? start = null;
            TimeSpan? end = null;
            int frameCount = 0;
            long startProcessCycleTime = 0;
            long startIdleCycleTime = 0;
            long endProcessCycleTime = 0;
            long endIdleCycleTime = 0;

            foreach (UIThreadPerfSample sample in _samples)
                if (sample != null)
                {
                    if (start == null)
                    {
                        start = sample.SampleTime;
                        startProcessCycleTime = sample.ProcessCycleTime;
                        startIdleCycleTime = sample.IdleCycleTime;
                    }
                    else if (sample.SampleTime < start)
                    {
                        start = sample.SampleTime;
                        startProcessCycleTime = sample.ProcessCycleTime;
                        startIdleCycleTime = sample.IdleCycleTime;
                    }

                    if (end == null)
                    {
                        end = sample.SampleTime;
                        endProcessCycleTime = sample.ProcessCycleTime;
                        endIdleCycleTime = sample.IdleCycleTime;
                    }
                    else if (sample.SampleTime > end)
                    {
                        end = sample.SampleTime;
                        endProcessCycleTime = sample.ProcessCycleTime;
                        endIdleCycleTime = sample.IdleCycleTime;
                    }

                    frameCount += sample.FrameCount;
                }

            if (start != null && end != null)
            {
                TimeSpan sampleSpan = end.Value - start.Value;
                FPS = frameCount / sampleSpan.TotalSeconds;
                ProcessCycleTime = endProcessCycleTime - startProcessCycleTime;
                IdleCycleTime = endIdleCycleTime - startIdleCycleTime;
            }
        }
    }
}