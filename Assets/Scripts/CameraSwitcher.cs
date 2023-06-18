using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;

    public Button buttonCamera1;
    public Button buttonCamera2;
    public Button buttonCamera3;

    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;

    private Camera currentCamera;

    private void Start()
    {
        // Enable the first camera and disable the others by default
        camera1.enabled = true;
        camera2.enabled = false;
        camera3.enabled = false;

        currentCamera = camera1;

        // Disable the button for the current camera
        DisableButtonForCurrentCamera();

        // Disable the balls for the current camera
        DisableBallsForCurrentCamera();
    }

    private void Update()
    {
        // Disable the button for the current camera
        DisableButtonForCurrentCamera();

        // Check for number key input to switch cameras
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCamera(camera1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCamera(camera2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCamera(camera3);
        }
    }

    public void SwitchToCamera1()
    {
        SwitchCamera(camera1);
    }

    public void SwitchToCamera2()
    {
        SwitchCamera(camera2);
    }

    public void SwitchToCamera3()
    {
        SwitchCamera(camera3);
    }

    private void SwitchCamera(Camera newCamera)
    {
        if (currentCamera != null)
        {
            currentCamera.enabled = false;
        }

        newCamera.enabled = true;
        currentCamera = newCamera;

        // Disable the button for the current camera
        DisableButtonForCurrentCamera();

        // Disable the balls for the current camera
        DisableBallsForCurrentCamera();
    }

    private void DisableButtonForCurrentCamera()
    {
        // Disable the button for the current camera
        if (currentCamera == camera1)
        {
            buttonCamera1.interactable = false;
            buttonCamera2.interactable = true;
            buttonCamera3.interactable = true;
        }
        else if (currentCamera == camera2)
        {
            buttonCamera1.interactable = true;
            buttonCamera2.interactable = false;
            buttonCamera3.interactable = true;
        }
        else if (currentCamera == camera3)
        {
            buttonCamera1.interactable = true;
            buttonCamera2.interactable = true;
            buttonCamera3.interactable = false;
        }
    }

    private void DisableBallsForCurrentCamera()
    {
        // Disable the balls for the current camera
        ball1.SetActive(currentCamera == camera1);
        ball2.SetActive(currentCamera == camera2);
        ball3.SetActive(currentCamera == camera3);
    }
}
