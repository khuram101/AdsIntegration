using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class UnityAds : Integration, IUnityAdsInitializationListener
{
    [SerializeField] private string gameId = "";

    [SerializeField, Header("Unity interstitial")]
    private UnityAdsInterstitial unityAdsInterstitial;

    [SerializeField, Header("Unity rewarded video")]
    private UnityAdsRewarded unityAdsRewarded;


    #region Getter
    public UnityAdsInterstitial SimpleInterstitial
    {
        get { return unityAdsInterstitial; }
    }

    public UnityAdsRewarded RewardedVideo
    {
        get { return unityAdsRewarded; }
    }

    #endregion


    #region SDk initialize

    public delegate void SDK_Unity_Initialized(bool status);
    public static SDK_Unity_Initialized SDK_Unity_Status;

    #endregion


    public override void InitializeSdk()
    {
        Advertisement.Initialize(gameId, IsTestAds, this);
    }


    #region Initilization Callback

    public void OnInitializationComplete()
    {
        //remove if you are not want to display logs
        Logging.instance.Log("Unity Ads initialization complete.");
        SDK_Unity_Status.Invoke(true);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        //remove if you are not want to display logs
        Logging.instance.Log("Unity Ads Initialization Failed");
        SDK_Unity_Status.Invoke(false);
    }

    #endregion



    #region Unity 

    void LoadRewardedUnity()
    {
        //0 Advertisement.Load(id);
    }
    void LoadNormalRewardedUnity()
    {
        //if (!Advertisement.IsReady(rewardPlacementId))
        //   Advertisement.Load(rewardPlacementId);
    }
    #endregion


}
