using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    private Vector3 posicionRelativa;
    void Start()
    {
        posicionRelativa = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = ball.transform.position;
        transform.position = ball.transform.position + posicionRelativa;
    }

  
}
