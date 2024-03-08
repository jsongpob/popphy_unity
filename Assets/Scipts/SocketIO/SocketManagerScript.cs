using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO3;
using BestHTTP.SocketIO.Events;
using static UnityEngine.Rendering.DebugUI;
using BestHTTP.SocketIO3.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SocketManagerScript : MonoBehaviour
{
    private SocketManager manager;
    public string UriAddress;

    public static string FilePathPhoto;
    string ImageURL;

    string base64string;

    CameraScript CameraScript;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SocketScript");

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
        Application.runInBackground = true;

        SocketOptions options = new SocketOptions();
        options.AutoConnect = false;
        this.manager = new SocketManager(new Uri(UriAddress), options);

        manager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
        manager.Socket.On<Error>(SocketIOEventTypes.Error, OnError);

        this.manager.Open();

        manager.Socket.On<string>("test", OnMethod);

        manager.Socket.On<string>("setUnityPlatFrom", DataFromSocketServer);
    }

    void OnConnected(ConnectResponse resp)
    {
        Debug.Log("Connected!");
    }
    void OnError(Error error)
    {
        Debug.Log($"An error occured: {error}");
    }
    void OnDestroy()
    {
        this.manager?.Close();
        this.manager = null;
    }

    void OnMethod(string data)
    {
        Debug.Log("OnMethod!" + data);
    }

    void ConvertGifToBase64()
    {
        try
        {
            // Read the GIF file as bytes
            byte[] gifBytes = File.ReadAllBytes(FilePathPhoto);

            // Convert the bytes to a base64 string
            base64string = Convert.ToBase64String(gifBytes);

            // Output the base64 string
            //Debug.Log("Base64 String:\n" + base64string);
        }
        catch (Exception e)
        {
            Debug.LogError("Error converting GIF to base64: " + e.Message);
        }
    }

    void DataFromSocketServer(string data)
    {
        //________PREVIEW_IDLE__________
        if (data == "onPreviewidle")
        {
            Debug.Log("+ onPreviewidle // Scene Loaded");
            SceneManager.LoadScene("onPreviewScene"); //SCENE
        }

        //_________LIST_OF_STYLES_______
        if (data == "selectedListOfStyle")
        {
            Debug.Log("+ selectedListOfStyle // Scene Loaded");
            SceneManager.LoadScene("ListStylesScene"); //SCENE
        }

        //__________PREVIEW_THEME_______
        //AMERICAN DINNER
        if (data == "selectedADpreview")
        {
            Debug.Log("+ selectedADpreview // Scene Loaded");
            SceneManager.LoadScene("AmericanDinnerPreview"); //SCENE
        }
        //ANIMAL PARTY
        if (data == "selectedAPpreview")
        {
            Debug.Log("+ selectedAPpreview // Scene Loaded");
            SceneManager.LoadScene("AnimalPartyPreview"); //SCENE
        }
        //HELLO WELCOME
        if (data == "selectedHWpreview")
        {
            Debug.Log("+ selectedHWpreview // Scene Loaded");
            SceneManager.LoadScene("HelloWelcomePreview"); //SCENE
        }
        //TECHNO SHOWOFF
        if (data == "selectedTSpreview")
        {
            Debug.Log("+ selectedTSpreview // Scene Loaded");
            SceneManager.LoadScene("TechnoShowoffPreview"); //SCENE
        }

        //__________PREVIEW_CAPTURED_________
        if (data == "previewCaptured")
        {
            Debug.Log("+ onPreviewCaptureScene // Scene Loaded");
            SceneManager.LoadScene("PreviewCapture"); //SCENE
        }

        //__________CAPTURING___________
        if (data == "startCapture")
        {
            ProGifRecording_Popphy.ValueToStart = true;
            Debug.Log("+ onCaptureGIF // Capturing GIF"); //SCENE
        }

        //____________FULL_VIEW_OF_THEME________________

        if (data == "goADfull")
        {
            Debug.Log("+ goADfull // Scene Loaded");
            SceneManager.LoadScene("AmericanDinnerFullView"); //SCENE
        }

        if (data == "goAPfull")
        {
            Debug.Log("+ goAPfull // Scene Loaded");
            SceneManager.LoadScene("AnimalPartyFullView"); //SCENE
        }

        if (data == "goHWfull")
        {
            Debug.Log("+ goHWfull // Scene Loaded");
            SceneManager.LoadScene("ThailandFullView"); //SCENE
        }

        if (data == "goTSfull")
        {
            Debug.Log("+ goTSfull // Scene Loaded");
            SceneManager.LoadScene("TechnoShowOffFullView"); //SCENE
        }

        //_________UPLOAD_PHOTO_TO_SERVER_______
        if(data == "onEnd")
        {
            ConvertGifToBase64();
            Debug.Log(">>>> Start upload photo to server...");

            StartCoroutine(UploadtoserverwBase64());
            manager.Socket.Emit("channel_data", ImageURL);

            SceneManager.LoadScene("EndScene"); //SCENE
        }
    }

    IEnumerator UploadtoserverwBase64()
    {
        string UrlApi = "http://funcslash.com/artistries/popphy/upload_now.php";

        WWWForm form = new WWWForm();
        form.AddField("Image", base64string);

        using UnityWebRequest www = UnityWebRequest.Post(UrlApi, form);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Photo uploaded successfully.");
            Debug.Log(www.downloadHandler.text);
            ImageURL = www.downloadHandler.text;
        }
    }
}
