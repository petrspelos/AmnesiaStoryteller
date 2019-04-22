using System;

using AmnesiaStoryteller.ViewModels;

using Windows.UI.Xaml.Controls;

namespace AmnesiaStoryteller.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return ViewModelLocator.Current.MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
