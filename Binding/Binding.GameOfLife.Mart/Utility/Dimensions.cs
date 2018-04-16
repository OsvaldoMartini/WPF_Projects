using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Binding.GameOfLife.Mart.Converters;

namespace Binding.GameOfLife.Mart.Utility
{
	[TypeConverter(typeof(DimensionsTypeConverter))]	// provided to allow setting of Dimension in XAML.
	public struct Dimensions
	{
		public int Width { get; set; }

		public int Height { get; set; }

		public Dimensions(int width, int height)
			: this()
		{
			Width = width;
			Height = height;
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})", Width, Height);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Dimensions))
				return false;
			Dimensions dim = (Dimensions)obj;
			return this.Width == dim.Width && this.Height == dim.Height;
		}

		public override int GetHashCode()
		{
			return Width ^ Height;
		}

		public static bool operator !=(Dimensions lhs, Dimensions rhs)
		{
			return lhs.Width != rhs.Width || lhs.Height != rhs.Height;
		}

		public static bool operator ==(Dimensions lhs, Dimensions rhs)
		{
			return lhs.Width == rhs.Width && lhs.Height == rhs.Height;
		}
	}

	
}
