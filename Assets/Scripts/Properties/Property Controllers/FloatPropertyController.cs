using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatPropertyController : PropertyController {
	public Slider slider;

	float min;
	float max;
	float increment;

	public override void Create(object value) {
		try {
			//Parse options string
			min = float.Parse(Options.Split(new char[] { ':' })[0]);
			max = float.Parse(Options.Split(new char[] { ':' })[1]);
			increment = float.Parse(Options.Split(new char[] { ':' })[2]);
		} catch {
			Debug.LogError("Failed to parse options for float property " + Title + ": \"" + Options + "\"");
		}

		//Set slider
		slider.value = Mathf.InverseLerp(min, max, (float) value);
	}

	public void OnValueChanged() {
		//Get value from 0-1 slider
		float value = Mathf.Lerp(min, max, slider.value);

		//Round to nearest increment
		value = Mathf.Round(value / increment) * increment;

		//Set slider to rounded value
		slider.value = Mathf.InverseLerp(min, max, value);

		//Set value of property
		Property.SetValue(Object, value, null);
	}
}
