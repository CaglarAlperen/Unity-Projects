using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    [SerializeField] private GameObject emptyCell;
    public static GameObject EmptyCell => Instance.emptyCell;

    [SerializeField] private GameObject captureCell;
    public static GameObject CaptureCell => Instance.captureCell;

    [SerializeField] private GameObject holdCurrentCell;
    public static GameObject HoldCurrentCell => Instance.holdCurrentCell;

    public static MoveIndicator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
