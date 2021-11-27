using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter(Collider other)
    {
        MonsterBehavior monsterBehavior = other.GetComponent<MonsterBehavior>();
        if (monsterBehavior != null) {
            // Monsters are immune to bullets of their own color
            if (gameObject.GetComponent<Renderer>().material.color == monsterBehavior.color) {
                monsterBehavior.damage(damage);
            }
            Destroy(gameObject);
        }
    }
}
