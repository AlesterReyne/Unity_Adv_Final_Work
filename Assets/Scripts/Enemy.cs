using Interfaces;
using SO_Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IInteractable
{
    [SerializeField] private float currentHealth;
    [SerializeField] private EnemySO enemySo;
    [SerializeField] private GameObject selectedAura;

    private void Start()
    {
        currentHealth = enemySo.maxHealth;
        selectedAura.SetActive(false);

        EventHandler.OnTargetChangedAction += argument => OnTargetHandler(argument);
    }

    public float TakeDamage(float damage)
    {
        currentHealth -= damage;
        return currentHealth;
    }

    public bool IsDead(float health)
    {
        return currentHealth <= 0;
    }

    public void OnTargetHandler(GameObject target)
    {
        if (target == this.gameObject)
        {
            selectedAura.SetActive(true);
        }
        else
        {
            selectedAura.SetActive(false);
        }
    }
}