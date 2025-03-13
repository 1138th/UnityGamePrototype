using UnityEngine;

public class CameraInit : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 60;
    }
}