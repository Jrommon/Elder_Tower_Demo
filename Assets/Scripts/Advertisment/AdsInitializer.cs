
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool testMode;
    
    private string _gameId;

    private void Awake()
    {
        InitializeAds();
        DontDestroyOnLoad(this);
    }

    void InitializeAds()
    {
        _gameId = Application.platform == RuntimePlatform.Android ? androidGameId : iosGameId;
        Advertisement.Initialize(_gameId, testMode, this);
    }
    
    public void LoadInterstitial()
    {
        Advertisement.Load("Interstitial_Android", this);
    }
    
    public void LoadRewarded()
    {
        Advertisement.Load("Rewarded_Android", this);
    }

    public void OnInitializationComplete()
    {
        print("Ads initialized");
        LoadInterstitial();
        
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("Ads failed to initialize");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Ad loaded");
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Ad failed to show");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Ad started");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Ad clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("Ad completed"); 
    }
}
