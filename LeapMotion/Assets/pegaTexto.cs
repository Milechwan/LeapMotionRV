using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pegaTexto : MonoBehaviour {
    public InputField campoAbdu;
    static string teste; 
	// Use this for initialization
	void Start () {
        teste = campoAbdu.textComponent.text; 
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(teste);
	}
}
