using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BindingTests.PersonDomain
{
    public class Test
    {
        public static int TestNum = 0;

        public Test()
        {
            if (TestNum == 0)
            {
                TestNum = 1;
            }

        }
        static Test()
        {
            if (TestNum == 0)
                TestNum = 2;
        }

      
        public int Print()
        {

            if (TestNum == 1)
            {
                TestNum = 3;
            }

            return TestNum;
        }
    }
}
