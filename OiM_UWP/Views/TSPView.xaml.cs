using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        private TextBox[,] gridTextBoxes;
        private double[,] distancesMatrix;

        private Point[] holes;
        private double[] distances_array;
        private List<int> vertices_array;
        private double[] costs_array;
        private double[] result;

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
            Point[] pointsList = new Point[1];
            try
            {
                pointsList = new Point[points.GetLength(0) + 1];
            }
            catch (Exception exp)
            {
                Utilities.showErrorMessage(exp.Message);
            }

            pointsList[0] = new Point(double.Parse(vertexAxTextBox.Text), double.Parse(vertexAyTextBox.Text));
            Point upperVertex = new Point(double.Parse(vertexBxTextBox.Text), double.Parse(vertexByTextBox.Text));

            for(int i = 1; i <pointsList.GetLength(0); i++)
            {
                try
                {
                    TravellingSalesmanProblem.CheckCoordinatesCorrectness(pointsList[0], upperVertex, double.Parse(points[i-1, 0].Text), double.Parse(points[i-1, 1].Text));
                    pointsList[i] = new Point(double.Parse(points[i-1, 0].Text), double.Parse(points[i-1, 1].Text));
                }
                catch(Exception exp)
                {
                    Utilities.showErrorMessage(exp.Message);
                    return new Point[0];
                }          
            }

            return pointsList;
        }

        private void generate(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(holesQuantityTextBox.Text);
            
            if (randomRadioButton.IsChecked == true)
            {
                distancesMatrix = new double[x + 1, x + 1];
                Random random = new Random();

                for (int i = 0; i < x + 1; ++i)
                {
                    for (int j = 0; j < x + 1; j++)
                    {
                        distancesMatrix[i, j] = random.Next(1, 99);
                        distancesMatrix[i, i] = 0;
                    }
                }
            }
            else if (holesRadioButton.IsChecked == true)
            {
                distancesMatrix = TravellingSalesmanProblem.CreateDistancesMatrix(generatePointList(pointsTextBoxes));
            }

            gridTextBoxes = createMatrixGrid(distancesMatrix);
        }

        private TextBox[,] createMatrixGrid(double[,] distancesMatrix)
        {
            matrixGrid.Children.Clear();
            matrixGrid.ColumnDefinitions.Clear();
            matrixGrid.RowDefinitions.Clear();
            int cols = distancesMatrix.GetLength(0);
            int rows = distancesMatrix.GetLength(1);
            TextBox[,] textBoxes = new TextBox[rows, cols];
            for (int i = 0; i < cols; i++)
            {

                ColumnDefinition col = new ColumnDefinition
                {
                    MinWidth = 20
                };
                matrixGrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    MinHeight = 20
                };
                matrixGrid.RowDefinitions.Add(row);
            }
            matrixGrid.Visibility = Visibility.Visible;
            matrixGrid.Opacity = 0;
            matrixGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
            matrixGrid.Opacity = 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        Text = distancesMatrix[i, j].ToString()
                    };
                    matrixGrid.Children.Add(textBox);
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    textBox.HorizontalAlignment = HorizontalAlignment.Center;
                    textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                    textBox.VerticalAlignment = VerticalAlignment.Center;
                    textBox.Header = "(" + (i).ToString() + " , " + (j).ToString() + ")";
                    textBox.Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorDark2"]);
                    textBoxes[i, j] = textBox;
                }
            }

            return textBoxes;
        }

        private double[,] getMatrixFromGrid(TextBox[,] textBoxes)
        {
            double[,] matrix = new double[textBoxes.GetLength(1), textBoxes.GetLength(0)];
            for (int i = 0; i < textBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < textBoxes.GetLength(1); j++)
                {
                    int val = int.Parse(textBoxes[i, j].Text);
                    matrix[i,j] = val;
                }
            }
            return matrix;
        }

        private void calculate(object sender, RoutedEventArgs e)
        {
            distancesMatrix = getMatrixFromGrid(gridTextBoxes);
            int q = int.Parse(holesQuantityTextBox.Text);
            int s = 0;

            vertices_array = new List<int>();
            // Lista length_array zawiera koszty przechodzenia koljnych fragmentow trasy
            costs_array = new double[q + 1];
            vertices_array.Add(s);
            costs_array[0] = 0;
            costs_array[q] = 0;
            // Lista distances_array zawiera jeden z wierszo macierzy distances_matrix wybrany na podstawie podanego
            // wierzcholka startowego
            distances_array = TravellingSalesmanProblem.CreateDistancesTable(distancesMatrix, s);
 
            if (q > 1)
            {
                vertices_array.Add(TravellingSalesmanProblem.FindMaxTableElementVertex(distances_array));           
                costs_array[1] = TravellingSalesmanProblem.CalculateMinCost(distancesMatrix, vertices_array)[0];
                distances_array = TravellingSalesmanProblem.ModifyDistanceArray(distances_array, distancesMatrix, vertices_array[1]);
                vertices_array.Add(s);

                for (int i = 2; i < q + 1; i++)
                {
                    s = TravellingSalesmanProblem.FindMaxTableElementVertex(distances_array);
                    result = TravellingSalesmanProblem.CalculateMinCost(distancesMatrix, vertices_array, s, i);
                    costs_array[i] = result[0];
                    vertices_array.Insert((int)result[1], s);
                    distances_array = TravellingSalesmanProblem.ModifyDistanceArray(distances_array, distancesMatrix, s);
                }

                resultGrid.Visibility = Visibility.Visible;
                cost.Text = TravellingSalesmanProblem.CalculateTotalCost(costs_array).ToString();
                route.Text = string.Join(" ", vertices_array);
            }
        }

        private async void openFileAsync(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".txt");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    IList<string> text = await FileIO.ReadLinesAsync(file);
                    int x = int.Parse(text[0]);
                    double[,] matrix = new double[x, x];

                    for (int i = 0; i < x; i++)
                    {
                        string[] line = text[i + 1].Split(" ");
                        for (int j = 0; j < x; j++)
                        {
                            matrix[i,j] = int.Parse(line[j]);
                        }
                    }

                    gridTextBoxes = createMatrixGrid(matrix);
                    distancesMatrix = matrix;

                }
                catch (Exception exp)
                {
                    ContentDialog dialog = new ContentDialog { Title = "Error", Content = exp.Message, CloseButtonText = "ok" };
                    ContentDialogResult result = await dialog.ShowAsync();
                }
            }
            else
            {
                ContentDialog dialog = new ContentDialog { Title = "Information", Content = "Nie wybrano pliku", CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }

        }
    }
}
