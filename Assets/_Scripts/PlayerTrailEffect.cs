using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailEffect : MonoBehaviour
{
    public float startTimeBtwSpawn;
    float timeBtwSpawn;
    public GameObject trailPrefab;
    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            GameObject trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
            Destroy(trail, 4f);
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
