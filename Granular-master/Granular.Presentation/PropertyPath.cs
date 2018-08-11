﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using Granular.Extensions;
using Granular.Compatibility.Linq;

namespace System.Windows
{
    [TypeConverter(typeof(PropertyPathElementTypeConverter))]
    public interface IPropertyPathElement
    {
        bool TryGetValue(object target, out object value);

        bool TryGetDependencyProperty(Type containingType, out DependencyProperty dependencyProperty);

        IPropertyObserver CreatePropertyObserver(Type baseValueType);
    }

    public static class PropertyPathElementExtensions
    {
        public static DependencyProperty GetDependencyProperty(this IPropertyPathElement propertyPathElement, Type containingType)
        {
            DependencyProperty dependencyProperty;
            if (propertyPathElement.TryGetDependencyProperty(containingType, out dependencyProperty))
            {
                return dependencyProperty;
            }

            throw new Granular.Exception("Type \"{0}\" does not contain a dependency property \"{1}\"", containingType.Name, propertyPathElement);
        }
    }

    public sealed class PropertyPathElement : IPropertyPathElement
    {
        public XamlName PropertyName { get; private set; }

        public PropertyPathElement(XamlName propertyName)
        {
            this.PropertyName = propertyName;
        }

        public override bool Equals(object obj)
        {
            PropertyPathElement other = obj as PropertyPathElement;

            return Object.ReferenceEquals(this, other) || !Object.ReferenceEquals(other, null) &&
                this.PropertyName == other.PropertyName;
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

        public override string ToString()
        {
            return PropertyName.HasContainingTypeName ? String.Format("({0})", PropertyName.LocalName) : PropertyName.LocalName;
        }

        public bool TryGetValue(object target, out object value)
        {
            return TryGetValue(target, PropertyName, out value);
        }

        public bool TryGetDependencyProperty(Type containingType, out DependencyProperty dependencyProperty)
        {
            dependencyProperty = DependencyProperty.GetProperty(PropertyName.ResolveContainingType(containingType), PropertyName.MemberName);
            return dependencyProperty != null;
        }

        public IPropertyObserver CreatePropertyObserver(Type baseValueType)
        {
            Type containingType = PropertyName.ResolveContainingType(baseValueType);

            DependencyProperty dependencyProperty = DependencyProperty.GetProperty(containingType, PropertyName.MemberName);
            if (dependencyProperty != null)
            {
                return new DependencyPropertyObserver(dependencyProperty);
            }

            PropertyInfo propertyInfo = containingType.GetInstanceProperty(PropertyName.MemberName);
            if (propertyInfo != null)
            {
                return new ClrPropertyObserver(propertyInfo, new object[0]);
            }

            return null;
        }

        public static bool TryGetValue(object target, XamlName propertyName, out object value)
        {
            Type containingType = propertyName.ResolveContainingType(target.GetType());

            DependencyProperty dependencyProperty = DependencyProperty.GetProperty(containingType, propertyName.MemberName);
            if (dependencyProperty != null && target is DependencyObject)
            {
                value = ((DependencyObject)target).GetValue(dependencyProperty);
                return true;
            }

            PropertyInfo propertyInfo = containingType.GetInstanceProperty(propertyName.MemberName);
            if (propertyInfo != null && !propertyInfo.GetIndexParameters().Any())
            {
                value = propertyInfo.GetValue(target, new object[0]);
                return true;
            }

            value = null;
            return false;
        }
    }

    public class IndexPropertyPathElement : IPropertyPathElement
    {
        public XamlName PropertyName { get; private set; }
        public IEnumerable<string> IndexRawValues { get; private set; }

        private XamlNamespaces namespaces;
        private Uri sourceUri;

        public IndexPropertyPathElement(XamlName propertyName, IEnumerable<string> indexRawValues, XamlNamespaces namespaces, Uri sourceUri)
        {
            this.PropertyName = propertyName;
            this.IndexRawValues = indexRawValues;
            this.namespaces = namespaces;
            this.sourceUri = sourceUri;
        }

        public override bool Equals(object obj)
        {
            IndexPropertyPathElement other = obj as IndexPropertyPathElement;

            return Object.ReferenceEquals(this, other) || !Object.ReferenceEquals(other, null) &&
                this.PropertyName == other.PropertyName &&
                this.IndexRawValues.SequenceEqual(other.IndexRawValues);
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

        public override string ToString()
        {
            string propertyName = PropertyName.HasContainingTypeName ? String.Format("({0})", PropertyName.LocalName) : PropertyName.LocalName;
            string indexRawValues = IndexRawValues.DefaultIfEmpty(String.Empty).Aggregate((s1, s2) => String.Format("{0}, {1}", s1, s2));

            return String.Format("{0}[{1}]", propertyName, indexRawValues);
        }

        public bool TryGetValue(object target, out object value)
        {
            Type containingType = PropertyName.ResolveContainingType(target.GetType());
            string propertyName = PropertyName.MemberName;

            bool isDefaultIndexProperty = propertyName.IsNullOrEmpty();

            object indexPropertyValue;

            // an index property that has a name (such as "Values[0]"), might be a regular property with the same name ("Values"), and a default index property ("[0]" or "Item[0]")
            if (!isDefaultIndexProperty && PropertyPathElement.TryGetValue(target, PropertyName, out indexPropertyValue))
            {
                if (indexPropertyValue == null)
                {
                    value = null;
                    return false;
                }

                target = indexPropertyValue;
                containingType = indexPropertyValue.GetType();
                isDefaultIndexProperty = true;
            }

            PropertyInfo indexPropertyInfo = isDefaultIndexProperty ? containingType.GetDefaultIndexProperty() : containingType.GetInstanceProperty(propertyName);

            if (indexPropertyInfo == null)
            {
                value = null;
                return false;
            }

            value = indexPropertyInfo.GetValue(target, ParseIndexValues(indexPropertyInfo).ToArray());
            return true;
        }

        public bool TryGetDependencyProperty(Type containingType, out DependencyProperty dependencyProperty)
        {
            dependencyProperty = null;
            return false;
        }

        public IPropertyObserver CreatePropertyObserver(Type baseValueType)
        {
            return new IndexPropertyObserver(baseValueType, this, namespaces);
        }

        public IEnumerable<object> ParseIndexValues(PropertyInfo indexPropertyInfo)
        {
            if (indexPropertyInfo.GetIndexParameters().Count() != IndexRawValues.Count())
            {
                throw new Granular.Exception("Invalid number of index parameters for \"{0}.{1}\"", indexPropertyInfo.DeclaringType.Name, indexPropertyInfo.Name);
            }

            return indexPropertyInfo.GetIndexParameters().Zip(IndexRawValues, (parameter, rawValue) => TypeConverter.ConvertValue(rawValue, parameter.ParameterType, namespaces, sourceUri)).ToArray();
        }
    }

    public sealed class DependencyPropertyPathElement : IPropertyPathElement
    {
        public DependencyProperty DependencyProperty { get; private set; }

        public DependencyPropertyPathElement(DependencyProperty dependencyProperty)
        {
            this.DependencyProperty = dependencyProperty;
        }

        public override bool Equals(object obj)
        {
            DependencyPropertyPathElement other = obj as DependencyPropertyPathElement;

            return Object.ReferenceEquals(this, other) || !Object.ReferenceEquals(other, null) &&
                this.DependencyProperty == other.DependencyProperty;
        }

        public override int GetHashCode()
        {
            return DependencyProperty.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("({0})", DependencyProperty);
        }

        public bool TryGetDependencyProperty(Type containingType, out DependencyProperty dependencyProperty)
        {
            dependencyProperty = this.DependencyProperty;
            return true;
        }

        public IPropertyObserver CreatePropertyObserver(Type baseValueType)
        {
            return new DependencyPropertyObserver(DependencyProperty);
        }

        public bool TryGetValue(object target, out object value)
        {
            if (target is DependencyObject)
            {
                value = ((DependencyObject)target).GetValue(DependencyProperty);
                return true;
            }

            value = null;
            return false;
        }
    }

    [TypeConverter(typeof(PropertyPathTypeConverter))]
    public class PropertyPath
    {
        public static readonly PropertyPath Empty = new PropertyPath(new IPropertyPathElement[0]);

        public IEnumerable<IPropertyPathElement> Elements { get; private set; }

        public bool IsEmpty { get; private set; }

        public PropertyPath(IEnumerable<IPropertyPathElement> elements)
        {
            this.Elements = elements;
            this.IsEmpty = !Elements.Any();
        }

        public override string ToString()
        {
            return Elements.Select(element => element.ToString()).DefaultIfEmpty(String.Empty).Aggregate((s1, s2) => String.Format("{0}.{1}", s1, s2));
        }

        public static PropertyPath Parse(string value, XamlNamespaces namespaces = null)
        {
            PropertyPathParser parser = new PropertyPathParser(value, namespaces ?? XamlNamespaces.Empty, null);
            return new PropertyPath(parser.Parse());
        }

        public static PropertyPath FromDependencyProperty(DependencyProperty dependencyProperty)
        {
            return new PropertyPath(new[] { new DependencyPropertyPathElement(dependencyProperty) });
        }
    }

    public class PropertyPathTypeConverter : ITypeConverter
    {
        public object ConvertFrom(XamlNamespaces namespaces, Uri sourceUri, object value)
        {
            return PropertyPath.Parse((string)value, namespaces);
        }
    }

    public class PropertyPathElementTypeConverter : ITypeConverter
    {
        public object ConvertFrom(XamlNamespaces namespaces, Uri sourceUri, object value)
        {
            return new PropertyPathElement(XamlName.FromPrefixedName((string)value, namespaces));
        }
    }

    public static class PropertyPathExtensions
    {
        public static PropertyPath GetBasePropertyPath(this PropertyPath propertyPath)
        {
            return propertyPath.Elements.Count() > 1 ? new PropertyPath(propertyPath.Elements.Take(propertyPath.Elements.Count() - 1)) : PropertyPath.Empty;
        }

        public static PropertyPath Insert(this PropertyPath propertyPath, int index, IPropertyPathElement element)
        {
            IEnumerable<IPropertyPathElement> elements = propertyPath.Elements.Take(index).Concat(new [] { element }).Concat(propertyPath.Elements.Skip(index)).ToArray();
            return new PropertyPath(elements);
        }

        public static bool TryGetValue(this PropertyPath propertyPath, object root, out object value)
        {
            if (propertyPath.IsEmpty)
            {
                value = null;
                return false;
            }

            if (propertyPath.Elements.Count() > 1)
            {
                object baseValue;

                if (!propertyPath.GetBasePropertyPath().TryGetValue(root, out baseValue))
                {
                    value = null;
                    return false;
                }

                root = baseValue;
            }

            return propertyPath.Elements.Last().TryGetValue(root, out value);
        }
    }
}
