using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntPropertyController : PropertyController {
	public Slider slider;

	public void OnValueChanged() {
		try {
			//Parse options string
			float min = float.Parse(Options.Split(new char[] { ':' })[0]);
			float max = float.Parse(Options.Split(new char[] { ':' })[1]);
			float increment = float.Parse(Options.Split(new char[] { ':' })[2]);

			//Get value from 0-1 slider
			float value = Mathf.Lerp(min, max, slider.value);

			//Round to nearest increment
			value = Mathf.Round(value / increment) * increment;

			//Set slider to rounded value
			slider.value = Mathf.InverseLerp(min, max, value);

			//Set value of property
			Property.SetValue(Object, value, null);
		} catch {
			Debug.LogError("Failed to parse options for int property " + Title + ": \"" + Options + "\"");
		}
	}
}
