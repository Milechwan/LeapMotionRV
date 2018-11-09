using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class comecarExercicio : MonoBehaviour {
    public Button comecar;
    
    public static string passAbdAduInd, passAbdAduMed, passAbdAduAnl, passAbdAduMindi;
    public static string passarLevantamento, passarExtMed, passarExtAnl, passarExtMindi;
    public static string passPinchInd, passPinchMed, passPinchAnl, passPinchMindi;
    public static string generoPaciente, inputIdade, maoPaciente;
    public InputField i1, i2, i3, i4, i5, i6, i7, i8, i9;
    public InputField parametroExtMed, idadePaciente, parametroExtAnl, parametroExtMindi;//parametroExtAnl,parametroExtMindi
    public Dropdown dropGeneroPaciente, dropMao;
    //textos dos ângulos
    public static string anguloAbdInd, anguloAbdMed, anguloAbdAnl, anguloAbdMind, anguloLevantamento, anguloExtMed, anguloExtAnl, anguloExtMindi;
    public InputField anguloAbdIndIF, anguloAbdMedIF, anguloAbdAnlIF, anguloAbdMinIF, anguloLevantIF, anguloExtensaoMedIF,
        anguloExtensaoAnlIF, anguloExtensaoMindiIF;
    //anguloExtensaoAnlIF, anguloExtensaoMindiIF
    
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
        parametroExtMed.text = "0";
        parametroExtAnl.text = "0";
        parametroExtMindi.text = "0";
        anguloAbdMinIF.text = "0";
        anguloAbdIndIF.text = "0";
        anguloAbdMedIF.text = "0";
        anguloAbdAnlIF.text = "0";
        anguloLevantIF.text = "0";
        anguloExtensaoAnlIF.text = "0";
        anguloExtensaoMindiIF.text = "0";
        anguloExtensaoMedIF.text = "0";
        idadePaciente.text = "0";
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


        SceneManager.LoadScene("cena_menu");
       // Debug.Log("apertei o M");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("cena_menu"));
    }
    void exercicio()
    {
        //mão ecolhida
        int dropDownMaoValue = dropMao.value;
        maoPaciente = dropMao.options[dropDownMaoValue].text;
        //informações do paciente para salvar o csv
        inputIdade = idadePaciente.text;
        int dropDownValue = dropGeneroPaciente.value;
        generoPaciente = dropGeneroPaciente.options[dropDownValue].text;
        //informações dos exercícios passados
        passAbdAduInd = i1.text;
        passAbdAduMed = i2.text;
        passAbdAduAnl = i3.text;
        passAbdAduMindi = i4.text;
        passarLevantamento = i5.text;
        passPinchInd = i6.text;
        passPinchMed = i7.text;
        passPinchAnl = i8.text;
        passPinchMindi = i9.text;
        passarExtMed = parametroExtMed.text;
        passarExtAnl = parametroExtAnl.text;
        passarExtMindi = parametroExtMindi.text;
        //textos dos ângulos desejados
        anguloAbdAnl = anguloAbdAnlIF.text;
        anguloAbdInd = anguloAbdIndIF.text;
        anguloAbdMed = anguloAbdMedIF.text;
        anguloAbdMind = anguloAbdMinIF.text;
        anguloLevantamento = anguloLevantIF.text;
        anguloExtMed = anguloExtensaoMedIF.text;
        anguloExtAnl = anguloExtensaoAnlIF.text;
        anguloExtMindi = anguloExtensaoMindiIF.text;
        //passar para script de escrever o csv quais exercícios serão feitos
        exportarCsv.indiceLinha = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        exportarCsv.inicializarLinhasArquivo();
        SceneManager.LoadScene("cena_exercicios");
    }
}
