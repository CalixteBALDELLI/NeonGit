using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1Load : MonoBehaviour
{

    public Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadScene1);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(LoadScene1);
    }
    void LoadScene1()
    {
        Debug.Log("LOAD");
        SceneManager.LoadScene("1");
    }

}
