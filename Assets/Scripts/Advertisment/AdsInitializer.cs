
using System;
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// The process to display adds starts by initializing the ads service. This is done by calling the Advertisement.Initialize() method.
///
/// After the service is initialized, you can load ads by calling the Advertisement.Load() method. This method takes a string parameter that is the placement ID of the ad you want to load.
/// </summary>
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool testMode;
    
    private string _gameId;
    private bool _isInitialized;

    /// <summary>
    /// Initialize the advertisement service
    /// </summary>
    private void Awake()
    {
        InitializeAds();
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Initializes the advertisement service with the provided id for the android or ios platforms.
    /// </summary>
    void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iosGameId : androidGameId;
        Advertisement.Initialize(_gameId, testMode, this);
    }

    /// <summary>
    /// Called when initialization of the advertisement service is successful.
    /// </summary>
    public void OnInitializationComplete()
    {
        print("Ads initialized");
        // LoadInterstitial();
        _isInitialized = true;
    }

    /// <summary>
    /// Called when initialization of the advertisement service fails.
    /// </summary>
    /// <param name="error">The error of the initialization.</param>
    /// <param name="message"></param>
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print($"Ads failed to initialize: {error.ToString()} - {message}");
    }

    /// <summary>
    /// Loads a new interstitial ad. This does not show the ad.
    /// TODO: for now, the interstitial ad is loaded when the game starts. This should be changed to load the ad when the player dies.
    /// TODO: for now, only the android version of the ad is loaded. This should be changed to load the ad for the correct platform.
    /// </summary>
    public void LoadInterstitial()
    {
        if (!_isInitialized) return;

        Advertisement.Load("Interstitial_Android", this);
    }
    
    /// <summary>
    /// Loads a new Rewarded ad. This does not show the ad.
    /// TODO: for now, the add never loads. This should be changed to load when the player needs more lives.
    /// TODO: for now, only the android version of the ad is loaded. This should be changed to load the ad for the correct platform.
    /// </summary>
    public void LoadRewarded()
    {
        if (!_isInitialized) return;
        Advertisement.Load("Rewarded_Android", this);
    }

    /// <summary>
    /// Called when the ad finishes loading.
    /// </summary>
    /// <param name="placementId"></param>
    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Ad loaded");
        Advertisement.Show(placementId, this);
    }

    /// <summary>
    /// Called when the ad fails to load.
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    /// <summary>
    /// Called when the ad fails to show.
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Ad failed to show");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Ad started");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placementId"></param>
    public void OnUnityAdsShowClick(string placementId)
    {
        print("Ad clicked");
    }

    /// <summary>
    /// Called when the ad is closed.
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="showCompletionState"></param>
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("Ad completed");
        
        if (!_gameId.Equals(placementId) || !showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) return;
        print("Give reward"); // TODO: separate interstitial and rewarded ads in different scripts. Para futuro desarrollo.
        LoadRewarded();

    }
}
