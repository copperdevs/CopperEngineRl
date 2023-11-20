﻿namespace CopperEngine.Info;

public static class SystemInfo
{
    public static string UserName => Environment.UserName;
    public static string MachineName => Environment.MachineName;
    public static string Cpu => Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") ?? "Unknown";
    public static int MemorySize => (int) Math.Ceiling(GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / (1024.0F * 1024.0F * 1024.0F));
    public static int Threads => Environment.ProcessorCount;
    public static string Os => Environment.OSVersion.VersionString;
}