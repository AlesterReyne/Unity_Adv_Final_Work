using Data;
using Interfaces;
using Managers;
using SO_Scripts;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IInteractable
{
    [SerializeField] private float currentHealth;

    [SerializeField] private GameObject selectedAura;
    [SerializeField] private GameObject player;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private EnemySO enemySo;
    [SerializeField] private EnemyType enemyType;

    private void Start()
    {
        currentHealth = enemySo.maxHealth;
        selectedAura.SetActive(false);

        EventHandler.OnTargetChangedAction += argument => OnTargetHandler(argument);
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

    private void Attack()
    {
        switch (enemyType)
        {
            case EnemyType.Melee:
                agent.baseOffset = 1;
                agent.SetDestination(player.transform.position);
                break;
            case EnemyType.Range:
                agent.baseOffset = 10;
                agent.SetDestination(player.transform.position);
                break;
            case EnemyType.Suicide:
                agent.SetDestination(player.transform.position);
                break;
        }
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
}