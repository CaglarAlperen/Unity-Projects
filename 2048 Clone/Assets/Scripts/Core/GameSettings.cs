using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    public static float MoveDuration => Instance.moveDuration;

    [SerializeField] private float moveSmoothFactor;
    public static float MoveSmoothFactor => Instance.moveSmoothFactor;

    [SerializeField] private float reachDistance;
    public static float ReachDistance => Instance.reachDistance;

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

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public static GameSettings Instance { get; private set; }
}
