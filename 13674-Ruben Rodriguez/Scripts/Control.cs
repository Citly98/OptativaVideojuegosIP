using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public int vidas = 0;
    public int Velocidad = 0;

    public float giro = 0;

    public float Horizontal = 0;

    public float vertical = 0;

    public Rigidbody rb;

    public float altura_salto=0;

    bool esta_en_suelo;

    public float limite_x = 0;

    public float limite_z = 0;

    public Transform Respawn_zone;
    public Transform Bandera1;
    public Transform Bandera2;

    public Transform Bandera3;
    public Transform Bandera4;

    public Text GAME_OVER;
    public Text CONTINUAR;

    public Text ELIGE_DIFICULTAD;
    public Text VIDAS_FALTANTES;

    public Text Final;
    public Text jugar_otra_vez;

    public Text pausar;

    public string Dificultad;

    public GameObject viga;

    public GameObject col1;
    public GameObject col2;
    public GameObject col3;
    public GameObject col4;

    public GameObject muro1;
    public GameObject muro2;
    public GameObject muro3;
    public GameObject muro4;

    public int teclaS=0;

    public int pausado = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        esta_en_suelo = true;
        GAME_OVER.enabled = false;
        CONTINUAR.enabled = false;
        Final.enabled = false;
        jugar_otra_vez.enabled = false;
        VIDAS_FALTANTES.enabled = false;
        Velocidad = 0;
        giro = 0;
        altura_salto = 0;
        pausar.enabled=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            continuar();
        }

        if (Input.GetKeyDown(KeyCode.T)){
            salir();
        }

        if (Input.GetKeyDown(KeyCode.P)){
            pausa();
        }
        
        if (Input.GetKeyDown(KeyCode.S)){
            nivel();
        }
        
        if (Input.GetKeyDown(KeyCode.N)){
                Velocidad = 10;
                giro = 15;
                altura_salto = 7;
                ELIGE_DIFICULTAD.enabled = false;
                VIDAS_FALTANTES.enabled = true;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                teclaS=1;
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && esta_en_suelo == true){
            Salto();
        }
        Horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * Velocidad * vertical);
        transform.Translate(Vector3.right * Time.deltaTime * giro * Horizontal);
        if(vidas > 0){
            
            if((transform.position.x >= limite_x || transform.position.x <= -1 * limite_x) && (transform.position.z <= -59.71) ){
                vidas = vidas -1;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                this.transform.position = Respawn_zone.transform.position;
            }

            if((transform.position.x >= limite_x || transform.position.x <= -1 * limite_x) && (transform.position.z >= -59.7 && transform.position.z <= -16.9) ){
                vidas = vidas -1;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                this.transform.position = Bandera1.transform.position;
            }

            if((transform.position.x >= limite_x || transform.position.x <= -1 * limite_x) && (transform.position.z >= -16.8 && transform.position.z <= 8.85) ){
                vidas = vidas -1;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                this.transform.position = Bandera2.transform.position;
            }

            if((transform.position.x >= limite_x || transform.position.x <= -1 * limite_x) && (transform.position.z >= 8.86 && transform.position.z <= 45.73)){
                vidas = vidas -1;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                this.transform.position = Bandera3.transform.position;
            }

            if((transform.position.x >= limite_x || transform.position.x <= -1 * limite_x) && (transform.position.z >= 45.74 && transform.position.z <= 57.00)){
                vidas = vidas -1;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                this.transform.position = Bandera4.transform.position;
            }


            if(transform.position.z <= -1 * limite_z){
                vidas = vidas -1;
                VIDAS_FALTANTES.text="VIDAS " + vidas;
                this.transform.position = Respawn_zone.transform.position;
            }
            
        }
        else{
                VIDAS_FALTANTES.enabled = false;
                GAME_OVER.enabled = true;
                CONTINUAR.enabled = true;
                Velocidad = 0;
                giro = 0;
                altura_salto = 0;
                rb.transform.localScale = new Vector3 (0,0,0);
                rb.transform.localPosition = new Vector3 (0,0,-95);

                if(Input.GetKeyDown(KeyCode.C)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }    
        }

        if(transform.position.x >= -3.47 && transform.position.x <= 3.52 && transform.position.z >= 57 && transform.position.z <= 60 ){
            VIDAS_FALTANTES.enabled = false;
            Final.enabled = true;
            jugar_otra_vez.enabled = true;
            Velocidad = 0;
            giro = 0;
            altura_salto = 0;

            if(Input.GetKeyDown(KeyCode.V)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }    
        }
        /*CODIGO KONAMI. Este codigo NO se le menciona al jugador, el desarrollador lo usa para avanzar 
        entre las 4 fases y probarlas*/
        if(Input.GetKeyDown(KeyCode.Y) && Input.GetKeyDown(KeyCode.U)){
            this.transform.position = Bandera1.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Y) && Input.GetKeyDown(KeyCode.I)){
            this.transform.position = Bandera2.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Y) && Input.GetKeyDown(KeyCode.O)){
            this.transform.position = Bandera3.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Y) && Input.GetKeyDown(KeyCode.B)){
            this.transform.position = Bandera4.transform.position;
        }
        /*CODIGO KONAMI. Este codigo NO se le menciona al jugador, el desarrollador lo usa para avanzar 
        entre las 4 fases y probarlas*/
    }


    void pausa(){
        Velocidad = 0;
        giro = 0;
        altura_salto = 0;
        pausar.enabled=true;
        pausado=1;
        
    }

    void continuar(){
        if(pausado==1){
            Velocidad = 10;
            giro = 15;
            altura_salto = 7;
            pausado=0;
            pausar.enabled=false;
            
        }
        
    }

    void salir(){
        if(pausado==1){
            pausado=0;
            pausar.enabled=false;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            
        }
    }
    void nivel(){
        if(teclaS==0){
            viga.transform.localScale = new Vector3 (2,1,4);
            col1.transform.localScale = new Vector3 (2,2,2);
            col2.transform.localScale = new Vector3 (2,4,2);
            col3.transform.localScale = new Vector3 (2,6,2);
            col4.transform.localScale = new Vector3 (2,8,2);
            muro1.transform.localScale = new Vector3 (2,2,4);
            muro2.transform.localScale = new Vector3 (2,2,4);
            muro3.transform.localScale = new Vector3 (2,2,4);
            muro4.transform.localScale = new Vector3 (2,2,4);
            Velocidad = 10;
            giro = 15;
            altura_salto = 7;
            ELIGE_DIFICULTAD.enabled = false;
            VIDAS_FALTANTES.enabled = true;
            VIDAS_FALTANTES.text="VIDAS " + vidas;
            teclaS = 1;
        }
        
    } 
    void Salto(){
        esta_en_suelo = false;
        rb.AddForce(0,altura_salto,0,ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Suelo")){
            esta_en_suelo = true;
        }
        if(other.gameObject.CompareTag("mue1")){
            vidas = vidas -1;
            VIDAS_FALTANTES.text="VIDAS " + vidas;
            this.transform.position = Respawn_zone.transform.position;
        }
        
        if(other.gameObject.CompareTag("m1")){
            vidas = vidas -1;
            VIDAS_FALTANTES.text="VIDAS " + vidas;
            this.transform.position = Bandera1.transform.position;
        }

        if(other.gameObject.CompareTag("m2")){
            vidas = vidas -1;
            VIDAS_FALTANTES.text="VIDAS " + vidas;
            this.transform.position = Bandera2.transform.position;
        }

        if(other.gameObject.CompareTag("m3")){
            vidas = vidas -1;
            VIDAS_FALTANTES.text="VIDAS " + vidas;
            this.transform.position = Bandera3.transform.position;
        }
    }

   
}

