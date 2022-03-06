using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string Ganador = "";

    public Text txtInstrucciones;
    public string textoInstrucciones = @"Jugador Izquierda: W/S
Jugador Derecha: Flecha arriba/Fecla abajo
Haz clic o pulsa P para jugar";

    public AudioClip auidoEnd;
    public AudioSource player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    // when the screen load, debug "Ganador"
    void Start()
    {
        player = GetComponent<AudioSource>();
        var curGanador = Ganador;
        GameManager.Ganador = "";
        if (txtInstrucciones == null) return;
        if (curGanador == "")
        {
            txtInstrucciones.text = textoInstrucciones;
        }
        else
        {
            player.PlayOneShot(auidoEnd);
            txtInstrucciones.text = "Ganador: " + curGanador + "\n" + textoInstrucciones;
        }
    }
}
