using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseBar : MonoBehaviour {


    public Image m_image;
    private Color m_color;
    public Color color1;
    public Color color2;

    public int cycles = 100;
    private int currCycle = 0;
    private bool up = true;
	
	// Update is called once per frame
	void Update () {
        if (up) currCycle++;
        else currCycle--;
        if (currCycle >= cycles || currCycle == 0)
        {
            up = !up;
        }
        Debug.Log(up);
        Debug.Log(currCycle);
        Debug.Log(cycles);
        Debug.Log((float)currCycle / (float) cycles);
        m_image.color = new Color(color1.r, color1.g, color1.b, (float) currCycle / (float) cycles);
	}
}
