using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.Rss;
using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Twitter;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using WindowsphoneBD.Sections;


namespace WindowsphoneBD.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel LatestOnWindowsphoneBD { get; private set; }
        public ListViewModel YouTube { get; private set; }
        public ListViewModel WindowsInsider { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Windowsphone-BD";
            LatestOnWindowsphoneBD = ViewModelFactory.NewList(new LatestOnWindowsphoneBDSection(), visibleItems);
            YouTube = ViewModelFactory.NewList(new YouTubeSection(), visibleItems);
            WindowsInsider = ViewModelFactory.NewList(new WindowsInsiderSection(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return LatestOnWindowsphoneBD;
            yield return YouTube;
            yield return WindowsInsider;
        }
    }
}
