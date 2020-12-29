using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsSystem : MonoBehaviour , IUnityAdsListener
{
    string gameId = "1234567";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }
    public void ShowRewardedVideo()
    {
        Advertisement.Show();
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {

        }
        else if (showResult == ShowResult.Skipped) 
        {
            Debug.Log("The ad was skipped.");
        } 
        else if (showResult == ShowResult.Failed) 
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

}
