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
            //Debug.Log(objMao);
            maoEsquerda = objMao.IsLeft;
            maoDireita = objMao.IsRight;
            //Debug.Log("mao direita: "+maoDireita + "; mao esquerda: " + maoEsquerda);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        
		if ((string.Equals("Esquerda",maoEscolhida) && maoDireita) || (string.Equals(maoEscolhida,"Direita") && maoEsquerda)) //caso as duas mãos estejam ativas, uma delas precisa ser 
        {
            Debug.Log("desativei!"+maoEscolhida);
            this.gameObject.SetActive(false);
            //(maoEscolhida=="Esquerda" && maoDireita) || (maoEscolhida=="Direita" && maoEsquerda)
        }
        else
        {
            this.gameObject.SetActive(true);
        }
	}
    /*if(maoEsquerda && maoDireita)
        {

           
        }
        else*/
}
