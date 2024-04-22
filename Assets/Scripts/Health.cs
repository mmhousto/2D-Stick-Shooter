using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    public Slider healthBar;
    private int maxHealth = 100;
    private bool isColliding;

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
        healthBar.value = CurrentHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        CurrentHealth -= damageToTake;
        healthBar.value = CurrentHealth;

        if (CurrentHealth <= 0 && CompareTag("Player")) Destroy(gameObject);
        else if (CurrentHealth <= 0 && CompareTag("Enemy")) EnemyPool.instance.enemyPool.Release(GetComponent<EnemyAI>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isColliding = true;
            StartCoroutine(DealDamage());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
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

}
