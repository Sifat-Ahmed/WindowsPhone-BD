using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Rss;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using WindowsphoneBD.Navigation;
using WindowsphoneBD.ViewModels;

namespace WindowsphoneBD.Sections
{
    public class LatestOnWindowsphoneBDSection : Section<RssSchema>
    {
		private RssDataProvider _dataProvider;

		public LatestOnWindowsphoneBDSection()
		{
			_dataProvider = new RssDataProvider();
		}

		public override async Task<IEnumerable<RssSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new RssDataConfig
            {
                Url = new Uri("https://mspoweruser.com/feed"),
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<RssSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<RssSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<RssSchema>
                {
                    Title = "Latest on Windowsphone BD",

                    Page = typeof(Pages.LatestOnWindowsphoneBDListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
						viewModel.Header = item.Author.ToSafeString();
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.Description = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());

						viewModel.GroupBy = item.Author.SafeType();

						viewModel.OrderBy = item.Author;
                    },
					OrderType = OrderType.Ascending,
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.LatestOnWindowsphoneBDDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<RssSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, RssSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = "";
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.MediaUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<RssSchema>>
                {
                };

                return new DetailPageConfig<RssSchema>
                {
                    Title = "Latest on Windowsphone BD",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
