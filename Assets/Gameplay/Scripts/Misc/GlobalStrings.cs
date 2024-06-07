using System.Collections.Generic;

public static class GlobalStrings
{
    public enum GroupType
    {
        GRPLine10BotToTop,
        GRP3_INFINITY,
        GRP3_INFINITY_VER,
        TUTORIAL,
        GRPLine10RightToLeft,
        GRPLine10LeftToRight
    }

    public static class EventCall
    {
        public const string START_GAME = "StartGame";
        public const string START_GAME_2 = "StartGame2";
        public const string END_GAME = "EndGame";
        public const string SURE_END_GAME = "SureEndGame";
        public const string HOME = "home";
        public const string DELETE_BLOCK = "Delete";
        public const string REROLL = "Reroll";
        public const string CHECK = "Check";
        public const string CREATE_END_GAME = "winable";
    }

    // Strings displayed to the user
    public const string FB_LOGIN_MSG = "Facebook Login Found";
    public const string DEVICE_ID_LOGIN_MSG = "Device ID Found";
    public const string DEVICE_ID_CREATE_MSG = "No Login Found (Creating New Account with Device ID)";
    public const string TEST_LOGIN_MSG = "Unsupported Platform (Using Facebook in Test Mode)";
    public const string TEST_LOGIN_PROMPT = "Developer Login";
    public const string AUTO_STATUS_MSG = "Checking for Supported Platforms";
    public const string STATUS_PROMPT = "Status";
    public const string NET_FAIL_MSG = "Your device has poor or no internet connectivity. Please check your connection and try again.";
    public const string REGISTER_FAIL_MSG = "You must fill out all fields, and passwords must match.";
    public const string LOGOUT_BTN_TXT = "Return to Log In";
    public const string CREATE_BTN_TXT = "Create New Account";
    public const string PAGE_NUMBER_MSG = "Page {0} of {1}";
    public const string ACT_STATUS_REG_MSG = "Registered";
    public const string ACT_STATUS_UNREG_MSG = "Unregistered";
    public const string LINK_FB_BTN_MSG = "Link to Facebook";
    public const string UNLINK_FB_BTN_MSG = "Unlink from Facebook";
    public const string LOGOUT_BTN_MSG = "Logged Out";
    public const string LOADING_MSG = "Loading";
    public const string COMPLETE_MSG = "Complete!";
    public const string UNLOCKED_MSG = "Unlocked";
    public const string LOGOUT_PROMPT = "Log Out?";
    public const string LOGOUT_MSG = "Are you sure you want to log out?";

    // Programatic strings
    public const string PrimaryCatalogName = "CharacterClasses";
    public static readonly List<string> InitTitleKeys = new List<string> { "CharacterLevelExp", "CharacterLevelTime", "Dialog", "ListStage", "RoomEncounter" };
    public const string DEFAULT_UB_TITLE_ID = "4E22F";
    public const string FB_PREF_KEY = "LinkedFacebook";
    public const string DEVICE_PREF_KEY = "LastDeviceIdUsed";

    public const string CHARACTER = "Character";
    public const string GOLD_COIN = "GC";
    public const string VOID_HEART = "VH";
    public const string STAMINA = "ST";
    public const string STAMINA_MAX = "SM";

    // Events
    public const string QUEST_START_EVENT = "Starting Quest";
    public const string QUEST_COMPLETE_EVENT = "Quest Complete";
    public const string BEGIN_EVENT = "Turn Begins";
    public const string NEXT_ENCOUNTER_EVENT = "A new challenger";
    public const string BOSS_BATTLE_EVENT = "Starting Boss Battle";

    // Art

    public const string GOLD_SPRITE = "Materials/CoinsGold";

}