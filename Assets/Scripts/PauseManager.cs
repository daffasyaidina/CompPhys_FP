using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pauseMenu;

    private bool isPaused = false;
    private float previousTimeScale;

    private void Start()
    {
        // Add a click listener to the pause button
        pauseButton.onClick.AddListener(TogglePause);
    }

    private void Update()
    {
        // Check for the pause input (e.g., using the Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        // Pause or resume the game
        if (isPaused)
        {
            previousTimeScale = Time.timeScale; // Store the previous time scale
            Time.timeScale = 0f; // Set the time scale to 0 to pause the game
            pauseMenu.SetActive(true); // Show the pause menu
        }
        else
        {
            Time.timeScale = previousTimeScale; // Restore the previous time scale
            pauseMenu.SetActive(false); // Hide the pause menu
        }
    }

    public void ExitGame()
    {
        // Quit the game when the exit button is clicked (you can modify this behavior if needed)
        Application.Quit();
    }
}
