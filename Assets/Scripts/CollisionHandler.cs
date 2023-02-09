using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    Movement movement;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) LoadNextLevel();
        else if (Input.GetKeyDown(KeyCode.C)) collisionDisabled = !collisionDisabled;
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                // Something
                break;
            case "Finish":
                HandleFinish();
                break;
            default:
                HandleCrashing();
                break;
        }
    }

    void HandleCrashing()
    {
        isTransitioning = true;
        movement.enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();

        Invoke("ReloadLevel", levelLoadDelay);
    }

    void HandleFinish()
    {
        isTransitioning = true;
        movement.enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(finishAudio);
        finishParticles.Play();

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }
}
