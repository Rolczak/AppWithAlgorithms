using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace OiM_UWP
{
    public class Utilities
    {
        public static async void showErrorMessage(string Message)
        {
            ContentDialog dialog = new ContentDialog { Title = "Error", Content = Message, CloseButtonText = "ok" };
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
