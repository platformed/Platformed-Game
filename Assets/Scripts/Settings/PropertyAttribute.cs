using UnityEngine;
using System.Collections;
using System;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class PropertyAttribute : Attribute {
	public string Title { get; private set; }
	public string Description { get; private set; }

	public PropertyAttribute(string title, string description) {
		Title = title;
		Description = description;
	}
}
