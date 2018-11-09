using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class apertarBotaoMenu : MonoBehaviour {
    public RigidHand rigidHandDireita;
    public RigidHand rigidHandEsquerda;
    // Use this for initialization
    void Start () {
        GameObject goAux = GameObject.Find("RigidRoundHand_R");
        GameObject goAux2 = GameObject.Find("RigidRoundHand_L");
        if (string.Equals(comecarExercicio.maoPaciente, "Direita") && goAux!=null)
        {
            rigidHandDireita = GameObject.Find("RigidRoundHand_R").GetComponent<RigidHand>();
        }
        else if(string.Equals(comecarExercicio.maoPaciente, "Esquerda") && goAux2 != null)
        {
            rigidHandEsquerda = GameObject.Find("RigidRoundHand_L").GetComponent<RigidHand>();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (rigidHandDireita == null && string.Equals(comecarExercicio.maoPaciente, "Direita")) rigidHandDireita = GameObject.FindWithTag("rigidHandDireita").GetComponent<RigidHand>();
        if (rigidHandEsquerda == null && string.Equals(comecarExercicio.maoPaciente, "Esquerda")) rigidHandEsquerda = GameObject.FindWithTag("rigidHandEsquerda").GetComponent<RigidHand>();
        if (Input.GetKeyDown(KeyCode.M))
        {
            
            if (rigidHandDireita != null && string.Equals(comecarExercicio.maoPaciente, "Direita")) rigidHandDireita.resetarValores();
            if (rigidHandEsquerda != null && string.Equals(comecarExercicio.maoPaciente, "Esquerda")) rigidHandEsquerda.resetarValores();
            comecarExercicio.passarMenu();
        }
        
    }
}
