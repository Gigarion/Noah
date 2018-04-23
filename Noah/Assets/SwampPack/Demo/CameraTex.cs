using UnityEngine;
using System.Collections;

public class CameraTex : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Cursor.visible = false;
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	void OnGUI()
	{
		GUI.Label (new Rect (8, Screen.height - 20, Screen.width - 4, 20), "WLE Swamp Pack - Developed by Esteban Campos"      );   
		GUI.Label (new Rect (Screen.width - 200, Screen.height - 20, 200, 20), "Winterleaf Entertainment ©");
	}
}
