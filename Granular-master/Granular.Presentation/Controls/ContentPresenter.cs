﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace System.Windows.Controls
{
    public class ContentPresenter : FrameworkElement, IItemContainer
    {
        private class UIElementTemplate : IFrameworkTemplate
        {
            public UIElement Content { get; private set; }

            public UIElementTemplate(UIElement content)
            {
                this.Content = content;
            }

            public void Attach(FrameworkElement element)
            {
                element.TemplateChild = Content;
            }

            public void Detach(FrameworkElement element)
            {
                element.TemplateChild = null;
            }
        }

        private class DefaultContentTemplate : IFrameworkTemplate
        {
            public static readonly IFrameworkTemplate Default = new DefaultContentTemplate();

            private class ToStringConverter : IValueConverter
            {
                public static readonly ToStringConverter Default = new ToStringConverter();

                private ToStringConverter()
                {
                    //
                }

                public object Convert(object value, Type targetType, object parameter)
                {
                    return value != null ? value.ToString() : String.Empty;
                }

                public object ConvertBack(object value, Type targetType, object parameter)
                {
                    return null;
                }
            }

            private DefaultContentTemplate()
            {
                //
            }

            public void Attach(FrameworkElement element)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.SetValue(TextBlock.TextProperty, new Binding { Source = element, Path = PropertyPath.FromDependencyProperty(ContentProperty), Mode = BindingMode.OneWay, Converter = ToStringConverter.Default });

                element.TemplateChild = textBlock;
            }

            public void Detach(FrameworkElement element)
            {
                element.TemplateChild = null;
            }
        }

        public static readonly DependencyProperty ContentProperty = ContentControl.ContentProperty.AddOwner(typeof(ContentPresenter), new FrameworkPropertyMetadata(propertyChangedCallback: (sender, e) => ((ContentPresenter)sender).OnContentChanged(e)));
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentTemplateProperty = ContentControl.ContentTemplateProperty.AddOwner(typeof(ContentPresenter), new FrameworkPropertyMetadata(propertyChangedCallback: (sender, e) => ((ContentPresenter)sender).SetTemplate()));
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ContentTemplateSelectorProperty = ContentControl.ContentTemplateSelectorProperty.AddOwner(typeof(ContentPresenter), new FrameworkPropertyMetadata(propertyChangedCallback: (sender, e) => ((ContentPresenter)sender).SetTemplate()));
        public IDataTemplateSelector ContentTemplateSelector
        {
            get { return (IDataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }

        private IFrameworkTemplate template;
        private IFrameworkTemplate Template
        {
            get { return template; }
            set
            {
                if (template == value)
                {
                    return;
                }

                template = value;

                ApplyTemplate();
            }
        }

        private DataTemplate itemTemplate;
        private Style itemContainerStyle;

        public ContentPresenter()
        {
            //
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (TemplateChild == null)
            {
                return Size.Zero;
            }

            TemplateChild.Measure(availableSize);
            return TemplateChild.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (TemplateChild != null)
            {
                TemplateChild.Arrange(new Rect(finalSize));
            }

            return finalSize;
        }

        protected override IFrameworkTemplate GetTemplate()
        {
            return Template;
        }

        protected override void OnApplyTemplate()
        {
            SetDataContext();
        }

        protected override void OnResourcesChanged(ResourcesChangedEventArgs e)
        {
            base.OnResourcesChanged(e);
            SetTemplate();
        }

        protected virtual void OnContentChanged(DependencyPropertyChangedEventArgs e)
        {
            SetTemplate();
        }

        private void SetTemplate()
        {
            Template = FindTemplate();
        }

        private void SetDataContext()
        {
            if (Content is Visual)
            {
                this.ClearValue(DataContextProperty);
            }
            else
            {
                this.DataContext = Content;
            }
        }

        private IFrameworkTemplate FindTemplate()
        {
            if (Content == null)
            {
                return null;
            }

            if (Content is UIElement)
            {
                return Template is UIElementTemplate && ((UIElementTemplate)Template).Content == Content ? Template : new UIElementTemplate((UIElement)Content);
            }

            if (ContentTemplate != null)
            {
                return ContentTemplate;
            }

            if (ContentTemplateSelector != null)
            {
                ContentTemplateSelector.SelectTemplate(Content, this);
            }

            DataTemplate dataTemplate;
            if (TryFindDataTemplate(Content.GetType(), out dataTemplate))
            {
                return dataTemplate;
            }

            return DefaultContentTemplate.Default;
        }

        private bool TryFindDataTemplate(Type type, out DataTemplate dataTemplate)
        {
            object value;

            if (TryGetResource(new TemplateKey(type), out value))
            {
                dataTemplate = value as DataTemplate;
                return dataTemplate != null;
            }

            if (type.BaseType != null)
            {
                return TryFindDataTemplate(type.BaseType, out dataTemplate);
            }

            dataTemplate = null;
            return false;
        }

        public virtual void PrepareContainerForItem(object item, DataTemplate itemTemplate, Style itemContainerStyle)
        {
            if (!ContainsValue(ContentTemplateProperty) && !ContainsValue(ContentTemplateSelectorProperty))
            {
                ContentTemplate = itemTemplate;
                this.itemTemplate = itemTemplate;
            }

            if (!ContainsValue(StyleProperty))
            {
                Style = itemContainerStyle;
                this.itemContainerStyle = itemContainerStyle;
            }

            Content = item;
        }

        public virtual void ClearContainerForItem(object item)
        {
            if (itemTemplate == ContentTemplate)
            {
                ClearValue(ContentTemplateProperty);
                itemTemplate = null;
            }

            if (itemContainerStyle == Style)
            {
                ClearValue(StyleProperty);
                itemContainerStyle = null;
            }

            ClearValue(ContentProperty);
        }
    }
}
