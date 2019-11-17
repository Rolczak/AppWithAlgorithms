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
    public sealed partial class ArithmeticView : Page
    {
        TextBox[,] alphabet;
        int itemsNumber = -1;
        public ArithmeticView()
        {
            this.InitializeComponent();
        }

        private async void Generate(object sender, RoutedEventArgs e)
        {
            try
            {
                itemsNumber = int.Parse(letterCountTextBox.Text);
            }
            catch
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = "Podaną złą ilość liter alfabetu", CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }
            if (itemsNumber > 0)
            {
                alphabet = generateList(itemsNumber);
                alphabetGrid.Visibility = Visibility.Visible;
                alphabetGrid.Opacity = 0;
                alphabetGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                alphabetGrid.Opacity = 1;
            }

        }
        private decimal[] parseProbabilities(TextBox[] probs)
        {
            decimal[] probabilities = new decimal[probs.Length];
            for (int i = 0; i < probs.Length; i++)
            {
                probabilities[i] = decimal.Parse(probs[i].Text);
            }
            return probabilities;
        }
        private void addSubinterval(Interval[] intervals, char c, char[] alph)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = c + ":  ";
            stackPanel.Children.Add(textBlock);
            int index = 0;
            for (int i = 0; i < alphabet.GetLength(0); i++)
            {
                if (alph[i] == c)
                {
                    index = i;
                }
            }

            for (int i = 0; i < intervals.Length; i++)
            {
                TextBlock subInt = new TextBlock();
                subInt.Text = "[ " + intervals[i].l.ToString() + " ; " + intervals[i].r.ToString() + " )";
                stackPanel.Children.Add(subInt);
                if (i == index)
                    subInt.FontWeight = Windows.UI.Text.FontWeights.ExtraBlack;
            }

            subIntervalsGrid.Children.Add(stackPanel);
        }

        private bool checkLetterInAlphabet(char letter, char[] alphabet)
        {
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (letter == alphabet[i])
                    return true;
            }
            return false;
        }

        private async void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                char[] message = messageTextBox.Text.ToCharArray();
                Interval interval = new Interval();
                TextBox[] probablities = new TextBox[itemsNumber];
                char[] letters = new char[itemsNumber];
                for (int i = 0; i < itemsNumber; i++)
                {
                    probablities[i] = alphabet[i, 1];
                    var characters = alphabet[i, 0].Text.ToCharArray();
                    letters[i] = characters[0];
                }
                bool isMessageInAlphabet = false;
                for (int i = 0; i < message.Length; i++)
                {
                    isMessageInAlphabet = checkLetterInAlphabet(message[i], letters);
                    if (!isMessageInAlphabet)
                        break;
                }
                if (!isMessageInAlphabet)
                {
                    ContentDialog dialog = new ContentDialog { Title = "Error", Content = "Wiadomość niepoprawna", CloseButtonText = "ok" };
                    ContentDialogResult result = await dialog.ShowAsync();
                    return;
                }


                try
                {
                    subIntervalsGrid.Children.Clear();
                    checkProbability(probablities);
                    decimal[] probs = parseProbabilities(probablities);
                    probs = ArithmeticCoding.MakeProportions(probs);
                    for (int p = 0; p < message.GetLength(0); p++)
                    {
                        Interval[] intervals = ArithmeticCoding.MakeSubintervals(interval, probs);
                        addSubinterval(intervals, message[p], letters);
                        interval = ArithmeticCoding.SetCurrentInterval(intervals, letters, message[p]);
                    }
                    resultCodingGrid.Visibility = Visibility.Visible;
                    resultCodingGrid.Opacity = 0;
                    resultCodingGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                    resultCodingGrid.Opacity = 1;
                    resultCodingTextBlock.Text = "Wynik: " + ((interval.l + interval.r) / 2).ToString();
                    subIntervalsGrid.Visibility = Visibility.Visible;
                    subIntervalsGrid.Opacity = 0;
                    subIntervalsGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
                    subIntervalsGrid.Opacity = 1;
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog { Title = "Error", Content = ex.Message, CloseButtonText = "ok" };
                    ContentDialogResult result = await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog { Title = "Error", Content = ex.Message, CloseButtonText = "ok" };
                ContentDialogResult result = await dialog.ShowAsync();
            }
        }

        private void checkProbability(TextBox[] probs)
        {
            decimal sum = 0;
            for (int i = 0; i < probs.Length; i++)
            {
                try
                {
                    sum += decimal.Parse(probs[i].Text);
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog { Title = "Error", Content = ex.Message, CloseButtonText = "ok" };
                }

            }
            if (sum != 1m)
                throw new Exception("Prawdopodobieństwo nie równa się 1");
        }

        private TextBox[,] generateList(int number)
        {
            //First clear children
            alphabetGrid.Children.Clear();
            alphabetGrid.RowDefinitions.Clear();
            alphabetGrid.ColumnDefinitions.Clear();
            alphabetGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star) });
            alphabetGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Star) });
            TextBox[] letters = new TextBox[number];
            TextBox[] probablity = new TextBox[number];

            for (int i = 0; i < number; i++)
            {
                alphabetGrid.RowDefinitions.Add(new RowDefinition() { MinHeight = 20, MaxHeight = 50 });
                letters[i] = new TextBox
                {
                    Header = "Litera " + i + ": ",
                    PlaceholderText = "wpisz litere",
                    MaxLength = 1
                };
                probablity[i] = new TextBox
                {
                    Header = "Prawdopodobieństwo " + i + " : ",
                    PlaceholderText = "wpisz prawdopodobieństwo",
                };
                alphabetGrid.Children.Add(letters[i]);
                alphabetGrid.Children.Add(probablity[i]);
                Grid.SetRow(letters[i], i);
                Grid.SetColumn(letters[i], 0);
                Grid.SetRow(probablity[i], i);
                Grid.SetColumn(probablity[i], 1);
            }
            TextBox[,] alphabet = new TextBox[number, 2];
            for (int i = 0; i < number; i++)
            {
                alphabet[i, 0] = letters[i];
                alphabet[i, 1] = probablity[i];
            }
            return alphabet;
        }

        private int getLetterIndexInAlphabet(char letter, char[] alphabet)
        {
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (letter == alphabet[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private void GenerateAlphabet(object sender, RoutedEventArgs e)
        {
            char[] textToParse = alphabetTextBox.Text.ToLower().ToCharArray();
            char[] alphabetFromText = new char[0];
            int[] lettersCount = new int[0];
            int counter = 0;
            for (int i = 0; i < textToParse.Length; i++)
            {
                if (!(checkLetterInAlphabet(textToParse[i], alphabetFromText)))
                {
                    char[] temp = new char[alphabetFromText.Length + 1];
                    int[] tempCount = new int[alphabetFromText.Length + 1];
                    for (int j = 0; j < alphabetFromText.Length; j++)
                    {
                        temp[j] = alphabetFromText[j];
                        tempCount[j] = lettersCount[j];
                    }
                    temp[counter] = textToParse[i];
                    tempCount[counter] = 1;
                    alphabetFromText = temp;
                    lettersCount = tempCount;
                    counter++;
                }
                else
                {
                    if (getLetterIndexInAlphabet(textToParse[i], alphabetFromText) != -1)
                    {
                        lettersCount[getLetterIndexInAlphabet(textToParse[i], alphabetFromText)]++;
                    }
                }
            }
            TextBox[,] list = generateList(alphabetFromText.Length);
            alphabet = list;
            decimal sum = 0;
            for (int i = 0; i < alphabetFromText.Length; i++)
            {
                list[i, 0].Text = alphabetFromText[i].ToString();
                if (i == alphabetFromText.Length - 1)
                {
                    list[i, 1].Text = (1m - sum).ToString();
                }
                else
                {
                    sum += decimal.Round(((decimal)lettersCount[i] / (decimal)textToParse.Length), 3, MidpointRounding.AwayFromZero);
                    list[i, 1].Text = decimal.Round(((decimal)lettersCount[i] / (decimal)textToParse.Length), 3, MidpointRounding.AwayFromZero).ToString();
                }

            }
            itemsNumber = alphabetFromText.Length;
            alphabetGrid.Visibility = Visibility.Visible;
            alphabetGrid.Opacity = 0;
            alphabetGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
            alphabetGrid.Opacity = 1;
        }

        private void Decode(object sender, RoutedEventArgs e)
        {
            Interval interval = new Interval(0, 1);
            decimal message = decimal.Parse(messageToDecodeTextBox.Text);
            string outMessage = "";
            TextBox[] probablities = new TextBox[itemsNumber];
            char[] letters = new char[itemsNumber];
            for (int i = 0; i < itemsNumber; i++)
            {
                probablities[i] = alphabet[i, 1];
            }
            decimal[] probs = parseProbabilities(probablities);
            probs = ArithmeticCoding.MakeProportions(probs);
            for (int i = 0; i < probablities.Length; i++)
            {
                Interval[] intervals = ArithmeticCoding.MakeSubintervals(interval, probs);
                for (int j = 0; j < intervals.Length; j++)
                {

                    if ((message > intervals[j].l) && (message < intervals[j].r))
                    {
                        outMessage += alphabet[j, 0].Text.ToString();
                        if (((intervals[j].l + intervals[j].r) / 2) == message)
                        {
                            i = intervals.Length;

                        }
                        else
                            interval = new Interval(intervals[j].l, intervals[j].r);
                    }
                    if (((intervals[j].l + intervals[j].r) / 2) == message)
                    {
                        j = intervals.Length;
                    }

                }
            }

            resultDecodingTextBlock.Text = outMessage;
            resultDeCodingGrid.Visibility = Visibility.Visible;
            resultDeCodingGrid.Opacity = 0;
            resultDeCodingGrid.OpacityTransition = new ScalarTransition() { Duration = new TimeSpan(0, 0, 0, 0, 500) };
            resultDeCodingGrid.Opacity = 1;
        }

    }
}
