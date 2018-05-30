using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Algol_Contracts;

namespace ExportMultiplyLib
{
    [Export(typeof(IComponent))]
    [ExportMetadata("Symbol", '*')]
    public class MultiplyOfNumberComponent : IComponent
    {
        public string Description
        {
            get { return "Multiplication of components"; }
        }

        [DebuggerStepThrough]
        public string ManipulateOperation(params double[] args)
        {
            string result = "";
            double count = 0;
            bool first = true;

            foreach (double d in args)
            {
                if (first)
                {
                    count = d;
                    first = false;
                }
                else
                    count *= d;
                result += d.ToString() + " * ";
            }
            return result.Trim('*', ' ') + " = " + count.ToString();
        }
    }
}
