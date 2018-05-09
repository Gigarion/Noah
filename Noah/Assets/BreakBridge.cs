using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBridge : MonoBehaviour
{
    private Spin s;

    public void OnConversationEnd(Transform actor)
    {
        s = this.gameObject.GetComponent<Spin>();
        s.enabled = true;
    }

}