using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField] private Animator transitionAnim;

    private void Awake()
    {
        // Singleton Pattern to ensure only one instance of LevelLoader exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject); // Destroy duplicates if an instance already exists
        }
    }

    // Wrapper method for the button to call with no parameters
    public void OnButtonLoadNextLevel()
    {
        LoadNextLevel(3f, 3f); // You can hardcode or customize the values here
    }

    public void OnButtonLoadPreviousLevel()
    {
        LoadPreviousLevel(0.5f, 0.5f); // Customize values based on your needs
    }

    public void LoadNextLevel(float startTransitionTime, float endTransitionTime)
    {
        // Start loading the next level with the transition
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, "Start1", "Start2", startTransitionTime, endTransitionTime));
    }

    public void LoadPreviousLevel(float startTransitionTime, float endTransitionTime)
    {
        // Start loading the previous level with the transition
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1, "End1", "End2", startTransitionTime, endTransitionTime));
    }

    IEnumerator LoadLevel(int sceneNumber, string trans1, string trans2, float startTransitionTime, float endTransitionTime)
    {
        if (transitionAnim != null)
        {
            // Start the transition animation with the specified trigger
            transitionAnim.SetTrigger(trans1);

            // Wait for the start transition animation to complete
            yield return new WaitForSeconds(startTransitionTime);

            // Load the specified scene
            SceneManager.LoadScene(sceneNumber);

            // Optionally trigger another animation after the scene is loaded
            transitionAnim.SetTrigger(trans2);

            // Wait for the end transition animation to complete
            yield return new WaitForSeconds(endTransitionTime);
        }
        else
        {
            Debug.LogError("Animator not assigned to LevelLoader.");
        }
    }
}
