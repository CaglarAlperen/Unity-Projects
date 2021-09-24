using UnityEngine;

[CreateAssetMenu(menuName = "ColorPalette")]
public class ColorPalette : ScriptableObject
{
    [SerializeField] private Color[] colors;

    public Color this[int index]
    {
        get => colors[index];
    }
}
