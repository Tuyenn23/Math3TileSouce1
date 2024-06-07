using Utilities.Service;

public class AppManager : Singleton<AppManager>
{
    public static bool IsIAPInited = false;
    private void Awake()
    {
    }

    private void OnFirebaseInitDone(bool obj)
    {
        //throw new NotImplementedException();
    }

    private void Start()
    {
        //List<string> _iap_ids = GameUnityData.instance.mShopData.IAP_ids;
        //_iap_ids.Add(GameConstant.GameIAPID.ID_NO_ADS);
        //PaymentHelper.Instance.InitProducts(_iap_ids, new List<string>(), OnInitIAPDone);
        //AdsManager.Instance.ShowBannerAd(BannerAdPosition.Bottom);
    }

    private void OnInitIAPDone(bool obj)
    {
        if (obj)
        {
            IsIAPInited = true;
            GameUtils.RaiseMessage(GameEvent.OnIAPInitDoneEvent.Instance);
        }
    }

    public void OnNoAdsBuy()
    {
        PaymentHelper.Purchase(GameConstant.GameIAPID.ID_NO_ADS, OnNoAdsDone);
    }

    private void OnNoAdsDone(bool obj)
    {
        if (obj)
        {
            GameSave.NoAds = true;
        }
    }
}
