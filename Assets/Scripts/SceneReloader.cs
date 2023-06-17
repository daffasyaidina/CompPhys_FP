using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Get the current scene name
            string sceneName = SceneManager.GetActiveScene().name;

            // Load the current scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
