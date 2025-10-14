using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1Load : MonoBehaviour
{

    public MapData mapData;
    public Button button;
    public TextMeshProUGUI buttonText;
    private void Start()
    {
        button.onClick.AddListener(LoadScene1);
        buttonText.text = mapData.mapName;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(LoadScene1);
    }
    void LoadScene1()
    {
        Debug.Log("LOAD");
        SceneManager.LoadScene(mapData.mapId);
    }

}
