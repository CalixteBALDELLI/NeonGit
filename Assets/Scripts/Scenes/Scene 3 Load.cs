using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene3Load : MonoBehaviour
{

    public Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadScene3);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(LoadScene3);
    }
    void LoadScene3()
    {
        Debug.Log("LOAD");
        SceneManager.LoadScene("3");
    }

}
