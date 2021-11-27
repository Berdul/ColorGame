using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public Color color;
    public int healthPoints;
    public int maxHealthPoints;
    public Slider healthBarSlider;
    private GameObject score;
    private Vector3 runDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("Score");
        color = ColorManager.pickColor(Random.Range(0,5));
        gameObject.GetComponent<Renderer>().material.color = color;

        maxHealthPoints = 4;
        healthPoints = maxHealthPoints;
        healthBarSlider.maxValue = maxHealthPoints;
        healthBarSlider.value = healthPoints;
    }

    void FixedUpdate()
    {
        runDirection = player.transform.position - transform.position;
        runDirection.Normalize();
        transform.position += runDirection * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        gameObject.transform.LookAt(player.transform);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().damage(1);
        }
    }

    public void damage(int damage) {
        Debug.Log("MonsterBehavior - take damage : HP" + healthPoints + " - damage " + damage);
        healthPoints -= damage;
        healthBarSlider.value = healthPoints;
        if (healthPoints <= 0) {
            Destroy(gameObject);
            score.GetComponent<Score>().addToScore(1);
        }
    }
}
