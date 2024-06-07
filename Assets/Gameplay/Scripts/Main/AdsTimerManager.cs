using Greenknight;
using UnityEngine;

public class AdsTimerManager : Singleton<AdsTimerManager>
{
    private float _lastIntern = 0;
    private float _lastReward = 0;
    //private FirebaseConfigIntern ValueIntern =>
    //    JsonUtility.FromJson<FirebaseConfigIntern>(RFirebaseRemote.Instance.GetStringValue(GameConstant.Ads.INTER_ADS_CONFIG, GameConstant.Ads.INTER_ADS_DEFAULT));

    //private FirebaseConfigReward ValueReward =>
    //    JsonUtility.FromJson<FirebaseConfigReward>(RFirebaseRemote.Instance.GetStringValue(GameConstant.Ads.REWARD_ADS_CONFIG, GameConstant.Ads.REWARD_ADS_DEFAULT));


    //Time.realtimeSinceStartup
    public void ViewInternAds(string ev)
    {
        if (GameSave.NoAds)
        {
            return;
        }

        GameTracking.LogEvent("show_interstitial_ads_" + ev);
        bool view = AdsHelper.__ShowInterstitialAd(ev);
        if (view)
        {
            _lastIntern = Time.realtimeSinceStartup;
        }
    }
}
