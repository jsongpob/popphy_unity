using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public int currentCameraIndexBackgroundRemoved;
    WebCamTexture webCamTexture;

    //public RawImage cameraDisplay;

    public RawImage rawImageComponent;

    WebCamDevice device;

    GameObject rawImageObject;

    bool cameraStarted = false;
    string currentScene;

    void Awake()
    {
/*        GameObject[] objs = GameObject.FindGameObjectsWithTag("CameraController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }*/
    }

    void Start()
    {
        device = WebCamTexture.devices[currentCameraIndexBackgroundRemoved];
        webCamTexture = new WebCamTexture(device.name);

        //rawImageObject = GameObject.FindWithTag("RawImageTag");
        //rawImageComponent = rawImageObject?.GetComponent<RawImage>();

        webCamTexture.Play();

        if (webCamTexture.isPlaying)
        {
            rawImageComponent.texture = webCamTexture;
        }

/*        if (cameraStarted == false)
        {
            StartCamera();
            cameraStarted = true;
        }*/
    }

    void Update()
    {

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

    IEnumerator GetCurrentScene()
    {
        rawImageObject = GameObject.FindWithTag("RawImageTag");
        rawImageComponent = rawImageObject?.GetComponent<RawImage>();

        yield return new WaitForSeconds(2);

        rawImageComponent.texture = webCamTexture;
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
        //cameraDisplay.texture = null;
        //webCamTexture.Stop();
        //webCamTexture = null;
        if (webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }
}
