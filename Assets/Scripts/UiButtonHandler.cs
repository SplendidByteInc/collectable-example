using UnityEngine;

/// <summary>
/// Behaviour that handles button clicks.
/// </summary>
public class UiButtonHandler : MonoBehaviour {

    // Note: This should probably be the name of the scene being saved to avoid overwriting collectables in other scenes.
    /// <summary>
    /// The name of the file being saved/loaded.
    /// </summary>
    private const string FileName = "save.dat";

    /// <summary>
    /// Called in response to the save button being pushed.
    /// </summary>
    public void OnPressSave() {
        var serializer = new Serializer();
        serializer.SaveFile(FileName, CollectableBehaviour.FindAll());
    }

    /// <summary>
    /// Called in response to the load button being pushed.
    /// </summary>
    public void OnPressLoad() {
        var serializer = new Serializer();
        var collectables = CollectableBehaviour.FindAll();
        serializer.LoadFile(FileName, collectables);
        foreach (var collectable in collectables) {
            collectable.SendMessage("Loaded", SendMessageOptions.DontRequireReceiver);
        }
    }
}