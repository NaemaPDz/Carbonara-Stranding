using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // Private Variable
    float door_breaking_rate;
    float door_health;
    float time_left;
    string minutes;
    string seconds;
    bool game_hasEnded = false;
    bool game_pausing;
    bool game_finishing;
    bool exit_door_open;
    Canvas PauseMenu;
    Canvas FinishedMenu;

    // Set Method
    public void SetDoorHealth(float door_hp) { door_health = door_hp; }
    public void SetDoorBreakingRate(float door_breaking) { door_breaking_rate = door_breaking; }
    public void SetTime(float times) { time_left = times; }
    public void SetDefaultGameStatus() 
    { 
        game_finishing = false; 
        exit_door_open = false;
        PauseMenu.enabled = false;
        Time.timeScale = 1f;
        FinishedMenu.enabled = false;
    }

    public void SetCanvas(Canvas pause, Canvas finished)
    {
        PauseMenu = pause;
        FinishedMenu = finished;
    }
    public void SetEndGame() 
    { 
        game_finishing = true;
        exit_door_open = true;
    }

    // Get Method
    public bool GetPauseStatus() { return game_pausing; }
    public bool GetExitDoorOpen() { return exit_door_open; }
    public float GetDoorHP() { return door_health; }
    public string GetMinute() { return minutes; }
    public string GetSecond() { return seconds; }

    // Time Countdown Method
    public void TimeCountdown()
    {
        if (!game_finishing)
        {
            minutes = Mathf.Floor(time_left / 60).ToString("00");
            seconds = (time_left % 60).ToString("00");
            time_left -= Time.deltaTime;
        }
    }

    // Door Health Method
    public void RepairDoorHealth(float repair_amout)
    {
        door_health += repair_amout;
    }

    public void DoorBreaking()
    {
        if (!exit_door_open)
        {
            door_health -= (Time.deltaTime * door_breaking_rate);
        }
    }

    // Check Game Over Method
    public void CheckGameOver()
    {
        if (time_left <= 0f || door_health <= 0f)
        {
            game_hasEnded = true;
            time_left = 0f;
            door_health = 0f;
            if (game_hasEnded)
            {
                GameOver();
            }
        }
    }

    public void Exit(string stage_name)
    {
        if (exit_door_open)
        {
            print("test");
            FinishedMenu.enabled = true;
            game_finishing = true;

           StartCoroutine(NewStage(stage_name));
        }
    }

    IEnumerator NewStage(string stage_name)
    {
        yield return new WaitForSeconds(3);
        NextStage(stage_name);
    }

    public void CheckWin(GameObject exit_door)
    {
        if (door_health >= 100f)
        {
            door_health = 100f;
            exit_door_open = true;
            Destroy(exit_door);
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("DeathCutscene");
    }

    void NextStage(string stage_name)
    {
        SceneManager.LoadScene(stage_name);
    }

    // Pause Game Method
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            game_pausing = !game_pausing;
        }

        if (game_pausing)
        {
            Time.timeScale = 0f;
            PauseMenu.enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            PauseMenu.enabled = false;
        }
    }
    
    
}
