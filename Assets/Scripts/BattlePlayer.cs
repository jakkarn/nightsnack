using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlePlayer : MonoBehaviour {

    public int charisma     = 0;
    public int friendThreshold = 10;
    public int enemyThreshold = -10;
    public Text charismaMeter;
	
	// Update is called once per frame
	void Update () {
		if (charismaMeter != null)
        	charismaMeter.text = charisma.ToString();

        if(charisma <= enemyThreshold)
        {
            Global.charisma += charisma;
            Global.numFriendsMade = Global.numFriendsMade >= 1 ? Global.numFriendsMade - 1 : Global.numFriendsMade;
			SceneManager.LoadScene("Overworld");

        }
        else if(charisma >= friendThreshold)
        {
            Global.numFriendsMade++;
            Global.charisma += charisma;
            SceneManager.LoadScene("Overworld"); 
        }
	}
}
