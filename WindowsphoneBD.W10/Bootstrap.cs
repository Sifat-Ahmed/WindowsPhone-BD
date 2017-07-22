//---------------------------------------------------------------------------
//
// <copyright file="Bootstrap.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>7/22/2017 12:04:40 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Storage;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;

using WindowsphoneBD.Services;

namespace WindowsphoneBD
{
    static class Bootstrap
    {
        private static readonly Guid APP_ID = new Guid("897876d9-c164-42a0-92bf-f518d84b0879");

		public static void Init()
        {
			InitializeTelemetry();

			BitmapCache.ClearCacheAsync(TimeSpan.FromHours(48)).FireAndForget();
		}


		private static void InitializeTelemetry()
        {
            var init = !string.IsNullOrEmpty(ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string);
            if (!init)
            {
                string deviceType = IsOnPhoneExecution() ? LocalSettingNames.PhoneValue : LocalSettingNames.WindowsValue;
                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] = deviceType;
                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] = ValidateStoreId();
            }
        }

        private static bool IsOnPhoneExecution()
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            return (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
        }

        private static Guid ValidateStoreId()
        {
            try
            {
                Guid storeId = CurrentApp.AppId;
                if (storeId != Guid.Empty && storeId != APP_ID)
                {
                    return storeId;
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}