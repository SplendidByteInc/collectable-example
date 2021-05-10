using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Behaviour for all collectables.
/// </summary>
public abstract class CollectableBehaviour : MonoBehaviour {
    /// <summary>
    /// The tag used to search for collectables
    /// </summary>
    public const string Tag = "Collectable";

    /// <summary>
    /// Finds all collectables.
    /// </summary>
    /// <returns>The collectables</returns>
    public static IEnumerable<CollectableBehaviour> FindAll() {
        return GameObject.FindGameObjectsWithTag(Tag)
            .Select(o => o.GetComponent<CollectableBehaviour>())
            .Where(o => o != null)
            .OrderBy(o => o.serializatonId);
    }

    /// <summary>
    /// The unique identifier for this collectable.
    /// </summary>
    // [HideInInspector] 
    // TODO: This is visible in the editor for demonstation reasons, typically you wouldn't want this to be assignable in the editor.
    // TODO: If you want to hide it again, uncomment the [HideInInspector] line and delete these comments
    public int serializatonId = -1; 

    /// <summary>
    /// Indicates if this collectable has been collected.
    /// </summary>
    public bool collected = false;
}