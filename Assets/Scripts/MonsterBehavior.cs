using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public float movementSpeed;
    public Color color;
    public int maxHealthPoints;
    public Slider healthBarSlider;

    private GameObject player;
    private int healthPoints;
    private GameObject score;
    private Vector3 moveDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("Score");
        color = ColorManager.pickColor(Random.Range(0,ColorManager.colors.Length));
        gameObject.GetComponentInChildren<Renderer>().material.color = color;

        healthPoints = maxHealthPoints;
        healthBarSlider.maxValue = maxHealthPoints;
        healthBarSlider.value = healthPoints;
    }

    void Update()
    {
        moveDirection = (player.transform.position - transform.position);
        moveDirection.y = 0;
    }
    void FixedUpdate()
    {
        rb.velocity = moveDirection.normalized * movementSpeed;
        Vector3 lookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        gameObject.transform.LookAt(lookAt);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().damage(1);
        }
    }

    public void damage(int damage) {
        healthPoints -= damage;
        healthBarSlider.value = healthPoints;
        if (healthPoints <= 0) {
            Destroy(gameObject);
            score.GetComponent<Score>().addToScore(1);
        }
    }
}
