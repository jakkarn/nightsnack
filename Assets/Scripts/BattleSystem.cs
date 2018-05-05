using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class BattleSystem : MonoBehaviour {

	public TextAsset[] _questions = new TextAsset[1]; //all questions files 0=nerd
    Question[] nerd_questions;

	public RectTransform uiRoot;
	public Text text;
	public Button buttonPrefab;

	private List<Button> _buttons;
	private int currentChoice = 0;
	private bool buttonsCreated = false;


	private EnemyType currentEnemyType = EnemyType.NERD;

	// Use this for initialization
	void Start () {

        nerd_questions = JsonConvert.DeserializeObject<Question[]>(_questions[0].text);

		_buttons = new List<Button>();
	}

	// Update is called once per frame
	void Update () {
		{
			if (! buttonsCreated)
			{
				CreateButtons();
			}

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
		Question[] questions;
        Answer[]  answers;

		switch (currentEnemyType) {
		case EnemyType.NERD:
                questions = nerd_questions;
			break;
		default:
			throw new UnityException ("fuq");
		}

        Question question = questions[Random.Range(0, questions.Length)];
        text.text = question.question; // TODO: Nåt bättre
        answers = question.answers;

		// Shuffle the list so the options are presented in a rondom order.
		for (int n = answers.Length - 1; n > 0; --n)
		{
			int k = Random.Range (0, n+1);
			Answer tmp = answers [k];
			answers [k] = answers [n];
			answers [n] = tmp;
		}

		for (int i = 0; i < answers.Length; i++)
		{
			Button button = Instantiate<Button>(buttonPrefab);
			if (button != null)
			{
				_buttons.Add(button);
				button.transform.SetParent(uiRoot);
				button.GetComponentInChildren<Text> ().text = answers[i].text;
			}
		}
		buttonsCreated = true;
	}

}


public enum EnemyType {
	NERD ,COOL_KID, GOTH
}

public class Answer
{
    public string text { get; set; }
    public int effect { get; set; }
    public string response { get; set; }
}
public class Question
{
    public string question { get; set; }
    public Answer[] answers { get; set; }

}