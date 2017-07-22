//---------------------------------------------------------------------------
//
// <copyright file="LatestOnWindowsphoneBDDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/22/2017 12:04:40 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.Rss;
using WindowsphoneBD.Sections;
using WindowsphoneBD.Navigation;
using WindowsphoneBD.ViewModels;
using AppStudio.Uwp;

namespace WindowsphoneBD.Pages
{
    public sealed partial class LatestOnWindowsphoneBDDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public LatestOnWindowsphoneBDDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new LatestOnWindowsphoneBDSection());
            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

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
