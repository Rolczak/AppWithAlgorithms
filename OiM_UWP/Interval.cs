using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class Interval
    {
        public
        decimal l, r;

        public Interval()
        {
            l = 0;
            r = 1;
        }

        public Interval(decimal a, decimal b)
        {
            l = a;
            r = b;
        }
    }
}
