using UnityEngine;
using System.Collections;
using System;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class PropertyAttribute : Attribute {
	public string Title { get; private set; }
	public string Description { get; private set; }
	public string Options { get; private set; }

	/// <summary>
	/// Allows editing in design mode
	/// </summary>
	/// <param name="title">Title of the property</param>
	/// <param name="description">Short description of the property</param>
	/// <param name="options">
	/// <para>Options of the property</para>
	/// <para>Float: "min:max:increment"</para>
	/// <para>Int: "min:max:increment"</para>
	/// </param>
	public PropertyAttribute(string title, string description, string options) {
		Title = title;
		Description = description;
		Options = options;
	}
}
