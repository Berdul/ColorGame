using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")) {
            MonsterBehavior monsterBehavior = other.GetComponentInParent<MonsterBehavior>();
            if (gameObject.GetComponent<Renderer>().material.color == monsterBehavior.color) {
                monsterBehavior.damage(damage * 3);
            } else {
                monsterBehavior.damage(damage);
            }
            Destroy(gameObject);
        }
        if(other.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }

}
