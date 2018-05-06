using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class BattleSystem : MonoBehaviour {

    private static int numEnemyTypes = System.Enum.GetValues(typeof(EnemyType)).Length;


    public TextAsset[] greetingFiles;
    public TextAsset[] questionFiles;

    List<string>[] greetings = new List<string>[numEnemyTypes];
	List<Question>[] questions = new List<Question>[numEnemyTypes];
    private Answer[] answers;   //currently shown answers


    public RectTransform uiRoot;
	public Text questionText;
	public Text resposeText;
	public Button buttonPrefab;

	private List<Button> _buttons = new List<Button>();
	private int currentChoice = 0;

    public BattlePlayer player;

	public int MAX_ANSWER_OPTIONS; // Not sure the interface supports too many answers.

	private EnemyType currentEnemyType;

	public CharismaMeter charismaMeter;

	// Use this for initialization
	void Start () {
		currentEnemyType = Global.encounter;

		// If there are too many files, that's kinda fine. But if there are too few, we need to know ASAP.
		UnityEngine.Assertions.Assert.IsTrue (questionFiles.Length >= numEnemyTypes);
		UnityEngine.Assertions.Assert.IsTrue (greetingFiles.Length >= numEnemyTypes);
		foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType))) {
			if (questionFiles [(byte)type] == null) {
				Debug.Log ("No question file specified for enemy type " + System.Enum.GetName (typeof(EnemyType), type));
				questions [(byte)type] = null;
			} else {
				Debug.Log("Enemy type " + System.Enum.GetName (typeof(EnemyType), type) + " uses questions file " + questionFiles [(byte)type].name);
				questions [(byte)type] = new List<Question> (JsonConvert.DeserializeObject<Question[]> (questionFiles [(byte)type].text));
			}

            Debug.Log(numEnemyTypes);
            Debug.Log(greetingFiles.Length);

            if (greetingFiles [(byte)type] == null) {
				Debug.Log ("No greeting file specified for enemy type " + System.Enum.GetName (typeof(EnemyType), type));
				greetings [(byte)type] = new List<string> ();
				greetings [(byte)type].Add ("'ello, and what are you after then?");
			} else {
				Debug.Log("Enemy type " + System.Enum.GetName (typeof(EnemyType), type) + " uses greetings file " + greetingFiles [(byte)type].name);
				greetings [(byte)type] = new List<string> (questionFiles [(byte)type].text.Split (new char[] { '\n' }));
			}
		}

		_buttons = new List<Button>();

		CreateButtons ();

		resposeText.text = greetings [(byte)currentEnemyType] [Random.Range (0, greetings [(byte)currentEnemyType].Count)];
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Submit"))
		{
            player.charisma += answers[currentChoice].effect;
			charismaMeter.changeValue (answers[currentChoice].effect);
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
		List<Question> encounterQuestions = questions[(byte)currentEnemyType];

		Question question;
		if (encounterQuestions == null || encounterQuestions.Count == 0) {
			Debug.Log ("Found no questions when creating buttons for type " + currentEnemyType + ". Creating a dummy question.");
			question = new Question ();
			int default_num_answers = 3;
			int lowest_answer = -1;
			question.answers = new Answer[default_num_answers];
			for (int i = 0; i < default_num_answers; ++i) {
				Answer a = new Answer ();
				int effect = i + lowest_answer;
				a.effect = effect;
				string chg;
				if (effect < 0)
					chg = effect.ToString ();
				else
					chg = "+" + effect.ToString ();
				a.response = "NO REPONSE";
				a.text = "NO ANSWER (" + chg + ")";
				question.answers [i] = a;
			}
			question.question = "NO QUESTION FOUND!!!";
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


public enum EnemyType : byte {
	NERD = 0,
	COOL_KID = 1,
	GOTH = 2,
	NONE = 3
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