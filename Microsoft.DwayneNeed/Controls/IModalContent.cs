using System.Windows;

namespace Microsoft.DwayneNeed.Controls
{
    public interface IModalContent<T>
    {
        FrameworkElement Content { get; }
        T Accept();
        void Cancel();
    }
}