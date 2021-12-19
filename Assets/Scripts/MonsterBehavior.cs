using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float movementSpeed;
    public Color color;
    public int maxHealthPoints;
    public Slider healthBarSlider;
    public NavMeshAgent navMeshAgent;
    public GameObject objectToSpawnOnDeath;
    public float patrolMaxDistance;
    public float chasePlayerRange;
    public float attackPlayerRange;
    public float patrolSpeed;
    public float chaseSpeed;

    private Transform player;
    private int healthPoints;
    private GameObject score;
    private Vector3 moveDirection;
    private Vector3 spawnPosition;

    private bool bodySlamInCooldown = false;

    void Awake()
    {
        spawnPosition = gameObject.transform.position;
    }

    void Start()
    {
        player = PlayerManager.instance.player.transform;
        score = GameObject.FindGameObjectWithTag("Score");
        color = ColorManager.pickColor(Random.Range(0, ColorManager.colors.Length));
        gameObject.GetComponentInChildren<Renderer>().material.color = color;

        healthPoints = maxHealthPoints;
        healthBarSlider.maxValue = maxHealthPoints;
        healthBarSlider.value = healthPoints;
    }

    void Update()
    {
        if (!isPlayerInRange(chasePlayerRange)) {
            patrol();
        }
        if (isPlayerInRange(chasePlayerRange) && !isPlayerInRange(attackPlayerRange))
        {
            chasePlayer();
        }
        if (isPlayerInRange(attackPlayerRange)) {
            attackPlayer();
        }
    }

    private bool isPlayerInRange(float range) {
        return Vector3.Distance(player.position, transform.position) < range;
    }

    private void patrol() {
        // TODO: Revamp, currenlty jerking
        if(!navMeshAgent.isStopped) {
            Vector3 patrolDestination = spawnPosition + new Vector3(Random.Range(-patrolMaxDistance, patrolMaxDistance), 
                                                                    Random.Range(-patrolMaxDistance, patrolMaxDistance), Random.Range(-patrolMaxDistance, patrolMaxDistance));
            navMeshAgent.speed = patrolSpeed;
            navMeshAgent.SetDestination(patrolDestination);
            animator.SetTrigger("Idle");
        }
    }

    private void chasePlayer() {
            animator.SetTrigger("RunToPlayer");
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = chaseSpeed;
            navMeshAgent.destination = player.position;
            navMeshAgent.updatePosition = true;
    }

    private void attackPlayer() {
        if (!bodySlamInCooldown) {
            navMeshAgent.updatePosition = false;
            StartCoroutine(bodySlamCooldown());
            animator.SetTrigger("AttackBodySlam");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash ==  Animator.StringToHash("Base Layer.AttackBodySlam")) {
            navMeshAgent.ResetPath();
            navMeshAgent.isStopped = true;
        }
    }

    IEnumerator bodySlamCooldown() {
        bodySlamInCooldown = true;
        navMeshAgent.updatePosition = false;
        
        yield return new WaitForSeconds(3);

        bodySlamInCooldown = false;
    }

    void FixedUpdate()
    {
    }

    void LateUpdate()
    {
        animator.ResetTrigger("AttackBodySlam");
        animator.ResetTrigger("RunToPlayer");
        animator.SetTrigger("Idle");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().damage(1);
        }
    }

    public void damage(int damage)
    {
        healthPoints -= damage;
        healthBarSlider.value = healthPoints;
        if (healthPoints <= 0)
        {
            score.GetComponent<Score>().addToScore(1);
            spawnOnDeath(objectToSpawnOnDeath);
            Destroy(gameObject);
        }
    }

    void spawnOnDeath(GameObject objectToSpawnOnDeath)
    {
        GameObject obj = Instantiate(objectToSpawnOnDeath, transform.position, Quaternion.identity);
        obj.GetComponent<Renderer>().material.color = gameObject.GetComponentInChildren<Renderer>().material.color;
    }
}
