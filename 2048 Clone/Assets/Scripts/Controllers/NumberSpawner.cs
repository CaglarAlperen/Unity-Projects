using System.Collections;
using UnityEngine;

public class NumberSpawner : MBSingleton<NumberSpawner>
{
    [SerializeField] private GameObject _numberPrefab;
    [SerializeField] private float _scaleSmoothFactor;

    private void Awake()
    {
        GameManager.OnGameStarted += GameStart;
    }

    private void GameStart()
    {
        Spawn();
        Spawn();
    }

    public void Spawn()
    {
        Vector2 spawnPosition = RandomEmptyPosition();
        GameObject obj = Instantiate(_numberPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnAnim(obj.transform));
        int power = Random.Range(0.0f,1.0f) > 0.9f ? 2 : 1;
        obj.GetComponent<NumberCell>().Initialize((int)Mathf.Pow(2,power), power - 1);
        GridManager.Instance[(int)spawnPosition.y, (int)spawnPosition.x] = obj.GetComponent<NumberCell>().Value;
    }

    private Vector2 RandomEmptyPosition()
    {
        Vector2 pos;
        do
        {
            pos.x = Random.Range(0, 4);
            pos.y = Random.Range(0, 4);
        } while (GridManager.Instance[(int)pos.y, (int)pos.x] != 0);

        return pos;
    }

    IEnumerator SpawnAnim(Transform obj)
    {
        obj.localScale = Vector3.zero;
        for (float lerpValue = 0; lerpValue < 1; lerpValue += 1 / _scaleSmoothFactor) 
        {
            obj.localScale = Vector3.Slerp(Vector3.zero, new Vector3(0.5f, 0.5f, 1), lerpValue);
            yield return new WaitForEndOfFrame();
        }
        obj.localScale = new Vector3(0.5f, 0.5f, 1);
    }
}
