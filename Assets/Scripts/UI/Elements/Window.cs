﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Window : MonoBehaviour {
	public GameObject windowPanel;
	public GameObject topPanel;
	public GameObject bottomPanel;
	public GameObject titleObj;

	GameObject button;
	string title;
	Vector3 mouseOffset;

	void Start () {
		button = Resources.Load ("UI Elements/FlatButton") as GameObject;
	}

	void Update () {
	
	}

	public void close(){
		Destroy(gameObject);
	}

	public void setTitle(string t){
		title = t;
		Text c = titleObj.GetComponent<Text> ();
		c.text = title;
	}

	public void addButton(string name, int id){
		Transform b = Instantiate (button).transform;

		b.parent = bottomPanel.transform;

		Text t = b.GetComponentInChildren<Text> ();
		t.text = name.ToUpper();



	}

	public void startDrag(){
		windowPanel.transform.SetAsLastSibling();
		mouseOffset = transform.position - Input.mousePosition;
		UIManager.instance.isDragging = true;
	}

	public void stopDrag(){
		UIManager.instance.isDragging = false;
	}

	public void drag(){
		transform.position = Input.mousePosition + mouseOffset;
		clampPos ();
	}

	void clampPos(){
		if(transform.position.x < 0 + (getWidth() / 2)){
			transform.position = new Vector3(getWidth() / 2, transform.position.y, 0);
		}

		if(transform.position.x > Screen.width - (getWidth() / 2)){
			transform.position = new Vector3(Screen.width - (getWidth() / 2), transform.position.y, 0);
		}

		if(transform.position.y < 0 + (getHeight() / 2)){
			transform.position = new Vector3(transform.position.x, getHeight() / 2, 0);
		}

		if(transform.position.y > Screen.height - (getHeight() / 2)){
			transform.position = new Vector3(transform.position.x, Screen.height - (getHeight() / 2), 0);
		}
	}

	float getWidth(){
		RectTransform r = windowPanel.GetComponent<RectTransform> ();
		return r.rect.width;
	}

	float getHeight(){
		RectTransform r = windowPanel.GetComponent<RectTransform> ();
		return r.rect.height;
	}
}
