using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationBahavior : StateMachineBehaviour
{
    public float speed;

    private GameObject player;
    private Vector3 runDirection;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 runDirection = (player.transform.position - animator.transform.position).normalized;
        runDirection.y = 0;
        animator.transform.Translate(runDirection * Time.deltaTime * speed, Space.World);
        animator.transform.LookAt(player.transform);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
