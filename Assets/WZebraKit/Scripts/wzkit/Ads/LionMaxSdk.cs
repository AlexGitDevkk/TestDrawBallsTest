
#if MAXADS

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using LionStudios.Suite.Core;
using LionStudios.Suite.Ads;
using LionStudios.Suite.Debugging;
using LionStudios.Suite.Analytics;
using LionStudios.Suite.Analytics.Events;
using System;
using System.Threading.Tasks;
using LionStudios.Suite.GDPR;

namespace wzebra.kit.ads
{
    public class LionMaxSdk : ILionSdk, ILionAdProvider
    {
        public event UnityAction OnRewardedDisplayed;

        private static ApplovinMaxSettings _settings = new ApplovinMaxSettings();

        private float lastInterstitialShowTime;
        private float lastRvInterstitialShowTime;

        public int Priority => 1;

        public string Name => "MaxSDK";

        [LabelOverride("AppLovin MAX")]
        public class ApplovinMaxSettings : ILionSettingsInfo
        {
            [Header("General")] public string sdkKey = "";

            // rewarded ads
            [Header("Rewarded Ads")] public bool rewardedAdsDisabled = false;
            public string rewardedAdUnitIdAndroid = "";
            public string rewardedAdUnitIdIos = "";

            // interstitial ads
            [Header("Interstitial Ads")] public bool interstitialsDisabled = false;
            public string interstitialAdUnitIdAndroid = "";
            public string interstitialAdUnitIdIos = "";

            // banner ads
            [Header("Banner Ads")] public bool bannersDisabled = false;
            public string bannerAdUnitIdAndroid = "";
            public string bannerAdUnitIdIos = "";
        }

        private string InterstitialAdUnit
        {
            get
            {
#if UNITY_IOS
            return _settings.interstitialAdUnitIdIos;
#elif UNITY_ANDROID
                return _settings.interstitialAdUnitIdAndroid;
#endif
            }
        }

        private string RewardedAdUnit
        {
            get
            {
#if UNITY_IOS
            return _settings.rewardedAdUnitIdIos;
#elif UNITY_ANDROID
                return _settings.rewardedAdUnitIdAndroid;
#endif
            }
        }

        private string BannerAdUnit
        {
            get
            {
#if UNITY_IOS
            return _settings.bannerAdUnitIdIos;
#elif UNITY_ANDROID
                return _settings.bannerAdUnitIdAndroid;
#endif
            }
        }

        public void ApplySettings(ILionSettingsInfo newSettings)
        {
            _settings = (ApplovinMaxSettings)newSettings;
        }

        public ILionSettingsInfo GetSettings()
        {
            if (_settings == null)
            {
                _settings = new ApplovinMaxSettings();
            }

            return _settings;
        }

        public bool IsInitialized()
        {
            return MaxSdk.IsInitialized();
        }

        private void OnMaxInitialized(MaxSdkBase.SdkConfiguration sdkConfiguration)
        {
            if (MaxSdk.IsInitialized())
            {
                //AppLovinCrossPromo.Init();

#if DEVELOPMENT_BUILD
            MaxSdk.ShowMediationDebugger();
#endif

                bool gdprRequired = true;
#if UNITY_IOS
				if ((MaxSdkUtils.CompareVersions(UnityEngine.iOS.Device.systemVersion, "14.5") != MaxSdkUtils.VersionComparisonResult.Lesser)
					|| (MaxSdkUtils.CompareVersions(UnityEngine.iOS.Device.systemVersion, "14.5-beta") != MaxSdkUtils.VersionComparisonResult.Lesser))
				{
					gdprRequired = false;
				}
#endif

                if (gdprRequired)
                {
                    LionGDPR.OnAdConsentUpdated += () =>
                    {
                        MaxSdk.SetHasUserConsent(LionGDPR.AdConsentGranted);
                        LoadAds(LionAdTypeFlag.All);
                    };

                    switch (sdkConfiguration.ConsentDialogState)
                    {
                        case MaxSdkBase.ConsentDialogState.Applies:
                            // Show user consent dialog... 
                            // Note: If we have previously completed GDPR, the dialog will not appear.
                            LionGDPR.Initialize(UserStatus.Applies);
                            break;

                        case MaxSdkBase.ConsentDialogState.DoesNotApply:
                            // No need to show consent dialog, proceed with initialization
                            LionGDPR.Initialize(UserStatus.DoesNotApply);
                            break;

                        case MaxSdkBase.ConsentDialogState.Unknown:
                            // Consent dialog state is unknown. Proceed with initialization, but check if the consent
                            // dialog should be shown on the next application initialization
                            LionGDPR.Initialize(UserStatus.Unknown);
                            MaxSdk.SetHasUserConsent(LionGDPR.AdConsentGranted);
                            break;

                        default:
                            LionGDPR.Initialize(UserStatus.NotSet);
                            break;
                    }
                }

                Debug.Log("MaxSDK init Complete.  Consent Dialog State = " + sdkConfiguration.ConsentDialogState);
                LoadAds(LionAdTypeFlag.All);
                LionGDPR.OnOpen += () => HideAd(LionAdType.Banner);
                LionGDPR.OnClosed += () =>
                {
                    LoadAds(LionAdTypeFlag.Banner);
                    ShowAd(LionAdType.Banner);
                };
            }
            else
            {
                Debug.Log("Failed to init MaxSDK");
            }
        }

        public Task Initialize(LionCoreContext ctx)
        {
            ApplovinMaxSettings maxSettings = ctx.GetSettings<ApplovinMaxSettings>();
            LionDebug.LionDebugSettings debugSettings = ctx.GetSettings<LionDebug.LionDebugSettings>();

            string[] adUnitIds = new string[]
            {
            // rewarded
            maxSettings.rewardedAdUnitIdAndroid,
            maxSettings.rewardedAdUnitIdIos,

            // interstitial
            maxSettings.interstitialAdUnitIdAndroid,
            maxSettings.interstitialAdUnitIdIos,

            // banner
            maxSettings.bannerAdUnitIdAndroid,
            maxSettings.bannerAdUnitIdIos,
            };

            // init callback
            MaxSdkCallbacks.OnSdkInitializedEvent += OnMaxInitialized;

            // load callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnAdLoaded;
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnAdLoaded;
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnAdLoaded;

            //hidden callbacks
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnAdHidden;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnAdHidden;

            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnAdLoadFail;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnAdLoadFail;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnAdLoadFail;

            // show callbacks
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnAdDisplayed;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnAdDisplayed;
            MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnAdDisplayed;

            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnAdDisplayFailed;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnAdDisplayFailed;

            // init
            MaxSdk.SetSdkKey(maxSettings.sdkKey);
            MaxSdk.SetUserId(SystemInfo.deviceUniqueIdentifier);
            MaxSdk.SetVerboseLogging(debugSettings.debugLogLevel == LionDebug.DebugLogLevel.Verbose);
            MaxSdk.InitializeSdk(adUnitIds);

            // Re-init ads whenever the GDPR dialog is closed
            LionGDPR.OnClosed += delegate
            {
                LoadAds(LionAdTypeFlag.All);
                LionAds.ShowBanner<LionMaxSdk>("bottom");
            };

            LionGDPR.OnOpen += () => { LionAds.HideBanner<LionMaxSdk>("bottom"); };

            return Task.CompletedTask;
        }

        public void OnPostInitialize(LionCoreContext ctx)
        {
        }

        public void OnPreInitialize(LionCoreContext ctx)
        {
        }

#region Privacy Links

        private Dictionary<string, string> _privacyLinks = new Dictionary<string, string>
    {
        { "UNITY_NETWORK", "https://unity3d.com/fr/legal/privacy-policy" },
        { "APPLOVIN", "https://www.applovin.com/privacy/" },
        { "ADMOB_NETWORK", "https://policies.google.com/privacy/update" },
        { "ADCOLONY_NETWORK", "https://www.adcolony.com/privacy-policy/" },
        { "AMAZON_NETWORK", "https://advertising.amazon.com/resources/ad-policy/en/gdpr" },
        { "CHARTBOOST_NETWORK", "https://answers.chartboost.com/en-us/articles/200780269" },
        { "FYBER_NETWORK", "https://fyber.com/Privacy-policy/" },
        { "INMOBI_NETWORK", "https://www.inmobi.com/privacy-policy/" },
        { "IRONSOURCE_NETWORK", "https://ironsource.mobi/privacypolicy.html" },
        { "MINTEGRAL_NETWORK", "https://www.mintegral.com/en/privacy/" },
        { "SMAATO_NETWORK", "https://www.smaato.com/privacy/" },
        { "TIKTOK_NETWORK", "https://www.tiktok.com/legal/privacy-policy?lang=en#privacy-eea" },
        { "YANDEX_NETWORK", "https://yandex.com/legal/confidential/" },
        { "VUNGLE_NETWORK", "https://vungle.com/privacy/" },
        { "FACEBOOK_MEDIATE", "https://www.facebook.com/policy.php" },
        { "MYTARGET_NETWORK", "https://target.my.com/help/advertisers/legaldocuments/en" },
        { "OGURY_NETWORK", "https://ogury.com/privacy-policy/ " },
        { "ADJUST", "https://www.adjust.com/terms/privacy-policy/" },
        { "FIREBASE", "https://firebase.google.com/support/privacy" },
    };

        public string[] GetPrivacyLinks()
        {
            List<string> privacyLinks = new List<string>();
            foreach (var kvp in _privacyLinks)
            {
                privacyLinks.Add(kvp.Value);
            }

            return privacyLinks.ToArray();
        }

#endregion

#region Ads

        private void OnAdLoaded(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            if (string.IsNullOrEmpty(adUnit))
            {
                return;
            }

            if (adUnit == InterstitialAdUnit)
            {
                LionAnalytics.InterstitialLoad(adInfo.Placement, adInfo.NetworkName);
            }
            else if (adUnit == RewardedAdUnit)
            {
                LionAnalytics.RewardVideoLoad(adInfo.Placement, adInfo.NetworkName);
            }
            else if (adUnit == BannerAdUnit)
            {
                LionAnalytics.BannerLoad(adInfo.Placement, adInfo.NetworkName);
            }
        }

        private void OnAdLoadFail(string adUnit, MaxSdkBase.ErrorInfo errorInfo)
        {
            if (string.IsNullOrEmpty(adUnit))
            {
                return;
            }

            if (adUnit == InterstitialAdUnit)
            {
                LionAnalytics.InterstitialLoadFail("no_network", (AdErrorType)errorInfo.Code.GetHashCode());
            }
            else if (adUnit == RewardedAdUnit)
            {
                LionAnalytics.RewardVideoLoadFail("no_network", null, (AdErrorType)errorInfo.Code.GetHashCode());
            }
            else if (adUnit == BannerAdUnit)
            {
                LionAnalytics.BannerLoadFail("no_network", (AdErrorType)errorInfo.Code.GetHashCode());
            }
        }

        private void OnAdHidden(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            if (string.IsNullOrEmpty(adUnit))
            {
                return;
            }

            if (adUnit == InterstitialAdUnit)
            {
                LionAnalytics.InterstitialEnd(adInfo.Placement, adInfo.NetworkName);
                LoadAds(LionAdTypeFlag.Interstitial);
            }
            else if (adUnit == RewardedAdUnit)
            {
                LionAnalytics.RewardVideoEnd(adInfo.Placement, adInfo.NetworkName);
                LoadAds(LionAdTypeFlag.Rewarded);
            }
        }

        private void OnAdDisplayed(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            if (string.IsNullOrEmpty(adUnit))
            {
                return;
            }

            if (adUnit == InterstitialAdUnit)
            {
                LionAnalytics.InterstitialShow(adInfo.Placement, adInfo.NetworkName);
            }
            else if (adUnit == RewardedAdUnit)
            {
                LionAnalytics.RewardVideoShow(adInfo.Placement, adInfo.NetworkName);
            }
            else if (adUnit == BannerAdUnit)
            {
                LionAnalytics.BannerShow(adInfo.Placement, adInfo.NetworkName);
            }
        }

        private void OnAdDisplayFailed(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            if (string.IsNullOrEmpty(adUnit))
            {
                return;
            }

            if (adUnit == InterstitialAdUnit)
            {
                LionAnalytics.InterstitialShowFail(adInfo.Placement, adInfo.NetworkName, null,
                    (AdErrorType)errorInfo.Code.GetHashCode());
                LoadAds(LionAdTypeFlag.Interstitial);
            }
            else if (adUnit == RewardedAdUnit)
            {
                LionAnalytics.RewardVideoShowFail(adInfo.Placement, adInfo.NetworkName,
                    (AdErrorType)errorInfo.Code.GetHashCode());
                LoadAds(LionAdTypeFlag.Rewarded);
            }
            else if (adUnit == BannerAdUnit)
            {
                LionAnalytics.BannerShowFail(adInfo.Placement, adInfo.NetworkName,
                    (AdErrorType)errorInfo.Code.GetHashCode());
                LoadAds(LionAdTypeFlag.Banner);
            }
        }

        public void LoadAds(LionAdTypeFlag adType)
        {
            if (!_settings.interstitialsDisabled)
            {
                MaxSdk.LoadInterstitial(InterstitialAdUnit);
            }

            if (!_settings.rewardedAdsDisabled)
            {
                MaxSdk.LoadRewardedAd(RewardedAdUnit);
            }

            if (!_settings.bannersDisabled)
            {
                MaxSdk.CreateBanner(BannerAdUnit, MaxSdkBase.BannerPosition.BottomCenter);
                MaxSdk.SetBannerBackgroundColor(BannerAdUnit, Color.black);
            }
        }

        public void ShowAd(LionAdType adType, string placement = null,
            int? level = null,
            Dictionary<string, object> additionalData = null)
        {
            if (!IsInitialized()) return;
            switch (adType)
            {
                case LionAdType.Rewarded:
                    if (!_settings.rewardedAdsDisabled)
                    {
                        MaxSdk.ShowRewardedAd(RewardedAdUnit, placement);
                    }

                    break;
                case LionAdType.Interstitial:
                    if (!_settings.interstitialsDisabled)
                    {
                        MaxSdk.ShowInterstitial(InterstitialAdUnit, placement);
                        lastInterstitialShowTime = Time.time;
                    }

                    break;
                case LionAdType.Banner:
                    if (!_settings.bannersDisabled)
                    {
                        MaxSdk.ShowBanner(BannerAdUnit);
                    }

                    break;
            }
        }

        public void HideAd(LionAdType adType, string placement = null, int? level = null,
            Dictionary<string, object> additionalData = null)
        {
            if (adType == LionAdType.Banner)
            {
                MaxSdk.HideBanner(BannerAdUnit);
            }
        }

        public bool IsAdReady(LionAdType adType)
        {
            switch (adType)
            {
                case LionAdType.Banner:
                    return true;
                case LionAdType.Interstitial:
                    return MaxSdk.IsInterstitialReady(InterstitialAdUnit);
                case LionAdType.Rewarded:
                    return MaxSdk.IsRewardedAdReady(RewardedAdUnit);
                default:
                    return false;
            }
        }

#endregion

    }
}

#endif