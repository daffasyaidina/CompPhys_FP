using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private int currentSceneIndex = 0; // Initial scene index

    public void SwitchScene()
    {
        currentSceneIndex = (currentSceneIndex + 1) % 2; // Increment scene index cyclically

        switch (currentSceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Penalty");
                break;
            case 1:
                SceneManager.LoadScene("Freekick");
                break;
            default:
                Debug.LogError("Invalid scene index: " + currentSceneIndex);
                break;
        }
    }
}
