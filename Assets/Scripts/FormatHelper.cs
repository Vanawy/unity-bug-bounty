using System;

public static class FormatHelper
{
    public static string FormatTimeSpan(TimeSpan timeSpan)
    {
        return string.Format("{0:D2}:{1:D2}:{2:D3}", (int) timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }
}