using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {

	public TextAsset NerdInsults;
	public TextAsset NerdCompliments;
	public TextAsset NerdComments;

	public RectTransform uiRoot;
	public Text text;
	public Button buttonPrefab;

	private List<Button> _buttons;
	private int currentChoice = 0;
	private bool buttonsCreated = false;


	private EnemyType currentEnemyType = EnemyType.NERD;

	public int numberOfChoices = 3;

	// Use this for initialization
	void Start () {
		_buttons = new List<Button>();
		text.text = "Choose a line"; // TODO: Nåt bättre
	}

	// Update is called once per frame
	void Update () {
		{
			if (! buttonsCreated)
			{
				CreateButtons();
			}

			if (numberOfChoices > 0) // Always true, but cba removing the if
			{
				if (Input.GetButtonDown("Submit"))
				{
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
		TextAsset[] lists = new TextAsset[3];
		TextAsset insultList;
		TextAsset complimentList;
		TextAsset commentList;
		switch (currentEnemyType) {
		case EnemyType.NERD:
			lists [0] = insultList = NerdInsults;
			lists [1] = complimentList = NerdCompliments;
			lists [2] = commentList = NerdComments;
			break;
		default:
			throw new UnityException ("fuq");
		}

		// Shuffle the list so the options are presented in a rondom order.
		for (int n = lists.Length - 1; n > 0; --n)
		{
			int k = Random.Range (0, n+1);
			TextAsset tmp = lists [k];
			lists [k] = lists [n];
			lists [n] = tmp;
		}

		for (int i = 0; i < numberOfChoices; i++)
		{
			Button button = Instantiate<Button>(buttonPrefab);
			if (button != null)
			{
				_buttons.Add(button);
				button.transform.SetParent(uiRoot);
				button.GetComponentInChildren<Text> ().text = GetRandomLine(lists[i]); // TODO: Set from file?
			}
		}
		buttonsCreated = true;
	}

	string GetRandomLine(TextAsset ta)
	{
		string[] lines = ta.text.Split (new char[] { '\n' });
		return lines [Random.Range (0, lines.Length - 1)];
	}
}


public enum EnemyType {
	NERD ,COOL_KID, GOTH
}
