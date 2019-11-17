using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class ArithmeticCoding
    {
        public static decimal[] MakeProportions(decimal[] prob)
        {
            decimal[] proportions = new decimal[prob.GetLength(0)];
            proportions[0] = prob[0];
            for (int i = 1; i < proportions.GetLength(0); i++)
            {
                proportions[i] = proportions[i - 1] + prob[i];
            }
            return proportions;
        }

        public static Interval[] MakeSubintervals(Interval i, decimal[] list)
        {
            Interval[] si_list = new Interval[list.GetLength(0)];

            for (int k = 0; k < si_list.GetLength(0); k++)
            {
                si_list[k] = new Interval();
            }

            si_list[0].l = i.l;
            si_list[0].r = i.l + (i.r - i.l) * list[0];

            for (int j = 1; j < list.GetLength(0); j++)
            {
                si_list[j].l = i.l + (i.r - i.l) * list[j - 1];
                si_list[j].r = i.l + (i.r - i.l) * list[j];
            }

            return si_list;
        }


        public static Interval SetCurrentInterval(Interval[] subintervals, char[] alphabet, char letter)
        {
            Interval currentinterval = new Interval(-1, -1);

            for (int i = 0; i < alphabet.GetLength(0); i++)
            {
                if (alphabet[i] == letter)
                {
                    currentinterval = subintervals[i];
                }
            }

            return currentinterval;
        }
    }
}
