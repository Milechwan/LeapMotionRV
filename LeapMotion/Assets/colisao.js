#pragma strict

function OnCollisionEnter(col: Collision) {
    if (col.gameObject.name == "bone1" || col.gameObject.name == "bone2" || col.gameObject.name == "bone3") {
        Debug.Log("contato");
    } 
}
