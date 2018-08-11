﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Granular.Presentation.Tests
{
    [TestClass]
    public class HitTestTest
    {
        [TestMethod]
        public void HitTestBasicTest()
        {
            StackPanel stackPanel1 = new StackPanel { Name = "stackPanel1", Background = Brushes.Transparent, IsRootElement = true };
            StackPanel stackPanel2 = new StackPanel { Name = "stackPanel2", Background = Brushes.Transparent, Height = 100, Orientation = Orientation.Horizontal };

            Border child1 = new Border { Name = "child1", Background = Brushes.Transparent, Height = 100, Width = 100 };
            Border child2 = new Border { Name = "child2", Background = Brushes.Transparent, Height = 100 };
            Border child3 = new Border { Name = "child3", Background = Brushes.Transparent, Width = 100 };
            Border child4 = new Border { Name = "child4", Background = Brushes.Transparent, Width = 100 };

            stackPanel1.Children.Add(child1);
            stackPanel1.Children.Add(stackPanel2);
            stackPanel1.Children.Add(child2);
            stackPanel2.Children.Add(child3);
            stackPanel2.Children.Add(child4);

            // [child1]       ]
            // [child3][child4]
            // [child2        ]

            stackPanel1.Measure(Size.Infinity);
            stackPanel1.Arrange(new Rect(stackPanel1.DesiredSize));

            Assert.AreEqual(child1, stackPanel1.HitTest(new Point(50, 50)));
            Assert.AreEqual(stackPanel1, stackPanel1.HitTest(new Point(150, 50)));
            Assert.AreEqual(child3, stackPanel1.HitTest(new Point(50, 150)));
            Assert.AreEqual(child4, stackPanel1.HitTest(new Point(150, 150)));
            Assert.AreEqual(child2, stackPanel1.HitTest(new Point(50, 250)));
            Assert.AreEqual(child2, stackPanel1.HitTest(new Point(150, 250)));

            stackPanel2.IsEnabled = false;
            Assert.AreEqual(stackPanel1, stackPanel1.HitTest(new Point(50, 150)));
            Assert.AreEqual(stackPanel1, stackPanel1.HitTest(new Point(150, 150)));

            child2.IsHitTestVisible = false;
            Assert.AreEqual(stackPanel1, stackPanel1.HitTest(new Point(50, 250)));
            Assert.AreEqual(stackPanel1, stackPanel1.HitTest(new Point(150, 250)));
        }

        [TestMethod]
        public void HitTestRenderTransformTest()
        {
            Border parent = new Border { Name = "parent", Background = Brushes.Transparent, Height = 400, Width = 400, IsRootElement = true };
            Border child = new Border { Name = "child", Background = Brushes.Transparent, Height = 200, Width = 200 };

            parent.Child = child;

            parent.Measure(Size.Infinity);
            parent.Arrange(new Rect(parent.DesiredSize));

            Assert.AreEqual(parent, parent.HitTest(new Point(350, 50)));
            Assert.AreEqual(child, parent.HitTest(new Point(250, 150)));
            Assert.AreEqual(child, parent.HitTest(new Point(150, 250)));
            Assert.AreEqual(parent, parent.HitTest(new Point(50, 350)));

            child.RenderTransform = new ScaleTransform { ScaleX = -0.5, ScaleY = 1.5 };
            child.RenderTransformOrigin = new Point(0.5, 0);

            // Rect(100, 100, 200, 200) * (RenderTransform, RenderTransformOrigin) = Rect(150, 100, 100, 250)

            Assert.AreEqual(parent, parent.HitTest(new Point(100, 50)));
            Assert.AreEqual(child, parent.HitTest(new Point(200, 150)));
            Assert.AreEqual(child, parent.HitTest(new Point(200, 350)));
            Assert.AreEqual(parent, parent.HitTest(new Point(300, 350)));
        }
    }
}
