using System.Runtime.InteropServices;
using System.Text;
using Raylib_cs;

namespace CopperEngine.Info;

public static class Log
{
    public static void Info(object message) => CopperLogger.UninitializedLogInfo(message);
    public static void Warning(object message) => CopperLogger.UninitializedLogWarning(message);
    public static void Error(object message) => CopperLogger.UninitializedLogError(message);
    public static void Error(Exception e) => Error($"[{e.GetType()}] {e.Message} \n {e.StackTrace}");
}

public static unsafe partial class CopperLogger
{
    [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    private static extern int vsprintf(StringBuilder buffer, string format, IntPtr args);

    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _vscprintf(string format, IntPtr ptr);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static void RayLibLog(int msgType, sbyte* text, sbyte* args)
    {
        var textStr = Marshal.PtrToStringUTF8((IntPtr)text) ?? "";

        var sb = new StringBuilder(_vscprintf(textStr, (IntPtr)args) + 1);
        vsprintf(sb, textStr, (IntPtr)args);

        var messageLog = sb.ToString();

        switch ((TraceLogLevel)msgType)
        {
            case TraceLogLevel.LOG_INFO:
                WriteBaseLog("INFO", messageLog, ConsoleColor.DarkGray);
                break;
            case TraceLogLevel.LOG_ERROR:
                WriteBaseLog("ERROR", messageLog, ConsoleColor.DarkRed);
                break;
            case TraceLogLevel.LOG_WARNING:
                WriteBaseLog("WARN", messageLog, ConsoleColor.DarkYellow);
                break;
            case TraceLogLevel.LOG_DEBUG:
                WriteBaseLog("DEBUG", messageLog, ConsoleColor.DarkGreen);
                break;
            default:
                WriteBaseLog("???", messageLog, ConsoleColor.DarkGray);
                break;
        }
    }

    private static void RayLibInitialize()
    {
        Raylib.SetTraceLogCallback(&RayLibLog);
        Raylib.SetTraceLogLevel((int)TraceLogLevel.LOG_ALL);
    }
}

public static partial class CopperLogger
{
    public delegate void BaseLog(object message);

    public static BaseLog? Info;
    public static BaseLog? Warning;
    public static BaseLog? Error;

    private static bool initialized = false;

    private static bool includeTimestamps = false;

    internal static List<string> LogLines = new();
    private static readonly string LogsPath = $"Logs/{Engine.StartTime.ToShortDateString().Replace("/", "-")}/";
    private static readonly string LogsName = $"{Engine.StartTime.ToLongTimeString().Replace(":", "-")}.txt";

    public static void Initialize(bool timestamps = true)
    {
        Initialize(LogInfo, LogWarning, LogError, timestamps);
    }

    public static void Initialize(BaseLog infoLog, BaseLog warningLog, BaseLog errorLog, bool timestamps = true)
    {
        if (initialized)
            return;
        initialized = true;

        Info = infoLog;
        Warning = warningLog;
        Error = errorLog;

        includeTimestamps = timestamps;
        RayLibInitialize();
    }

    public static void Shutdown()
    {
        Directory.CreateDirectory(LogsPath);
        File.Create($"{LogsPath}/{LogsName}").Dispose();
        File.WriteAllLines($"{LogsPath}/{LogsName}", LogLines);
    }

    internal static void UninitializedLogInfo(object message)
    {
        Initialize(LogInfo, LogWarning, LogError);
        LogInfo(message);
    }

    internal static void UninitializedLogWarning(object message)
    {
        Initialize(LogInfo, LogWarning, LogError);
        LogWarning(message);
    }

    internal static void UninitializedLogError(object message)
    {
        Initialize(LogInfo, LogWarning, LogError);
        LogError(message);
    }

    public static void LogInfo(object message) => WriteBaseLog("INFO", message, ConsoleColor.DarkGray);
    public static void LogWarning(object message) => WriteBaseLog("WARN", message, ConsoleColor.DarkYellow);
    public static void LogError(object message) => WriteBaseLog("ERROR", message, ConsoleColor.DarkRed);

    internal static void WriteBaseLog(string prefix, object message, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        var logString = "";

        if (includeTimestamps)
            logString += $"[{DateTime.Now.ToShortTimeString()}] ";
        logString += $"[{prefix}] {message}";

        LogLines.Add(logString);

        Console.WriteLine(logString);
        Console.ResetColor();
    }
}