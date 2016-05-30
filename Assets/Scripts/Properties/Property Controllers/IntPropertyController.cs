using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntPropertyController : PropertyController {
	public Slider slider;

	int min;
	int max;
	int increment;

	public override void Create(object value) {
		try {
			//Parse options string
			min = int.Parse(Options.Split(new char[] { ':' })[0]);
			max = int.Parse(Options.Split(new char[] { ':' })[1]);
			increment = int.Parse(Options.Split(new char[] { ':' })[2]);
		} catch {
			Debug.LogError("Failed to parse options for int property " + Title + ": \"" + Options + "\"");
		}

		//Set slider
		slider.value = Mathf.InverseLerp(min, max, (int)value);
	}

	public void OnValueChanged() {
		//Get value from 0-1 slider
		float value = Mathf.Lerp(min, max, slider.value);

		//Round to nearest increment
		value = Mathf.Round(value / increment) * increment;

		//Set slider to rounded value
		slider.value = Mathf.InverseLerp(min, max, value);

		//Convert to int
		int valueInt = (int)value;

		//Set value of property
		Property.SetValue(Object, valueInt, null);
	}
}
