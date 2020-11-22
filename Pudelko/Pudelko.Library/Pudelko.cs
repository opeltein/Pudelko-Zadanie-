using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>, IEnumerator<double>
    {
        private readonly double _a = 0.1;
        private readonly double _b = 0.1;
        private readonly double _c = 0.1;
        private readonly UnitOfMeasure _unit;
        private double[] TablicaD => new double[] { A, B, C };

        public double A => PoPrzecinku(_a);
        public double B => PoPrzecinku(_b);
        public double C => PoPrzecinku(_c);

        public double Objetosc => Math.Round((A * B * C), 9);
        public double Pole => Math.Round((2 * A * B + 2 * A * C + 2 * B * C), 6);
                

        public double this[int index]
        {
            get => TablicaD[index];
        }

        private void Check(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException();

            if (a > 10 || b > 10 || c > 10)
                throw new ArgumentOutOfRangeException();
        }

        private double PoPrzecinku(double num)
        {
            return Math.Truncate(num * 1000) / 1000;
        }

      
        #region Constructors
        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.meter)
        {

            _a = NaMetry(a, unit);
            _b = NaMetry(b, unit);
            _c = NaMetry(c, unit);
            _unit = unit;

            Check(A, B, C);

        }

        public Pudelko(double a, double b, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _a = NaMetry(a, unit);
            _b = NaMetry(b, unit);

            Check(A, B, C);
        }

        public Pudelko(double a = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            _a = NaMetry(a, unit);

            Check(A, B, C);
        }

        #endregion
        
        
        #region ToString

        public override string ToString()
        {
            return ToString("m", CultureInfo.GetCultureInfo("en-US"));

        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.GetCultureInfo("en-US"));
        }

        public string ToString(string format, IFormatProvider JakiFormat)
        {
            if (format == null)
            {
                return $"{ A.ToString("F3", JakiFormat) } m \u00D7 { B.ToString("F3", JakiFormat) } m \u00D7 { C.ToString("F3", JakiFormat) } m";

            }
            switch (format.ToUpperInvariant())
            {
                case "M":
                    return $"{ A.ToString("F3", JakiFormat) } { format } \u00D7" +
                        $" { B.ToString("F3", JakiFormat) } { format } \u00D7" +
                        $" { C.ToString("F3", JakiFormat) } { format }";
                case "CM":
                    return $"{ (A * 100).ToString("F1", JakiFormat) } { format } \u00D7" +
                        $" { (B * 100).ToString("F1", JakiFormat) } { format } \u00D7 " +
                        $"{ (C * 100).ToString("F1", JakiFormat) } { format }";
                case "MM":
                    return $"{ A * 1000 } { format } \u00D7" +
                        $" { B * 1000 } { format } \u00D7" +
                        $" { C * 1000 } { format }";
                default:
                    throw new FormatException();
            }
        }

        #endregion

        
        #region Equals

        public bool Equals(Pudelko other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if(obj is Pudelko pudelko)
                return this == pudelko;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        
        #region Operators

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            var pudelkoX1 = new double[] { p1.A, p1.B, p1.C };
             var pudelkoX2 = new double[] { p2.A, p2.B, p2.C };
              var posortowaneX1 = pudelkoX1.OrderBy(x => x);
               var posortowaneX2 = pudelkoX2.OrderBy(x => x);
            return Enumerable.SequenceEqual(posortowaneX1, posortowaneX2);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !(p1 == p2);
        }

        public static Pudelko operator +(Pudelko x1, Pudelko x2)
        {
            var lista = new double[]
            {
                x1.A, x1.B, x1.C, x2.A, x2.B, x2.C
            };
            var sortedArr = lista.OrderByDescending(x => x).ToArray();
            return new Pudelko(sortedArr[0], sortedArr[1], sortedArr[2]);         
        }

        #endregion

        // Enumerator
        #region Enumerator

        public IEnumerator<double> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        int position = -1;
        private readonly Pudelko _p;

        public double Current => this[position];

        object IEnumerator.Current => Current;        

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            position++;

            return (position < 3);
        }

        public void Reset()
        {
            position = -1;
        }
        #endregion

        // Converters
        #region Converters

        private double NaMetry(double number, UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    return number / 1000;
                case UnitOfMeasure.centimeter:
                    return number / 100;
                default:
                    return number;
            }
        }

        public static explicit operator double[](Pudelko p)
        {
            double[] pudelkoArr =
            {
                p.A,
                p.B,
                p.C
            };

            return pudelkoArr;
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> dimensions)
        {
            return new Pudelko(dimensions.Item1, dimensions.Item2, dimensions.Item3, UnitOfMeasure.milimeter);
        }

        #endregion

        // Parse
        #region Parse

        public Pudelko Parse(string stringToParse)
        {
            return Parse(stringToParse, CultureInfo.GetCultureInfo("en-US"));
        }

        public Pudelko Parse(string stringToParse, IFormatProvider format)
        {
            var arr = stringToParse.Split(' ');

            UnitOfMeasure unit;

            switch (arr[1])
            {
                case "cm":
                    unit = UnitOfMeasure.centimeter;
                    break;
                case "mm":
                    unit = UnitOfMeasure.milimeter;
                    break;
                default:
                    unit = UnitOfMeasure.meter;
                    break;
            }

            var a = NaMetry(double.Parse(arr[0], format), unit);
            var b = NaMetry(double.Parse(arr[3], format), unit);
            var c = NaMetry(double.Parse(arr[6], format), unit);

            return new Pudelko(a, b, c);

        }

        #endregion


    }
}
