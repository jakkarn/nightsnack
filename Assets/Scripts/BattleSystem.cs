using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class BattleSystem : MonoBehaviour {

	public TextAsset[] greetingFiles = new TextAsset[System.Enum.GetValues (typeof(EnemyType)).Length];
	List<string>[] greetings;

	public TextAsset[] questionFiles = new TextAsset[System.Enum.GetValues(typeof(EnemyType)).Length];
	List<Question>[] questions = new List<Question>[System.Enum.GetValues(typeof(EnemyType)).Length];
    private Answer[] answers;   //currently shown answers


    public RectTransform uiRoot;
	public Text questionText;
	public Text resposeText;
	public Button buttonPrefab;

	private List<Button> _buttons = new List<Button>();
	private int currentChoice = 0;

    public BattlePlayer player;

	public int MAX_ANSWER_OPTIONS; // Not sure the interface supports too many answers.

	private EnemyType currentEnemyType = EnemyType.NERD; // TODO: get from elsewhere

	// Use this for initialization
	void Start () {
		foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType))) {
			if (questionFiles [(int)type] == null) {
				Debug.Log ("No question file specified for enemy type " + System.Enum.GetName (typeof(EnemyType), type));
				questions [(int)type] = null;
			} else {
				Debug.Log("Enemy type " + System.Enum.GetName (typeof(EnemyType), type) + " uses questions file " + questionFiles [(int)type].name);
				questions [(int)type] = new List<Question> (JsonConvert.DeserializeObject<Question[]> (questionFiles [(int)type].text));
			}

			if (greetingFiles [(int)type] == null) {
				Debug.Log ("No greeting file specified for enemy type " + System.Enum.GetName (typeof(EnemyType), type));
				greetings [(int)type] = new List<string> ();
				greetings [(int)type].Add ("'ello, and what are you after then?");
			} else {
				Debug.Log("Enemy type " + System.Enum.GetName (typeof(EnemyType), type) + " uses greetings file " + greetingFiles [(int)type].name);
				greetings [(int)type] = new List<string> (questionFiles [(int)type].text.Split (new char[] { '\n' }));
			}
		}

		_buttons = new List<Button>();

		CreateButtons ();

		resposeText.text = greetings [(int)currentEnemyType] [Random.Range (0, greetings [(int)currentEnemyType].Count)];
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Submit"))
		{
            player.charisma += answers[currentChoice].effect;
			resposeText.text = answers [currentChoice].response;
			ResetButtons();
			currentChoice = 0;
			questionText.text = "";
			CreateButtons ();
		}

		if (_buttons.Count > 0) {
			_buttons [currentChoice].Select ();
		}

		if (Input.GetButtonDown("Up"))
		{
			currentChoice = Mathf.Max(currentChoice - 1, 0);
		}
		else if (Input.GetButtonDown("Down"))
		{
			currentChoice = Mathf.Min(currentChoice + 1, _buttons.Count - 1);
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
		}
	}

	void CreateButtons()
	{
		List<Question> encounterQuestions = questions[(int)currentEnemyType];

		Question question;
		if (encounterQuestions == null || encounterQuestions.Count == 0) {
			Debug.Log ("Found no questions when creating buttons. Creating a dummy question.");
			question = new Question ();
			Answer a = new Answer ();
			a.effect = 0;
			a.response = "NO RESPONSE!!!";
			a.text = "NO ANSWER!!!";
			question.question = "NO QUESTION FOUND!!!";
			question.answers = new Answer[] { a };
		} else {
			question = encounterQuestions [Random.Range (0, encounterQuestions.Count)];
		}
        questionText.text = question.question;
        answers = question.answers;

		// Shuffle the list so the options are presented in a rondom order.
		for (int n = answers.Length - 1; n > 0; --n)
		{
			int k = Random.Range (0, n+1);
			Answer tmp = answers [k];
			answers [k] = answers [n];
			answers [n] = tmp;
		}

		for (int i = 0; i < answers.Length && i < MAX_ANSWER_OPTIONS; i++)
		{
			Button button = Instantiate<Button>(buttonPrefab);
			if (button != null)
			{
				_buttons.Add(button);
				button.transform.SetParent(uiRoot);
				button.GetComponentInChildren<Text> ().text = answers[i].text;
			}
		}
	}

	public void SetActiveButton(Button btn)
	{
		for (int i = 0; i < _buttons.Count; ++i) {
			if (btn == _buttons [i]) {
				currentChoice = i;
				return;
			}
		}
	}

}


public enum EnemyType {
	NERD, COOL_KID, GOTH, NONE
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