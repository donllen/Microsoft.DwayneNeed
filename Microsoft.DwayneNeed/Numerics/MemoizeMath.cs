using System;

namespace Microsoft.DwayneNeed.Numerics
{
    // A simple class that memoizes the results of the Math class.
    public class MemoizeMath
    {
        private double? _abs;

        private double? _acos;

        private double? _asin;

        private double? _atan;

        private double? _ceiling;

        private double? _cos;

        private double? _cosh;

        private double? _exp;

        private double? _floor;

        private double? _log10;

        private int? _sign;

        private double? _sin;

        private double? _sinh;

        private double? _sqrt;

        private double? _tan;

        private double? _tanh;

        public MemoizeMath(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public double Abs
        {
            get
            {
                if (!_abs.HasValue) _abs = Math.Abs(Value);

                return _abs.Value;
            }
        }

        public double ACos
        {
            get
            {
                if (!_acos.HasValue) _acos = Math.Acos(Value);

                return _acos.Value;
            }
        }

        public double ASin
        {
            get
            {
                if (!_asin.HasValue) _asin = Math.Asin(Value);

                return _asin.Value;
            }
        }

        public double ATan
        {
            get
            {
                if (!_atan.HasValue) _atan = Math.Atan(Value);

                return _atan.Value;
            }
        }

        public double Ceiling
        {
            get
            {
                if (!_ceiling.HasValue) _ceiling = Math.Ceiling(Value);

                return _ceiling.Value;
            }
        }

        public double Cos
        {
            get
            {
                if (!_cos.HasValue) _cos = Math.Cos(Value);

                return _cos.Value;
            }
        }

        public double CosH
        {
            get
            {
                if (!_cosh.HasValue) _cosh = Math.Cosh(Value);

                return _cosh.Value;
            }
        }

        public double Exp
        {
            get
            {
                if (!_exp.HasValue) _exp = Math.Exp(Value);

                return _exp.Value;
            }
        }

        public double Floor
        {
            get
            {
                if (!_floor.HasValue) _floor = Math.Floor(Value);

                return _floor.Value;
            }
        }

        public double Log10
        {
            get
            {
                if (!_log10.HasValue) _log10 = Math.Log10(Value);

                return _log10.Value;
            }
        }

        public int Sign
        {
            get
            {
                if (!_sign.HasValue) _sign = Math.Sign(Value);

                return _sign.Value;
            }
        }

        public double Sin
        {
            get
            {
                if (!_sin.HasValue) _sin = Math.Sin(Value);

                return _sin.Value;
            }
        }

        public double SinH
        {
            get
            {
                if (!_sinh.HasValue) _sinh = Math.Sinh(Value);

                return _sinh.Value;
            }
        }

        public double Sqrt
        {
            get
            {
                if (!_sqrt.HasValue) _sqrt = Math.Sqrt(Value);

                return _sqrt.Value;
            }
        }

        public double Tan
        {
            get
            {
                if (!_tan.HasValue) _tan = Math.Tan(Value);

                return _tan.Value;
            }
        }

        public double TanH
        {
            get
            {
                if (!_tanh.HasValue) _tanh = Math.Tanh(Value);

                return _tanh.Value;
            }
        }
    }
}