using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedSwitch : MonoBehaviour
{
    // Public GameObject to be assigned through the Inspector or another script
    public GameObject firstSelectedGameObject;

    // This method can be called on button click
    public void SetFirstSelected()
    {
        // Ensure the EventSystem is available in the scene
        EventSystem eventSystem = EventSystem.current;

        if (eventSystem != null && firstSelectedGameObject != null)
        {
            // Set the first selected GameObject
            eventSystem.firstSelectedGameObject = firstSelectedGameObject;

            // Optionally, you can also select the GameObject immediately
            eventSystem.SetSelectedGameObject(firstSelectedGameObject);
        }
        else
        {
            Debug.LogWarning("Event System or First Selected GameObject is not assigned.");
        }
    }
}
