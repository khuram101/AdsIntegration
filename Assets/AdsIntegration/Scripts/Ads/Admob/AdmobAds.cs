
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdmobAds : Integration
{
    [SerializeField, Header("admob interstitial")]
    private AdmobInterstitial admobInterstitial;

    [SerializeField, Header("admob small banner")]
    private AdmobBanner smallBanner;

    [SerializeField, Header("admob large banner")]
    private AdmobBanner largeBanner;
    [SerializeField, Header("admob rewarded video")]
    private AdmobRewardedVideo admobRewardedVideo;


    #region Getter
    public AdmobInterstitial SimpleInterstitial
    {
        get { return admobInterstitial; }
    }
    public AdmobBanner SmallBanner
    {
        get { return smallBanner; }
    }

    public AdmobBanner LargeBanner
    {
        get { return largeBanner; }
    }
    public AdmobRewardedVideo RewardedVideo
    {
        get { return admobRewardedVideo; }
    }

    #endregion



    #region SDk initialize

    public delegate void SDK_Admob_Initialized(bool status);
    public static SDK_Admob_Initialized SDK_Admob_Status;

    #endregion



    public override void InitializeSdk()
    {
        // Initialize the Google Mobile Ads SDK.
        GoogleMobileAds.Api.MobileAds.Initialize
        (initStatus =>
        {
            //remove if you are not want to display logs
            Logging.instance.Log("Admob Initialized");

            SDK_Admob_Status.Invoke(true);

            /*            Invoke(nameof(RequestSmallBanner), 0.2f);
                        Invoke(nameof(RequestInterstitial), 0.5f);
                        Invoke(nameof(RequestRewardedVideo), 1f);*/
        }
        );
    }

    public override bool IsRewardedLoaded()
    {
        return admobRewardedVideo.IsAdLoaded();
    }


    void RequestSmallBanner()
    {
        smallBanner.RequestAd(IsTestAds);
    }
    void RequestInterstitial()
    {
        admobInterstitial.RequestAd(IsTestAds);
    }

    void RequestRewardedVideo()
    {
        admobRewardedVideo.RequestAd(IsTestAds);
    }

    public void ShowRewardedVideo()
    {
        admobRewardedVideo.ShowAd();
    }





}






