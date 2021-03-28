using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{

    [SerializeField] GameObject spike;

    int difficultyLevel = 1;
    float wall_offset_x = 2.52f;
    int min_y = -4;
    int max_y = 4;


    private void Start()
    {
        SpawnSpikes(difficultyLevel);
    }

    private void SpawnSpikes(int count)
    {
        DestroySpikes();
        List<int> leftSpikes = RandomArrayGenerator(count, min_y, max_y);
        List<int> rightSpikes = RandomArrayGenerator(count, min_y, max_y);
        foreach (int y_pos in leftSpikes)
        {
            Instantiate(spike, new Vector3(-wall_offset_x, y_pos, 0f), Quaternion.identity);
        }
        foreach (int y_pos in rightSpikes)
        {
            GameObject spike_obj = Instantiate(spike, new Vector3(wall_offset_x, y_pos, 0f), Quaternion.identity) as GameObject;
            spike_obj.transform.localScale = new Vector3(-spike_obj.transform.localScale.x, spike_obj.transform.localScale.y, spike_obj.transform.localScale.z);
        }
    }

    private List<int> RandomArrayGenerator(int size, int min, int max)
    {
        List<int> randomArray = new List<int>();
        
        while (randomArray.Count < size)
        {
            int randomNumber = Random.Range(min,max+1);
            bool exists = false;
            foreach (int num in randomArray)
            {
                if (num == randomNumber) exists = true;
            }
            if (!exists) randomArray.Add(randomNumber);
        }

        return randomArray;
    }

    public void UpdateSpikes()
    {
        StartCoroutine(WaitAndSpawn());
    }

    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        SpawnSpikes(difficultyLevel);
    }

    private void DestroySpikes()
    {
        Spike[] spikes = FindObjectsOfType<Spike>();
        foreach (Spike spike in spikes)
        {
            Destroy(spike.gameObject);
        }
    }

    public void IncreaseDifficulty()
    {
        if (difficultyLevel < 5) difficultyLevel++;
    }

    public void DecreaseDifficulty()
    {
        if (difficultyLevel > 1) difficultyLevel -= 2;
    }
}
