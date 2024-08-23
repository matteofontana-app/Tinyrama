using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene1Controller : MonoBehaviour
{
    // Reference to the button in the scene
    public Button nextLevelButton;

    private void Start()
    {
        // Ensure LevelLoader is accessible and the button is valid
        if (LevelLoader.instance != null && nextLevelButton != null)
        {
            // Assign the LevelLoader's function to the button's onClick event
            nextLevelButton.onClick.AddListener(LevelLoader.instance.OnButtonLoadNextLevel);
        }
        else
        {
            Debug.LogError("LevelLoader instance or Button reference is missing!");
        }
    }
}