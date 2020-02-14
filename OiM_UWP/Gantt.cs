using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class Gantt
    {
        public enum CellType
        {
            Pause,
            Filled
        }
        public Gantt(int startTime, int duration, string contentText, CellType type = CellType.Filled)
        {
            this.startTime = startTime;
            this.duration = duration;
            this.contentText = contentText;
            this.type = type;
        }
        public int startTime { get; set; }
        public int duration { get; set; }
        public string contentText { get; set; }

        public CellType type { get; set;}
    }
}
