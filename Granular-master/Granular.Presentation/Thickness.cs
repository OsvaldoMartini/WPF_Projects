﻿using System;
using System.Linq;
using System.Windows.Markup;

namespace System.Windows
{
    [TypeConverter(typeof(ThicknessTypeConverter))]
    public sealed class Thickness
    {
        public static readonly Thickness Zero = new Thickness();

        public double Left { get; private set; }
        public double Top { get; private set; }
        public double Right { get; private set; }
        public double Bottom { get; private set; }

        public Point Location { get; private set; }
        public Size Size { get; private set; }

        public bool IsUniform { get { return Left == Top && Left == Right && Left == Bottom; } }

        public Thickness() :
            this(0, 0, 0, 0)
        {
            //
        }

        public Thickness(double uniformLength) :
            this(uniformLength, uniformLength, uniformLength, uniformLength)
        {
            //
        }

        public Thickness(double leftRight, double topBottom) :
            this(leftRight, topBottom, leftRight, topBottom)
        {
            //
        }

        public Thickness(double left, double top, double right, double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;

            this.Location = left == 0 && top == 0 ? Point.Zero : new Point(left, top);
            this.Size = left + right == 0 && top + bottom == 0 ? Size.Zero : new Size(left + right, top + bottom);
        }

        public override string ToString()
        {
            return IsUniform ?
                String.Format("Thickness({0})", Left) :
                String.Format("Thickness({0}, {1}, {2}, {3})", Top, Right, Bottom, Left);
        }

        public override bool Equals(object obj)
        {
            Thickness other = obj as Thickness;

            return Object.ReferenceEquals(this, other) || !Object.ReferenceEquals(other, null) &&
                Granular.Compatibility.EqualityComparer.Double.Equals(this.Left, other.Left) &&
                Granular.Compatibility.EqualityComparer.Double.Equals(this.Top, other.Top) &&
                Granular.Compatibility.EqualityComparer.Double.Equals(this.Right, other.Right) &&
                Granular.Compatibility.EqualityComparer.Double.Equals(this.Bottom, other.Bottom);
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Top.GetHashCode() ^ Right.GetHashCode() ^ Bottom.GetHashCode();
        }

        public static bool operator ==(Thickness thickness1, Thickness thickness2)
        {
            return Object.Equals(thickness1, thickness2);
        }

        public static bool operator !=(Thickness thickness1, Thickness thickness2)
        {
            return !(thickness1 == thickness2);
        }

        public static Thickness operator -(Thickness thickness)
        {
            if (thickness == Thickness.Zero)
            {
                return thickness;
            }

            return new Thickness(-thickness.Left, -thickness.Top, -thickness.Right, -thickness.Bottom);
        }

        public static Thickness operator +(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == Thickness.Zero)
            {
                return thickness2;
            }

            if (thickness2 == Thickness.Zero)
            {
                return thickness1;
            }

            return new Thickness(thickness1.Left + thickness2.Left, thickness1.Top + thickness2.Top, thickness1.Right + thickness2.Right, thickness1.Bottom + thickness2.Bottom);
        }

        public static Thickness operator -(Thickness thickness1, Thickness thickness2)
        {
            if (thickness1 == Thickness.Zero)
            {
                return -thickness2;
            }

            if (thickness2 == Thickness.Zero)
            {
                return thickness1;
            }

            return new Thickness(thickness1.Left - thickness2.Left, thickness1.Top - thickness2.Top, thickness1.Right - thickness2.Right, thickness1.Bottom - thickness2.Bottom);
        }

        public static Thickness operator *(Thickness thickness, double scalar)
        {
            if (scalar == 1 || ReferenceEquals(thickness, Thickness.Zero))
            {
                return thickness;
            }

            return new Thickness(thickness.Left * scalar, thickness.Top * scalar, thickness.Right * scalar, thickness.Bottom * scalar);
        }

        public static Thickness operator *(double scalar, Thickness thickness)
        {
            return thickness * scalar;
        }

        public static implicit operator Thickness(double uniformLength)
        {
            return new Thickness(uniformLength);
        }

        public static Thickness Parse(string value)
        {
            double[] values = value.Split(',').Select(v => Double.Parse(v)).ToArray();

            if (values.Length == 1)
            {
                return new Thickness(values[0]);
            }

            if (values.Length == 2)
            {
                return new Thickness(values[0], values[1]);
            }

            if (values.Length == 4)
            {
                return new Thickness(values[0], values[1], values[2], values[3]);
            }

            throw new Granular.Exception("Can't parse Thickness value \"{0}\"", value);
        }
    }

    public static class ThicknessExtensions
    {
        public static Thickness DefaultIfNull(this Thickness thickness, Thickness defaultValue = null)
        {
            return thickness ?? defaultValue ?? Thickness.Zero;
        }
    }

    public class ThicknessTypeConverter : ITypeConverter
    {
        public object ConvertFrom(XamlNamespaces namespaces, Uri sourceUri, object value)
        {
            return Thickness.Parse(value.ToString().Trim());
        }
    }
}
