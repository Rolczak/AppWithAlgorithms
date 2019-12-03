using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OiM_UWP
{
    public class Utilities
    {
        public static async void showErrorMessage(string Message)
        {
            await ContentDialogMaker.CreateContentDialogAsync(new ContentDialog
            {
                Title = "Warning",
                Content = new TextBlock
                {
                    Text = Message,
                    TextWrapping = TextWrapping.Wrap
                },
                PrimaryButtonText = "OK"
            }, awaitPreviousDialog: true);
        }

        public enum MatrixDisplayMethod
        {
            TextBoxesWithHeaders,
            OnlyTextBoxes
        }
        public static void PlayFadeAnim(UIElement uIElement)
        {
            uIElement.Visibility = Visibility.Visible;
            uIElement.Opacity = 0;
            uIElement.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
            uIElement.Opacity = 1;
        }
        private static TextBox createTextBox()
        {
            return new TextBox()
            {
                MaxWidth = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
        }
        public static TextBox[,] drawMatrix(Grid grid, Matrix matrix, MatrixDisplayMethod matrixDisplay, string rowHeaders = null, string colHeaders = null)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            //ADD Headers
            if (matrixDisplay == MatrixDisplayMethod.TextBoxesWithHeaders)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star), MaxWidth = 200 });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Star), MaxHeight = 200 });
            }

            TextBox[,] textBoxes = new TextBox[matrix.getMatrix().GetLength(0), matrix.getMatrix().GetLength(1)];
            //ADD Rows and Colums definition
            for (int rows = 0; rows < matrix.getMatrix().GetLength(0); rows++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { MinHeight = 20, MaxHeight = 50 });
            }
            for (int cols = 0; cols < matrix.getMatrix().GetLength(1); cols++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 20, MaxWidth = 50 });
            }

            //ADD texboxes
            if (matrixDisplay == MatrixDisplayMethod.TextBoxesWithHeaders)
            {
                for (int rows = 0; rows < matrix.getMatrix().GetLength(0); rows++)
                {
                    for (int cols = 0; cols < matrix.getMatrix().GetLength(1) ; cols++)
                    {
                        textBoxes[rows, cols] = createTextBox();
                        textBoxes[rows, cols].Text = matrix.getMatrix()[rows, cols].ToString();
                        grid.Children.Add(textBoxes[rows, cols]);
                        Grid.SetColumn(textBoxes[rows, cols], cols + 1);
                        Grid.SetRow(textBoxes[rows, cols], rows + 1);
                    }
                }
            }
            else
            {
                for (int rows = 0; rows < matrix.getMatrix().GetLength(0); rows++)
                {
                    for (int cols = 0; cols < matrix.getMatrix().GetLength(1); cols++)
                    {
                        textBoxes[rows, cols] = createTextBox();
                        textBoxes[rows, cols].Text = matrix.getMatrix()[rows, cols].ToString();
                        grid.Children.Add(textBoxes[rows, cols]);
                        Grid.SetColumn(textBoxes[rows, cols], cols);
                        Grid.SetRow(textBoxes[rows, cols], rows);
                    }
                }
            }

            //FILL headers
            if (matrixDisplay == MatrixDisplayMethod.TextBoxesWithHeaders)
            {
                for (int columns = 1; columns < matrix.getMatrix().GetLength(1) + 1; columns++)
                {
                    TextBlock text = new TextBlock()
                    {
                        Text = colHeaders + " " + columns,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        MaxHeight = 100,
                        MaxWidth = 100,
                    
                    };
                    grid.Children.Add(text);
                    Grid.SetColumn(text, columns);
                    Grid.SetRow(text, 0);
                }
                for (int rows = 1; rows < matrix.getMatrix().GetLength(0) + 1; rows++)
                {
                    TextBlock text = new TextBlock() 
                    {
                        Text = rowHeaders + " " + rows,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        MaxHeight = 100,
                        MaxWidth = 100,
                    };
                    grid.Children.Add(text);
                    Grid.SetColumn(text, 0);
                    Grid.SetRow(text, rows);
                }
            }
            PlayFadeAnim(grid);
            return textBoxes;
        }
    }
}
