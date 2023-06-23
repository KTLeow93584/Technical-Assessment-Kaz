/// Code Attribution [ReadOnlyDrawer.cs]
/// Solution provided by it3ration.
/// Link: https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/
/// Note1: This is the simpler/more lightweight approach without utilizing a third party package such as Odin Inspector
/// or custom editors.
/// Note 2: Does not fully work on containers (Lists, Arrays) because the function to add/remove is still available. It only
/// greys out the contents in the container.

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	/// <summary>
	/// Unity method for drawing GUI in Editor
	/// </summary>
	/// <param name="position">Position.</param>
	/// <param name="property">Property.</param>
	/// <param name="label">Label.</param>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Saving previous GUI enabled value
		var previousGUIState = GUI.enabled;
		// Disabling edit for property
		GUI.enabled = false;
		// Drawing Property
		EditorGUI.PropertyField(position, property, label);
		// Setting old GUI enabled value
		GUI.enabled = previousGUIState;
	}
}