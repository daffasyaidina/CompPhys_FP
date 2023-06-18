using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void SwitchToPenalty()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchToFreekick()
    {
        SceneManager.LoadScene(1);
    }
}
