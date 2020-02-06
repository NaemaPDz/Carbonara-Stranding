using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{   
    public void StartGame()
    {
        SceneManager.LoadScene("IntroCutscene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
