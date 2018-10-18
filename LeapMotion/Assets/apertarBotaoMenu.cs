using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apertarBotaoMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //  não tá funcionando essa bosta
        if (Input.GetKeyDown(KeyCode.M))
        {
            
            comecarExercicio.passarMenu();
        }
    }
}
