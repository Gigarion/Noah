using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class PlantHUDScript : MonoBehaviour
{
    private bool on = true;
    public double upTime = 0.5;
    public double downTime = 0.5;
    private double timeup = 0;
    private double timedown = 0;
    public int count;
    double cooldown = 1.0;
    private GameObject image;
    private GameObject newSpeciesText;
    private Text countText;
    private bool newspecies = false;


    void Start()
    {
        image = gameObject.transform.Find("Image").gameObject;
        newSpeciesText = gameObject.transform.Find("NewSpeciesText").gameObject;
        countText = gameObject.transform.Find("CountText").gameObject.transform.Find("Count").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = 0.5f;
        if (timeup < upTime * 2)
        {
            timeup += Time.deltaTime;
            alpha += Mathf.Min(1.0f, (float) (timeup / upTime / 2));
            Debug.Log("up");
            setAlpha(alpha);
        }
        else if (timedown >= 0) {
            timedown -= Time.deltaTime;
            alpha += (float)(timedown / downTime / 2);
            Debug.Log("Down");
            setAlpha(alpha);
        } else {
            newSpeciesText.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            newSpeciesText.transform.Find("Text").GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            cooldown -= Time.deltaTime;
        }
    }

    void toggle()
    {
        float alpha = (on) ? 1.0f : 0.0f;
        on = !on;
    }

    public void OnUse()
    {
        Flash(true);
    }

    private void setAlpha(float alpha)
    {
        gameObject.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        image.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        newSpeciesText.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        newSpeciesText.transform.Find("Text").GetComponent<CanvasRenderer>().SetAlpha(alpha);
    }

    public void Flash(bool newSpecies)
    {
        newspecies = newSpecies;
        timeup = 0;
        timedown = downTime * 1.75f;
        float toAdd = (newSpecies) ? 0.2f : 0.05f;
        count++;
        countText.text = count + "";
    }
}
