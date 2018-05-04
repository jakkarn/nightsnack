using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogSystem : MonoBehaviour {

    public TextAsset _inkFile;
    public RectTransform _uiRoot;
    private Story _inkStory;

    private Button[] _buttons;

    private int currentChoice = 0;

	// Use this for initialization
	void Start () {
        _inkStory = new Story(_inkFile.text);

        _inkStory.ObserveVariable("action", OnInkUpdate);

        _buttons = _uiRoot.GetComponentsInChildren<Button>();
        
	}
	
	// Update is called once per frame
	void Update () {
        _buttons[currentChoice].Select();

        if (Input.GetButtonDown("Up"))
        {
            currentChoice = Mathf.Max(currentChoice - 1, 0);
        }
        else if(Input.GetButtonDown("Down"))
        {
            currentChoice = Mathf.Min(currentChoice + 1, _buttons.Length - 1);
        }
    }

    void OnInkUpdate(string name, object value)
    {
        Debug.Log(string.Format("{0} - {1}", name, value.ToString()));

        _inkStory.variablesState["action"] = -1;
    }
}
