using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkScript : MonoBehaviour {

	public TextAsset inkAsset;

	Story _inkStory;


	void Awake() {
		_inkStory = new Story (inkAsset.text);
	}

	KeyCode[] numberKeys = {
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5
	};

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (_inkStory.canContinue) {
				_inkStory.Continue ();
				Debug.Log (_inkStory.currentText);

				for (int i = 0; i < _inkStory.currentChoices.Count; ++i) {
					Debug.Log (i+1 + ": " + _inkStory.currentChoices [i].text);
				}
			}
		} else {
			for (int i = 0; i < _inkStory.currentChoices.Count; ++i) {
				if (Input.GetKeyDown (numberKeys [i])) {
					Debug.Log ("Chose option " + i + 1);
					_inkStory.ChooseChoiceIndex (i);
				}
			}
		}

		if (!_inkStory.canContinue && _inkStory.currentChoices.Count == 0) {
			throw new UnityException ("Not sure how else to end the simulation, so here we are.");
		}
	}
}
