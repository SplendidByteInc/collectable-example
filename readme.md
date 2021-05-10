# Description
A proof of concept for serializing and deserializing collectable objects.  This is achieved by providing 3 components:
1. A `CollectableBehaviour` script which all collectables should extend from.  This provides a `serializationId` and `collectable` field which enables automatic serialization.  
2. A `CollectableBehaviourEditor` script which automatically assigns each new collectable a unique `serializationId`.
3. A `Serializer` script which reads and writes all collectable's `collectable` states using the `serializationId` to identify the collectable.

# Usage
Suggested usage is to create a script for your new collectable, extend `CollectableBehaviour`, and attach it to a prefab.  Each time the prefab is added to the scene, it should be assigned a new `serializationId`.  Finally, `Serializer` can be used to save or load all collectables in the scene to a file.

:warning: There are two subtle requirements that must be met for this to work:
1. The `serializationId` for the prefab should be `-1`.  This value is used by `CollectableBehaviourEditor` to determine if a new ID needs to be assigned.  In future iterations, the `serializationId` will be handled automatically.
2. The collectables need to all exist in the scene when the scene is loaded.  Dynamically loading and removing collectables may result in unpredictable values for `serializationId` which will lead to inconsistent loading.

# Demo
A demo scene has been provided in the /Scenes/ subdirectory.  
* To run the demo, load the scene and press play.  
* Clicking on a rotating coin will toggle between collected and uncollected.
* You can save and load using the provided buttons.
* You should be able to save, shut down Unity, and then reload the collectables in another session.

# Licence
There is no licence for any assets in this repository.  Feel free to modify and/or ship the assets as-is, with or without credit, in personal or commercial products.