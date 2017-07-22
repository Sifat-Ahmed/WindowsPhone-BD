//---------------------------------------------------------------------------
//
// <copyright file="YouTubeListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/22/2017 12:04:40 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using WindowsphoneBD.Sections;
using WindowsphoneBD.ViewModels;
using AppStudio.Uwp;

namespace WindowsphoneBD.Pages
{
    public sealed partial class YouTubeListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public YouTubeListPage()
        {
			ViewModel = ViewModelFactory.NewList(new YouTubeSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("9bb5c57c-d6a9-4635-804d-10e63a3ab1f0");
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
