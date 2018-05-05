using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogSystem : MonoBehaviour {

    public TextAsset inkFile;
    public RectTransform uiRoot;
    public Text text;
    public Button buttonPrefab;

    private Story _inkStory;
    private List<Button> _buttons;
    private int currentChoice = 0;
    private bool buttonsCreated = false;

	// Use this for initialization
	void Start () {
        _inkStory = new Story(inkFile.text);

        _inkStory.ObserveVariable("action", OnInkUpdate);
        _buttons = new List<Button>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_inkStory.canContinue)
        {
            _inkStory.Continue();
            text.text += _inkStory.currentText;
        }
        else
        {
            if (! buttonsCreated)
            {
                CreateButtons();
            }

            if (_inkStory.currentChoices.Count > 0)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    _inkStory.ChooseChoiceIndex(currentChoice);
                    ResetButtons();
                    currentChoice = 0;
                    text.text = "";
                }

                if (_buttons.Count > 0)
                    _buttons[currentChoice].Select();

                if (Input.GetButtonDown("Up"))
                {
                    currentChoice = Mathf.Max(currentChoice - 1, 0);
                }
                else if (Input.GetButtonDown("Down"))
                {
                    currentChoice = Mathf.Min(currentChoice + 1, _buttons.Count - 1);
                }
            }
            

            
        }
    }

    void OnInkUpdate(string name, object value)
    {
        Debug.Log(string.Format("{0} - {1}", name, value.ToString()));

        _inkStory.variablesState["action"] = -1;
    }

    void ResetButtons()
    {
        if (_buttons.Count > 0)
        {
            foreach (var button in _buttons)
            {
                Destroy(button.gameObject);
            }
            _buttons.Clear();
            buttonsCreated = false;
        }
    }

    void CreateButtons()
    {
        if (_inkStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _inkStory.currentChoices.Count; i++)
            {
                Choice choice = _inkStory.currentChoices[i];

                Button button = Instantiate<Button>(buttonPrefab);
                if (button != null)
                {
                    _buttons.Add(button);
                    button.transform.SetParent(uiRoot);
                    button.GetComponentInChildren<Text>().text = choice.text;
                }
            }
            buttonsCreated = true;
        }
    }
}
