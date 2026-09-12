
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay = 2f;
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] AudioClip crashSFX;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



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
        GetComponent<Movement>().enabled = false; // turning off movement when finished
        audioSource.PlayOneShot(finishSFX);
        Invoke("LoadNextLevel", nextLevelDelay); // 2 second delay
    }

    void StartCrashSequence()
    {
        // TODO: add sfx and particles
        GetComponent<Movement>().enabled = false; // turning off movement when crashed
        audioSource.PlayOneShot(crashSFX);
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
