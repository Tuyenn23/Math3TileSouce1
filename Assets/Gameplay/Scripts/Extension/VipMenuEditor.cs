﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using System;
using SimpleJSON;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
#endif

public class VipMenuEditor
{
    public static readonly string[] DEFINE_BUILD = { "ODIN_INSPECTOR", "ACTIVE_MAX",
        "ACTIVE_FIREBASE_ANALYTICS", "ACTIVE_FIREBASE_CRASHLYTICS","ACTIVE_FIREBASE_REMOTE","ACTIVE_FIREBASE" ,
        "UNITY_IAP"};
    //public static readonly string[] DEFINE_BUILD = { "UNITY_IAP", "USE_DOTWEEN",
    //    "ACTIVE_FIREBASE", "ACTIVE_FIREBASE_REMOTE","ACTIVE_FIREBASE_CRASHLYTICS","ACTIVE_FIREBASE_ANALYTICS" ,
    //    "ODIN_INSPECTOR","TUTORIAL","SPINE_SKIP","NO_TUT","ACTIVE_FACEBOOK","ACTIVE_IRONSOURCE"};
    //  public static readonly string[] DEFINE_BUILD = { "UNITY_IAP", "USE_DOTWEEN",
    //      "ACTIVE_FIREBASE", "ACTIVE_FIREBASE_REMOTE","ACTIVE_FIREBASE_CRASHLYTICS","ACTIVE_FIREBASE_ANALYTICS",
    //"ACTIVE_FACEBOOK","ACTIVE_IRONSOURCE" ,"ODIN_INSPECTOR","TUTORIAL","SPINE_SKIP","NO_TUT","GDMODE"};
    public static readonly string[] DEFINE_BUILD_WEBGL = { "USE_DOTWEEN", "ODIN_INSPECTOR", "TUTORIAL", "SPINE_SKIP", "NO_TUT" };
    public static readonly string[] DEFINE_BUILD_PC = { "USE_DOTWEEN", "ODIN_INSPECTOR", "TUTORIAL", "SPINE_SKIP", "NO_TUT" };
    const string DEFINE_UNLOCKALL = "DEVELOPMENT";
    const string DEFINE_GD = "GDMODE";
    const string DEFINE_RED_BULL = "RED_BULL";
    //D:\DongNHBeemobDrive\Build
    //const string BUILD_PATH = "D:/DongNHBeemobDrive/Build/Cyber/";


    const string MENU_NAME = "(=^･ω･^=)";

    private const string ALT = "&";
    private const string SHIFT = "#";
    private const string CTRL = "%";


#if UNITY_EDITOR
    [MenuItem(MENU_NAME + "/Scene/Splash " + ALT + "1")]
    static void OpenSP()
    {
        LoadSceneByName("Splash");
        //EditorSceneManager.OpenScene("Assets/Core/Scenes/Splash.unity");
    }

    [MenuItem(MENU_NAME + "/Scene/Home " + ALT + "2")]
    static void OpenHome()
    {
        LoadSceneByName("Home");
        //EditorSceneManager.OpenScene("Assets/Core/Scenes/Home.unity");
    }

    [MenuItem(MENU_NAME + "/Scene/Gameplay " + ALT + "3")]
    static void OpenGP()
    {
        LoadSceneByName("GamePlay");
        //EditorSceneManager.OpenScene("Assets/Core/Scenes/GamePlay.unity");
    }

    [MenuItem(MENU_NAME + "/Tools/Level Design ")]
    static void OpenLVD()
    {
        EditorSceneManager.OpenScene("Assets/Core/Scenes/Tools/LevelDesign.unity");
    }
    [MenuItem(MENU_NAME + "/Tools/AvataMaker")]
    static void OpenAvatarHero()
    {
        EditorSceneManager.OpenScene("Assets/Core/Scenes/Tools/HeroGenerater.unity");
    }

    [MenuItem(MENU_NAME + "/Tools/CheckHero")]
    static void OpenCheckHero()
    {
        EditorSceneManager.OpenScene("Assets/Core/Scenes/Tools/CheckHero.unity");
    }
    [MenuItem(MENU_NAME + "/Tools/CheckEnemy")]
    static void OpenCheckEnemy()
    {
        EditorSceneManager.OpenScene("Assets/Core/Scenes/Tools/CheckEnemy.unity");
    }
    [MenuItem(MENU_NAME + "/Play Game")]
    static void PlayerGame()
    {
        EditorSceneManager.OpenScene("Assets/Core/Scenes/Splash.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    static void LoadSceneByName(string _nameScene)
    {
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Core/Scenes/" + _nameScene + ".unity");
    }


    //=====================
    [MenuItem(MENU_NAME + "/Build/Config")]
    static void BuildCongfig()
    {
        PlayerSettings.keystorePass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.keyaliasPass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.bundleVersion = VipEditorSetting.Instance.buildNumber + "." + VipEditorSetting.Instance.Version;
        PlayerSettings.Android.bundleVersionCode = VipEditorSetting.Instance.buildVersionCode;
        PlayerSettings.iOS.buildNumber = VipEditorSetting.Instance.buildVersionCode.ToString();
        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        //  SetupBuild(false, false);
        //EditorUserBuildSettings.activeScriptCompilationDefines=_script.ToArray();
    }

    [MenuItem(MENU_NAME + "/Build/ConfigDev")]
    static void BuildCongfigDev()
    {
        PlayerSettings.keystorePass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.keyaliasPass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.bundleVersion = VipEditorSetting.Instance.buildNumber + "." + VipEditorSetting.Instance.Version;
        PlayerSettings.Android.bundleVersionCode = VipEditorSetting.Instance.buildVersionCode;

        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        ActiveDevOption(true);
    }
    [MenuItem(MENU_NAME + "/Build/ConfigRelease")]
    static void BuildCongfigRelease()
    {
        PlayerSettings.keystorePass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.keyaliasPass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.bundleVersion = VipEditorSetting.Instance.buildNumber + "." + VipEditorSetting.Instance.Version;
        PlayerSettings.Android.bundleVersionCode = VipEditorSetting.Instance.buildVersionCode;
        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        ActiveDevOption(false);
        ActiveRedBull(false);
        ActiveRedBullHack(false);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }



    [MenuItem(MENU_NAME + "/Build/RED_BULL/Active")]
    static void BuildCongfigRedBull()
    {
        PlayerSettings.keystorePass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.keyaliasPass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.bundleVersion = VipEditorSetting.Instance.buildNumber + "." + VipEditorSetting.Instance.Version;
        PlayerSettings.Android.bundleVersionCode = VipEditorSetting.Instance.buildVersionCode;
        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        ActiveDevOption(false);
        ActiveRedBull(true);
    }
    [MenuItem(MENU_NAME + "/Build/RED_BULL/DisActive")]
    static void BuildCongfigRedBullDis()
    {
        PlayerSettings.keystorePass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.keyaliasPass = VipEditorSetting.Instance.buildPassword;
        PlayerSettings.bundleVersion = VipEditorSetting.Instance.buildNumber + "." + VipEditorSetting.Instance.Version;
        PlayerSettings.Android.bundleVersionCode = VipEditorSetting.Instance.buildVersionCode;
        Debug.Log("Config"
           + "\nPassword=  " + PlayerSettings.keystorePass
            + "\nVersion Name=  " + PlayerSettings.bundleVersion
            + "\nVersion Code=  " + PlayerSettings.Android.bundleVersionCode);
        ActiveDevOption(false);
        ActiveRedBull(false);
    }

    [MenuItem(MENU_NAME + "/Build/Config/Mode/Active")]
    static void BuildCongfigRedBullHack()
    {
        BuildCongfigRedBull();
        ActiveRedBull(true);
        ActiveRedBullHack(true);
    }
    [MenuItem(MENU_NAME + "/Build/Config/Mode/Disactive")]
    static void BuildCongfigRedBullHackDisactive()
    {
        BuildCongfigRedBull();
        ActiveRedBull(false);
        ActiveRedBullHack(false);
    }
    [MenuItem(MENU_NAME + "/Build/Config/Active GD")]
    static void BuildCongfigGD()
    {
        ActiveGDMode(true);
    }
    [MenuItem(MENU_NAME + "/Build/Config/Remove GD")]
    static void BuildCongfigRemoveGD()
    {
        ActiveGDMode(false);
    }

    [MenuItem(MENU_NAME + "/Build/Build/Build Apk-Dev ")]
    public static void AutoBuildAPKDEV()
    {
        Debug.Log("Building APK-DEV...");
        VipEditorSetting.Instance.DevBuildVersion++;
        SetupBuild(false, true);
    }
    [MenuItem(MENU_NAME + "/Build/Build/Build Final-apk ")]
    public static void AutoBuildApk()
    {
        Debug.Log("Building apk-Final...");
        VipEditorSetting.Instance.DevBuildVersion++;

        BuildCongfigRelease();
        SetupBuild(false, false);
    }
    [MenuItem(MENU_NAME + "/Build/Build/Build Final-aab ")]
    public static void AutoBuildaab()
    {
        Debug.Log("Building aab-Final...");
        VipEditorSetting.Instance.Version++;
        VipEditorSetting.Instance.buildVersionCode++;
        BuildCongfigRelease();
        SetupBuild(true, false);
    }
    [MenuItem(MENU_NAME + "/Build/Build/Build ALL ")]
    public static void AutoBuilAll()
    {
        Debug.Log("Building ALL...");
        Debug.Log("Building APK-DEV...");
        SetupBuild(false, true, () =>
        {
            Debug.Log("Building apk-Final...");
            SetupBuild(false, false, () =>
            {
                Debug.Log("Building aab-Final...");
                SetupBuild(true, false);
            });
        });
    }
    static void ActiveGDMode(bool isActive)
    {
        List<string> _scrips = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android).Split(',').ToList();
        if (isActive)
        {
            if (!_scrips.Contains(DEFINE_GD))
                _scrips.Add(DEFINE_GD);
        }
        else
        {
            if (_scrips.Contains(DEFINE_GD))
                _scrips.Remove(DEFINE_GD);
        }


        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _scrips.ToArray());
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, _scrips.ToArray());
    }

    static void ActiveRedBull(bool isActive)
    {
        AddDefine(BuildTargetGroup.Android, DEFINE_RED_BULL, isActive);
        AddDefine(BuildTargetGroup.iOS, DEFINE_RED_BULL, isActive);
        AddDefine(BuildTargetGroup.WebGL, DEFINE_RED_BULL, isActive);
        AddDefine(BuildTargetGroup.Standalone, DEFINE_RED_BULL, isActive);
    }

    static void ActiveRedBullHack(bool isActive)
    {
        AddDefine(BuildTargetGroup.Android, "RED_BULL_HACK", isActive);
        AddDefine(BuildTargetGroup.iOS, "RED_BULL_HACK", isActive);
        AddDefine(BuildTargetGroup.WebGL, "RED_BULL_HACK", isActive);
        AddDefine(BuildTargetGroup.Standalone, "RED_BULL_HACK", isActive);
    }

    static void AddDefine(BuildTargetGroup target, string define, bool isAdd)
    {
        List<string> _scrips = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Split(',').ToList();
        if (isAdd)
        {
            if (!_scrips.Contains(define))
                _scrips.Add(define);
        }
        else
        {
            if (_scrips.Contains(define))
                _scrips.Remove(define);
        }


        PlayerSettings.SetScriptingDefineSymbolsForGroup(target, _scrips.ToArray());

    }

    static void ActiveDevOption(bool buildDev)
    {
        //  ActiveRedBull(false);
        // GameUnityData.instance.SeverHost = buildDev ? ServerHostType.Test : ServerHostType.Product;
        //  return;
        //string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        //List<string> _script = definesString.Split(';').ToList();

        List<string> _script = new List<string>(DEFINE_BUILD);
        List<string> _scriptWebGL = new List<string>(DEFINE_BUILD_WEBGL);
        List<string> _scriptPC = new List<string>(DEFINE_BUILD_PC);
        if (buildDev)
        {
            if (!_script.Contains(DEFINE_UNLOCKALL))
            {
                _script.Add(DEFINE_UNLOCKALL);
                Debug.Log("Add Build " + DEFINE_UNLOCKALL);
            }
            //if (!_scriptWebGL.Contains(DEFINE_UNLOCKALL))
            //{
            //    _scriptWebGL.Add(DEFINE_UNLOCKALL);
            //    Debug.Log("Add Build " + DEFINE_UNLOCKALL);
            //}

            //if (_script.Contains(DEFINE_UNLOCKALL))
            //{
            //    _script.Remove(DEFINE_UNLOCKALL);
            //    Debug.Log("Remove Build " + DEFINE_UNLOCKALL);
            //}
            //if (_scriptWebGL.Contains(DEFINE_UNLOCKALL))
            //{
            //    _scriptWebGL.Remove(DEFINE_UNLOCKALL);
            //    Debug.Log("Remove Build " + DEFINE_UNLOCKALL);
            //}

#if UNITY_2020_1_11
			string definesString = "";
			foreach (var item in _script)
			{
				definesString += item + ";";
			}
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, definesString);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, definesString);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _script.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, _script.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, _scriptWebGL.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, _scriptPC.ToArray());
#endif

        }
        else
        {
            if (_script.Contains(DEFINE_UNLOCKALL))
            {
                _script.Remove(DEFINE_UNLOCKALL);
                Debug.Log("Remove Build " + DEFINE_UNLOCKALL);
            }
            if (_scriptWebGL.Contains(DEFINE_UNLOCKALL))
            {
                _scriptWebGL.Remove(DEFINE_UNLOCKALL);
                Debug.Log("Remove Build " + DEFINE_UNLOCKALL);
            }
#if UNITY_2020_1_11
			string definesString = "";
			foreach (var item in _script)
			{
				definesString += item + ";";
			}
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, definesString);
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, definesString);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _script.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, _script.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, _scriptWebGL.ToArray());
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, _scriptPC.ToArray());
#endif
        }
        Debug.Log("Build Define: " + _script.ToString());

    }
    static void SetupBuild(bool AppBundle = true, bool buildDev = true, System.Action OnBuildDone = null)
    {
        BuildCongfig();
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        //EditorBuildSettings.
        BuildOptions bo = BuildOptions.None;
        //if (buildDev)
        //    bo = BuildOptions.Development;

        AndroidArchitecture aac = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        // EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        // PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        PlayerSettings.Android.targetArchitectures = aac;
        EditorUserBuildSettings.buildAppBundle = AppBundle;


        ActiveDevOption(buildDev);

        string BUILD_PATH = VipEditorSetting.Instance.BuildPath;

        string fileName = buildDev ? VipEditorSetting.Instance.fileName + "_dev" : VipEditorSetting.Instance.fileName;
        fileName += VipEditorSetting.Instance.buildNumber + "." + VipEditorSetting.Instance.DevBuildVersion;
        string path = BUILD_PATH + fileName;
        path += AppBundle ? ".aab" : ".apk";
        BuildReport report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.Android, bo);

        BuildSummary summary = report.summary;
        Debug.Log("Building...");
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded:\nFile Name:" + summary.outputPath
                + "\nVersion Name" + PlayerSettings.bundleVersion
                 + "\nVersion Code" + PlayerSettings.Android.bundleVersionCode
                + "\nFile Size:" + summary.totalSize / 1024 / 1024 + " MB\nGood Luck");
            if (OnBuildDone != null)
                OnBuildDone();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
        if (summary.result == BuildResult.Succeeded)
            OpenFolderInWin(BUILD_PATH);

    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }

    [MenuItem(MENU_NAME + "/Build/Setting")]
    static void BuildSetting()
    {
        VipEditorSetting.Focus();
    }


    #region Tools
    [MenuItem(MENU_NAME + "/Tools/UI/Anchor Around Object")]
    static void uGUIAnchorAroundObject()
    {
        var o = Selection.activeGameObject;
        if (o != null && o.GetComponent<RectTransform>() != null)
        {
            var r = o.GetComponent<RectTransform>();
            var p = o.transform.parent.GetComponent<RectTransform>();

            var offsetMin = r.offsetMin;
            var offsetMax = r.offsetMax;
            var _anchorMin = r.anchorMin;
            var _anchorMax = r.anchorMax;

            var parent_width = p.rect.width;
            var parent_height = p.rect.height;

            var anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
                                        _anchorMin.y + (offsetMin.y / parent_height));
            var anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
                                        _anchorMax.y + (offsetMax.y / parent_height));

            r.anchorMin = anchorMin;
            r.anchorMax = anchorMax;

            r.offsetMin = new Vector2(0, 0);
            r.offsetMax = new Vector2(1, 1);
            r.pivot = new Vector2(0.5f, 0.5f);

        }
    }
    [MenuItem(MENU_NAME + "/Tools/Data/Clear Data")]
    static void ClearData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    //[MenuItem(MENU_NAME + "/Tools/Excel/Create Report")]
    //static void CreateReport()
    //{
    //    ExcelExportEditor.ExportLevelData();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/Update From Excel: LevelValues")]
    //static void UpdateLevelDataFromExcel_LevelValues()
    //{
    //    LevelConfigExtend.UpdateDataLevelValues();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/Update From Excel: LevelPOW")]
    //static void UpdateLevelDataFromExcel_LevelPow()
    //{
    //    LevelConfigExtend.UpdateDataLevelPOW();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/Update Excel from IngameData")]
    //static void UpdateExcel()
    //{
    //    LevelConfigExtend.FillDataFromIngame();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/Create Data to Firebase")]
    //static void CreateDataToUploadFirebase()
    //{
    //    LevelConfigExtend.GetDataToFirebase();
    //}

    //[MenuItem(MENU_NAME + "/Tools/Excel/PvP/Load PvPRank From Excel")]
    //static void LoadPvPRank()
    //{
    //    PvPConfigEditor.PvPData_UpdateRankData();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/PvP/Load PvPLevel From Excel")]
    //static void LoadPvPLevel()
    //{
    //    PvPConfigEditor.PvPData_UpdateLevels();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/PvP/PvPRank JSON")]
    //static void LoadPvPRankJSON()
    //{
    //    PvPConfigEditor.PvPData_GetJSON();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/PvP/PvPLevel JSON")]
    //static void LoadPvPLevelJSON()
    //{
    //    PvPConfigEditor.PvPLevel_GetJSON();
    //}
    //[MenuItem(MENU_NAME + "/Tools/Excel/DownloadFromExcel")]
    //static void DownloadExcel()
    //{
    //	LevelConfigExtend.DownloadFromExcel();
    //}
    //[MenuItem(MENU_NAME + "/Tools/GD Config")]
    //static void GDConfig()
    //{
    //    Selection.activeObject = GameUnityData.instance.ConstantsConfig;

    //}
    //[MenuItem(MENU_NAME + "/Tools/Game Config")]
    //static void GDConfigV2()
    //{
    //    Selection.activeObject = GameUnityData.instance;

    //}
    #endregion

    [MenuItem(MENU_NAME + "/Check IOS Custom Post")]
    static void Test()
    {
#if UNITY_EDITOR
        //  bool isAds = VipEditorSetting.Instance.IOS_Build_EnableAds;
        Debug.LogError(VipEditorSetting.Instance.ToString());
        VipEditorSetting.Focus();
        //  SetupBuild(true, false);
#endif
    }

    [MenuItem(MENU_NAME + "/LogLevel")]
    static void LogLevel()
    {
        //  bool isAds = VipEditorSetting.Instance.IOS_Build_EnableAds;
        string output = "";
        for (int i = 8; i <= 10; i++)
        {
            for (int j = 1; j <= 15; j++)
            {
                output += i * 1000 + j + ",";
            }
        }
        Debug.Log(output);
        //  SetupBuild(true, false);
    }

    [MenuItem(MENU_NAME + "/Capture Editor")]
    public static void Capture()
    {
        var datetimeNow = System.DateTime.Now;
        var name = $"Capture {datetimeNow}.png";
        ScreenCapture.CaptureScreenshot(name);
    }



    #region Extension
    public static void OpenFolderInWin(string path)
    {
        bool openInsidesOfFolder = false;

        // try windows
        string winPath = path.Replace("/", "\\"); // windows explorer doesn't like forward slashes

        if (System.IO.Directory.Exists(winPath)) // if path requested is a folder, automatically open insides of that folder
        {
            openInsidesOfFolder = true;
        }

        try
        {
            System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            // tried to open win explorer in mac
            // just silently skip error
            // we currently have no platform define for the current OS we are in, so we resort to this
            e.HelpLink = ""; // do anything with this variable to silence warning about not using it
        }
    }
    #endregion

#endif
}
