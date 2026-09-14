
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Delays")]
    [SerializeField] float reloadDelay = 2f;
    [SerializeField] float nextLevelDelay = 2f;
    [Header("Audio Clips")]
    [SerializeField] AudioClip finishSFX;
    [SerializeField] AudioClip crashSFX;
    [Header("Particles")]
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isControllable = true;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    void OnCollisionEnter(Collision collision)
    {

        if (!isControllable) { return; }

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
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false; // turning off movement when finished

        successParticles.Play();
        audioSource.PlayOneShot(finishSFX);

        Invoke("LoadNextLevel", nextLevelDelay); // 2 second delay
        // any code after Invoke (in the same method) works just fine.
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false; // turning off movement when crashed

        crashParticles.Play();
        audioSource.PlayOneShot(crashSFX);

        Invoke("ReloadLevel", reloadDelay); // 2 second delay
        // any code after Invoke (in the same method) works just fine.
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
