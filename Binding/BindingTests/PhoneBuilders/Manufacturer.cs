using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binding.Tests.PhoneBuilders
{
    class Manufacturer
    {
        public void Construct(IPhoneBuilder phoneBuilder)
        {
            phoneBuilder.BuildBattery();
            phoneBuilder.BuildOS();
            phoneBuilder.BuildScreen();
            phoneBuilder.BuildStylus();
        }
    }
}