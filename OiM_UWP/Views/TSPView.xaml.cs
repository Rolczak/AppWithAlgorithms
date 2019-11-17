using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace OiM_UWP.Views
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class TSPView : Page
    {
        private TextBox[,] pointsTextBoxes;
        private Matrix matrixToCalc;

        public TSPView()
        {
            this.InitializeComponent();
        }

        private async void generatePoints(object sender, RoutedEventArgs e)
        {
            int quantity = -1;
            try
            {
                quantity = int.Parse(holesQuantityTextBox.Text);
            }
            catch(Exception ex)
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = ex.Message, CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }
            if(quantity > 0)
            {
                pointsTextBoxes = createPointsTable(quantity);
            }
        }

        private TextBox[,] createPointsTable(int quantity)
        {
            pointsTable.Children.Clear();
            TextBox[,] points = new TextBox[quantity,2];

            for(int i = 0; i < quantity; i++)
            {
                points[i, 0] = new TextBox
                {
                    Header = "Punkt " + (i+1).ToString() + "x :",
                    PlaceholderText = "x"
                };
                points[i, 1] = new TextBox
                {
                    Header = "Punkt " + (i+1).ToString() + "y :",
                    PlaceholderText = "y"    
                };

                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(1,5,0,1), HorizontalAlignment = HorizontalAlignment.Center };
                stackPanel.Children.Add(points[i,0]);
                stackPanel.Children.Add(points[i,1]);

                pointsTable.Children.Add(stackPanel);    
            }

            return points;
        }

        
        private Point[] generatePointList(TextBox[,] points)
        {
            Point[] pointsList = new Point[points.GetLength(0)];

            for(int i = 0; i <pointsList.GetLength(0); i++)
            {
                try
                {
                    pointsList[i] = new Point(double.Parse(points[i, 0].Text), double.Parse(points[i, 1].Text));
                }
                catch(Exception exp)
                {
                   Utilities.showErrorMessage(exp.Message);
                }          
            }

            return pointsList;
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            bool radio;
            int x = int.Parse(holesQuantityTextBox.Text);
            if((bool)randomRadioButton.IsChecked == true)
            {
                radio = true;
            }
            else if((bool)holesRadioButton.IsChecked)
            {
                radio = false;
            }
            else
            {
                radio = false;
            }
            Matrix matrix = new Matrix(x, x, radio);
            matrixToCalc = matrix;
  
        } 

    }
}
