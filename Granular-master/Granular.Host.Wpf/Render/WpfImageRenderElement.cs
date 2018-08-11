﻿extern alias wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Granular.Host.Wpf.Render
{
    public class WpfImageRenderElement : IWpfRenderElement, IImageRenderElement
    {
        private System.Windows.Rect bounds;
        public System.Windows.Rect Bounds
        {
            get { return bounds; }
            set
            {
                bounds = value;
                wpf::System.Windows.Controls.Canvas.SetLeft(image, bounds.Left);
                wpf::System.Windows.Controls.Canvas.SetTop(image, bounds.Top);
                image.Width = bounds.Width;
                image.Height = bounds.Height;
            }
        }

        private ImageSource source;
        public ImageSource Source
        {
            get { return source; }
            set
            {
                source = value;
                image.Source = converter.Convert(source, factory);
            }
        }

        public wpf::System.Windows.FrameworkElement WpfElement { get { return image; } }

        private WpfValueConverter converter;
        private wpf::System.Windows.Controls.Image image;
        private IRenderElementFactory factory;

        public WpfImageRenderElement(IRenderElementFactory factory,  WpfValueConverter converter)
        {
            this.factory = factory;
            this.converter = converter;
            image = new wpf::System.Windows.Controls.Image();
            image.Stretch = wpf::System.Windows.Media.Stretch.Fill;
        }
    }
}
