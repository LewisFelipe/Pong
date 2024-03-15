using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    //float mov;
    public Vector3 grauVisaoBot;
    public bool isPlayerOne;
    public float movSpeed;
    public GameObject[] bounds; // 0 e baixo 1 e cima 
    public KeyCode baixo, cima;
    public bool isBot;
    public LayerMask bolaLM;

    private void Update()
    {
      //  mov = Input.GetAxis("Vertical");
      if(isPlayerOne)
        {
            cima = KeyCode.W;
            baixo = KeyCode.S;
        }
      else
        {
            cima = KeyCode.UpArrow;
            baixo = KeyCode.DownArrow;
        }

        if (!isOnBounds() && GameObject.FindGameObjectWithTag("GameManager").GetComponent<LocalManager>().startedGame)
        {
            if (!isBot)
            {
                transform.Translate(0, 0, Mov() * movSpeed * Time.deltaTime);
            }
            else
            {
                if(GameObject.FindGameObjectWithTag("Bola") != null)
                {
                    if(PercepcaoBot())//GameObject.FindGameObjectWithTag("Bola").GetComponent<Rigidbody>().velocity.x > 0)
                    {
                        if(GameObject.FindGameObjectWithTag("Bola").transform.position.z > transform.position.z)
                        {
                            transform.Translate(Vector3.Lerp(transform.position, new Vector3(0, 0, movSpeed * Time.deltaTime), 1));
                        }
                        else if (GameObject.FindGameObjectWithTag("Bola").transform.position.z < transform.position.z)
                        {
                            transform.Translate(Vector3.Lerp(transform.position, new Vector3(0, 0, -movSpeed * Time.deltaTime), 1));
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, grauVisaoBot);
    }

    bool PercepcaoBot()
    {
        bool enxergaBola = false;
        Collider[] col = Physics.OverlapBox(transform.position, grauVisaoBot, Quaternion.identity, bolaLM);
        if (col.Length > 0)
        {
            enxergaBola = true;
        }
        else
            enxergaBola = false;

        return enxergaBola;
    }

    float Mov()
    {
        if (Input.GetKey(cima))
        {
            return 1;
        }
        else if (Input.GetKey(baixo))
        {
            return -1;
        }
        else
            return 0;
    }

    private bool isOnBounds()
    {
        float whatBound = 3.250544f * Mov();
        if (transform.position.z >= whatBound  && Mov() > 0 ||
            transform.position.z <= whatBound && Mov() < 0)
        {
            return true;
        }
        else
            return false;
    }

}
