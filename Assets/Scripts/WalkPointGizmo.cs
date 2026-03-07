using UnityEngine;

public class WalkPointGizmo : MonoBehaviour
{
    [SerializeField] private float radus = 0.5f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radus);
    }
#endif
}