using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class comecarExercicio : MonoBehaviour {
    public Button comecar;
    public static string passAbdAduInd, passAbdAduMed, passAbdAduAnl, passAbdAduMindi;
    public static string passarLevantamento;
    public static string passPinchInd, passPinchMed, passPinchAnl, passPinchMindi;
    public InputField i1, i2, i3, i4, i5, i6, i7, i8, i9;
    // Use this for initialization
    void Start () {
        i1.text = "0";
        i2.text = "0";
        i3.text = "0";
        i4.text = "0";
        i5.text = "0";
        i6.text = "0";
        i7.text = "0";
        i8.text = "0";
        i9.text = "0";
        Button btn = comecar.GetComponent<Button>();        
        btn.onClick.AddListener(exercicio);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string checkNull(string t) {
        if (t.Equals(null)) return "0";
        else return t;
    }

    public static void passarMenu() {
        SceneManager.UnloadSceneAsync("cena_menu");
    }
    void exercicio()
    {
        passAbdAduInd = i1.text;
        passAbdAduMed = i2.text;
        passAbdAduAnl = i3.text;
        passAbdAduMindi = i4.text;
        passarLevantamento = i5.text;
        passPinchInd = i6.text;
        passPinchMed = i7.text;
        passPinchAnl = i8.text;
        passPinchMindi = i9.text;
        SceneManager.LoadScene("cena_exercicios");
    }
}
