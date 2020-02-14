using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class GanttRow
    {
        public GanttRow()
        {
            elements = new List<Gantt>();
        }
        public List<Gantt> elements { get; set; }

        public string header { get; set; }
        public int GetDurationTillEndOf(int elementIndex)
        {
            int duration = 0;

            for(int i = 0; i<elementIndex; i++)
            {
                duration += elements[i].duration; 
            }

            if (elements[0].startTime > 0)
                duration += elements[0].startTime;

            return duration;
        }

        public int GetTotalDuration()
        {
            int duration = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                duration += elements[i].duration;
            }

            if (elements[0].startTime > 0)
                duration += elements[0].startTime;
            return duration;
        }

        public void FillEmptySpaces()
        {
            if(elements[0].startTime > 0)
            {
                Gantt gantt = new Gantt(0, elements[0].startTime, "-", Gantt.CellType.Pause);
                elements.Insert(0, gantt);
            }
            for (int i = 1; i < elements.Count; i++)
            {
                if(elements[i-1].startTime+elements[i - 1].duration < elements[i].startTime)
                {
                    Gantt gantt = new Gantt(elements[i - 1].startTime + elements[i - 1].duration, elements[i].startTime - (elements[i - 1].startTime + elements[i - 1].duration), "-", Gantt.CellType.Pause);
                    elements.Insert(i, gantt);
                }
            }
        }
    }

}
