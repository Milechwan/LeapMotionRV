using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisaoCsharp : MonoBehaviour
{

    public static void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "bone1" || col.gameObject.name == "bone2" || col.gameObject.name == "bone3")
        {
            Debug.Log("contato");
        }
    }

    public static void testando() {
        Debug.Log("testando");
    }
}