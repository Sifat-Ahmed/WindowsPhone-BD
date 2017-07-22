using System;
using System.Windows.Input;

using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Commands;

namespace WindowsphoneBD.ViewModels
{
    public class AboutThisAppViewModel : PageViewModelBase
    {
		public AboutThisAppViewModel()
        {
            this.AppName = "Windowsphone-BD";
            this.Title = "NavigationPaneAbout".StringResource();
            this.Publisher = "NerdDev Studio";
            this.AppVersion = string.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            this.AboutText = "Windowsphone Bangladesh app is here ! Now you can visit our blog directly from th" +
    "is app. no need to search for links. Keep up to date with new tricks and tips. p" +
    "lay youtube tutorials directly from the app and many more !";
            this.AppLogo = new BitmapImage(new Uri("ms-appx:///Assets/ApplicationLogo.png"));
            this.Privacy = "https://appstudio.windows.com/home/appprivacyterms";
            this.WasLibs = "https://github.com/wasteam/waslibs";
            this.WindowsAppStudioWeb = "https://appstudio.windows.com/";
            this.NewtonsoftWeb = "http://www.newtonsoft.com/json";
        }

		public string AppName { get; set; }
        public string Publisher { get; set; }
        public string AppVersion { get; set; }
        public string AboutText { get; set; }
        public string Privacy { get; set; }
        public string WasLibs { get; set; }
        public string WindowsAppStudioWeb { get; set; }
        public string NewtonsoftWeb { get; set; }
        public BitmapImage AppLogo { get; set; }

		private bool _isMoreInfoVisible;
        public bool IsMoreInfoVisible
        {
            get { return _isMoreInfoVisible; }
            set { SetProperty(ref _isMoreInfoVisible, value); }
        }

        private ICommand _viewMoreInfoCommand;
        public ICommand ViewMoreInfoCommand
        {
            get
            {
                if (_viewMoreInfoCommand == null)
                {
                    _viewMoreInfoCommand = new RelayCommand(() => { IsMoreInfoVisible = !IsMoreInfoVisible; });
                }
                return _viewMoreInfoCommand;
            }
        }
    }
}

