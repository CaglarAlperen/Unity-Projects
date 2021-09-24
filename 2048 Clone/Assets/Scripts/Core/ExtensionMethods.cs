using UnityEngine;

public static class ExtensionMethods
{
    public static void SnapToGrid(this Transform transform)
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y), 0f);
    }

    public static Vector3 AddVector2(this Vector3 vector3, Vector2 vector2)
    {
        return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y, vector3.z);
    } 

    public static Vector2 Direction(this Vector2 vector)
    {
        return (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y)) ?
            Vector2.right * Mathf.Sign(vector.x) :
            Vector2.up * Mathf.Sign(vector.y);
    }

    public static bool IsInGrid(this Vector2 vector)
    {
        return vector.x >= 0 && vector.x < 4 && vector.y >= 0 && vector.y < 4;
    }
}
