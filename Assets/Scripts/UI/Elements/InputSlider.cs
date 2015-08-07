using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputSlider : MonoBehaviour {
	public Slider slider;
	public InputField inputField;
	
	float value;
	float min = 0;
	float max = 1;

	void Start () {
	
	}

	void Update () {
		value = slider.value;
		inputField.text = value.ToString();
	}

	public void setMinMax(float m, float mm){
		min = m;
		max = mm;
		slider.minValue = min;
		slider.maxValue = max;
	}
}
