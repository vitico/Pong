using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porteria : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D bola)
    {
        var name = this.name == "Izquierda" ? "Derecha" : "Izquierda";
        if (bola.name == "Bola")
        {
            bola.GetComponent<Bola>().reiniciarBola(name);
        }
    }
}
