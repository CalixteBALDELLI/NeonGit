using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public MapData         mapData;
    public TextMeshProUGUI buttonText;
    Camera           playerCamera;
    
    //public TextMeshProUGUI buttonText;
    private void Start()
    {
        buttonText.text = mapData.mapName;
        playerCamera = Camera.main;
    }
    public void LoadScene()
    {
        PlayerStats.SINGLETON.teleporterKeyObtained = false;
        Timer.SINGLETON.objectiveMessage.text = "Survivez";
        SceneManager.LoadScene(mapData.mapId);
        playerCamera.fieldOfView                    = mapData.cameraFOV;
        PlayerStats.SINGLETON.teleporterKeyObtained = false;
    }
}
