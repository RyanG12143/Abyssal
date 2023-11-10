using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPre;
    public int rockSpawnLimit;
    private int rockSpawnCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRocks());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnRocks()
    {
        Vector2 spawnPos1 = transform.position + new Vector3(0, 0, 0);
        Vector2 spawnPos2 = transform.position + new Vector3(1, 0, 0);
        Vector2 spawnPos3 = transform.position + new Vector3(-1, 0, 0);

        while (rockSpawnCount < rockSpawnLimit)
        {
            Instantiate(rockPre, spawnPos1, Quaternion.identity);
            Instantiate(rockPre, spawnPos2, Quaternion.identity);
            Instantiate(rockPre, spawnPos3, Quaternion.identity);
            yield return new WaitForSeconds(.1f);
            rockSpawnCount++;
        }
        
    }
}
