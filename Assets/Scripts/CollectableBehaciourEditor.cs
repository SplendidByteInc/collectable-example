using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The custom editor for <see cref="CollectableBehaviour" />.
/// </summary>
[CustomEditor(typeof(CollectableBehaviour), true)]
[CanEditMultipleObjects]
public class CollectableBehaviourEditor : Editor {
    private SerializedProperty _serializationId;

    /// <summary>
    /// Finds the first available id.
    /// </summary>
    private static int FindNextId() {
        // Find every single id that has been asigned, 
        // grab the highest one and record that as the next available id
        var collectables = CollectableBehaviour.FindAll();
        var last = collectables.LastOrDefault();
        var nextId = last == null ? 0 : last.serializatonId + 1;

        // Now find any ids that have not been assigned try to reassign them
        var exists = new HashSet<int>(collectables.Select(o => o.serializatonId));
        for (int i = 0; i < nextId; i++) {
            if (!exists.Contains(i))
                return i;
        }
        return nextId;
    }

    /// <summary>
    /// Called to re-render the inspector.
    /// </summary>
    public override void OnInspectorGUI() {
        // Need to make sure we don't assign an id in prefab mode
        var collectable = (CollectableBehaviour)target;
        var isPrefab = PrefabUtility.GetCorrespondingObjectFromSource(collectable.gameObject) == null;

        // Assign a unique id if one has not been assigned.
        if (!isPrefab && collectable.serializatonId == -1) {
            collectable.serializatonId = FindNextId();
            collectable.gameObject.tag = CollectableBehaviour.Tag;
            _serializationId.intValue = collectable.serializatonId;
            EditorUtility.SetDirty(target);
        }
        
        // Draw the inspector.
        DrawDefaultInspector();
    }

    /// <summary>
    /// Called when this editor initializes.
    /// </summary>
    void Awake() {
        _serializationId = serializedObject.FindProperty("serializatonId");
    }
}