using UnityEngine;

public class GameSave
{
    public static string Cache_UserName
    {
        get => PlayerPrefs.GetString(GameSaveKey.KEY_USERNAME, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_USERNAME, value);
            PlayerPrefs.Save();
        }
    }

    public static string Cache_Password
    {
        get => PlayerPrefs.GetString(GameSaveKey.KEY_PASSWORD, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.KEY_PASSWORD, value);
            PlayerPrefs.Save();
        }
    }

    public static int Date
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_DATE, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_DATE, value);
            PlayerPrefs.Save();
        }
    }

    public static int WinCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_WIN, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_WIN, value);
            PlayerPrefs.Save();
        }
    }


    public static int PlayCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_LEVEL, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_LEVEL, value);
            PlayerPrefs.Save();
        }
    }

    public static int TotalPlayCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_TOTALLEVEL, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_TOTALLEVEL, value);
            PlayerPrefs.Save();
        }
    }

    public static bool Music
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_MUSIC, true);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_MUSIC, value);
            PlayerPrefs.Save();
        }
    }

    public static bool sound
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_SOUND, true);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_SOUND, value);
            PlayerPrefs.Save();
        }
    }


    public static int SessionInternCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_INTERN_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_INTERN_COUNT, value);
            PlayerPrefs.Save();
        }
    }
    public static int SessionRewardCount
    {
        get => PlayerPrefs.GetInt(GameSaveKey.KEY_REWARD_COUNT, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.KEY_REWARD_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static string OfflineTime
    {
        get => PlayerPrefs.GetString(GameSaveKey.OFFTIME, "");
        set
        {
            PlayerPrefs.SetString(GameSaveKey.OFFTIME, value);
            PlayerPrefs.Save();

        }
    }

    public static int DailyGift
    {
        get => PlayerPrefs.GetInt(GameSaveKey.Daily, 0);
        set
        {
            PlayerPrefs.SetInt(GameSaveKey.Daily, value);
            PlayerPrefs.Save();

        }
    }

    public static bool NoAds
    {
        get => PlayerPrefsX.GetBool(GameSaveKey.KEY_NOADS, false);
        set
        {
            PlayerPrefsX.SetBool(GameSaveKey.KEY_NOADS, value);
            PlayerPrefs.Save();
        }
    }

}

public class GameSaveKey
{
    public const string KEY_NOADS = "KEY_NOADS";

    public const string KEY_USERNAME = "KEY_USERNAME";
    public const string KEY_PASSWORD = "KEY_PASSWORD";

    public const string KEY_TOTALLEVEL = "KEY_TOTALLEVEL";
    public const string KEY_LEVEL = "KEY_LEVEL";
    public const string KEY_DATE = "KEY_DATE";
    public const string KEY_WIN = "KEY_WIN";
    public const string KEY_COIN = "KEY_COIN";

    public const string KEY_INTERN_COUNT = "KEY_INTERN_COUNT";
    public const string KEY_REWARD_COUNT = "KEY_REWARD_COUNT";

    public const string KEY_UNIT_COUNT = "KEY_UNIT_COUNT";
    public const string KEY_BALL_COUNT = "KEY_BALL_COUNT";
    public const string KEY_TOWER_COUNT = "KEY_TOWER_COUNT";
    public const string KEY_DRONE_COUNT = "KEY_DRONE_COUNT";

    public const string OFFTIME = "OFFTIME";

    public const string GIFT_BOX = "GIFT_BOX";
    public const string Daily = "Dailygift";

    public const string KEY_SOUND = "KEY_SOUND";
    public const string KEY_MUSIC = "KEY_MUSIC";
    public const string KEY_VIBR = "KEY_VIBR";

}

public enum ResourceType
{
    COIN
}