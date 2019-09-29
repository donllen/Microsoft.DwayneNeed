using System;

namespace Microsoft.DwayneNeed.Threading
{
    /// <summary>
    ///     This class represents a perf sample taken from a UI thread.  The
    ///     time stamp of the same is usually taken from the RenderingEventArgs
    /// </summary>
    internal class UIThreadPerfSample
    {
        public UIThreadPerfSample(TimeSpan sampleTime,
            int frameCount,
            long processCycleTime,
            long idleCycleTime)
        {
            SampleTime = sampleTime;
            FrameCount = frameCount;
            ProcessCycleTime = processCycleTime;
            IdleCycleTime = idleCycleTime;
        }

        public TimeSpan SampleTime { get; }
        public int FrameCount { get; }
        public long ProcessCycleTime { get; }
        public long IdleCycleTime { get; }
    }
}