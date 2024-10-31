using Saves;
using UnityEditor;
using UnityEngine;

namespace Editor {
	[CustomEditor(typeof(GameData))]
	public sealed class EditorButton : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var function = (GameData)target;
			if (GUILayout.Button("ClearSave")) {
				function.ClearSave();
			}
			
			if (GUILayout.Button("Save")) {
				function.Save();
			}

			if (GUILayout.Button("LoadSave")) {
				function.Load();
			}
		}
	}
}
