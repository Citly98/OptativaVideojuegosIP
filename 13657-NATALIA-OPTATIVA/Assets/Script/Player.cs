using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    Rigidbody rb; //me sirve para moverme
    public float speedX = 3; //velocidad con laque corre el player o jugador

    bool enSuelo = true; //cuando el cubo esta en el suelo
    public float fuerzaSalto = 150; //fuerza salto
    static int hp = 5; //vidas
    bool puedoMoverme = true; //para poder y detectar si puedo moverme
    public Image barraVerde; //esta barra es el me contara las vidas
    bool puedoSerGolpeado = true; // esta si yo me golpeo contra el muro 
    public Material enemyMaterial; //cuando subo las escaleras se pone un color depende de que nivel sea 
    public GameObject GameOver; // si llego a la pantalla de game over
    // Start is called before the first frame update
    private void Awake() //la llamada al script o sea cuando se arranca el juego
    {
        if (hp <= 0) hp = 5; //me cuenta las vidas cuando me las acabo
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // rb o rigidbody es la fisica que permite dar movimiento alobjeto
        GameOver.SetActive(false); //cuando no esta activado el game over
        
    }
    

    // Update is called once per frame
    void Update()
    {
        barraVerde.fillAmount = (float)hp / 5; // es la barra que da mis vidas
        if (hp >= 1) //Si tengo mas de 1 de vida puedo ejecutar todo lo de abajo
        {
            if (puedoMoverme) //Si puedo Moverme entonces puedo ejecutar todo lo de abajo
            {
                float movX = Input.GetAxis("Horizontal"); //eje horizontal de movimiento
                rb.velocity = new Vector3(speedX * movX, rb.velocity.y, 0); //velocidad en X,Y,Z el speed es el es la velocidad con la que se mueve el personaje
                transform.position = new Vector3(transform.position.x, transform.position.y, -1.69f); //congelar profunidad cubo a -1.69
                if (Input.GetKeyDown(KeyCode.Space)) //si pulso espacio
                {
                    if (enSuelo) //si estoy en el suelo
                    {
                        rb.velocity = Vector2.zero; // salto hacia arriba
                        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode.Impulse); //salto hacia arriba
                    }

                }
            }
        }
        
        Camera.main.transform.position = transform.position + new Vector3(3, 1, -7); //posicion de la camara
    }

  

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "enemy") //Si colisiono con un objeto con el tag "enemy"
        {
            if (puedoSerGolpeado) //si caigo me puedo golpear
            {
                puedoSerGolpeado = false; //impide volver a colisionar
                hp -= 1; //pierdo uno de vida
                puedoMoverme = false;

                if (hp >= 1)
                {
                    StartCoroutine(ReiNivel()); //Si tengo mas de 1 de vida reinicia el nivel
                }
                else
                {
                    StartCoroutine(ReiTodo()); //En caso contrario reinicia todo
                }
            }
        }

        if (collision.collider.tag =="victoria")
        {
            if (puedoSerGolpeado)
            {
                if (hp>=1)
                {
                    Debug.Log("GanarColision");
                    StartCoroutine(Ganar()); //Si colisiono con un objeto con tag victoria el cuadro azul  y estoy vivo (hp>=1) entonces activa Game over
                    
                }
            }
        }

        if (collision.collider.tag == "victoria2")
        {
            if (puedoSerGolpeado)
            {
                if (hp >= 1)
                {
                    StartCoroutine(Ganar2()); //Aqui se activa el game over2 del mismo juego

                }
            }
        }

        if (collision.collider.tag == "escalon") //si choco con el esacalon
        {
            if (transform.position.y >= collision.gameObject.transform.position.y) //Y si mi posicion es mayor a la del escalon
            {
                enSuelo = true; //entonces estoy en el suelo y puedo saltar
            }
        }

       
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag =="suelo")
        {
            enSuelo = true; //Igual que escalon o si estoy abajo
        }

        if (collision.collider.tag == "escalon")
        {
            if (transform.position.y >= collision.gameObject.transform.position.y)
            {
                enSuelo = true; //igual abajo o arriba
            }
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "suelo") //Si salgo de la colision con el objeto con tag suelo entonces no estoy en el suelo, y no puedo saltar (estoy en el aire)
        {
            enSuelo = false;
        }

        if (collision.collider.tag == "escalon") //igual que con el suelo, pero le cambia el enemigo a rojo el suelo
        {
            enSuelo = false;
            collision.gameObject.tag = "enemy";//color y suelo rojo
            collision.gameObject.GetComponent<MeshRenderer>().material = enemyMaterial;
        }
    }
    int escenaActual()
    {
        return SceneManager.GetActiveScene().buildIndex; //Detecta la escena en la que estamos jugando
    }
    IEnumerator Ganar2() // Activa game over y tras  si me tardo en presionar carga el nivel 1
    {
        yield return new WaitForSeconds(0.5f);
        GameOver.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(5);//depende de los segundos que hagamos 
        Time.timeScale = 1;
        SceneManager.LoadScene(0);//reinicia la escena
    }
    IEnumerator Ganar() //Igual pero espera segundos para cargar el siguiente nivel
    {
        yield return new WaitForSeconds(0.5f);
        GameOver.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        SceneManager.LoadScene(escenaActual() + 1);
    }
    IEnumerator ReiNivel()
    {
        Time.timeScale = 0.8f;
        yield return new WaitForSecondsRealtime(1.5f); //Reinicia el nivel en el que estamos en segundos
        Time.timeScale = 1;
        SceneManager.LoadScene(escenaActual());

    }
    IEnumerator ReiTodo() //me lleva al nivel 0 y reinicia todas mis variables.
    {
        Time.timeScale = 0.8f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    

}
