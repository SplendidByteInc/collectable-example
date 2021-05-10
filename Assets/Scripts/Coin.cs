using UnityEngine;

/// <summary>
/// Behaviour for a coin collectable.
/// </summary>
public class Coin : CollectableBehaviour {

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update() {
        var newRotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + 30.0f, 0.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime);
    }

    /// <summary>
    /// Called to initialize the object.
    /// </summary>
    void Awake() {
        UpdateColor();
    }

    /// <summary>
    /// Called when this object is deserialized.
    /// </summary>
    void Loaded() {
        UpdateColor();
    }

    /// <summary>
    /// Called in response to a click.
    /// </summary>
    void OnMouseDown(){
        collected = !collected;
        UpdateColor();
    }

    /// <summary>
    /// Updates the current color of the coin.
    /// </summary>
    private void UpdateColor() {
        if (collected)
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        else
            GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
    }
}