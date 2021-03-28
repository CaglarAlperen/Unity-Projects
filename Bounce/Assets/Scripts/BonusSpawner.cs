using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{

    [SerializeField] float minWait = 5f;
    [SerializeField] float maxWait = 10f;
    [SerializeField] float bonusLifeTime = 3f;
    [SerializeField] float startWaitTime = 10f;
    [SerializeField] GameObject[] bonuses;


    private void Start()
    {
        StartCoroutine(WaitAndStart());
    }

    IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(startWaitTime);
        StartCoroutine(SpawnBonuses());
    }

    IEnumerator SpawnBonuses()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            int score = FindObjectOfType<Score>().GetScore();
            if (score < 30)
            {
                StartCoroutine(Spawn(bonuses[0]));
            }
            else
            {
                StartCoroutine(Spawn(bonuses[Random.Range(0, 2)]));
            }
        }
    }

    IEnumerator Spawn(GameObject bonus)
    {
        int y_pos = Random.Range(-4, 5);
        GameObject bonusObj = Instantiate(bonus, new Vector3(0f, y_pos, 0f), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(bonusLifeTime);
        Destroy(bonusObj);
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }
}
