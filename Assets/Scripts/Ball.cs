using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Ball : MonoBehaviour
{
    GameObject ultimoPlayerEncostado;
    public float forcaRebate, forcaDiagonal;
    int escolheLadoX, escolheLadoZ;
    public LocalManager lm;
    Rigidbody rb;
    public float ballSpeedX;
    public float ballSpeedZ;
    bool gameStart;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        lm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LocalManager>();
        StartCoroutine("Inicio");
    }
    IEnumerator Inicio()
    {
        yield return new WaitUntil(() => lm.startedGame);
        yield return new WaitForSeconds(0.5f);
        // Vector3 pos = gameObject.transform.position;
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomNumX = Random.Range(0, 2);
        switch(randomNumX)
        {
            case 0:
                escolheLadoX = -1;
                break;
            case 1:
                escolheLadoX = 1;
                break;
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomNumZ = Random.Range(2, 4);
        switch(randomNumZ)
        {
            case 2:
                escolheLadoZ = 1;
                break;
            case 3:
                escolheLadoZ = -1;
                break;
        }
        switch (gameStart)
        {
            case false:
                rb.AddForce(new Vector3(ballSpeedX * escolheLadoX, 0,ballSpeedZ * escolheLadoZ), ForceMode.Impulse);
                break;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            rb.AddForce(forcaRebate * col.gameObject.transform.parent.gameObject.transform.forward);
            ultimoPlayerEncostado = col.gameObject;
        }
        if(col.gameObject.tag == "Bounds")
        {
            if (ultimoPlayerEncostado != null)
            {
                rb.AddForce((forcaDiagonal / rb.velocity.x) * ultimoPlayerEncostado.transform.parent.gameObject.transform.forward);
            }
        }
    }
}
