using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer, playerLayer;

    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;
    private bool _walkPointSet;

    [SerializeField] private float timeBetweenAttacks;
    private bool _alreadyAttacked;

    [SerializeField] private float sightRange, attackRange;
    private bool _playerInSightRange, _playerInAttackRange;
    private EnemyState _enemyState;

    [SerializeField] private Transform[] walkPoints;
    private bool _isArived;
    private int _pointIndex;
    [SerializeField] FieldOfView fieldOfView;

    private void Start()
    {
        _isArived = true;
        _pointIndex = -1;
        _enemyState = EnemyState.Patrolling;
        StartCoroutine(StageRoutine());
    }

    private IEnumerator StageRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (true)
        {
            yield return wait;
            if (_enemyState == EnemyState.Patrolling && fieldOfView.canSeePlayer)
            {
                _enemyState = EnemyState.Chasing;
            }

            EnemyStageHandler();
        }
    }

    private void EnemyStageHandler()
    {
        switch (_enemyState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                agent.SetDestination(player.position);

                break;
            case EnemyState.Attacking:
                // logic
                break;
        }
    }

    private void Patrol()
    {
        if (_isArived)
        {
            _pointIndex = (_pointIndex + 1) % walkPoints.Length;
            agent.SetDestination(walkPoints[_pointIndex].position);
            _isArived = false;
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoints[_pointIndex].position;
        _isArived = distanceToWalkPoint.magnitude < 1f;
    }
}