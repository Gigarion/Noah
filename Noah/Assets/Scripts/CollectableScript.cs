using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class CollectableScript : MonoBehaviour {

    public PlantHUDScript hud;
    public bool newSpecies;
    public bool deleteOnUse;

    void OnUse(Transform actor)
    {
        Debug.Log(actor);
        hud.Flash(newSpecies);
        if (deleteOnUse)
        {
            gameObject.SetActive(false);
        }
    }
}
