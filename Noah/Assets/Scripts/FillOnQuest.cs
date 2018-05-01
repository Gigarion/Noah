using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class FillOnQuest : MonoBehaviour {

    private PulseBar[] pulseBars;

    public void OnUse()
    {
        gameObject.GetComponent<Slider>().value = 0.4f;

        // turn off pulsing
        pulseBars = gameObject.GetComponentsInChildren<PulseBar>();
        foreach (PulseBar p in pulseBars)
            p.enabled = false;
    }
}
