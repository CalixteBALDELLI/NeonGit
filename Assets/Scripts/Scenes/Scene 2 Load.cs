using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene2Load : MonoBehaviour
{

    public Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadScene2);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(LoadScene2);
    }
    void LoadScene2()
    {
        Debug.Log("LOAD2");
        SceneManager.LoadScene("2");
    }

}
