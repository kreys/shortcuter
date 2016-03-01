using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using Intentor.Shortcuter.ValueObjects;

namespace Intentor.Shortcuter.Util {
	/// <summary>
	/// Utility class for assets.
	/// </summary>
	public static class AssetUtils {
		/// <summary>Editor default resources path.</summary>
		private const string EDITOR_DEFAULT_RESOURCES = "Editor Default Resourcest";
		/// <summary>Shortcuts data path.</summary>
		private const string SHORTCUTS_DATA_PATH = EDITOR_DEFAULT_RESOURCES + "/Shortcuts.asset";

		/// <summary>
		/// Loads shortcuts data
		/// </summary>
		/// <returns>The shortcut data.</returns>
		public static ShortcutData LoadShorcutData() {
			//Checks whether editor default resources path exists.
			var resourcesPath = Path.Combine(Application.dataPath, EDITOR_DEFAULT_RESOURCES);
			if (!Directory.Exists(resourcesPath)) {
				Directory.CreateDirectory(resourcesPath);
			}

			//Checks whether the shourtcuts data path exists.
			var dataPath = Path.Combine(Application.dataPath, SHORTCUTS_DATA_PATH);
			if (!File.Exists(dataPath)) {
				return CreateShortcutData();
			} else {
				return (ShortcutData)EditorGUIUtility.Load("Shortcuts.asset");
			}
		}

		/// <summary>
		/// Creates the shortcut data.
		/// </summary>
		/// <returns>The shortcut data.</returns>
		public static ShortcutData CreateShortcutData() {
			var shortcuts = ScriptableObject.CreateInstance<ShortcutData>();
			shortcuts.types = new List<ShortcutType>();

			var dataPath = Path.Combine("Assets", SHORTCUTS_DATA_PATH);
			AssetDatabase.CreateAsset(shortcuts, dataPath);
			AssetDatabase.SaveAssets();

			return shortcuts;
		}

		/// <summary>
		/// Gets all assets GUID f a given type.
		/// </summary>
		/// <param name="assetType">Asset type.</param>
		/// <returns>All assets of the given type.</returns>
		public static string[] GetAssetsGuid(Type assetType) {
			//Unity types don't require full qualified names. 
			var typeName = (string.IsNullOrEmpty(assetType.Namespace) || assetType.Namespace.StartsWith("Unity") ? 
				assetType.Name : assetType.FullName);

			return GetAssetsGuid(typeName);
		}

		/// <summary>
		/// Gets all assets GUID of a given type name (full qualified or Unity).
		/// </summary>
		/// <param name="typeName">Type name.</param>
		/// <returns>All assets of the given type.</returns>
		public static string[] GetAssetsGuid(string typeName) {
			return AssetDatabase.FindAssets(string.Format("t:{0}", typeName));		
		}
	}
}