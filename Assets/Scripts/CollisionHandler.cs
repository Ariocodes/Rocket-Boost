using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay = 2f;
    [SerializeField] float nextLevelDelay = 2f;

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartFinishSequence()
    {
        // TODO: add sfx and particles
        GetComponent<Movement>().enabled = false; // turning off movement when crashed
        Invoke("LoadNextLevel", nextLevelDelay); // 2 second delay
    }

    void StartCrashSequence()
    {
        // TODO: add sfx and particles
        GetComponent<Movement>().enabled = false; // turning off movement when crashed
        Invoke("ReloadLevel", reloadDelay); // 2 second delay
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
        
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
