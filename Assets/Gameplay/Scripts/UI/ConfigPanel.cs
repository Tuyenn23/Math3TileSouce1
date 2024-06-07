using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : MonoBehaviour
{
    [SerializeField] private GameObject SurrenderPanel;
    [SerializeField] private GameObject configPanel;

    [SerializeField] private Button homeBtn;

    [SerializeField] private Toggle tog_Music, tog_Sound;

    private void Awake()
    {
        if (homeBtn)
        {
            homeBtn.onClick.AddListener(MainUi.Instance.HomeUI);
        }

        tog_Music.onValueChanged.AddListener(OnMusicChangeEvent);
        tog_Sound.onValueChanged.AddListener(OnSoundChangeEvent);

    }

    private void OnEnable()
    {
        configPanel.SetActive(true);
        if (SurrenderPanel)
        {
            SurrenderPanel.SetActive(false);
        }
        Check();
    }

    public void BackToGame()
    {
        GridManager.Instance.Playable = false;
        configPanel.gameObject.SetActive(false);
    }

    public void Close()
    {
        if (SurrenderPanel)
        {
            GridManager.Instance.Playable = true;
        }
        gameObject.SetActive(false);
    }



    private void OnMusicChangeEvent(bool value)
    {
        GameSave.Music = value;
        MusicManager.Instance.MusicVolume = GameSave.Music ? 0.1f : 0;
        Check();
    }
    private void OnSoundChangeEvent(bool value)
    {
        GameSave.sound = value;
        MusicManager.Instance.SoundVolume = GameSave.sound ? 1 : 0;
        Check();
    }

    private void Check()
    {
        tog_Sound.isOn = GameSave.sound;
        tog_Sound.targetGraphic.color = GameSave.sound ? new Color(0, 0.5f, 0) : Color.white;
        tog_Music.isOn = GameSave.Music;
        tog_Music.targetGraphic.color = GameSave.Music ? new Color(0, 0.5f, 0) : Color.white;
    }

}
