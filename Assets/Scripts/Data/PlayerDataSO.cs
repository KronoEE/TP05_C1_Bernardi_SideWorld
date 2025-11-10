using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]

public class PlayerDataSO : ScriptableObject
{
    public float velocity = 6f;
    public float reboundForce = 10f;
    public float lengthRayCast = 1f;
    public int maxHealth = 3;
    public int maxJumpForce = 10;
    public int jumpForceToAdd = 3;
    public float jumpBoostDuration = 5.5f;
}
