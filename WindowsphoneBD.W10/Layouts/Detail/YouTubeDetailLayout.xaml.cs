using System;
using Windows.Devices.Input;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsphoneBD.Services;

namespace WindowsphoneBD.Layouts.Detail
{
    public sealed partial class YouTubeDetailLayout : BaseDetailLayout
    {
        public YouTubeDetailLayout()
        {
            InitializeComponent();
        }

        private void WebView_Unloaded(object sender, RoutedEventArgs e)
        {
            WebView webView = sender as WebView;
            if (webView != null) webView.NavigateToString(string.Empty);
        }
    }
}
