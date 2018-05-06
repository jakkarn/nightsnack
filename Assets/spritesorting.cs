using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritesorting : MonoBehaviour {

    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /* move sprites behind character */
        if (this.transform.position.y > player.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }
}
