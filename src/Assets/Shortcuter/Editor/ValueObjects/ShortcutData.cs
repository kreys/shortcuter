using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;

namespace Intentor.Shortcuter.ValueObjects {
	/// <summary>
	/// Shortcut data.
	/// </summary>
	public class ShortcutData : ScriptableObject {
		/// <summary>Quantity of columns to display.</summary>
		public int columns = 3;
		/// <summary>Shortcut types to be displayed.</summary>
		public List<ShortcutType> types;

        /// <summary>Shortcut types to be displayed (by column name).</summary>
        public List<ShortcutType> mergedTypes
        {
            get
            {
                if( areMergedTypesDirty )
                {
                    Dictionary<string, ShortcutType> mergedTypesTemp = new Dictionary<string, ShortcutType>();
                    foreach( ShortcutType type in types )
                    {
                        if( mergedTypesTemp.ContainsKey( type.columnTitle ) )
                        {
                            mergedTypesTemp[type.columnTitle].guids.AddRange( type.guids );
                        }
                        else
                        {
                            mergedTypesTemp.Add( type.columnTitle, new ShortcutType( type ) );
                        }
                    }
                    _mergedTypes.Clear();
                    _mergedTypes.AddRange( mergedTypesTemp.Values );
                    areMergedTypesDirty = false;
                }
                return _mergedTypes;
            }
        }

        [ContextMenu("SetTypesDirty")]
        public void SetTypesDirty()
        {
            areMergedTypesDirty = true;
        }

        private List<ShortcutType> _mergedTypes = new List<ShortcutType>();
        private bool areMergedTypesDirty = true;
    }
}