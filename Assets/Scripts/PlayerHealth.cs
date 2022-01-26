using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthPoints;
    public int healthPoints;
    public Image healthBar;

    public float invincibleDuration;
    private bool isInvincible;

    void Start()
    {
        healthBar.fillAmount = 1;
        Debug.Log("healthBar.fillAmount : " + healthBar.fillAmount);
        invincibleDuration = 0.5f;   
    }

    public void damage(int damage) {
        if (isInvincible) return;

        healthPoints -= damage;
        healthBar.fillAmount = (float)healthPoints / (float)maxHealthPoints;
        Debug.Log("healthBar.fillAmount damaged : " + healthBar.fillAmount);
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
