using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace OiM_UWP.Views
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class HungarianView : Page
    {
        private TextBox[,] gridTextBox;
        private Matrix matrixToCalc;
        public HungarianView()
        {
            InitializeComponent();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(sizeTextBox.Text);
            Matrix matrix = new Matrix(x, x, (bool)randomCheckBox.IsChecked);
            matrixToCalc = matrix;
            gridTextBox = createMatrixGrid(matrix);
        }

        private Matrix getMatrixFromGrid(TextBox[,] textBoxes)
        {
            Matrix matrix = new Matrix(textBoxes.GetLength(1), textBoxes.GetLength(0), false);
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
            int cols = matrix.getMatrix().GetLength(0);
            int rows = matrix.getMatrix().GetLength(1);
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
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        Text = matrix.getMatrix()[i, j].ToString()
                    };
                    matrixGrid.Children.Add(textBox);
                    Grid.SetColumn(textBox, i);
                    Grid.SetRow(textBox, j);
                    textBox.HorizontalAlignment = HorizontalAlignment.Center;
                    textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                    textBox.VerticalAlignment = VerticalAlignment.Center;
                    textBox.Header = "(" + (i + 1).ToString() + " , " + (j + 1).ToString() + ")";
                    textBox.Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorDark2"]);
                    textBoxes[i, j] = textBox;
                }
            }

            return textBoxes;
        }
        private async void calculateMatrix(object sender, RoutedEventArgs e)
        {
            try
            {
                matrixToCalc = getMatrixFromGrid(gridTextBox);
                Matrix matrixCopy = getMatrixFromGrid(gridTextBox);
                matrixCopy = HungarianAlgorithm.subMinFromRow(matrixCopy);
                matrixCopy = HungarianAlgorithm.subMinFromCols(matrixCopy);
                matrixCopy = HungarianAlgorithm.markLines(matrixCopy);
                matrixCopy = HungarianAlgorithm.testResult(matrixCopy);

                for (int i = 0; i < matrixCopy.getMatrix().GetLength(0); ++i)
                {
                    for (int j = 0; j < matrixCopy.getMatrix().GetLength(1); ++j)
                    {
                        if (matrixCopy.getMatrix()[i, j] == 999)
                        {
                            matrixCopy.setCell(i, j, 1);
                            gridTextBox[i, j].Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColorLight2"]);
                        }
                        else
                        {
                            matrixCopy.setCell(i, j, 0);
                        }
                    }

                }

                int cost = 0;
                for (int i = 0; i < matrixToCalc.getMatrix().GetLength(0); ++i)
                {
                    for (int j = 0; j < matrixToCalc.getMatrix().GetLength(1); ++j)
                    {
                        cost += matrixToCalc.getMatrix()[i, j] * matrixCopy.getMatrix()[i, j];
                    }
                }
                resultGrid.Visibility = Visibility.Visible;
                resultGrid.Opacity = 0;
                resultGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                resultGrid.Opacity = 1;
                resultTextBlock.Text = "Wynik: " + cost.ToString();

                matrixGrid.Visibility = Visibility.Visible;
                matrixGrid.Opacity = 0;
                matrixGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                matrixGrid.Opacity = 1;
            }
            catch (Exception exp)
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = exp.Message, CloseButtonText = "ok" };
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
            }
            else
            {
                ContentDialog dialog = new ContentDialog { Title = "Information", Content = "Nie wybrano pliku", CloseButtonText = "ok" };
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

    }
}
