using UnityEngine;
using System.Diagnostics;
using Object = UnityEngine.Object;

/// <summary>
/// カラーリスト
/// </summary>
public enum DColor
{
    white,
    grey,
    black,
    red,
    green,
    blue,
    yellow,
    cyan,
    brown,
    
}

/// <summary>
/// Debug再包装
/// Unity でのみ表示する。
/// </summary>
public static class Debug
{
    /******************************************
    *                roperties                *
    ******************************************/
    #region
    public static ILogger logger => UnityEngine.Debug.unityLogger;
    public static ILogger unityLogger => UnityEngine.Debug.unityLogger;
    public static bool developerConsoleVisible
    {
        get => UnityEngine.Debug.developerConsoleVisible;
        set => UnityEngine.Debug.developerConsoleVisible = value;
    }
    public static bool isDebugBuild => UnityEngine.Debug.isDebugBuild;
    #endregion
    
    /******************************************
    *                   Log                   *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message, UnityEngine.Object context) => UnityEngine.Debug.Log(message, context);
    [Conditional("UNITY_EDITOR")]
    public static void Log(object message) => UnityEngine.Debug.Log(message);
    #endregion

    /******************************************
    *                LogFormat                *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void LogFormat(string format, params object[] args) => UnityEngine.Debug.LogFormat(format, args);
    [Conditional("UNITY_EDITOR")]
    public static void LogFormat(string text, DColor color) => UnityEngine.Debug.LogFormat("<color={0}>{1}</color>",color.ToString(), text);
    [Conditional("UNITY_EDITOR")]
    public static void LogFormat(Object context, string format, params object[] args) => UnityEngine.Debug.LogFormat(context, format, args);
    #endregion
    
    /******************************************
    *                 LogError                *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void LogError(object message) => UnityEngine.Debug.LogError(message);
    [Conditional("UNITY_EDITOR")]
    public static void LogErrorFormat(string format, params object[] args) => UnityEngine.Debug.LogErrorFormat(format, args);
    #endregion

    /******************************************
    *               LogWarning                *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message) => UnityEngine.Debug.LogWarning(message);
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message, UnityEngine.Object context) => UnityEngine.Debug.LogWarning(message, context);
    [Conditional("UNITY_EDITOR")]
    public static void LogWarningFormat(string format, params object[] args) => UnityEngine.Debug.LogWarningFormat(format, args);
    #endregion

    /******************************************
    *              LogAssertion               *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertion(object message, Object context) => UnityEngine.Debug.LogAssertion(message, context);
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertion(object message) => UnityEngine.Debug.LogAssertion(message);
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertionFormat(Object context, string format, params object[] args) => UnityEngine.Debug.LogAssertionFormat(context, format, args);
    [Conditional("UNITY_EDITOR")]
    public static void LogAssertionFormat(string format, params object[] args) => UnityEngine.Debug.LogAssertionFormat(format, args);
    #endregion
    
    /******************************************
    *                  Assert                 *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, string message, Object context) => UnityEngine.Debug.Assert(condition, message, context);
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition) => UnityEngine.Debug.Assert(condition);
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, object message, Object context) => UnityEngine.Debug.Assert(condition, message, context);
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, string message) => UnityEngine.Debug.Assert(condition, message);
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, object message) => UnityEngine.Debug.Assert(condition, message);
    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, Object context) => UnityEngine.Debug.Assert(condition, context);
    [Conditional("UNITY_EDITOR")]
    public static void AssertFormat(bool condition, Object context, string format, params object[] args) => UnityEngine.Debug.AssertFormat(condition, context, format, args);
    [Conditional("UNITY_EDITOR")]
    public static void AssertFormat(bool condition, string format, params object[] args) => UnityEngine.Debug.AssertFormat(condition, format, args);
    #endregion
    
    /******************************************
    *               WriteLine                 *
    ******************************************/
    #region
    [Conditional("UNITY_EDITOR")]
    public static void WriteLine(string? message, string? category) => System.Diagnostics.Debug.WriteLine(message, category);
    [Conditional("UNITY_EDITOR")]
    public static void WriteLine(string format, params object?[] args) => System.Diagnostics.Debug.WriteLine(format, args);
    [Conditional("UNITY_EDITOR")]
    public static void WriteLine(string? message) => System.Diagnostics.Debug.WriteLine(message);
    [Conditional("UNITY_EDITOR")]
    public static void WriteLine(object? value) => System.Diagnostics.Debug.WriteLine(value);
    [Conditional("UNITY_EDITOR")]
    public static void WriteLine(object? value, string? category) => System.Diagnostics.Debug.WriteLine(value, category);
    #endregion
}
