using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class AdmobRewardedVideo : AdsFunction
{
    private RewardedAd rewardedAd;
    private bool isRewardedFinished = false;

    public delegate void RewardedAction();
    public static event RewardedAction RewardedVideoAction;


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
        if (!isAdRequested || !isSDKInitialized) return;
        Debug.Log("isAdRequested: "+ isAdRequested);
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded -= HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }

    #endregion



    public override void RequestAd(bool isTestAds)
    {
        isRewardedFinished = false;
        isAdLoaded = false;

        string id = isTestAds == true ? adID : "ca-app-pub-3940256099942544/5224354917";

        if (!isSDKInitialized) return;

        isAdRequested = true;

        if (this.rewardedAd != null)
            this.rewardedAd.Destroy();

        this.rewardedAd = new RewardedAd(id);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        LoadAd();
    }

    public override void LoadAd()
    {
        if (!isSDKInitialized || !isAdRequested) return;
        //remove if you are not want to display logs
        Logging.instance.Log("Load Admob rewarded");
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(CreateAdRequest());
    }

    public override bool IsAdLoaded()
    {
        return isAdLoaded;
    }

    public override void ShowAd()
    {
        isRewardedFinished = false;
        if (!isAdRequested) return;

        if (this.rewardedAd.IsLoaded())
        {
            //remove if you are not want to display logs
            Logging.instance.Log("Show Admob Rewarded");
            this.rewardedAd.Show();
        }
    }


    #region Event Handler
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //remove if you are not want to display logs
        Logging.instance.Log("HandleRewardedAdLoaded event received");
        isAdLoaded = true;
        AdsController.instance.RewardedLoaded(AdNetwork.ADMOB);
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //remove if you are not want to display logs
        Logging.instance.Log("HandleRewardedAdFailedToLoad event received");
        isAdLoaded = false;
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        //MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: ");
        isAdLoaded = false;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleRewardedAdClosed event received");
   /*     if (isRewardedFinished)
        {
            isRewardedFinished = false;
            isAdLoaded = false;
            //reward user
            Invoke(nameof(Reward), 0.15f);
        }*/
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        isRewardedFinished = true;
        //reward user
        //remove if you are not want to display logs
        Logging.instance.Log("Rewarded video complete");
        isAdLoaded = false;
        //reward user
        Invoke(nameof(Reward), 0.15f);

    }

    #endregion

    void Reward()
    {
        //remove if you are not want to display logs
        Logging.instance.Log("Reward user");

        RewardedVideoAction?.Invoke();
        Invoke(nameof(LoadAd), 1);
    }

   
}
