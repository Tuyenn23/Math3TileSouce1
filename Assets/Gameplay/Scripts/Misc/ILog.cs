using UnityEngine;

public static class ILog
{
    public static void Log(object log)
    {
#if UNITY_EDITOR
        Debug.Log(log);
#endif
    }

    public static void LogError(object log)
    {
#if UNITY_EDITOR
        Debug.LogError(log);
#endif
    }

    public static void LogWarning(object log)
    {
#if UNITY_EDITOR
        Debug.LogWarning(log);
#endif
    }
}
