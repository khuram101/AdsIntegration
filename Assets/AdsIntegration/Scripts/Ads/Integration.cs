using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integration : MonoBehaviour, ISdkInitialize
{
    
    protected bool IsTestAds = false;




    public void Hide()
    {
        throw new System.NotImplementedException();
    }

    public virtual void InitializeSdk()
    {
        throw new System.NotImplementedException();
    }
    public virtual bool IsRewardedLoaded()
    {
        return false;
    }
}
public interface ISdkInitialize
{
    void InitializeSdk();
}



public interface IRewardedIntegration
{
    void Show();//for callback
}

