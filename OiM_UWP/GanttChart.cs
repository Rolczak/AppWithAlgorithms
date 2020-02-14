using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace OiM_UWP
{
    class GanttChart
    {
        public GanttChart()
        {
            rows = new List<GanttRow>();
        }
        public List<GanttRow> rows { get; set; }

        public void DrawChart(RelativePanel grid)
        {
            grid.Children.Clear();
            double x = grid.ActualWidth-50;
            double y = grid.ActualHeight;
            int maxDuration = 0;
            foreach(GanttRow row in rows)
            {
                if (maxDuration < row.GetTotalDuration())
                    maxDuration = row.GetTotalDuration();
            }

            double widthPerOneDuration = x / maxDuration;
            Random rand = new Random();

            for (int i = 0; i < rows.Count; i++)
            {
                Grid rectForHeader = new Grid()
                {
                    Width = 50,
                    Height = 50,
                    Margin = new Windows.UI.Xaml.Thickness(0, i * 60, 0, 0),
                };
                TextBlock header = new TextBlock()
                {
                    Text = rows[i].header,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                rectForHeader.Children.Add(header);
                grid.Children.Add(rectForHeader);
            for (int j = 0; j< rows[i].elements.Count; j++)
                {
                    Border rect = new Border()
                    {
                        Height = 50,
                        Width = rows[i].elements[j].duration*widthPerOneDuration,
                        Margin = new Windows.UI.Xaml.Thickness(rows[i].elements[j].startTime*widthPerOneDuration+50, i * 60, 0, 0),
                        MaxHeight = 50,
                        CornerRadius = new CornerRadius(3),
                        BorderBrush = new SolidColorBrush(Color.FromArgb(255,0,0,0)),
                        BorderThickness = new Thickness(1),

                    };
                    if(rows[i].elements[j].type == Gantt.CellType.Filled)
                    {
                        rect.Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorDark2"]);
                    }
                    else
                    {
                        rect.Background = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                    }
                    TextBlock text = new TextBlock()
                    {
                        Text = rows[i].elements[j].contentText,
                        VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                        HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                    };
                    rect.Child = text;
                    grid.Children.Add(rect);
                }
            }
        }
    }
}
