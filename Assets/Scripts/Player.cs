using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float attention = 0f;
    public float passiveAttentionGain = 0.01f;
    public float attentionModifier = 0f;
    public Text attentionText;
    	
	void Update () {
        attention += passiveAttentionGain;
        if (attention >= 100f)
        {
            attention = 0f;
        }
        attentionText.text = attention.ToString();

	}
}
