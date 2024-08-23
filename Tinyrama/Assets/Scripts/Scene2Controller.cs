using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene2Controller : MonoBehaviour
{
    // Reference to the button in the scene
    public Button previousLevelButton;

    private void Start()
    {
        // Ensure LevelLoader is accessible and the button is valid
        if (LevelLoader.instance != null && previousLevelButton != null)
        {
            // Assign the LevelLoader's function to the button's onClick event
            previousLevelButton.onClick.AddListener(LevelLoader.instance.OnButtonLoadPreviousLevel);
        }
        else
        {
            Debug.LogError("LevelLoader instance or Button reference is missing!");
        }
    }
}