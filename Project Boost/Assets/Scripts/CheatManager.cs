using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private Collider _playerCollider;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        _playerCollider = player.GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SwapCollisionActivity();
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (SceneManager.sceneCountInBuildSettings <= nextSceneIndex)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

    private void SwapCollisionActivity()
    {
        _playerCollider.enabled = !_playerCollider.enabled;
    }
}