using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using WindowsphoneBD.Navigation;
using WindowsphoneBD.ViewModels;

namespace WindowsphoneBD.Sections
{
    public class WindowsInsiderSection : Section<TwitterSchema>
    {
		private TwitterDataProvider _dataProvider;

		public WindowsInsiderSection()
		{
			_dataProvider = new TwitterDataProvider(new TwitterOAuthTokens
			{
				ConsumerKey = "O0aR1RwIP2z9WU7rrjN9xuS07",
                    ConsumerSecret = "kTkpxUPOkna7lR0ljMYUfPfdf0vgNWCna8D42RzOHPSj97I8Ar",
                    AccessToken = "193341077-Tj668nMWteStmdXwC0AEQ2eHEX68FNdlbGGhWfZX",
                    AccessTokenSecret = "1ZeI0YAwtNxujK2bTVKZig7JkyrMnhJ93bdh6VAQXuMHO"
			});
		}

		public override async Task<IEnumerable<TwitterSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new TwitterDataConfig
            {
                QueryType = TwitterQueryType.User,
                Query = @"@windowsinsider"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<TwitterSchema>> GetNextPageAsync()
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

        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "@WindowsInsider",

                    Page = typeof(Pages.WindowsInsiderListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.UserProfileImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return new NavInfo
                        {
                            NavigationType = NavType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage
        {
            get { return null; }
        }
    }
}
