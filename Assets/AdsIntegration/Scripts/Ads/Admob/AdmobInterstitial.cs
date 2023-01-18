using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class AdmobInterstitial : AdsFunction
{
    private InterstitialAd interstitial = null;



    #region IStart

    public override void Start()
    {
        AdmobAds.SDK_Admob_Status += InitializedSucess;
    }

    public override void InitializedSucess(bool status)
    {
        isSDKInitialized = status;
    }

    public override void OnDisable()
    {
        AdmobAds.SDK_Admob_Status -= InitializedSucess;
        if (!isAdRequested) return;

        this.interstitial.OnAdLoaded -= HandleOnAdLoaded;

        this.interstitial.OnAdFailedToLoad -= HandleOnAdFailedToLoad;

        this.interstitial.OnAdOpening -= HandleOnAdOpening;

        this.interstitial.OnAdClosed -= HandleOnAdClosed;
    }

    #endregion

    public override void RequestAd(bool isTestAds)
    {
        string id = isTestAds == true ? adID : "ca-app-pub-3940256099942544/1033173712";

        if (!isSDKInitialized) return;

        isAdRequested = true;
        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
        // Debug.Log("Requesting Ression Interstitial Ad");
        // Create an interstitial.
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(id);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        LoadAd();

    }

    public override void LoadAd()
    {
        if (!isSDKInitialized || !isAdRequested) return;
        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());

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
            this.interstitial.Show();
        }
    }



    #region Events Handler
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        isAdLoaded = true;
        //remove if you are not want to display logs
        Logging.instance.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isAdLoaded = false;
        Logging.instance.Log("HandleFailedToReceiveAd event received");
        //  MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        isAdLoaded = false;
        //MonoBehaviour.print("HandleAdClosed event received");
        Invoke(nameof(LoadAd), 1);
    }




    #endregion

}

