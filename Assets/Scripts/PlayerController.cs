using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sampleDistance;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        agent.speed = moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
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

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GetClickedObject();
        }
    }

    private void GetClickedObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity))
        {
            GameObject target = hit.collider.gameObject;

            if (target.TryGetComponent(out IInteractable interactable))
            {
                EventHandler.OnTargetChangedAction.Invoke(target);
            }
        }
    }
}