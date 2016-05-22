using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

public class PropertiesTool : MonoBehaviour {
	public Transform windowCanvas;
	public Camera designCam;

	public AnimationCurve animationCurve;

	GameObject propertiesDialogPrefab;

	const float animationDuration = 0.2f;

	void Start() {
		propertiesDialogPrefab = Resources.Load("UI/Dialog/Properties Dialog") as GameObject;
	}

	void Update() {
		if (UIManager.canInteract() && UIManager.tool == Tool.Properties && UIManager.gamemode == Gamemode.Design) {
			if (Input.GetMouseButtonDown(0)) {
				Block block = Raycast();
				if (block != null) {
					//Get properties
					var props = block.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PropertyAttribute)));

					List<BlockProperty> blockProperties = new List<BlockProperty>();

					foreach (PropertyInfo p in props) {
						//Get attribute
						PropertyAttribute attribute = p.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute;

						//Create block property
						BlockProperty property = new BlockProperty();

						//Set values
						property.Title = attribute.Title;
						property.Description = attribute.Description;
						property.Value = p.GetValue(block, null);

						//Add to list
						blockProperties.Add(property);
					}

					//Create dialog
					GameObject go = Instantiate(propertiesDialogPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
                    PropertiesDialog propertiesDialog = go.GetComponent<PropertiesDialog>();
					propertiesDialog.transform.SetParent(windowCanvas);
					propertiesDialog.transform.SetAsLastSibling();
					propertiesDialog.transform.name = block.GetDisplayName() + " Properties Dialog";

					propertiesDialog.StartAnimation(Input.mousePosition, new Vector2(Screen.width * 0.75f, Screen.height * 0.5f), animationCurve, animationDuration);

					propertiesDialog.SetTitle(block.GetDisplayName());
					propertiesDialog.SetProperties(blockProperties);
				}
			}
		}
	}

	Block Raycast() {
		//Get ray
		Ray ray = designCam.ScreenPointToRay(Input.mousePosition);

		//Raycast
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if (hit.transform.GetComponent<Chunk>() != null) {
				return hit.transform.GetComponent<Chunk>().GetBlockFromCollider(hit.collider);
			}
		}

		return null;
	}
}
