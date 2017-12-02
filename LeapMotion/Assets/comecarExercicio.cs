using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class comecarExercicio : MonoBehaviour {
    public Button comecar;
 
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
        SceneManager.LoadScene("cena_exercicios");
    }
}
