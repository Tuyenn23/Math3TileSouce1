using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUi : Singleton<MainUi>
{
    [SerializeField] private Button StartBtn;
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button RetryBtn;
    [SerializeField] private Button RevivalBtn;
    [SerializeField] private Button CancelBtn;


    [SerializeField] private Button RerollBtn;
    [SerializeField] private Button RerollBtn2;
    [SerializeField] private Button UndoBtn;
    [SerializeField] private Button EmitBtn;
    [SerializeField] private Button EmitBtn2;
    [SerializeField] private Button Add1Btn;

    [SerializeField] private Button ConfigBtn;
    [SerializeField] private Button NextLevel;

    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject RevivalPanel;
    [SerializeField] private GameObject PlayPanel;
    [SerializeField] private GameObject AlertPanel;
    [SerializeField] private ConfigPanel configPanel;
    [SerializeField] private ConfigPanel configPanel2;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject WinPanelLevel1;
    [SerializeField] public MainPlay mainPlay;

    [SerializeField] private Text loseText;
    [SerializeField] private List<ParticleSystem> effect;

    public GameObject PlayPanel1 => PlayPanel;

    private int DropCount;
    private int RerollCount;

    private void Awake()
    {
        StartBtn.onClick.AddListener(GridManager.Instance.StartGame);
        HomeBtn.onClick.AddListener(HomeUI);
        RetryBtn.onClick.AddListener(Retry);

        RevivalBtn.onClick.AddListener(OnViewAdsRevival);
        EmitBtn.onClick.AddListener(OnViewAdsEmit);
        UndoBtn.onClick.AddListener(OnUndo);
        Add1Btn.onClick.AddListener(OnAdd1Box);

        NextLevel.onClick.AddListener(GridManager.Instance.NextGame);
        ConfigBtn.onClick.AddListener(ConfigPanel);
        Messenger.AddListener(GlobalStrings.EventCall.START_GAME, HideUI);
        Messenger.AddListener(GlobalStrings.EventCall.START_GAME_2, StartGame2);
        Messenger.AddListener(GlobalStrings.EventCall.END_GAME, EndGame);
        HomeUI();
        CancelBtn.onClick.AddListener(EndGame);
        RerollBtn.onClick.AddListener(OnViewAdsReroll);
    }



    private void HideUI()
    {
        GridManager.Instance.DeletedBoxGrid.gameObject.SetActive(true);
        MainPanel.SetActive(false);
        EndPanel.SetActive(false);
        configPanel2.gameObject.SetActive(false);
        configPanel.gameObject.SetActive(false);
        WinPanel.SetActive(false);
        PlayPanel.SetActive(true);
        DropCount = 0;
        RerollCount = 0;
        EmitBtn2.interactable = true;
        RerollBtn2.interactable = true;
    }


    private void StartGame2()
    {
        AlertPanel.SetActive(true);
        AlertPanel.transform.localPosition = new Vector3(-3000, 0, 0);
        AlertPanel.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() =>
        {
            AlertPanel.transform.DOLocalMove(new Vector3(3000, 0, 0), 0.5f).SetDelay(1.5f).OnComplete(() => AlertPanel.SetActive(false));
        });
    }


    private void EndGame()
    {
        GridManager.Instance.Playable = false;

        if (GridManager.Instance.Revival)
        {
            RevivalPanel.SetActive(true);
            WinPanel.SetActive(false);
            GridManager.Instance.Revival = false;
        }
        else
        {
            EndPanel.SetActive(true);
            PlayPanel.SetActive(false);
            MainPanel.SetActive(false);
            AlertPanel.SetActive(false);
            RevivalPanel.SetActive(false);
            WinPanel.SetActive(false);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                loseText.text = "Hôm nay bạn đã chơi \n<color=red>" + GameSave.PlayCount + "</color> lần.";
                loseText.font = GridManager.Instance.Data.Vi;
            }
            else
            {
                loseText.text = "Today you had play \n<color=red>" + GameSave.PlayCount + "</color> times.";
                loseText.font = GridManager.Instance.Data.En;
            }
        }
    }

    private void ConfigPanel()
    {
        if (GridManager.Instance.Playable)
        {
            GridManager.Instance.Playable = false;

            configPanel.gameObject.SetActive(true);
            configPanel2.gameObject.SetActive(false);
        }
        else
        {
            configPanel2.gameObject.SetActive(true);
            configPanel.gameObject.SetActive(false);
        }
    }

    public void WinGame()
    {
        GridManager.Instance.DeletedBoxGrid.gameObject.SetActive(false);
        WinPanel.SetActive(true);
        GridManager.Instance.Playable = false;
        for (int i = 0; i < effect.Count; i++)
        {
            effect[i].Play();
        }
        Messenger.Broadcast(GlobalStrings.EventCall.SURE_END_GAME);
    }

    public void WinGame1()
    {
        WinPanelLevel1.SetActive(true);
        for (int i = 0; i < effect.Count; i++)
        {
            effect[i].Play();
        }
    }

    public void HomeUI()
    {
        GridManager.Instance.DeletedBoxGrid.gameObject.SetActive(false);
        MainPanel.SetActive(true);
        EndPanel.SetActive(false);
        PlayPanel.SetActive(false);
        AlertPanel.SetActive(false);
        RevivalPanel.SetActive(false);
        configPanel2.gameObject.SetActive(false);
        configPanel.gameObject.SetActive(false);
        WinPanel.SetActive(false);
        GridManager.Instance.Playable = false;
        Messenger.Broadcast(GlobalStrings.EventCall.SURE_END_GAME);
    }

    public void Retry()
    {
        EndPanel.SetActive(false);
        StartCoroutine(GridManager.Instance.RetryGame());
    }

    private void OnViewAdsRevival()
    {
#if DEV
        Revival();
        return;
#endif
        GoogleAdMobController.Instance.ShowRewardedAd(Revival);
    }


    public void Revival()
    {
        GameSave.SessionRewardCount++;
        GridManager.Instance.Playable = true;
        PlayPanel.SetActive(true);
        RevivalPanel.SetActive(false);
        WinPanel.SetActive(false);
        GridManager.Instance.DeletedBoxGrid.Emit();
        GameTracking.LogEvent("Ads_Revive");
    }



    private void OnViewAdsEmit()
    {
#if DEV
        EmitTile();
        return;
#endif
        GoogleAdMobController.Instance.ShowRewardedAd(EmitTile);
    }

    public void EmitTile()
    {
        GameSave.SessionRewardCount++;
        GridManager.Instance.emited = true;
        GridManager.Instance.DeletedBoxGrid.Emit();
        GameTracking.LogEvent("Ads_Drop3");
        mainPlay.HideAll();

        DropCount++;
#if DEV
        return;
#endif
        if (DropCount >= 2)
        {
            EmitBtn2.interactable = false;
        }
    }

    private void OnViewAdsReroll()
    {
#if DEV
        Reroll();
        return;
#endif
        GoogleAdMobController.Instance.ShowRewardedAd(Reroll);
    }


    public void Reroll()
    {
        GameSave.SessionRewardCount++;
        GridManager.Instance.RerollAllBox();
        GameTracking.LogEvent("Ads_Reroll");
        mainPlay.HideAll();
        RerollCount++;
#if DEV
        return;
#endif
        if (RerollCount >= 2)
        {
            RerollBtn2.interactable = false;
        }
    }

    private void OnUndo()
    {
#if DEV
        Undo();
        return;
#endif
        GoogleAdMobController.Instance.ShowRewardedAd(Undo);
    }


    public void Undo()
    {
        GameSave.SessionRewardCount++;
        GridManager.Instance.DeletedBoxGrid.Undo();
        mainPlay.HideAll();
        GameTracking.LogEvent("Ads_Undo");
    }


    public void OnAdd1Box()
    {
#if DEV
        AddGridToBox();
        return;
#endif
        GoogleAdMobController.Instance.ShowRewardedAd(AddGridToBox);
    }
    public void AddGridToBox()
    {
        GameSave.SessionRewardCount++;
        GridManager.Instance.DeletedBoxGrid.add = 1;
        GridManager.Instance.DeletedBoxGrid.RemoveAddBtn();
        mainPlay.HideAll();
        GameTracking.LogEvent("Ads_Add_1");
    }
}
