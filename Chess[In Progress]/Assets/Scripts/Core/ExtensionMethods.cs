using UnityEngine;

public static class ExtensionMethods
{
    public static void Round(this Vector2 vector)
    {
        vector.x = Mathf.Round(vector.x);
        vector.y = Mathf.Round(vector.y);
    }
}
