using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class FillOnQuest : MonoBehaviour {

    private PulseBar[] pulseBars;
    private Image[] images;

    public void OnUse()
    {
        gameObject.GetComponent<Slider>().value = 0.4f;

        // turn off pulsing
        pulseBars = gameObject.GetComponentsInChildren<PulseBar>();
        images = gameObject.GetComponentsInChildren<Image>();
        foreach (PulseBar p in pulseBars)
            p.enabled = false;
        int c = 0;
        foreach (Image i in images)
            if (c > 0)
                i.color = Color.blue;
            else
                c++;
    }
}
