using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy")]

public class EnemyDataSO : ScriptableObject
{
    public float detectionRadius = 5f;
    public float speed = 2f;
}