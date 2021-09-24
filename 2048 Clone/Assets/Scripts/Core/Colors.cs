using UnityEngine;

public class Colors : MBSingleton<Colors>
{
    [SerializeField] private ColorPalette colorPalette;

    public Color this[int index]
    {
        get => colorPalette[index];
    }
}
