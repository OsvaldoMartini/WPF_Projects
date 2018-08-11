﻿using System;
using System.Collections;
using System.Collections.Generic;
using Bridge;
using Bridge.Html5;
using Granular.Extensions;

namespace System.Xml.Linq
{
    public abstract class XNode
    {
        //
    }

    public class XText : XNode
    {
        public string Value { get { return node.NodeValue; } }

        private Node node;

        public XText(Node node)
        {
            this.node = node;
        }
    }

    public abstract class XContainer : XNode
    {
        private XNode[] nodes;
        private XElement[] elements;

        public XContainer(Node node)
        {
            nodes = new XNode[0];
            elements = new XElement[0];

            for (int i = 0; i < node.ChildNodes.Length; i++)
            {
                Node childNode = node.ChildNodes[i];

                if (childNode.NodeType == NodeType.Element)
                {
                    XElement childElement = new XElement((Element)childNode);
                    elements.Push(childElement);
                    nodes.Push(childElement);
                }

                if (childNode.NodeType == NodeType.Text && !childNode.NodeValue.IsNullOrWhiteSpace())
                {
                    XText childText = new XText(childNode);
                    nodes.Push(childText);
                }
            }
        }

        public XNode[] Nodes()
        {
            return nodes;
        }

        public XElement[] Elements()
        {
            return elements;
        }
    }

    public class XDocument : XContainer
    {
        public XElement Root { get; private set; }

        private Node node;

        public XDocument(Node node) :
            base(node)
        {
            this.node = node;
            this.Root = new XElement((Element)node.FirstChild);
        }

        public static XDocument Parse(string text)
        {
            DOMParser parser = new DOMParser();
            return new XDocument(parser.ParseFromString(text, SupportedType.ApplicationXml));
        }
    }

    public class XElement : XContainer
    {
        public XName Name { get; private set; }

        private Element element;
        private XAttribute[] attributes;

        public XElement(Element element) :
            base(element)
        {
            this.element = element;
            this.Name = XName.Get(element.GetLocalName(), element.GetNamespaceURI());

            this.attributes = new XAttribute[element.Attributes.Length];
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i] = new XAttribute(element.Attributes[i]);
            }
        }

        public XAttribute[] Attributes()
        {
            return attributes;
        }
    }

    public class XName
    {
        public string LocalName { get; private set; }
        public string NamespaceName { get; private set; }

        private XName(string localName, string namespaceName)
        {
            this.LocalName = localName;
            this.NamespaceName = namespaceName;
        }

        public override string ToString()
        {
            return String.IsNullOrEmpty(NamespaceName) ? LocalName : $"{{{NamespaceName}}}{LocalName}";
        }

        public static XName Get(string localName, string namespaceName)
        {
            return new XName(localName, namespaceName);
        }
    }

    public class XAttribute
    {
        public XName Name { get; private set; }
        public string Value { get; private set; }

        public bool IsNamespaceDeclaration { get; private set; }

        private Node node;

        public XAttribute(Node node)
        {
            this.node = node;

            string nodeName = node.NodeName;

            if (nodeName == "xmlns")
            {
                this.Name = XName.Get(String.Empty, node.GetNamespaceURI());
                this.IsNamespaceDeclaration = true;
            }
            else
            {
                this.Name = XName.Get(node.GetLocalName(), node.GetNamespaceURI());
                this.IsNamespaceDeclaration = nodeName.StartsWith("xmlns:");
            }

            this.Value = node.NodeValue;
        }
    }

    public static class NodeExtensions
    {
        [Bridge.Template("{node}.namespaceURI")]
        public static extern string GetNamespaceURI(this Node node);

        public static string GetLocalName(this Node node)
        {
            return node.NodeName.Substring(node.NodeName.IndexOf(':') + 1);
        }
    }

    [External]
    [Enum(Emit.StringName)]
    public enum SupportedType
    {
        [Name("text/html")]
        TextHtml,
        [Name("text/xml")]
        TextXml,
        [Name("application/xml")]
        ApplicationXml,
        [Name("application/xhtml+xml")]
        ApplicationXhtmlXml,
        [Name("image/svg+xml")]
        ImageSvgXml
    };

    [External]
    [Name("DOMParser")]
    public class DOMParser
    {
        [Name("parseFromString")]
        public virtual extern DocumentInstance ParseFromString(string str, SupportedType type);
    }
}