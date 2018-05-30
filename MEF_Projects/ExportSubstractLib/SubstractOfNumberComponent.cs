using System.ComponentModel.Composition;
using System.Diagnostics;
using Algol_Contracts;

namespace ExportSubstractLib
{
    [Export(typeof(IComponent))]
    [ExportMetadata("Symbol", '-')]
    public class SubstractOfNumberComponent : IComponent
    {
        public string Description
        {
            get { return "Subtraction of components"; }
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
                    count -= d;
                result += d.ToString() + " - ";
            }

            return result.Trim('-', ' ') + " = " + count.ToString();
        }
    }
}
