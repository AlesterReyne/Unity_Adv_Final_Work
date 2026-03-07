using UnityEngine;

namespace SO_Scripts
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        [SerializeField] public int maxHealth;
        [SerializeField] private int strength;
        [SerializeField] private int dexterity;


        // [SerializeField] private int constitution;
        // [SerializeField] private int intelligence;
        // [SerializeField] private int wisdom;
        // [SerializeField] private int charisma;
    }
}