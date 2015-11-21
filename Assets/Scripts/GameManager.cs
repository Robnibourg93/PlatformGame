﻿using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject player;
    private GameObject currentPlayer;
    private GameCamera cam;
    private Vector3 checkpoint;

    public static int levelCount = 3;
    public static int currentLevel = 1;

	// Use this for initialization
	void Start () {
        cam = GetComponent<GameCamera>();
        if (GameObject.FindGameObjectWithTag("Spawn")) {
            checkpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        }
        SpawnPlayer(checkpoint);

	}

    private void SpawnPlayer(Vector3 spawnPos) {
        currentPlayer = Instantiate(player, spawnPos, Quaternion.identity) as GameObject;
        cam.SetTarget(currentPlayer.transform);
    }

    void Update() {
        if (!currentPlayer)
        {
            if (Input.GetButtonDown("Respawn"))
            {
                SpawnPlayer(checkpoint);
            }
        }
    }

    public void SetCheckpoint(Vector3 cp) {
        checkpoint = cp;
    }

    public void EndLevel()
    {
        if (currentLevel < levelCount) {
            currentLevel++;
            Application.LoadLevel("level "+currentLevel);
        }
    }
}
