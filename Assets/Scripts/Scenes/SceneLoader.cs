using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void OnDestroy()
    {
        
    }
    public void LoadScene()
    {
        Debug.Log("LOAD");
        DontDestroyOnLoad(GameObject.Find("Player"));
        DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene(mapData.mapId);
        playerCamera.fieldOfView = mapData.cameraFOV;
    }

}
