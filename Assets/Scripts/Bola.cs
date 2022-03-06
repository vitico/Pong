using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    //velocidad
    public float velocidad = 30.0f;

    public int golesIzquierda = 0, golesDerecha;

    AudioSource fuenteDeAudio;

    public AudioClip audioGol, audioRaquete, audioRebote, audioInicio;

    public Text contadorIzquierda, contadorDerecha, txtTiempo;
    // create a timer to count the time
    public float timer = 0.0f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        fuenteDeAudio = GetComponent<AudioSource>();

        fuenteDeAudio.PlayOneShot(audioInicio);
    }

    void OnCollisionEnter2D(Collision2D micolision)
    {
        //Col contiene toda la informacion de la colision
        // si la bola colision con la raqueta:
        // mi colision.gameObject es la raqueta
        // micolision.transform.position es la posiciojn de la raqueta
        if (micolision.gameObject.name == "Raqueta Izquierda" || micolision.gameObject.name == "Raqueta Derecha")
        {
            int x = micolision.gameObject.name == "Raqueta Izquierda" ? 1 : -1;
            int y = direccionY(transform.position, micolision.transform.position);
            Vector2 direccion = new Vector2(x, y);
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            fuenteDeAudio.PlayOneShot(audioRaquete);
        }
        else if (micolision.gameObject.name == "Arriba" || micolision.gameObject.name == "Abajo")
        {
            fuenteDeAudio.PlayOneShot(audioRebote);
        }

    }
    public void reiniciarBola(string direccion)
    {
        transform.position = Vector2.zero;

        velocidad = 30 + (golesDerecha + golesIzquierda) * 5;


        if (direccion == "Derecha")
        {
            golesDerecha++;
            contadorDerecha.text = golesDerecha.ToString();

            GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        }
        else if (direccion == "Izquierda")
        {
            golesIzquierda++;
            contadorIzquierda.text = golesIzquierda.ToString();

            GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
        }

        if (golesDerecha >= 5 || golesIzquierda >= 5)
        {
            this.doEnd();
        }
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }
    public void doEnd()
    {
        GameManager.Ganador = golesDerecha > golesIzquierda ? "Derecha" : "Izquierda";
        SceneManager.LoadScene("Inicio");
    }
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        // get the difference in y between the ball and the racket
        float diferencia = posicionRaqueta.y - posicionBola.y;
        // debug the difference
        Debug.Log(diferencia);

        // if the absolute value of the difference is greater than 1.5, the it should move up or down
        // otherwise it should go straight

        if (Mathf.Abs(diferencia) > 1.5)
        {
            // if the difference is positive, the ball should move up
            if (diferencia > 0)
            {
                return -1;
            }
            // if the difference is negative, the ball should move down
            else
            {
                return 1;
            }
        }
        else
        {
            return 0;
        }
    }

    void FixedUpdate()
    {
        velocidad = velocidad + 0.1f;

        // create a timer to count the time
        timer += Time.deltaTime;

        // display the time in the text with the format "mm:ss"
        txtTiempo.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timer / 60), timer % 60);
        // if the timer is greater than 3 minutes, end the game
        if (timer >= 180)
        {
            GameManager.Ganador = golesDerecha > golesIzquierda ? "Derecha" : "Izquierda";
            SceneManager.LoadScene("Inicio");
        }
    }
}
