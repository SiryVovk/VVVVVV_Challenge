using System.Collections.Generic;

[System.Serializable]
public class LocalizationWrapper
{
    public LocalizationEntry[] entries;

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();
        foreach (var e in entries)
            dict[e.key] = e.value;

        return dict;
    }
}
