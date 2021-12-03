using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject ZonePrefab;
    private int zoneSideLength = 20;
    public int mapSideSize; // must be odd number

    public GameObject monsterPrefab;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        createMap();
        spawnInfiniteMob();
    }

    private void spawnInfiniteMob() {
        InvokeRepeating("spawnMonster", 2f, 4f);
    }

    private void spawnMonster() {
        Vector3 rngVector = Random.insideUnitCircle.normalized * 5f;
        rngVector.y = 1;
        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(rngVector) + player.transform.position;

        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }

    private void createMap() {
        int offset = (mapSideSize - 1) * zoneSideLength / 2;
        for (int i = 0; i < mapSideSize; i++) {
            for (int j = 0; j < mapSideSize; j++) {
                Instantiate(ZonePrefab, new Vector3(i * zoneSideLength - offset, -1, j * zoneSideLength - offset), Quaternion.identity);
            }
        }
    }
}
