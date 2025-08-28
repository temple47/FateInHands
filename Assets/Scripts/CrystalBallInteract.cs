using UnityEngine;
using UnityEngine.UI;   // Needed for UI
using TMPro;            // If using TextMeshPro
using UnityEngine.SceneManagement;

public class CrystalBallInteract : MonoBehaviour
{
    public string sceneToLoad = "NextScene"; // Set in Inspector
    public KeyCode keyToPress = KeyCode.E;   // The key player presses
    public GameObject pressKeyUI;            // Assign the UI Text object

    private bool playerInTrigger = false;

    private void Start()
    {
        if (pressKeyUI != null)
            pressKeyUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (pressKeyUI != null)
                pressKeyUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            if (pressKeyUI != null)
                pressKeyUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(keyToPress))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
