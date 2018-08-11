﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Granular.Presentation.Tests.Markup
{
    [TestClass]
    public class MarkupExtensionParserTest
    {
        [TestMethod]
        public void ParseImplicitPropertiesTest()
        {
            XamlNamespaces namespaces = new XamlNamespaces(String.Empty, "default-namespace");
            XamlElement root1 = (XamlElement)MarkupExtensionParser.Parse("{root1 value1, value2}", namespaces);
            Assert.AreEqual("root1", root1.Name.LocalName);
            Assert.AreEqual(2, root1.Members.Count());
            Assert.IsTrue(root1.Members.ElementAt(0).Name.IsEmpty);
            Assert.IsTrue(root1.Members.ElementAt(1).Name.IsEmpty);
            Assert.AreEqual("value1", root1.Members.ElementAt(0).Values.Single());
            Assert.AreEqual("value2", root1.Members.ElementAt(1).Values.Single());
        }

        [TestMethod]
        public void ParseExplicitPropertiesTest()
        {
            XamlNamespaces namespaces = new XamlNamespaces(String.Empty, "default-namespace");
            XamlElement root1 = (XamlElement)MarkupExtensionParser.Parse("{root1 property1=value1, property2=value2}", namespaces);
            Assert.AreEqual("root1", root1.Name.LocalName);
            Assert.AreEqual(2, root1.Members.Count());
            Assert.AreEqual("property1", root1.Members.ElementAt(0).Name.LocalName);
            Assert.AreEqual("value1", root1.Members.ElementAt(0).Values.Single());
            Assert.AreEqual("property2", root1.Members.ElementAt(1).Name.LocalName);
            Assert.AreEqual("value2", root1.Members.ElementAt(1).Values.Single());
        }

        [TestMethod]
        public void ParseChildrenTest()
        {
            XamlNamespaces namespaces = new XamlNamespaces(String.Empty, "default-namespace");
            XamlElement root1 = (XamlElement)MarkupExtensionParser.Parse("{root1 property1={child1 value1, property2=value2}, property3={child3 property4=value4}}", namespaces);

            Assert.AreEqual("root1", root1.Name.LocalName);
            Assert.AreEqual(2, root1.Members.Count());
            Assert.AreEqual("property1", root1.Members.ElementAt(0).Name.LocalName);

            Assert.IsTrue(root1.Members.ElementAt(0).Values.Single() is XamlElement);
            Assert.AreEqual("child1", (root1.Members.ElementAt(0).Values.Single() as XamlElement).Name.LocalName);
            Assert.IsTrue((root1.Members.ElementAt(0).Values.Single() as XamlElement).Members.Count() == 2);
            Assert.AreEqual("value1", (root1.Members.ElementAt(0).Values.Single() as XamlElement).Members.ElementAt(0).Values.Single());
            Assert.AreEqual("value2", (root1.Members.ElementAt(0).Values.Single() as XamlElement).Members.ElementAt(1).Values.Single());

            Assert.IsTrue(root1.Members.ElementAt(1).Values.Single() is XamlElement);
            Assert.AreEqual("child3", (root1.Members.ElementAt(1).Values.Single() as XamlElement).Name.LocalName);
            Assert.IsTrue((root1.Members.ElementAt(1).Values.Single() as XamlElement).Members.Count() == 1);
            Assert.AreEqual("property4", (root1.Members.ElementAt(1).Values.Single() as XamlElement).Members.ElementAt(0).Name.LocalName);
            Assert.AreEqual("value4", (root1.Members.ElementAt(1).Values.Single() as XamlElement).Members.ElementAt(0).Values.Single());
        }

        [TestMethod]
        public void ParseNamespaceTest()
        {
            XamlNamespaces namespaces = new XamlNamespaces(new[]
            {
                new NamespaceDeclaration("ns1", "namespace1"),
                new NamespaceDeclaration("ns2", "namespace2"),
            });

            XamlElement root1 = (XamlElement)MarkupExtensionParser.Parse("{ns1:root1 value1={ns2:child2}}", namespaces);
            Assert.AreEqual("root1", root1.Name.LocalName);
            Assert.AreEqual("namespace1", root1.Name.NamespaceName);
            Assert.AreEqual("child2", (root1.Members.First().Values.Single() as XamlElement).Name.LocalName);
            Assert.AreEqual("namespace2", (root1.Members.First().Values.Single() as XamlElement).Name.NamespaceName);
        }

        [TestMethod]
        public void ParseEscapedTextTest()
        {
            XamlNamespaces namespaces = new XamlNamespaces(String.Empty, "default-namespace");

            string value = (string)MarkupExtensionParser.Parse("{}{0}{1}{2}", namespaces);
            Assert.AreEqual("{0}{1}{2}", value);

            try
            {
                MarkupExtensionParser.Parse("{0}{1}{2}", namespaces);
                Assert.Fail();
            }
            catch
            {
                //
            }
        }
    }
}
