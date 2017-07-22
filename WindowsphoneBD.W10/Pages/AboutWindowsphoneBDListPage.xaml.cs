//---------------------------------------------------------------------------
//
// <copyright file="AboutWindowsphoneBDListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/22/2017 12:04:40 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Html;
using WindowsphoneBD.Sections;
using WindowsphoneBD.ViewModels;
using AppStudio.Uwp;

namespace WindowsphoneBD.Pages
{
    public sealed partial class AboutWindowsphoneBDListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }

        private DataTransferManager _dataTransferManager;

		#region	HtmlContent
		public string HtmlContent
        {
            get { return (string)GetValue(HtmlContentProperty); }
            set { SetValue(HtmlContentProperty, value); }
        }

		public static readonly DependencyProperty HtmlContentProperty = DependencyProperty.Register("HtmlContent", typeof(string), typeof(AboutWindowsphoneBDListPage), new PropertyMetadata(string.Empty));
		#endregion
        public AboutWindowsphoneBDListPage()
        {
			ViewModel = ViewModelFactory.NewList(new AboutWindowsphoneBDSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("21996ff9-c110-4d20-ae57-53bc4d78dd65");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
			
			if (ViewModel.Items != null && ViewModel.Items.Count > 0)
			{
                HtmlContent = ViewModel.Items[0].Content;
            }			
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;		
            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
