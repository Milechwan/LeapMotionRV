using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOVO : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(".gameObject.name" + col.gameObject.name);
        if (col.gameObject.name == "ProximoExercicio")
        {
            Debug.Log("Proximo entrou");
        }
    }
}
