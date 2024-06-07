using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public class PlayfabManager : Singleton<PlayfabManager>
{
    public string device_id = string.Empty;
    public string android_id = string.Empty; // device ID to use with PlayFab login
    public string ios_id = string.Empty; // device ID to use with PlayFab login
    public string custom_id = string.Empty; // custom id for other platforms     
    public int TotalPlayer;


    private void Awake()
    {
        base.Awake();
        TotalPlayer = 0;
        LoginWithDeviceId();
    }
    #region Login

    public void LoginWithDeviceId()
    {
        Action<bool> processResponse = (bool response) =>
        {
            if (response && GetDeviceId())
            {
                if (!string.IsNullOrEmpty(android_id))
                {
                    Debug.Log("Using Android Device ID: " + android_id);
                    var request = new LoginWithAndroidDeviceIDRequest
                    {
                        AndroidDeviceId = android_id,
                        TitleId = PlayFabSettings.TitleId,
                        CreateAccount = true
                    };
                    PlayerPrefs.SetString("Username", android_id);
                    PlayerPrefs.SetString("Password", null);
                    PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, (PlayFabError error) =>
                    {
                        if (error.Error == PlayFabErrorCode.AccountNotFound)
                        {
                            Debug.Log("Account not found");
                        }
                        else
                        {
                            OnLoginError(error);
                        }

                    });
                }
                else if (!string.IsNullOrEmpty(ios_id))
                {
                    Debug.Log("Using IOS Device ID: " + ios_id);
                    var request = new LoginWithIOSDeviceIDRequest
                    {
                        DeviceId = ios_id,
                        TitleId = PlayFabSettings.TitleId,
                        CreateAccount = true
                    };

                    PlayerPrefs.SetString("Username", ios_id);
                    PlayerPrefs.SetString("Password", null);
                    // DialogCanvasController.RequestLoadingPrompt(PlayFabAPIMethods.GenericLogin);
                    PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, (PlayFabError error) =>
                    {
                        if (error.Error == PlayFabErrorCode.AccountNotFound)
                        {
                            Debug.Log("Account not found");
                        }
                        else
                        {
                            OnLoginError(error);
                        }
                    });
                }
            }
            else
            {
                Debug.Log("Using custom device ID: " + custom_id);
                var request = new LoginWithCustomIDRequest
                {
                    CustomId = custom_id,
                    TitleId = PlayFabSettings.TitleId,
                    CreateAccount = true
                };

                PlayerPrefs.SetString("Username", custom_id);
                PlayerPrefs.SetString("Password", null);
                //DialogCanvasController.RequestLoadingPrompt(PlayFabAPIMethods.GenericLogin);
                PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, error =>
                {
                    if (error.Error == PlayFabErrorCode.AccountNotFound)
                    {
                        Debug.Log("Account not found");
                    }
                    else
                    {
                        OnLoginError(error);
                    }
                });
            }
        };

        processResponse(true);
        //DialogCanvasController.RequestConfirmationPrompt("Login With Device ID", "Logging in with device ID has some issue. Are you sure you want to contine?", processResponse);
    }

    /// <summary>
    /// Raises the login error event.
    /// </summary>
    /// <param name="error">Error.</param>
    private void OnLoginError(PlayFabError error) //PlayFabError
    {
        string errorMessage;
        if (error.Error == PlayFabErrorCode.InvalidParams && error.ErrorDetails.ContainsKey("Password"))
        {
            errorMessage = "Invalid Password";
        }
        else if (error.Error == PlayFabErrorCode.InvalidParams && error.ErrorDetails.ContainsKey("Username") || (error.Error == PlayFabErrorCode.InvalidUsername))
        {
            errorMessage = "Invalid Username";
        }
        else if (error.Error == PlayFabErrorCode.AccountNotFound)
        {
            errorMessage = "Account Not Found, you must have a linked PlayFab account. Start by registering a new account or using your device id";
        }
        else if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            errorMessage = "Account Banned";
        }
        else if (error.Error == PlayFabErrorCode.InvalidUsernameOrPassword)
        {
            errorMessage = "Invalid Username or Password";
        }
        else
        {
            errorMessage = string.Format("Error {0}: {1}", error.HttpCode, error.ErrorMessage);
        }

        Debug.LogError(errorMessage);

    }

    public void OnLoginSuccess(LoginResult result)
    {
        if (result.NewlyCreated)
        {
            Debug.Log("Create new " + result.PlayFabId);
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "makeFirstCall",
                GeneratePlayStreamEvent = true
            };

            PlayFabClientAPI.ExecuteCloudScript(request, success =>
            {
            }, error => { });
        }
        else
        {
            //SetUserDataStatic();
            Debug.Log("Login with " + result.PlayFabId);
        }
        ClientGetTitleData();
    }

    #endregion


    public void ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result =>
            {
                if (result.Data == null || !result.Data.ContainsKey("TotalPlayer"))
                {
                    Debug.Log("No MonsterName");
                }
                else
                {
                    int.TryParse(result.Data["TotalPlayer"], out TotalPlayer);
                }
            },
            error =>
            {
                Debug.Log("Got error getting titleData:");
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }


    #region GetId
    /// <summary>
    /// Gets the device identifier and updates the static variables
    /// </summary>
    /// <returns><c>true</c>, if device identifier was obtained, <c>false</c> otherwise.</returns>
    public bool GetDeviceId(bool silent = false) // silent suppresses the error
    {
        if (CheckForSupportedMobilePlatform())
        {
#if UNITY_ANDROID
            //http://answers.unity3d.com/questions/430630/how-can-i-get-android-id-.html
            AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
            device_id = android_id;
#endif

#if UNITY_IPHONE
			ios_id = UnityEngine.iOS.Device.vendorIdentifier;
            device_id = ios_id;
#endif
            return true;
        }
        else
        {
            custom_id = SystemInfo.deviceUniqueIdentifier;
            device_id = custom_id;
            return false;
        }
    }

    /// <summary>
    /// Check to see if our current platform is supported (iOS & Android)
    /// </summary>
    /// <returns><c>true</c>, for supported mobile platform, <c>false</c> otherwise.</returns>
    public bool CheckForSupportedMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
    #endregion
}