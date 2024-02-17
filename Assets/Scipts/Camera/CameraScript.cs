using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public int currentCameraIndexBackgroundRemoved;
    WebCamTexture webCamTexture;
    public RawImage cameraDisplay;
    RawImage rawImageComponent;

    WebCamDevice device;

    public string rawImageTag = "RawImageTag";
    GameObject rawImageObject;

    float TimerDelay;

    void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("CameraController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        rawImageObject = GameObject.FindWithTag(rawImageTag);
        rawImageComponent = rawImageObject?.GetComponent<RawImage>();

        device = WebCamTexture.devices[currentCameraIndexBackgroundRemoved];
        webCamTexture = new WebCamTexture(device.name);

        rawImageComponent.texture = webCamTexture;

        if (MainManager.dontStart)
        {
            StartCamera();
            MainManager.dontStart = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //StartCameraBackgroundRemoved();
        }
    }

    public void StartCamera()
    {
        if (webCamTexture != null)
        {
            device = WebCamTexture.devices[currentCameraIndexBackgroundRemoved];
            webCamTexture = new WebCamTexture(device.name);
            rawImageComponent.texture = webCamTexture;
            webCamTexture.Play();
        }
    }

/*    public void StartCameraBackgroundRemoved()
    {
        if (webCamTexture != null)
        {
            cameraDisplay.texture = null;
            webCamTexture.Stop();
            webCamTexture = null;
        }
        else
        {
            device = WebCamTexture.devices[currentCameraIndexBackgroundRemoved];
            webCamTexture = new WebCamTexture(device.name);
            cameraDisplay.texture = webCamTexture;
            webCamTexture.Play();
        }
    }*/

    public void StopCamera()
    {
        cameraDisplay.texture = null;
        webCamTexture.Stop();
        webCamTexture = null;
    } 
}
