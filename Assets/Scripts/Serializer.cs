using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Used to save or load collectables.
/// </summary>
public class Serializer {

    /// <summary>
    /// Saves all collectable information to a file.
    /// </summary>
    /// <param name="fileName">The name of the file to save</param>
    /// <param name="collectables">The collectables being saved</param>
    public void SaveFile(string fileName, IEnumerable<CollectableBehaviour> collectables) {
        // Iterate over all collectables and write if they have been collected.
        var asArray = collectables.ToArray();
        var collected = new List<bool>();
        var last = asArray.Last();
        for (int i = 0, j = 0; i < asArray.Length && j <= last.serializatonId; i++) {
            // Account for any gaps in ids padding with false
            var collectable = asArray[i];
            while (j++ != collectable.serializatonId) {
                collected.Add(false);
            }
            collected.Add(collectable.collected);
        }
        var bitArray = new BitArray(collected.ToArray());
        var data = new byte[(bitArray.Length - 1) / 8 + 1];
        bitArray.CopyTo(data, 0);
        
        // Write the information as binary.
        var destination = Path.Combine(Application.persistentDataPath, fileName);
        var file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);
        var formatter = new BinaryFormatter();
        formatter.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Loads all collectable information from a file.
    /// </summary>
    /// <param name="fileName">The name of the file to save</param>
    /// <param name="collectables">The collectables being loaded</param>
    public void LoadFile(string fileName, IEnumerable<CollectableBehaviour> collectables) {
        // If the file doesn't exist, there is nothing to load.
        var destination = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(destination))
            return;

        // Read the binary data.
        var file = File.OpenRead(destination);
        var formatter = new BinaryFormatter();
        var data = (byte[])formatter.Deserialize(file);
        file.Close();
        var bitArray = new BitArray(data);

        // Iterate over all collectables and read if they have been collected.
        var asArray = collectables.ToArray();
        for (int i = 0, j = 0; j < asArray.Length; j++) {
            // Account for gaps that were saved but no longer exist (maybe deleted in editor)
            var collectable = asArray[j];
            while (collectable.serializatonId != i) {
                i++;
            }
            if (i >= bitArray.Count)
                break;
            collectable.collected = bitArray.Get(i);
        }
    }
}