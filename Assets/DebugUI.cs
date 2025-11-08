using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DebugUI: MonoBehaviour
{
    [SerializeField] PlayerMovement  playerMovement;
    [SerializeField] SwordManager    playerSword;
    [SerializeField] TextMeshProUGUI playerX;
    [SerializeField] TextMeshProUGUI playerY;
    [SerializeField] TextMeshProUGUI startingpos;
    [SerializeField] TextMeshProUGUI targetpos;
    [SerializeField] TextMeshProUGUI angle;
    [SerializeField] TextMeshProUGUI currentRotation;
    [SerializeField] TextMeshProUGUI currentSetRotation;
    void Start()
    {
        
    }

    void Update()
    {
        PlayerDebug();
        SwordDebug();
    }

    void PlayerDebug()
    {
        playerX.text = "Last Player X : " + playerMovement.lastHorizontalVector;
        playerY.text = "Last Player Y : " + playerMovement.lastVerticalVector;
    }

    void SwordDebug()
    {
        currentRotation.text = "Current Rotation : "  + playerSword.transform.rotation.eulerAngles;
        currentSetRotation.text = "Current Set Rotation : "  + playerSword.currentRotation;
        startingpos.text     = "Starting position : " + playerSword.startingPosition;
        targetpos.text       = "Target position : "   + playerSword.targetPosition;
        angle.text           = " Sword Angle : "      + playerSword.angle;
    }
}
