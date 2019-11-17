using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace OiM_UWP
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private NavigationViewItem _lastItem;
        private void nvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == _lastItem)
            {
                return;
            }

            string clickedView = item.Tag?.ToString() ?? "SettingsView";
            if (!NavigateToView(clickedView))
            {
                return;
            }

            _lastItem = item;
        }

        private bool NavigateToView(string clickedView)
        {
            Type view = Assembly.GetExecutingAssembly().GetType($"OiM_UWP.Views.{clickedView}");
            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
            {
                return false;
            }
            contentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void nvSample_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (contentFrame.CanGoBack)
            {
                contentFrame.GoBack(new EntranceNavigationTransitionInfo());
            }
        }

        private void contentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new NavigationException(
               $"Navigation failed {e.Exception.Message} for {e.SourcePageType.FullName}");
        }


    }
    internal class NavigationException : Exception
    {
        public NavigationException(string msg) : base(msg)
        {

        }
    }
}
