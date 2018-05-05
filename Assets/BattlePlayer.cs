using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayer : MonoBehaviour {

    public int karisma     = 0;
    public int friendThreshold = 10;
    public int enemyThreshold = -10;
    public Text karismaMeter;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        karismaMeter.text = karisma.ToString();
	}
}
