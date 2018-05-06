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
		char[] chars = new char[] { '-', '±', '+' };
		Color[] colors = new Color[] { Color.red, Color.white, Color.cyan };

		Debug.Log (delta);

		string writeme = chars[1+(int)Mathf.Sign(delta)] + ((int)Mathf.Abs(delta)).ToString();
		Debug.Log (writeme);
		Color textcol = colors[1+(int)Mathf.Sign(delta)];

		charismaChange.color = textcol;
		charismaChange.text = writeme;

		expectedAlpha = 255;
	}

	private float expectedAlpha;

	void Update() {
		Color prevCol = charismaChange.color;

		float fadeTime = 3;
		float fadePerSecond = 255 / fadeTime;
		float fadeThisFrame = Time.deltaTime * fadePerSecond;
		expectedAlpha -= fadeThisFrame;

		if (expectedAlpha <= 0) {
			// Text has faded - deactivate.
			charismaChange.text = "";
		} else {
			int alpha = (int)expectedAlpha;
			Color newCol = new Color (prevCol.r, prevCol.g, prevCol.b, alpha);
			charismaChange.color = newCol;
		}
	}
}