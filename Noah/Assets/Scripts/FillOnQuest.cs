using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class FillOnQuest : MonoBehaviour {

    public void OnUse()
    {
        gameObject.GetComponent<Slider>().value = 0.3f;
    }
}
