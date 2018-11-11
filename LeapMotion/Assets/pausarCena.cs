using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausarCena : MonoBehaviour {

    public static bool scenePaused = false;
    public GameObject painelPausa;
	// script para pausar o tempo que afeta alguns elementos do canvas (tudo que depende de taxa de quadros)
    //ainda que o modelo 3d da mão se mova quando pausar o jogo, não são computados novos ângulos
    //feito especialmente para os testes
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (scenePaused)
            {
                
                painelPausa.SetActive(false);
                Time.timeScale = 1f;
                scenePaused = false;
            }
            else
            {
                painelPausa.SetActive(true);
                Time.timeScale = 0f;
                scenePaused = true;
            }
        }
	}
}
