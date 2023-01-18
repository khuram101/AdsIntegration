using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedVideoButton : MonoBehaviour
{
    [SerializeField] AdNetwork adNetwork;
    [SerializeField] Button thisButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        AdsController.RewardedAdLoaded += RewardedAdLoaded;

        if (adNetwork == AdNetwork.ADMOB)
        {
            AdmobRewardedVideo.RewardedVideoAction += GrantReward;
            thisButton.interactable = false;
        }
        else
            if (adNetwork == AdNetwork.UNITY)
        {
            UnityAdsRewarded.RewardedVideoAction += GrantReward;
            thisButton.interactable = false;
        }
    }

    public void WatchRewardedVideo()
    {
        switch (adNetwork)
        {
            case AdNetwork.ADMOB:
                AdsController.instance.ShowRewardedAd();
                break;
            case AdNetwork.UNITY:
                AdsController.instance.ShowUnityRewardedAd();
                break;
        }
    }

    void GrantReward()
    {
        Logging.instance.Log("Grant Reward Succeed");
    }

    void RewardedAdLoaded(AdNetwork _adNetwork)
    {
        if (_adNetwork == adNetwork)
        {
            thisButton.interactable = true;
            Logging.instance.Log("Rewarded Ad Loaded: " + _adNetwork.ToString());
        }
    }


    private void OnDisable()
    {
        AdmobRewardedVideo.RewardedVideoAction -= GrantReward;
        UnityAdsRewarded.RewardedVideoAction -= GrantReward;
        AdsController.RewardedAdLoaded -= RewardedAdLoaded;
    }
}
