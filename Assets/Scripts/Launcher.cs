using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// formula movimiento parabolico xmax = (v0pow2/g) sen 2a

public class Launcher : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocidadIni;
    public float angulo;
    public float xpredic;
    public GameObject ball;
    private Rigidbody2D rigidbody2D;
    public GameObject mensaje;
    public Text xmax;
    public Text velocidadInitext;
    public Text angulotext;
    public Text xpredictext;
    public Button lanzarB;
    public Button ReiniciarB;
    public float maximox;
    private bool lanzamiento;
    public GameObject canvas;
    public Text errorVelocidad;
    public Text errorAngulo;
    public Text errorXpredic;
    public bool form;
    public GameObject CanvasFinal;
    public Text MensajeFinal;





    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
        lanzarB.onClick.AddListener(lanzar);
        ReiniciarB.onClick.AddListener(reiniciar);
        lanzamiento = false;
        form = true;
        CanvasFinal.SetActive(false);
        MensajeFinal.text = "";

        //rigidbody2D.velocity = new Vector3(velocidadIni*, , 0);
        //lanzar();
    }
    public void reiniciar(){
        SceneManager.LoadScene("inicio");
    }

    public void lanzar(){
        form = true;

        try
        {
            velocidadIni = float.Parse(velocidadInitext.text);
            if(velocidadIni <= 0 ){
                 form = false;
                 Debug.Log("velocidad 0");
                 errorVelocidad.text = "La velocidad no puede ser negativa y con 0 no dispara el proyectil";
            }else{
                errorVelocidad.text = "";
            }
            
        }
        catch (System.Exception)
        {
            form = false;
            errorVelocidad.text = "La Velocidad debe ser un numero decimal y no puede estar vacio";
        }

        try
        {
            angulo = float.Parse(angulotext.text);
            if(angulo < 0 || angulo > 90){
                 form = false;
                 errorAngulo.text = "El Angulo debe ser un numero comprendido entre 0 y 90";
            }else{
                errorAngulo.text = "";
            }
        }
        catch (System.Exception)
        {
            form = false;
            errorAngulo.text = "El Angulo debe ser un numero decimal y no puede estar vacio";
        }
        try
        {
            xpredic = float.Parse(xpredictext.text);
            if(xpredic < 0 ){
                 form = false;
                 errorXpredic.text = "Prediccion de X final no puede ser negativa";
            }else{
                errorXpredic.text = "";
            }
            errorXpredic.text = "";
        }
        catch (System.Exception)
        {
            form = false;
            errorXpredic.text = "Prediccion de X final debe ser un numero y no puede estar vacio";
        }
        

        /* velocidadIni = float.Parse(velocidadInitext.text);
        angulo = float.Parse(angulotext.text);
        xpredic = float.Parse(xpredictext.text); */




        if(form){
            float randians = angulo * Mathf.Deg2Rad;
            //Debug.Log(randians);
            float velocidadlanzamiento =0;
            if(velocidadIni > 12){
                velocidadlanzamiento = 12;
            }else{
                velocidadlanzamiento = velocidadIni;
            }
            lanzamiento = true;
            rigidbody2D.velocity = new Vector3(velocidadlanzamiento* Mathf.Cos(randians),velocidadlanzamiento* Mathf.Sin(randians) , 0);
            Debug.Log(Mathf.Sin(randians));
            maximox = (velocidadIni*velocidadIni*Mathf.Sin(randians*2 ))/9.8f;

            Debug.Log(maximox);
            
            canvas.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionStay2D(Collision2D other) {
        
         if(lanzamiento || other.collider.tag != "Respawn"){
            xmax.text = maximox.ToString("F3") + " mts";
            mensaje.SetActive(true);
            
            CanvasFinal.SetActive(true);
            float PorcentajeError = ((maximox - xpredic)/(maximox))*100 ;
            Debug.Log(PorcentajeError);
            if(PorcentajeError<=5){
                MensajeFinal.text = "Felicitaciones lograste acertar el lugar de caida";
            }else{
                MensajeFinal.text = "No lograste acertar el lugar de caida";
            }

            //if(maximo)
            
         }


            
        
        
    }

    void OnCollisionExit2D(Collision2D other) {
        mensaje.SetActive(false);
        xmax.text = "";
    }


}
