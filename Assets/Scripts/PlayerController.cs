using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sampleDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        agent.speed = moveSpeed;
    }

    private void OnMove()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, sampleDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
            }
            else
                Debug.Log(
                    $"Ray hit {hit.collider.name} at {hit.point}, but no NavMesh was found within {sampleDistance} units.");
        }
        else Debug.Log("Raycast didn't hit anything on the ground layer.");
    }
}