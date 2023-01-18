using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public static AdsController instance;
    [SerializeField]
    private bool IsTestAds = false;

    public AdmobAds AdmobNetwork;
    public UnityAds UnityAdNetwork;

    public delegate void OnRewardedAdLoaded(AdNetwork adNetwork);
    public static event OnRewardedAdLoaded RewardedAdLoaded;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            DestroyImmediate(this.gameObject);
    }
    void Start()
    {


    }
    #region Initialize Sdks
    public void InitializeAdmob()
    {
        AdmobNetwork.InitializeSdk();
    }

    public void InitializeUnity()
    {
        UnityAdNetwork.InitializeSdk();
    }

    #endregion


    #region Admob Callings


    #region Small Banner
    public void RequestSmallBanner()
    {
        AdmobNetwork.SmallBanner.RequestAd(IsTestAds);
    }
    public void ShowSmallBanner()
    {
        AdmobNetwork.SmallBanner.ShowAd();
    }
    public void HideSmallBanner()
    {
        AdmobNetwork.SmallBanner.HideAd();
    }

    #endregion


    #region Large Banner
    public void RequestLargeBanner()
    {
        AdmobNetwork.LargeBanner.RequestAd(IsTestAds);
    }
    public void ShowLargeBanner()
    {
        AdmobNetwork.LargeBanner.ShowAd();
    }
    public void HideLargeBanner()
    {
        AdmobNetwork.LargeBanner.HideAd();
    }

    #endregion


    #region Interstitial
    public void RequestInterstitial()
    {
        AdmobNetwork.SimpleInterstitial.RequestAd(IsTestAds);
    }
    public void LoadInterstitial()
    {
        AdmobNetwork.SimpleInterstitial.LoadAd();
    }

    public void ShowInterstitial()
    {
        AdmobNetwork.SimpleInterstitial.ShowAd();
    }

    #endregion


    #region Rewarded Admob
    public void RequestRewardedAd()
    {
        AdmobNetwork.RewardedVideo.RequestAd(IsTestAds);
    }
    public void ShowRewardedAd()
    {
        AdmobNetwork.RewardedVideo.ShowAd();
    }

    #endregion



    #endregion



    #region Unity Ads

    public void RequestUnityInterstitial()
    {
        UnityAdNetwork.SimpleInterstitial.RequestAd(IsTestAds);

    }
    public void ShowUnityInterstitial()
    {
        UnityAdNetwork.SimpleInterstitial.ShowAd();
    }




    public void RequestUnityRewardedAd()
    {
        UnityAdNetwork.RewardedVideo.RequestAd(IsTestAds);

    }
    public void ShowUnityRewardedAd()
    {
        UnityAdNetwork.RewardedVideo.ShowAd();
    }


    #endregion




    public void WatchRewardedVideo()
    {
        if (AdmobNetwork.IsRewardedLoaded())
        {
            AdmobNetwork.RewardedVideo.ShowAd();
        }
        else
            if (UnityAdNetwork.IsRewardedLoaded())
        {
            UnityAdNetwork.RewardedVideo.ShowAd();
        }
    }

    public void RewardedLoaded(AdNetwork _adNetwork)
    {
        RewardedAdLoaded?.Invoke(_adNetwork);
    }

}

