using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player1Control : MonoBehaviour
{
    // Component Variable
    public Transform player_handle;
    public GameSystem systems;
    public Rigidbody2D player_rb;
    public Animator anim;

    // Public Variable
    public float default_movement_speed = 5f;
    public KeyCode interact_key = KeyCode.E;

    // Private Variable
    string[] state = { "Ready", "LightCarry", "MediumCarry", "HeavyCarry" };
    string current_state;
    float horizontal_movement;
    float vertical_movement;
    float movement_speed;
    Collider2D collision_object;
    string col_tag;
    Vector3 movement;
    Vector3 angle_rotation;
    bool is_walk;

    // Main Method
    void Start()
    {
        systems = FindObjectOfType<GameSystem>();
        SetState(state[0]);
        is_walk = false;
    }

    void Update()
    {
        Movement();
        Interact();

        if (player_handle.childCount <= 0)
        {
            SetState(state[0]);
            movement_speed = default_movement_speed;
        }
    }

    // State Set Method
    public void SetState(string state)
    {
        current_state = state; 
    }

    // Get State Method
    public string GetState()
    {
        return current_state;
    }

    // Movement Method
    void Movement()
    {
        horizontal_movement = Input.GetAxis("Horizontal");
        vertical_movement = Input.GetAxis("Vertical");

        movement = new Vector2(horizontal_movement, vertical_movement);

        player_rb.velocity = movement * movement_speed;

        RunAnimation();
        if((horizontal_movement != 0 || vertical_movement != 0) && !is_walk)
        {
            FindObjectOfType<AudioManager>().Play("Walk");
            is_walk = true;
        }
        else if (horizontal_movement == 0 && vertical_movement == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Walk");
            is_walk = false;
        }
    }

    // Animation Method
    void RunAnimation()
    {
        anim.SetFloat("SpeedX", horizontal_movement);
        anim.SetFloat("SpeedY", vertical_movement);
        anim.SetFloat("Magnitude", movement.magnitude);
    }



    // Interact Method
    void Interact()
    {
        if (Input.GetKeyDown(interact_key) && !systems.GetPauseStatus())
        {
            if (current_state == state[0] && collision_object != null)
            {
                if (collision_object.CompareTag("LightMat"))
                {
                    CarryItems(default_movement_speed * 0.75f, collision_object, state[1]);
                }
                else if (collision_object.CompareTag("MediumMat"))
                {
                    CarryItems(default_movement_speed *0.6f, collision_object, state[2]);
                }
                else if (collision_object.CompareTag("HeavyMat"))
                {
                    CarryItems(default_movement_speed * 0.3f, collision_object, state[3]);
                }
            }
            else if (current_state != state[0])
            {
                PutItems();
            }
        }
    }

    void CarryItems(float speed, Collider2D collision, string state)
    {
        movement_speed = speed;
        collision.transform.SetParent(player_handle);
        SetState(state);
        print("Carrying : " + collision_object.tag);
        FindObjectOfType<AudioManager>().Play("Interact");
    }

    void PutItems()
    {
        movement_speed = default_movement_speed;
        print("Putting : " + player_handle.GetChild(0).tag);
        player_handle.DetachChildren();
        current_state = state[0];
        FindObjectOfType<AudioManager>().Play("Interact");
    }

    // Unity Method
    void OnTriggerEnter2D(Collider2D collision)
    {
        col_tag = collision.tag;
        print(col_tag);
        if (col_tag == "LightMat" || col_tag == "MediumMat" || col_tag == "HeavyMat")
        {
            collision_object = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision_object = null;
    }
}
