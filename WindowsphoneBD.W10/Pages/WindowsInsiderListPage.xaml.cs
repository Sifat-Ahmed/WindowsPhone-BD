//---------------------------------------------------------------------------
//
// <copyright file="WindowsInsiderListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/22/2017 12:04:40 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Twitter;
using WindowsphoneBD.Sections;
using WindowsphoneBD.ViewModels;
using AppStudio.Uwp;

namespace WindowsphoneBD.Pages
{
    public sealed partial class WindowsInsiderListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public WindowsInsiderListPage()
        {
			ViewModel = ViewModelFactory.NewList(new WindowsInsiderSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("18ec7610-97dc-44e9-86a6-3ec49a6f3720");
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
