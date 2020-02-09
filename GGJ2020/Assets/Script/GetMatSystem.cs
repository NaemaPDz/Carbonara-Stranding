using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMatSystem : MonoBehaviour
{
    // Component Variable
    public GameSystem systems;
    public Player1Control player;

    // Public Variable
    public float light_repair = 5f;
    public float medium_repair = 10f;
    public float heavy_repair = 15f;

    // Private Variable


    // Main Method
    void Start()
    {
        systems = FindObjectOfType<GameSystem>();
        player = FindObjectOfType<Player1Control>();
    }

    void Update()
    {
        
    }

    // Door Repairing Method
    void DoorRepairing(float repair_amout, GameObject object_mat)
    {
        FindObjectOfType<AudioManager>().Play("Repair");
        Destroy(object_mat);
        systems.RepairDoorHealth(repair_amout);
        print("Repairing : +" + repair_amout + "%");
    }

    // Unity Method
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LightMat"))
        {
            DoorRepairing(light_repair, collision.gameObject);
        }
        else if (collision.CompareTag("MediumMat"))
        {
            DoorRepairing(medium_repair, collision.gameObject);
        }
        else if (collision.CompareTag("HeavyMat"))
        {
            DoorRepairing(heavy_repair, collision.gameObject);
        }
    }
}
