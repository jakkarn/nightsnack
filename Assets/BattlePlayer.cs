﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayer : MonoBehaviour {

    public int charisma     = 0;
    public int friendThreshold = 10;
    public int enemyThreshold = -10;
    public Text charismaMeter;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		if (charismaMeter != null)
        	charismaMeter.text = charisma.ToString();

        if(charisma < enemyThreshold)
        {
            Global.charisma += charisma;
            Application.LoadLevel("Overworld");

        }
        else if(charisma > friendThreshold)
        {
            Global.numFriendsMade++;
            Global.charisma += charisma;
            Application.LoadLevel("Overworld"); 
        }
	}
}
