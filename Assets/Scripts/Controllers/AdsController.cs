using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : IStarter, IUnityAdsListener
{
    private const string gameId = "4450757";
    private const string interstitialId = "Interstitial_Android";
    public void Starter()
    {
        Advertisement.Initialize(gameId, true);
    }

    public void ShowInterstitial()
    {
        Advertisement.Show(interstitialId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Skipped)
            Debug.Log("Skipped");
    }
}
