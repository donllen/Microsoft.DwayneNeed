namespace Microsoft.DwayneNeed.Geometry
{
    /// <summary>
    ///     A generic interface for q 3-dimensional point.
    /// </summary>
    public interface IPoint3D<T>
    {
        T X { get; }
        T Y { get; }
        T Z { get; }
    }

    /// <summary>
    ///     A simple implementation of a 3-dimensional point.
    /// </summary>
    public struct Point3D<T> : IPoint3D<T>
    {
        public Point3D(T x, T y, T z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public T X { get; }
        public T Y { get; }
        public T Z { get; }
    }
}