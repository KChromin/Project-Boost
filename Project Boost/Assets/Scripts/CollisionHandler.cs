using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Movement playerMovement;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip crashSound;

    [SerializeField]
    private AudioClip finishSound;

    [SerializeField]
    private float delayAfterCrashInSeconds;

    [SerializeField]
    private float delayAfterFinishInSeconds;

    private bool inTransition;

    private void Awake()
    {
        inTransition = false;
        playerMovement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (inTransition) return;

        if (other.gameObject.CompareTag("FriendlyCollisions"))
        {
            return;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            StartFinishSequence();
            return;
        }

        StartCrashSequence();
    }


    private void StartCrashSequence()
    {
        inTransition = true;
        Invoke(nameof(ReloadScene), delayAfterCrashInSeconds);
        PlaySoundCrash();
        playerMovement.enabled = false;
    }

    void StartFinishSequence()
    {
        inTransition = true;
        Invoke(nameof(LoadNextLevel), delayAfterFinishInSeconds);
        PlaySoundFinish();
        playerMovement.enabled = false;
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
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

    private void PlaySoundCrash()
    {
        audioSource.PlayOneShot(crashSound);
    }

    private void PlaySoundFinish()
    {
        audioSource.PlayOneShot(finishSound);
    }
}