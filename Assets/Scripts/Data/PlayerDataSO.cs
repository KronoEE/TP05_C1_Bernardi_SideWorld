using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]
public class PlayerDataSO : ScriptableObject
{
    public KeyCode KeyCodeJump = KeyCode.Space;
    public KeyCode KeyCodeLeft = KeyCode.A;
    public KeyCode KeyCodeRight = KeyCode.D;
}
