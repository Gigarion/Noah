using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

// remove the wolf when the water conversation ends.
public class RemoveMe : MonoBehaviour {
    public void OnConversationEnd(Transform actor)
    {
        Destroy(this.gameObject);
    }
}
