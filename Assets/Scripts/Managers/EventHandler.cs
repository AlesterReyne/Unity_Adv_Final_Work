using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class EventHandler : MonoBehaviour
    {
        private static UnityEvent<GameObject> _onTargetChanged;
        public static UnityAction<GameObject> OnTargetChangedAction;

        private void Start()
        {
            if (_onTargetChanged == null)
            {
                _onTargetChanged = new UnityEvent<GameObject>();
            }

            _onTargetChanged.AddListener(OnTargetChangedAction);
        }
    }
}