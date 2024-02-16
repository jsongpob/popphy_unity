using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public int currentCameraIndexBackgroundRemoved;
    WebCamTexture webCamTexture;
    public RawImage cameraDisplay;

    // Start is called before the first frame update
    void Start()
    {
        StartCameraBackgroundRemoved();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartCameraBackgroundRemoved()
    {
        if (webCamTexture != null)
        {
            cameraDisplay.texture = null;
            webCamTexture.Stop();
            webCamTexture = null;
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[currentCameraIndexBackgroundRemoved];
            webCamTexture = new WebCamTexture(device.name);
            cameraDisplay.texture = webCamTexture;
            webCamTexture.Play();
        }
    }
}
