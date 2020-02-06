using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    // Component Variable
    public GameSystem systems;
    public GameObject exit_door;
    public Canvas PauseMenu;
    public Canvas FinishedMenu;
    public TextMeshProUGUI door_health_text;
    public TextMeshProUGUI time_text;

    // Public Variable
    public float door_hp = 83f;

    // Main Method
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
        systems = GetComponent<GameSystem>();
        systems.SetCanvas(PauseMenu, FinishedMenu);
        systems.SetDoorHealth(door_hp);
        systems.SetDefaultGameStatus(); 
    }

    // Update is called once per frame
    void Update()
    {
        systems.PauseGame();
        SetDoorHPText();
        systems.CheckWin(exit_door);
    }

    // UI Text Set Method
    public void SetDoorHPText()
    {
        string door_hp = systems.GetDoorHP().ToString("00.00");
        door_health_text.SetText(door_hp + "%");
    }

    // Unity Method
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && systems.GetExitDoorOpen())
        {
            systems.Exit("Stage2");
        }
    }
}
