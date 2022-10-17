using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabs;

    // Spawn location support
    const int SpawnBorderSize = 50;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 3)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1)
        {
            Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), -Camera.main.transform.position.z);
            Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);

            Instantiate(prefabs, worldLocation, Quaternion.identity);
            timer -= 5;
        }

    }
}
