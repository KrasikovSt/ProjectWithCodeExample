﻿
using BouncingBall.Game.Data;
using BouncingBall.Game.Data.ObjectData;
using NUnit.Framework;
using System;
using System.Security.Policy;
using UniRx;
using Zenject;

namespace BouncingBall.Ads
{
    public class AdsMediator
    {
        [Inject] private readonly GameDataProvider _gameDataManager;

        private bool _isTestMode = true;
        private AdsInitializer _adsInitializer;
        private InterstitialAdsBanner _interstitialAdsBanner;
        private AdsData _adsData;

        public IObservable<Unit> AdvertisingHasStarted => _interstitialAdsBanner.AdvertisingHasStarted;
        public IObservable<Unit> AdvertisingHasEnded => _interstitialAdsBanner.AdvertisingHasEnded;

        public void Init()
        {
            _adsData = _gameDataManager.AdsData;
            var gameId = string.Empty;
            var interstitalId = string.Empty;

#if UNITY_IOS
               gameId = _adsData.GameIOSId;
               interstitalId = _adsData.InterstitialBannerIOSId;
#elif UNITY_ANDROID ||UNITY_EDITOR
            gameId = _adsData.GameAndroidId;
            interstitalId = _adsData.InterstitialBannerAndroidId;
#endif

            _adsInitializer = new AdsInitializer(_isTestMode, gameId);
            _interstitialAdsBanner = new InterstitialAdsBanner(interstitalId);

            _adsInitializer.Init();
        }

        public void ShowInterstitialBanner()
        {
            _interstitialAdsBanner.LoadAd();
            _interstitialAdsBanner.ShowAd();
        }

    }
}
