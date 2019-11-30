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
    public sealed partial class LZWView : Page
    {
        Dictionary<string, int> alphabet = null;
        List<int> encodeList = null;
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        List<int> indexes = new List<int>();
        string message;
        char[] text;

        public LZWView()
        {
            this.InitializeComponent();
        }

        private TextBlock[,] CreateDictionary(Dictionary<string, int> dictionary)
        {
            dictionaryPanel.Children.Clear();
            TextBlock[,] pairs = new TextBlock[dictionary.Count, 2];
            int i = 0;

            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                pairs[i, 0] = new TextBlock
                {
                    Text = "Litera " + (pair.Key) + "   Indeks ",
                };
                pairs[i, 1] = new TextBlock
                {
                    Text = pair.Value.ToString(),
                };

                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(1, 5, 0, 1), HorizontalAlignment = HorizontalAlignment.Center };
                stackPanel.Children.Add(pairs[i, 0]);
                stackPanel.Children.Add(pairs[i, 1]);

                dictionaryPanel.Children.Add(stackPanel);
                i++;
            }

            return pairs;
        }
        private void Decode(object sender, RoutedEventArgs e)
        {
            resultDecode.Text = LZW.Decode(alphabet, encodeList);
        }
        private void encodeResult(object sender, RoutedEventArgs e)
        {
            text = messageTextBox.Text.ToCharArray(0, messageTextBox.Text.Length);
            dictionary = LZW.InitializeDictionary(text);
            alphabet = new Dictionary<string, int>(dictionary);
            encodeList = LZW.Encode(dictionary, text);
            result.Text = String.Join(", ", encodeList);
            CreateDictionary(dictionary);
        }
    }
}
