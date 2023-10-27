using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMapSpawn : MonoBehaviour
{
    public GameObject prefabs;
    public int totalNumberRenderPerZone = 15;

    public float leftMostX = -11f;
    public float rightMostX = 11f;
    public float lowestY = -7f;
    public float highestY = 4.5f;

    float spawnPositionX, spawnPositionY;
    Vector3 spawnPosition;


    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            int j = 0;
            while (j < totalNumberRenderPerZone)
            {
                if (!CellHasCollider(SpawnEnemy(i)))
                {
                    Instantiate(prefabs, SpawnEnemy(i), Quaternion.identity);
                    j++;
                }
            }
        }

    }

    private Vector3 SpawnEnemy(int zone)
    {
        /**
         * Zone 0: Top left corner
         * Zone 1: Top right corner
         * Zone 2: Bottom left corner
         * Zone 3: Bottom right corner
         */
        switch (zone)
        {
            case 0:
                spawnPositionX = Random.Range(leftMostX, (rightMostX + leftMostX) / 2);
                spawnPositionY = Random.Range((highestY + lowestY) / 2, highestY);
                break;
            case 1:
                spawnPositionX = Random.Range((rightMostX + leftMostX) / 2, rightMostX);
                spawnPositionY = Random.Range((highestY + lowestY) / 2, highestY);
                break;
            case 2:
                spawnPositionX = Random.Range(leftMostX, (rightMostX + leftMostX) / 2);
                spawnPositionY = Random.Range(lowestY, (highestY + lowestY) / 2);
                break;
            case 3:
                spawnPositionX = Random.Range((rightMostX + leftMostX) / 2, rightMostX);
                spawnPositionY = Random.Range(lowestY, (highestY + lowestY) / 2);
                break;
        }
        spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0f);
        return spawnPosition;
    }

    private bool CellHasCollider(Vector3 cellWorldPosition)
    {
        // Raycast at position
        // If not null -> true
        // Else -> false
        var c = Physics2D.OverlapBox((Vector2)cellWorldPosition, new Vector2(1, 1), 0);

        return c != null;
    }
}
