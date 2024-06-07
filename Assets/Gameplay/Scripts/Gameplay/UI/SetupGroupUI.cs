using UnityEngine;
using UnityEngine.UI;

public class SetupGroupUI : MonoBehaviour
{
    [SerializeField] private GameObject SettingBox;

    [SerializeField] private Button tog_Music, tog_Sound, tog_Quality, ResetBtn, HomeBtn;
    [SerializeField] private GameObject MusicOn, MusicOff, SoundOn, SoundOff, QualityOn, QualityOff;
    private bool OpenSetting;
    private float time = 0;
    private void Awake()
    {
        tog_Music.onClick.AddListener(OnMusicChangeEvent);
        tog_Sound.onClick.AddListener(OnSoundChangeEvent);
        Check();
    }

    private void OnMusicChangeEvent()
    {
        GameSave.Music = !GameSave.Music;
        MusicManager.Instance.MusicVolume = GameSave.Music ? 0.1f : 0;
        time = 10f;
        Check();
    }
    private void OnSoundChangeEvent()
    {
        GameSave.sound = !GameSave.sound;
        MusicManager.Instance.SoundVolume = GameSave.sound ? 1 : 0;
        time = 10f;
        Check();
    }

    private void OnQualityChangeEvent()
    {
        Debug.Log("Change Quality");

        time = 10f;
        Check();
    }

    private void Check()
    {
        if (GameSave.sound)
        {
            SoundOn.gameObject.SetActive(true);
            SoundOff.gameObject.SetActive(false);
        }
        else
        {
            SoundOn.gameObject.SetActive(false);
            SoundOff.gameObject.SetActive(true);
        }

        if (GameSave.Music)
        {
            MusicOn.gameObject.SetActive(true);
            MusicOff.gameObject.SetActive(false);
        }
        else
        {
            MusicOn.gameObject.SetActive(false);
            MusicOff.gameObject.SetActive(true);
        }



        //GameController.Instance.OnChangeQuality();
    }


}
