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
    public sealed partial class KnapsackView : Page
    {
        private TextBox[,] gridTextBox;
        private Matrix matrixToCalc;
        public KnapsackView()
        {
            this.InitializeComponent();
        }

        private async void Generate(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(itemsNumberTextBox.Text);
            Matrix matrix = new Matrix(2, x, (bool)randomCheckBox.IsChecked);
            matrixToCalc = matrix;
            try
            {
                gridTextBox = createMatrixGrid(matrix);
            }
            catch (Exception exp)
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = exp.Message, CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }
        }
        private Matrix getMatrixFromGrid(TextBox[,] textBoxes)
        {
            Matrix matrix = new Matrix(textBoxes.GetLength(0), textBoxes.GetLength(1), false);
            for (int i = 0; i < textBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < textBoxes.GetLength(1); j++)
                {
                    int val = int.Parse(textBoxes[i, j].Text);
                    matrix.setCell(i, j, val);
                }
            }
            return matrix;
        }

        private TextBox[,] createMatrixGrid(Matrix matrix)
        {
            matrixGrid.Children.Clear();
            matrixGrid.ColumnDefinitions.Clear();
            matrixGrid.RowDefinitions.Clear();
            int cols = matrix.getMatrix().GetLength(1);
            int rows = matrix.getMatrix().GetLength(0);
            TextBox[,] textBoxes = new TextBox[rows, cols];
            for (int i = 0; i <= cols; i++)
            {

                ColumnDefinition col = new ColumnDefinition
                {
                    MinWidth = 20
                };
                matrixGrid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i <= rows; i++)
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

            TextBlock textBlock = new TextBlock()
            {
                Text = "Wartość",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            matrixGrid.Children.Add(textBlock);
            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(textBlock, 1);

            textBlock = new TextBlock()
            {
                Text = "Waga",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            matrixGrid.Children.Add(textBlock);
            Grid.SetColumn(textBlock, 0);
            Grid.SetRow(textBlock, 2);

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        Text = matrix.getMatrix()[i - 1, j - 1].ToString()
                    };
                    matrixGrid.Children.Add(textBox);
                    Grid.SetColumn(textBox, j);
                    Grid.SetRow(textBox, i);
                    textBox.HorizontalAlignment = HorizontalAlignment.Center;
                    textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                    textBox.VerticalAlignment = VerticalAlignment.Center;
                    textBox.Header = "(" + (i).ToString() + " , " + (j).ToString() + ")";
                    textBox.Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorDark2"]);
                    textBoxes[i - 1, j - 1] = textBox;
                }
            }
            for (int j = 1; j <= cols; j++)
            {
                textBlock = new TextBlock()
                {
                    Text = "Przedmiot " + j.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                matrixGrid.Children.Add(textBlock);
                Grid.SetColumn(textBlock, j);
                Grid.SetRow(textBlock, 0);
            }
            return textBoxes;
        }

        private async void calculateMatrixGreedy(object sender, RoutedEventArgs e)
        {
            try
            {
                Matrix matrix = getMatrixFromGrid(gridTextBox);
                int capacity = int.Parse(capacityTextBox.Text);
                if (KnapsackProblem.CheckWeights(matrix, capacity) == 0)
                {
                    ContentDialog dialog = new ContentDialog { Title = "Information", Content = "Nic się nie zmieści do plecaka", CloseButtonText = "Ok" };
                    ContentDialogResult result = await dialog.ShowAsync();
                }
                else
                {
                    double[] quot = KnapsackProblem.CountQuotient(matrix);
                    int value = 0;
                    Matrix newMatrix = KnapsackProblem.GreedyMethod(matrix, capacity, quot, ref value);
                    resultGreedySumTextBlock.Text = "Wartość Plecaka: " + value.ToString();
                    resultGreedyTextBlock.Text = "Przedmioty do zabrania: ";
                    for (int i = 0; i < newMatrix.getMatrix().GetLength(1); i++)
                    {
                        resultGreedyTextBlock.Text += " Przedmiot ";
                        resultGreedyTextBlock.Text += newMatrix.getMatrix()[1, i].ToString();
                        resultGreedyTextBlock.Text += " x ";
                        resultGreedyTextBlock.Text += newMatrix.getMatrix()[0, i].ToString();
                        resultGreedyTextBlock.Text += " ; ";
                    }
                    resultGridGreedy.Visibility = Visibility.Visible;
                    resultGridGreedy.Opacity = 0;
                    resultGridGreedy.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                    resultGridGreedy.Opacity = 1;
                }
            }
            catch (Exception exp)
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = exp.Message, CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }
        }

        private async void calculateMatrixDynamic(object sender, RoutedEventArgs e)
        {
            try
            {
                Matrix matrix = getMatrixFromGrid(gridTextBox);
                int capacity = int.Parse(capacityTextBox.Text);
                Matrix[] pq = KnapsackProblem.DynamicMethod(matrix, capacity);
                int[] resultTable = KnapsackProblem.ShowItems(matrix, pq[1]);
                resultDynamicTextBlock.Text = "Przedmioty do zabrania: " + string.Join(",", resultTable.Select(i => i.ToString()));
                resultDynamicSumTextBlock.Text = "Wartość Plecaka: " + pq[0].getMatrix()[pq[0].getMatrix().GetLength(0) - 1, pq[0].getMatrix().GetLength(1) - 1].ToString();
                resultGridDynamic.Visibility = Visibility.Visible;
                resultGridDynamic.Opacity = 0;
                resultGridDynamic.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                resultGridDynamic.Opacity = 1;
            }
            catch (Exception exp)
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = exp.Message, CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
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
                    Matrix matrix = new Matrix(x, x, false);

                    for (int i = 0; i < x; i++)
                    {
                        string[] line = text[i + 1].Split(" ");
                        for (int j = 0; j < x; j++)
                        {
                            matrix.setCell(i, j, int.Parse(line[j]));
                        }
                    }
                    string[] cap = text[x + 1].Split(" ");
                    capacityTextBox.Text = cap[0].ToString();

                    gridTextBox = createMatrixGrid(matrix);
                    matrixToCalc = matrix;

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

        private async void saveFileAsync(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            picker.SuggestedFileName = "Nowy Dokument";
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, string.Join(Environment.NewLine, matrixToCalc.getAsText()));
                await FileIO.AppendTextAsync(file, Environment.NewLine + capacityTextBox.Text);
            }
            else
            {
                ContentDialog dialog = new ContentDialog { Title = "Information", Content = "Nie wybrano pliku", CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }
        }
    }
}
