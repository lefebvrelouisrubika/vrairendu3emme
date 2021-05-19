using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    // Update is called once per frame

    private void Awake()
    {
        Instance = this;

    }
    void Update()
    {
        QuitBuild();
        if (Input.GetKeyDown("r"))
        {
            ResetScene();
        }
    }

    public void QuitBuild()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
    public void ResetScene()
    {
            SceneManager.LoadScene("SceneGameplay");

    }
}
