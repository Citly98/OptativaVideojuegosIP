using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Control : MonoBehaviour
{
    public int Velocidad = 0;
    public float giro = 0;

    public int vidas = 0;
    public float limite_x = 0;
    public float limite_z = 0;
    public float limite_y = 0;
    public float Horizontal = 0;
    public float vertical = 0;

    public Rigidbody rb;

    public float altura_salto=0;
    //public int Limites_saltos = 0;
    bool esta_en_suelo;
     public Transform Respawn_zone;
     public Text GAME_OVER;
     public Text CONTINUAR;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        esta_en_suelo = true;
        GAME_OVER.enabled = false;
        CONTINUAR.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        /* 
        && AND O "Y"
        || OR - "O"
        */
        
        if(Input.GetKeyDown(KeyCode.Space) && esta_en_suelo == true){ 
          Jump();
        }
        Horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //ASIGNAR VELOCIDAD EN EJE Z
        transform.Translate(Vector3.forward * Time.deltaTime * Velocidad * vertical);
        //ASIGNAR VELOCIDAD EN EJE X
        transform.Translate(Vector3.right * Time.deltaTime * giro * Horizontal);
        //DEATH ZONE eje x(IZQUIERDA DERECHA)
            Debug.Log(vidas);
        if(vidas > 0){
                if(transform.position.x > limite_x || transform.position.x < -limite_x){
            vidas = vidas - 1;
            this.transform.position = Respawn_zone.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(transform.position.z > limite_z || transform.position.z < -limite_z){
            vidas = vidas - 1;
            this.transform.position = Respawn_zone.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
         if(transform.position.y > limite_y || transform.position.y < -limite_y){
            vidas = vidas - 1;
            this.transform.position = Respawn_zone.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        } else{
        
        // Destroy(this.gameObject);
        GAME_OVER.enabled = true;
        CONTINUAR.enabled = true;
        if(Input.GetKeyDown(KeyCode.C)){ 
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        }





    
    }

    void Jump(){
        esta_en_suelo = true;
        rb.AddForce(0,altura_salto,0, ForceMode.Impulse);
    }

     void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Suelo")){
            esta_en_suelo = true;
        }
    }
}
