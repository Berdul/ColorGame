using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehavior : MonoBehaviour
{
    public int damage;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other) {
        MonsterBehavior monsterBehavior = other.transform.GetComponent<MonsterBehavior>();

        // Check if it's a monster, and that the melee_attack animation is playing 
        // (avoid damaging when weapon is standing still)
        if (monsterBehavior != null && 
        animator.GetCurrentAnimatorStateInfo(0).fullPathHash ==  Animator.StringToHash("Base Layer.melee_attack")) {
            Debug.Log("épée");
            monsterBehavior.damage(damage);
        }
    }

    // private void OnTriggerEnter(Collider other) {
    //     MonsterBehavior monsterBehavior = other.GetComponent<MonsterBehavior>();

    //     // Check if it's a monster, and that the melee_attack animation is playing 
    //     // (avoid damaging when weapon is standing still)
    //     if (monsterBehavior != null && 
    //     animator.GetCurrentAnimatorStateInfo(0).fullPathHash ==  Animator.StringToHash("Base Layer.melee_attack")) {
    //         Debug.Log("épée");
    //         monsterBehavior.damage(damage);
    //     }
    // }
}
