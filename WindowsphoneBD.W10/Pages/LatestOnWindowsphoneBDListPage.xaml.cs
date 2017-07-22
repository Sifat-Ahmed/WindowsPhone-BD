//---------------------------------------------------------------------------
//
// <copyright file="LatestOnWindowsphoneBDListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/22/2017 12:04:40 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using WindowsphoneBD.Sections;
using WindowsphoneBD.ViewModels;
using AppStudio.Uwp;

namespace WindowsphoneBD.Pages
{
    public sealed partial class LatestOnWindowsphoneBDListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public LatestOnWindowsphoneBDListPage()
        {
			ViewModel = ViewModelFactory.NewList(new LatestOnWindowsphoneBDSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("a04a9153-df82-4398-9c88-6c0ca419cd22");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
