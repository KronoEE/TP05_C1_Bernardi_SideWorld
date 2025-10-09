using UnityEngine;

[CreateAssetMenu(fileName = "PanelSettings", menuName = "ScriptableObjects/Panels")]

public class PanelDataSO : ScriptableObject
{
    public GameObject panelHealthUI;
    public GameObject panelPauseUI;
    public GameObject panelDeathUI;
}