using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class comecarExercicio : MonoBehaviour {
    public Button comecar;
    public static string passarAbdAdu;
    public static string passarLevantamento;
    public static string passarPinch;
    public Text abdadu;
    public Text levantamento;
    public Text pinch;

    // Use this for initialization
    void Start () {
        Button btn = comecar.GetComponent<Button>();        
        btn.onClick.AddListener(exercicio);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void exercicio()
    {
        passarAbdAdu = abdadu.text;
        passarLevantamento = levantamento.text;
        passarPinch = pinch.text;
        SceneManager.LoadScene("cena_exercicios");
    }
}
