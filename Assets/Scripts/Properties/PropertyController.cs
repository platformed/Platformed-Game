using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;

public class PropertyController : MonoBehaviour {
	public Text titleText;

	string title;
	[HideInInspector]
	public string Title {
		get {
			return title;
		}
		set {
			titleText.text = value;
			title = value;
		}
	}
	[HideInInspector]
	public string Description { get; set; }
	[HideInInspector]
	public string Options { get; set; }
	[HideInInspector]
	public object Object { get; set; }
	[HideInInspector]
	public PropertyInfo Property { get; set; }
}
