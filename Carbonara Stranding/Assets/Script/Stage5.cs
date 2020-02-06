using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage5 : MonoBehaviour
{
    // Component Variable
    public GameSystem systems;
    public Canvas PauseMenu;
    public Canvas FinishedMenu;
    public TextMeshProUGUI door_health_text;
    public TextMeshProUGUI time_text;

    // Public Variable
    public float door_hp = 70f;
    public float times = 120f;
    public float door_breaking_rate = 0.5f;

    // Main Method
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
        systems = FindObjectOfType<GameSystem>();
        systems.SetCanvas(PauseMenu, FinishedMenu);
        systems.SetDoorHealth(door_hp);
        systems.SetTime(times);
        systems.SetDoorBreakingRate(door_breaking_rate);
        systems.SetDefaultGameStatus();
    }

    void Update()
    {
        systems.PauseGame();
        systems.DoorBreaking();
        systems.TimeCountdown();
        SetDoorHPText();
        systems.CheckGameOver();
        SetTimeText();
    }

    // UI Text Set Method
    public void SetDoorHPText()
    {
        string door_hp = systems.GetDoorHP().ToString("00.00");
        door_health_text.SetText(door_hp + "%");
    }

    public void SetTimeText()
    {
        time_text.SetText(systems.GetMinute() + ":" + systems.GetSecond());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            systems.SetEndGame();
            SceneManager.LoadScene("EndingCutscene");
        }
    }
}
