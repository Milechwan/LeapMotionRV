using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class renderizarMaoPaciente : MonoBehaviour {

    private HandModelBase maoRenderizada;
    public static string maoEscolhida;
    public static bool maoEsquerda = false;
    public static bool maoDireita = false;

    // Use this for initialization
    void Start () {
        maoRenderizada = this.gameObject.GetComponent<HandModelBase>();
           
        maoEscolhida = comecarExercicio.maoPaciente;
        
        if (maoRenderizada != null)
        {
         //   Debug.Log(maoRenderizada);
            Hand objMao = maoRenderizada.GetLeapHand();
            Debug.Log(objMao);
            maoEsquerda = objMao.IsLeft;
            maoDireita = objMao.IsRight;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        
		if((maoEscolhida=="Esquerda" && maoDireita) || (maoEscolhida=="Direita" && maoEsquerda))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
	}
}
