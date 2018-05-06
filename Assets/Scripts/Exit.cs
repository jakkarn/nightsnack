using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Collider2D exit;
    private SpriteRenderer bubble;      // speech bubble before winning
    private TextMesh bubbleText;
    private SpriteRenderer bodyGuard;   // body guard blocking exit
    private string textString = "Some friend wanted\nto see you!";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            bubble.enabled = Global.numFriendsMade <= 0 ? false : true;
            bubbleText.text = Global.numFriendsMade <= 0 ? "" : textString;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            bubble.enabled = false;
            bubbleText.text = "";
        }
    }

    private void Start()
    {
        /* NOTE: set by order in game-component list */
        bodyGuard   = this.GetComponentsInChildren<SpriteRenderer>()[0];
        bubble      = this.GetComponentsInChildren<SpriteRenderer>()[1];
        bubbleText  = this.GetComponentInChildren<TextMesh>();
        bodyGuard.enabled   = true;
        bubble.enabled      = false;
        bubbleText.text     = "";
    }

    void Update()
    {
        exit.enabled = Global.numFriendsMade <= 0 ? false : true;
        bodyGuard.enabled = Global.numFriendsMade <= 0 ? false : true;
    }
}