using System.Collections;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    WebCamDevice[] devices;
    [SerializeField] private GameObject cameraProcessor;
    [SerializeField] private GameObject cameraSelector;
    [SerializeField] private GameObject instructorCameraOn;
    [SerializeField] private GameObject instructorCameraOff;
    private void Awake()
    {
        devices = WebCamTexture.devices;
        StartCoroutine(maskDialogue());
    }


    IEnumerator maskDialogue() 
    {
        yield return new WaitForSeconds(4);
        if (devices.Length == 0)
        {
            cameraProcessor.SetActive(false);
            cameraSelector.SetActive(false);
            instructorCameraOff.SetActive(true);
            instructorCameraOn.SetActive(false);

        }
        else
        {
            instructorCameraOff.SetActive(false);
            instructorCameraOn.SetActive(true);
        }

    }
}
