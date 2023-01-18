using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class UnityAdsRewarded : AdsFunction, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public delegate void RewardedAction();
    public static event RewardedAction RewardedVideoAction;
    private bool isRewardedFinished = false;

    #region IStart

    public override void Start()
    {
        UnityAds.SDK_Unity_Status += InitializedSucess;
    }

    public override void InitializedSucess(bool status)
    {
        isSDKInitialized = status;
    }

    public override void OnDisable()
    {
        UnityAds.SDK_Unity_Status -= InitializedSucess;
        if (!isAdRequested) return;
    }

    #endregion



    public override void RequestAd(bool isTestAds)
    {
        isRewardedFinished = false;
        isAdLoaded = false;

        //string id = isTestAds == true ? adID : "ca-app-pub-3940256099942544/5224354917";

        if (!isSDKInitialized) return;

        isAdRequested = true;

        LoadAd();
    }

    public override void LoadAd()
    {
        if (!isSDKInitialized || !isAdRequested) return;
        //remove if you are not want to display logs
        Logging.instance.Log("Load Admob rewarded");
        // Load the rewarded ad with the request.
        Advertisement.Load(adID, this);
    }

    public override bool IsAdLoaded()
    {
        return isAdLoaded;
    }

    public override void ShowAd()
    {
        isRewardedFinished = false;
        if (!isAdRequested) return;
        

        if (isAdLoaded)
        {
            //remove if you are not want to display logs
            Logging.instance.Log("Show Admob Rewarded");
            Advertisement.Show(adID, this);
       
        }
    }


    #region Event Handler

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(adID))
        {
            //remove if you are not want to display logs
            Logging.instance.Log("Unity Ads Rewarded Loaded");
            isAdLoaded = true;
            AdsController.instance.RewardedLoaded(AdNetwork.UNITY);
            // Configure the button to call the ShowAd() method when clicked:
            // _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            // _showAdButton.interactable = true;
        }
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            //remove if you are not want to display logs
            Logging.instance.Log("Unity Ads rewarded Complete");

            // Grant a reward.
            Invoke(nameof(Reward), 0.15f);
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        isAdLoaded = false;
        //Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        //remove if you are not want to display logs
        Logging.instance.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        isAdLoaded = false;
        // Use the error details to determine whether to try to load another ad.
        //remove if you are not want to display logs
        Logging.instance.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }





    #endregion

    void Reward()
    {
        RewardedVideoAction?.Invoke();

        Invoke(nameof(RequestAd), 1);
    }


}
