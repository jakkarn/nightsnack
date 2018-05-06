using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningScript : MonoBehaviour
{
    public Collider2D exit;
    private SpriteRenderer bubble;      // speech bubble before winning
    private SpriteRenderer bodyGuard;   // body guard blocking exit

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            bubble.enabled = Global.numFriendsMade <= 0 ? false : true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            bubble = this.GetComponentsInChildren<SpriteRenderer>()[1];
            bubble.enabled = false;
        }
    }

    private void Start()
    {
        /* NOTE: set by order in game-component list */
        bodyGuard = this.GetComponentsInChildren<SpriteRenderer>()[0];
        bubble = this.GetComponentsInChildren<SpriteRenderer>()[1];
        bodyGuard.enabled = true;
        bodyGuard.enabled = false;
    }

    void Update()
    {
        exit.enabled = Global.numFriendsMade <= 0 ? false : true;
        bodyGuard.enabled = Global.numFriendsMade <= 0 ? false : true;
    }
}