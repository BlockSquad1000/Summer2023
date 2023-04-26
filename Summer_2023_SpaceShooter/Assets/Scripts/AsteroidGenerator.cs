using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public int numberOfAsteroids;
    public GameObject asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfAsteroids; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-1000, 1100), Random.Range(-1000, 1100), Random.Range(-1000, 1100));
            Instantiate(asteroidPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
