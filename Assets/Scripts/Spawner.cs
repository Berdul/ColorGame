using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float positionSpawnY;
    public float spwanRadius;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("spawnMonster", 2f, 4f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnMonster() {
        Vector2 rngVector2 = Random.insideUnitCircle.normalized * spwanRadius;
        Vector3 rngVector = new Vector3(rngVector2.x, positionSpawnY, rngVector2.y);
        Vector3 spawnPosition = player.transform.position + rngVector;

        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
