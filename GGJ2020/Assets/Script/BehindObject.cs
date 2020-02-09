using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindObject : MonoBehaviour
{
    // Component Variable
    public SpriteRenderer objects;
    public GameObject player;
    public GameObject obj;
    public Player1Control p;

    // Main Method
    void Start()
    {
        p = FindObjectOfType<Player1Control>();
        objects = GetComponent<SpriteRenderer>();
    }

    // Unity Method
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (obj.transform.position.y < player.transform.position.y)
            {
                objects.color = new Color(1, 1, 1, 0.65f);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.2f);
            }
        }
            
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && p.GetState() == "Ready")
        {
            objects.color = new Color(1, 1, 1, 1);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.2f);
        }
        else if (collision.CompareTag("Player"))
        {
            objects.color = new Color(1, 1, 1, 1);
        }
    }
}
