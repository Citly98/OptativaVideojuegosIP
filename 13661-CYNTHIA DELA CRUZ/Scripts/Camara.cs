using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject Personaje;

    public int ejex = 0;

    public int ejey = 0;

    public int ejez = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    transform.position = Personaje.transform.position + new Vector3(ejex,ejey,ejez);    
    }
}

