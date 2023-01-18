using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class UnityAdsInterstitial : AdsFunction, IUnityAdsLoadListener, IUnityAdsShowListener
{

    #region IStart

    public override void Start()
    {
        UnityAds.SDK_Unity_Status += InitializedSucess;
    }

    public override void InitializedSucess(bool status)
    {
        isSDKInitialized = status;
        //remove if you are not want to display logs
        Logging.instance.Log("Unity Sdk Initialize " + isSDKInitialized);
    }

    public override void OnDisable()
    {
        UnityAds.SDK_Unity_Status -= InitializedSucess;
        if (!isAdRequested) return;

    }

    #endregion

    public override void RequestAd(bool isTestAds)
    {
        //string id = isTestAds == true ? adID : "ca-app-pub-3940256099942544/1033173712";

        if (!isSDKInitialized) return;

        isAdRequested = true;


        LoadAd();

    }

    public override void LoadAd()
    {
        if (!isSDKInitialized || !isAdRequested) return;

        Advertisement.Load(adID, this);
        //remove if you are not want to display logs
        Logging.instance.Log("Load Admob interstitial");
    }


    public override bool IsAdLoaded()
    {
        return isAdLoaded;
    }

    public override void ShowAd()
    {
        if (!isAdRequested) return;

        if (isAdLoaded)
        {
            //remove if you are not want to display logs
            Logging.instance.Log("Show Admob interstitial");

        }
    }



    #region Events Handler

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        isAdLoaded = true;
        // Optionally execute code if the Ad Unit successfully loads content.
        Logging.instance.Log("On UnityAds Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        isAdLoaded = false;
        //Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        //remove if you are not want to display logs
        Logging.instance.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        isAdLoaded = false;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }


    public void OnUnityAdsShowStart(string adUnitId)
    {

    }
    public void OnUnityAdsShowClick(string adUnitId)
    {

    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {

    }


    #endregion


}
