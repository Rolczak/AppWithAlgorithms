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

        public static void drawMatrix(ref Grid grid, Matrix matrix)
        {

        }
    }
}
