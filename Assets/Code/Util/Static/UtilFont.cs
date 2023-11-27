using UnityEngine;

public static class UtilFont
{
    public static Font GetFont()
    {
        return GetFontArial();
    }

    private static Font GetFontArial()
    {
        return Resources.GetBuiltinResource<Font>("Arial.ttf");
    }
}
