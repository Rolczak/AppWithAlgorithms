using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class JohnsonView : Page
    {
        private TextBox[,] costs_TextBoxes;
        private List<int>[] lists;
        private List<int> queue;
        private List<string>[] axis;

        private int[,] costs;


        public JohnsonView()
        {
            this.InitializeComponent();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            int columns;

            if(int.Parse(sizeTextBox.Text) < 0)
            {
                columns = int.Parse(sizeTextBox.Text) * -1;
                sizeTextBox.Text = (int.Parse(sizeTextBox.Text) * -1).ToString();
            }
            else
            {
                columns = int.Parse(sizeTextBox.Text);
            }

            Matrix matrix = new Matrix(2, columns, true);
            try
            {
                costs_TextBoxes = Utilities.drawMatrix(matrixGrid, matrix, Utilities.MatrixDisplayMethod.TextBoxesWithHeaders, "Masz", "Zad");
            }
            catch(Exception exp)
            {
                Utilities.showErrorMessage(exp.Message);
            }  
        }

        private int[,] CreateCostsMatrix(TextBox[,] cost_textBoxes)
        {
            int[,] cost_matrix = new int[cost_textBoxes.GetLength(0), cost_textBoxes.GetLength(1)];

            for(int i=0; i< cost_textBoxes.GetLength(0); i++)
            {
                for(int j=0; j< cost_textBoxes.GetLength(1); j++)
                {
                    cost_matrix[i, j] = int.Parse(cost_textBoxes[i, j].Text);
                }
            }

            return cost_matrix;
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            costs = CreateCostsMatrix(costs_TextBoxes);
            lists = JohnsonAlgorithm.CreateLists(costs);
            queue = JohnsonAlgorithm.ConnectLists(lists[0], lists[1]);
            QueueList.Text = "Kolejka: "+ String.Join(",", queue);
            axis = JohnsonAlgorithm.CreateTasksAxis(costs, queue);
            Axis1.Text = String.Join(" ", axis[0]);
            Axis2.Text = String.Join(" ", axis[1]);
            cost.Text = axis[1].Count.ToString();
            Utilities.PlayFadeAnim(SBResult);

        }
    }
}
