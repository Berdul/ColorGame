using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private class Point3D {
        public float x;
        public float y;
        public float z;
         
        public Point3D(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 toVector3() {
            return new Vector3(x, y, z);
        }
    }


    public GameObject monsterPrefab;
    public float positionSpawnY;
    public float spwanRadius;
    public float tileSideLength;
    public float tileThickness;
    public float minSpawnDistance;

    private GameObject player;
    private List<Point3D> spawnPoints = new List<Point3D>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("defaultTile"))
        {
            spawnPoints.Add(new Point3D(tile.gameObject.transform.position.x, tile.gameObject.transform.position.y, tile.gameObject.transform.position.z));
        }
        
        InvokeRepeating("spawnMonster", 2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnMonster() {
        bool canSpawn = false;
        Vector3 spawnPosition;
        do {
            spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].toVector3();
            spawnPosition.x = spawnPosition.x + Random.Range(-tileSideLength * 0.8f / 2, tileSideLength * 0.8f / 2);
            spawnPosition.z = spawnPosition.z + Random.Range(-tileSideLength * 0.8f / 2, tileSideLength * 0.8f / 2);

            if (Vector3.Distance(player.transform.position, spawnPosition) > minSpawnDistance) {
                canSpawn = true;
            }
        } while (!canSpawn);
         
        // Define a random area where the monster can spawn on the tile accordiing to tile side length, scale with a "safety coef" to avoid spawning in walls


        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}


