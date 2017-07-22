using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsphoneBD.Sections;
namespace WindowsphoneBD.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Windowsphone-BD";
            LatestOnWindowsphoneBD = ViewModelFactory.NewList(new LatestOnWindowsphoneBDSection());
            YouTube = ViewModelFactory.NewList(new YouTubeSection());
            WindowsInsider = ViewModelFactory.NewList(new WindowsInsiderSection());
            AboutWindowsphoneBD = ViewModelFactory.NewList(new AboutWindowsphoneBDSection());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel LatestOnWindowsphoneBD { get; private set; }
        public ListViewModel YouTube { get; private set; }
        public ListViewModel WindowsInsider { get; private set; }
        public ListViewModel AboutWindowsphoneBD { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return LatestOnWindowsphoneBD;
            yield return YouTube;
            yield return WindowsInsider;
            yield return AboutWindowsphoneBD;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
