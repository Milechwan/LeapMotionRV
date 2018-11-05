using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class apertarBotaoMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.M))
        {
            
            comecarExercicio.passarMenu();
        }
        
    }
}
