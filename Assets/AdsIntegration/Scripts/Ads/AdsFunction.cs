using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdsFunction : MonoBehaviour, IStart, IAdsIntegration
{
    [SerializeField]
    protected string adID = "12354555555555";
    protected bool isAdLoaded = false;
    protected bool isSDKInitialized = false;
    protected bool isAdRequested = false;


    public AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    public virtual void InitializedSucess(bool status)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnDisable()
    {
        throw new System.NotImplementedException();
    }

    #endregion
    public virtual void RequestAd(bool isTestAds)
    {
        throw new System.NotImplementedException();
    }

    public virtual void LoadAd()
    {
        throw new System.NotImplementedException();
    }
    public virtual void ShowAd()
    {
        throw new System.NotImplementedException();
    }

    public virtual void ShowAd(float time)
    {
        throw new System.NotImplementedException();
    }

    public virtual void HideAd()
    {
        throw new System.NotImplementedException();
    }
    public virtual void HideAd(float time)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsAdLoaded()
    {
        throw new System.NotImplementedException();
    }

}

public interface IStart
{
    void Start();
    void InitializedSucess(bool status);
    void OnDisable();
}

public interface IAdsIntegration
{
    void RequestAd(bool isTestAds);
    void LoadAd();
    void ShowAd();
    void ShowAd(float time);
    void HideAd();
    void HideAd(float time);
}
public enum AdNetwork
{
    //auto check for available ad
    NEUTRAL = 0,
    ADMOB = 1,
    UNITY = 2

};