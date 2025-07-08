using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    public Slider healthBar;
    public int maxHealth = 100;
    private bool isColliding;

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
        healthBar.value = CurrentHealth;
        if (CompareTag("Enemy") || CompareTag("Breakable")) healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(int damageToTake)
    {
        if ((CompareTag("Enemy") || CompareTag("Breakable")) && !healthBar.gameObject.activeInHierarchy) healthBar.gameObject.SetActive(true);

        CurrentHealth -= damageToTake;
        healthBar.value = CurrentHealth;

        if (CurrentHealth <= 0 && CompareTag("Player")) Destroy(gameObject);
        else if (CurrentHealth <= 0 && CompareTag("Enemy"))
        {
            XPManager.IncreaseXP(10);
            EnemyPool.instance.enemyPool.Release(GetComponent<EnemyAI>());
        }
        else if (CurrentHealth <= 0 && CompareTag("Breakable"))
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = true;
            StartCoroutine(DealDamage());
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }

    IEnumerator DealDamage()
    {
        while (isColliding)
        {
            TakeDamage(5);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GetFullHP()
    {
        CurrentHealth = maxHealth;
        healthBar.value = CurrentHealth;
    }

    public void IncreaseMaxHP()
    {
        maxHealth = maxHealth + (int)(maxHealth * 0.1f);
        healthBar.maxValue = maxHealth;
        CurrentHealth = maxHealth;
        healthBar.value = CurrentHealth;
        Time.timeScale = 1f;
    }

}
