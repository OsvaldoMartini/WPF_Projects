﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Granular.Extensions;

namespace System.Windows.Media
{
    public abstract class DrawingContext
    {
        public abstract void Close();
        public abstract void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY);
        public abstract void DrawGeometry(Brush brush, Pen pen, Geometry geometry);
        public abstract void DrawImage(ImageSource imageSource, Rect rectangle);
        public abstract void DrawLine(Pen pen, Point point0, Point point1);
        public abstract void DrawRectangle(Brush brush, Pen pen, Rect rectangle);
        public abstract void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY);
        public abstract void DrawText(FormattedText formattedText, Point origin);
        public abstract void Pop();
        public abstract void PushClip(Geometry clipGeometry);
        public abstract void PushOpacity(double opacity);
        public abstract void PushTransform(Transform transform);
    }

    public class RenderElementDrawingContext : DrawingContext
    {
        public event EventHandler Closed;

        private List<object> children;
        public IEnumerable<object> Children { get { return children; } }

        public bool IsEmpty { get { return children.Count == 0; } }

        private IContainerRenderElement container;
        private IRenderElementFactory factory;
        private List<object> availableChildren;
        private RenderElementDrawingContext innerContext;
        private bool isClosed;

        public RenderElementDrawingContext(IContainerRenderElement container, IRenderElementFactory factory)
        {
            this.container = container;
            this.factory = factory;
            this.availableChildren = container.Children?.ToList();
            this.children = new List<object>();
        }

        private void SetInnerContext(IContainerRenderElement innerContainer)
        {
            if (innerContext != null)
            {
                throw new Granular.Exception("Inner context is already set");
            }

            innerContext = new RenderElementDrawingContext(innerContainer, factory);
            innerContext.Closed += (sender, e) => innerContext = null;
        }

        public override void Close()
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.Close();
            }

            container.SetChildren(children);

            foreach (IDisposable child in availableChildren.OfType<IDisposable>())
            {
                child.Dispose();
            }

            isClosed = true;

            Closed.Raise(this);
        }

        public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.DrawGeometry(brush, pen, geometry);
                return;
            }

            IDrawingGeometryRenderElement child = GetChild(factory.CreateDrawingGeometryRenderElement);

            child.Fill = brush;
            child.Stroke = pen?.Brush;
            child.StrokeThickness = pen?.Thickness ?? 0;
            child.Geometry = geometry;
        }

        public override void Pop()
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.Pop();
                return;
            }

            Close();
        }

        public override void DrawImage(ImageSource imageSource, Rect rectangle)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.DrawImage(imageSource, rectangle);
                return;
            }

            IDrawingImageRenderElement child = GetChild(factory.CreateDrawingImageRenderElement);

            child.ImageSource = imageSource;
            child.Rectangle = rectangle;
        }

        public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.DrawEllipse(brush, pen, center, radiusX, radiusY);
                return;
            }

            IDrawingGeometryRenderElement child = GetChild(factory.CreateDrawingGeometryRenderElement);

            EllipseGeometry geometry = child.Geometry as EllipseGeometry;
            if (geometry == null)
            {
                geometry = new EllipseGeometry();
            }

            geometry.Center = center;
            geometry.RadiusX = radiusX;
            geometry.RadiusY = radiusY;

            child.Fill = brush;
            child.Stroke = pen?.Brush;
            child.StrokeThickness = pen?.Thickness ?? 0;
            child.Geometry = geometry;
        }


        public override void DrawLine(Pen pen, Point point0, Point point1)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.DrawLine(pen, point0, point1);
                return;
            }

            IDrawingGeometryRenderElement child = GetChild(factory.CreateDrawingGeometryRenderElement);

            LineGeometry geometry = child.Geometry as LineGeometry;
            if (geometry == null)
            {
                geometry = new LineGeometry();
            }

            geometry.StartPoint = point0;
            geometry.EndPoint = point1;

            child.Stroke = pen?.Brush;
            child.StrokeThickness = pen?.Thickness ?? 0;
            child.Geometry = geometry;
        }

        public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
        {
            DrawRoundedRectangle(brush, pen, rectangle, 0, 0);
        }

        public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.DrawRoundedRectangle(brush, pen, rectangle, radiusX, radiusY);
                return;
            }

            IDrawingGeometryRenderElement child = GetChild(factory.CreateDrawingGeometryRenderElement);

            RectangleGeometry geometry = child.Geometry as RectangleGeometry;
            if (geometry == null)
            {
                geometry = new RectangleGeometry();
            }

            geometry.Rect = rectangle;
            geometry.RadiusX = radiusX;
            geometry.RadiusY = radiusY;

            child.Fill = brush;
            child.Stroke = pen?.Brush;
            child.StrokeThickness = pen?.Thickness ?? 0;
            child.Geometry = geometry;
        }

        public override void DrawText(FormattedText formattedText, Point origin)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.DrawText(formattedText, origin);
                return;
            }

            IDrawingTextRenderElement child = GetChild(factory.CreateDrawingTextRenderElement);

            child.FormattedText = formattedText;
            child.Origin = origin;
        }

        public override void PushClip(Geometry clipGeometry)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.PushClip(clipGeometry);
                return;
            }

            IDrawingContainerRenderElement child = GetChild(factory.CreateDrawingContainerRenderElement);

            SetInnerContext(child);

            child.Clip = clipGeometry;
        }

        public override void PushOpacity(double opacity)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.PushOpacity(opacity);
                return;
            }

            IDrawingContainerRenderElement child = GetChild(factory.CreateDrawingContainerRenderElement);

            SetInnerContext(child);

            child.Opacity = opacity;
        }

        public override void PushTransform(Transform transform)
        {
            VerifyNotClosed();

            if (innerContext != null)
            {
                innerContext.PushTransform(transform);
                return;
            }

            IDrawingContainerRenderElement child = GetChild(factory.CreateDrawingContainerRenderElement);

            SetInnerContext(child);

            child.Transform = transform;
        }

        private T GetChild<T>(Func<T> factory)
        {
            T child = availableChildren.OfType<T>().FirstOrDefault();

            if (child != null)
            {
                availableChildren.Remove(child);
            }
            else
            {
                child = factory();
            }

            children.Add(child);

            return child;
        }

        private void VerifyNotClosed()
        {
            if (isClosed)
            {
                throw new Granular.Exception("DrawingContext is closed");
            }
        }
    }
}