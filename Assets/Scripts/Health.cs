using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public int CurrentHealth { get; private set; }
    public NetworkVariable<int> HealthAmount = new NetworkVariable<int>();
    private Slider healthBar;
    public int maxHealth = 100;
    private bool isColliding;

    private void OnEnable()
    {
        if (CompareTag("Player"))
            healthBar = GameObject.FindWithTag("Health").GetComponent<Slider>();
        else
            healthBar = GetComponentInChildren<Slider>();
        CurrentHealth = maxHealth;
        if(IsOwner)
            HealthAmount.Value = CurrentHealth;
        healthBar.value = CurrentHealth;
        if (CompareTag("Enemy") || CompareTag("Breakable")) healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(int damageToTake)
    {
        if ((CompareTag("Enemy") || CompareTag("Breakable")) && !healthBar.gameObject.activeInHierarchy) healthBar.gameObject.SetActive(true);

        CurrentHealth -= damageToTake;
        if (IsServer)
            HealthAmount.Value = CurrentHealth;
        healthBar.value = CurrentHealth;

        if (CurrentHealth <= 0 && CompareTag("Player"))
        {
            if(MainManager.players == MainManager.Players.Solo)
                Destroy(gameObject);
            else if (IsOwner)
                NetworkManager.Destroy(gameObject);
            
        }
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
        if (IsOwner)
            HealthAmount.Value = CurrentHealth;
        healthBar.value = CurrentHealth;
    }

    public void IncreaseMaxHP()
    {
        maxHealth = maxHealth + (int)(maxHealth * 0.1f);
        healthBar.maxValue = maxHealth;
        CurrentHealth = maxHealth;
        if (IsOwner)
            HealthAmount.Value = CurrentHealth;
        healthBar.value = CurrentHealth;
        Time.timeScale = 1f;
    }

}
