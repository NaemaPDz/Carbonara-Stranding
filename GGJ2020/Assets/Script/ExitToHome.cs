using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToHome : MonoBehaviour
{
    public void ExitToMenu()
    {
        SceneManager.LoadScene("HomeMenu");
    }
}
