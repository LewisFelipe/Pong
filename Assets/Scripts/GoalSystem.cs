using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GoalSystem : MonoBehaviour
{
    public Text scoreText;
    public Transform spawnBolaPos;
    public GameObject bolaPrefab;
    public bool isPlayer2;
    public int scorePlayer1 = 0, scorePlayer2 = 0;
    GameObject bolaEncosta;

    private void Update()
    {
        if (isPlayer2)
        {
            scoreText.text = scorePlayer2.ToString();
        }
        else
            scoreText.text = scorePlayer1.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!isPlayer2)
            scorePlayer1++;
        else
            scorePlayer2++;

        bolaEncosta = collider.gameObject;
        StartCoroutine("BolaNova");
    }
    IEnumerator BolaNova()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(bolaEncosta);
        Instantiate(bolaPrefab, spawnBolaPos.position, Quaternion.identity);
    }
}
