using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlePlayer : MonoBehaviour {

    public int charisma     = 0;
    
    public Text charismaMeter;
    private int friendThreshold = 5;
    private int enemyThreshold = -5;

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
