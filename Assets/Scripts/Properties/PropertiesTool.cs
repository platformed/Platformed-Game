﻿using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

public class PropertiesTool : MonoBehaviour {
	public LayerMask raycastLayerMask;

	public Transform windowCanvas;
	public Camera designCam;

	public AnimationCurve speedCurve;
	public AnimationCurve circleCurve;

	GameObject propertiesDialogPrefab;

	const float animationDuration = 0.6f;
	const float circleDuration = 0.3f;

	void Start() {
		propertiesDialogPrefab = Resources.Load("UI/Properties/Properties Dialog") as GameObject;
	}

	void Update() {
		if (DesignManager.instance.CanInteractLevel() && DesignManager.instance.tool == Tool.Properties && GamemodeManager.instance.Gamemode == Gamemode.Design) {
			if (Input.GetMouseButtonDown(0)) {
				Block block = Raycast();
				if (block != null) {
					//Create dialog
					GameObject instance = Instantiate(propertiesDialogPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
					instance.transform.SetParent(windowCanvas);
					instance.transform.SetAsLastSibling();
					instance.transform.name = block.GetDisplayName() + " Properties Dialog";

					//Get properties dialog
					PropertiesDialog propertiesDialog = instance.GetComponentInChildren<PropertiesDialog>();

					//Animate dialog
					Vector2 endPos = new Vector2(Screen.width * 0.75f, Screen.height * 0.5f);
                    propertiesDialog.StartAnimation(Input.mousePosition, endPos, speedCurve, circleCurve, animationDuration, circleDuration);

					//Set title of dialog
					propertiesDialog.SetTitle(block.GetDisplayName());
					
					//Get properties
					var props = block.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PropertyAttribute)));

					foreach (PropertyInfo p in props) {
						//Get attribute
						PropertyAttribute attribute = p.GetCustomAttributes(typeof(PropertyAttribute), false)[0] as PropertyAttribute;

						//Add attribute to dialog
						propertiesDialog.AddProperty(block, p, attribute);
					}
				}
			}
		}
	}

	Block Raycast() {
		//Get ray
		Ray ray = designCam.ScreenPointToRay(Input.mousePosition);

		//Raycast
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask)) {
			if (World.useChunks) {
				if (hit.transform.GetComponent<Chunk>() != null) {
					return hit.transform.GetComponent<Chunk>().GetBlockFromCollider(hit.collider);
				}
			} else {
				return World.instance.GetBlockFromCollider(hit.collider);
			}
		}

		return null;
	}
}
