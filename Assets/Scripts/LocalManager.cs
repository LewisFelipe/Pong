using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LocalManager : MonoBehaviour
{
    public GameObject[] labels; // 0 e barra de espaço e 1 e setas
    public bool startedGame;
    int playerCount = 0;
    public GameObject playerPrefab, player1, botPrefab;
    Vector3 player2Pos;
    private void Start()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerCount++;
            if (playerCount == 1)
            {
                player1 = player;
                player.GetComponent<PlayerMov>().isPlayerOne = true;
            }
        }
        player2Pos = new Vector3(player1.transform.position.x * -1, player1.transform.position.y,
    player1.transform.position.z);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            startedGame = true;
            foreach(var label in labels)
            {
                if (label != null)
                {
                    label.SetActive(false);
                }
            }
        }

        if(playerCount < 2 && !startedGame)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Instantiate(playerPrefab, player2Pos, Quaternion.Euler(0, -90, 180));
                labels[1].SetActive(false);
                playerCount++;
            }
        }
        else if(startedGame && playerCount == 1)
        {
            //spawnar bot
            Instantiate(botPrefab, player2Pos, Quaternion.Euler(0, -90, 180));
            playerCount++;
        }
    }
}
