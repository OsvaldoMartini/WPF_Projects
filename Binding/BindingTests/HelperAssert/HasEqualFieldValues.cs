﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace BindingTests.HelperAssert
{
 public class AssertHelper
    {
        public static void HasEqualFieldValues<T>(T expected, T actual)
        {
            var failures = new List<string>();
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var v1 = field.GetValue(expected);
                var v2 = field.GetValue(actual);
                if (v1 == null && v2 == null) continue;
                if (!v1.Equals(v2)) failures.Add(string.Format("{0}: Expected:<{1}> Actual:<{2}>", field.Name, v1, v2));
            }
            if (failures.Any())
                Assert.Fail("AssertHelper.HasEqualFieldValues failed. " + Environment.NewLine + string.Join(Environment.NewLine, failures));

            if (!fields.Any())
                Assert.Fail("AssertHelper.HasEqualFieldValues failed. " + Environment.NewLine + string.Join(Environment.NewLine, "Fields Not Detected. Check the Types Of the Objects"));

        }


        public static void GetClassFields(Type t)
        {
            List<string> fieldNames = new List<string>(t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name));
            foreach (string name in fieldNames)
            {
                Console.WriteLine(name);
            }       
        }
    }
}
