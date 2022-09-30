using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScripts : MonoBehaviour
{
    public void Ply_btn()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit_btn()
    {
        Application.Quit();
    }
}
