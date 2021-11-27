using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthPoints;
    public int healthPoints;
    public Slider healthBarSlider;

    public float invincibleDuration;
    private bool isInvincible;

    void Start()
    {
        maxHealthPoints = 10;
        healthPoints = 10;
        healthBarSlider.maxValue = maxHealthPoints;
        healthBarSlider.value = healthPoints;
        invincibleDuration = 0.5f;   
    }

    public void damage(int damage) {
        if (isInvincible) return;

        //Debug.Log("Player - take damage : HP" + healthPoints + " - damage " + damage);
        healthPoints -= damage;
        healthBarSlider.value = healthPoints;
        if (healthPoints <= 0) {
            SceneManager.LoadScene("Main menu");
        }

        StartCoroutine(invincible(invincibleDuration));
    }

    IEnumerator invincible(float invincibleDuration) {
        isInvincible = true;

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
    }
}
