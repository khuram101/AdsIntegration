using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class AdmobBanner : AdsFunction
{

    [SerializeField] private AdSize bannerSize = AdSize.Banner;
    [SerializeField] private bool isLargeBanner = false;
    [SerializeField] private AdPosition bannerPosition = AdPosition.Top;

    private BannerView bannerView = null;

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

        this.bannerView.OnAdLoaded -= this.HandleOnAdLoaded;

        this.bannerView.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;

        this.bannerView.OnAdOpening -= this.HandleOnAdOpened;

        this.bannerView.OnAdClosed -= this.HandleOnAdClosed;
    }

    #endregion


    public override void RequestAd(bool isTestAds)
    {
        if (isLargeBanner)
            bannerSize = AdSize.MediumRectangle;

        string id = isTestAds == true ? adID : "ca-app-pub-3940256099942544/6300978111";

        if (!isSDKInitialized) return;
        isAdRequested = true;

        if (this.bannerView != null)
            this.bannerView.Destroy();


        this.bannerView = new BannerView(id, bannerSize, bannerPosition);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;

        LoadAd();
    }

    public override void LoadAd()
    {
        if (!isSDKInitialized || !isAdRequested) return;

        //remove if you are not want to display logs
        Logging.instance.Log("Load Admob Banner");

        // Load the banner with the request.
        this.bannerView.LoadAd(CreateAdRequest());
    }
    public override bool IsAdLoaded()
    {
        return isAdLoaded;
    }

    public override void ShowAd()
    {
        if (this.bannerView != null && isAdRequested)
            this.bannerView.Show();
        //remove if you are not want to display logs
        Logging.instance.Log("Show Admob Banner");
    }

    public override void ShowAd(float time)
    {
        Invoke(nameof(ShowAd), time);
    }

    /// <summary>
    /// ////////////////////
    /// </summary>

    public override void HideAd()
    {
        if (this.bannerView != null && isAdRequested)
            this.bannerView.Hide();
    }

    public override void HideAd(float time)
    {
        Invoke(nameof(HideAd), time);
    }



    public void DestroyAd()
    {
        if (this.bannerView != null && isAdRequested)
            this.bannerView.Destroy();
    }


    /// <summary>
    /// ///////
    /// </summary>


    #region Event Handler

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        isAdLoaded = true;

        //remove if you are not want to display logs
        Logging.instance.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isAdLoaded = false;
        //remove if you are not want to display logs
        Logging.instance.Log("HandleFailedToReceiveAd event received");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
       // MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdClosed event received");
    }



    #endregion

}
