using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("??????????" + Time.time);
        if (other.CompareTag("Monster")) {
            Debug.Log("MONSTEEEER" + Time.time);
            MonsterBehavior monsterBehavior = other.GetComponentInParent<MonsterBehavior>();
            if (gameObject.GetComponent<Renderer>().material.color == monsterBehavior.color) {
                monsterBehavior.damage(damage * 3);
            } else {
                monsterBehavior.damage(damage);
            }
            Destroy(gameObject);
        }
        if(other.CompareTag("Wall")) {
            Debug.Log("HAAAAAAAAAAAAAAAAAAAAAH" + Time.time);
            Destroy(gameObject);
        }
    }

}
