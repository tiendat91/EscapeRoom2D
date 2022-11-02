using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public GameObject prefabs;
    public int totalNumberRenderPerZone = 3;
    public float maxLength = 5;

    public float leftMostX = -11f;
    public float rightMostX = 11f;
    public float lowestY = -7f;
    public float highestY = 4.5f;

    public List<GameObject> chests = new List<GameObject>();

    Vector3 spawnPosition;
    float randomLength;

    void Start()
    {
        for (int i = 0; i < chests.Count; i++)
        {
            int j = 0;
            while (j < totalNumberRenderPerZone)
            {
                if (true)
                {
                    Instantiate(prefabs, GetRandomPosition(chests[i].transform.position), Quaternion.identity);
                    j++;
                }
            }
        }
    }

    Vector3 GetRandomPosition(Vector3 chestPos)
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0f);

        do
        {
            randomLength = Random.Range(1f, maxLength);
            spawnPosition = (chestPos - randomDirection) * randomLength;
        } while (Vector3.Distance(spawnPosition, chestPos) > maxLength);

        return spawnPosition;
    }

    private bool CellHasCollider(Vector3 cellWorldPosition)
    {
        var c = Physics2D.OverlapBox((Vector2)cellWorldPosition, new Vector2(1, 1), 0);

        return c != null;
    }
}
