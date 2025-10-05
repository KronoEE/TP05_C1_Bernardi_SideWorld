using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]

public class PlayerDataSO : ScriptableObject
{
    public float velocity = 8f;
    public float jumpForce = 8f;
    public float lengthRayCast = 1f;
}
