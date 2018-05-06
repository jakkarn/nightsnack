using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class CharismaMeter : MonoBehaviour {
	
	private Slider slider;
	public Text charismaChange;

	// Use this for initialization
	void Start () {
		slider = GetComponentInParent<Slider> ();
		Assert.IsNotNull (slider);
		Assert.IsNotNull (charismaChange);
		

		slider.value = 0;

	}

	public void changeValue(int delta) {
		slider.value += delta;

		Debug.Log (delta);

		Color color;
		string writeme;
		if (delta > 0) {
			writeme = "+";
			color = Color.cyan;
		} else if (delta == 0) {
			writeme = "±";
			color = Color.white;
		} else {
			writeme = "-";
			color = Color.red;
		}
		writeme += ((int)Mathf.Abs (delta)).ToString ();

		charismaChange.color = color;
		charismaChange.text = writeme;
	}

	void Update() {
		if (charismaChange.color.a <= 0)
			return;

		float fadeTime = 3;
		float fadeThisFrame = Time.deltaTime / fadeTime;

		Color old = charismaChange.color;
		charismaChange.color = new Color (old.r, old.g, old.b, old.a - fadeThisFrame);
	}
}