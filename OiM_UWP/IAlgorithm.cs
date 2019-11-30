using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    public interface IAlgorithm
    {
        string Title { get; set; }
        string Descripiton { get; set; }

        void Calculate();

        void Draw();
    }
}
