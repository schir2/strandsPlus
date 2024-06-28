using System;

namespace Managers
{
    [Flags]
    public enum GameModeOptions
    {
        None = 0,
        TimeTrial = 1 << 0,
        Timed = 1 << 1,
        Hints = 1 << 2,
        OnlyCorrectWords = 1 << 3,
        SpangramOnly = 1 << 4
    }
}