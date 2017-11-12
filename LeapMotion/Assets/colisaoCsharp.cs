using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colisaoCsharp : MonoBehaviour
{
    public Text booleano;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "bone3")
        {
            //Debug.Log("contato");
            booleano.text = "sim";
            booleano.enabled = false;
         
        }
    }

    public static void testando() {
        Debug.Log("testando");
    }
}