using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO3;
using BestHTTP.SocketIO.Events;
using static UnityEngine.Rendering.DebugUI;
using static UnityEditor.Progress;
using BestHTTP.SocketIO3.Events;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SocketManagerScript : MonoBehaviour
{
    private SocketManager manager;

    private void Awake()
    {

    }

    void Start()
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



        Application.runInBackground = true;

        SocketOptions options = new SocketOptions();
        options.AutoConnect = false;
        this.manager = new SocketManager(new Uri("http://localhost:3000"), options);

        manager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
        manager.Socket.On<Error>(SocketIOEventTypes.Error, OnError);

        this.manager.Open();

        manager.Socket.On<string>("test", OnMethod);
        manager.Socket.Emit("channel_platfrom", "msg 1");

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

    void DataFromSocketServer(string data)
    {
        if (data == "onPreviewScene")
        {
            Debug.Log("+ onPreviewScene // Scene Loaded");
            SceneManager.LoadScene("onPreviewScene");
        }

        if (data == "onListStyleScene")
        {
            Debug.Log("+ onListStyleScene // Scene Loaded");
            SceneManager.LoadScene("ListStylesScene");
        }

        if (data == "onStyleSelectedScene")
        {
            Debug.Log("+ onStyleSelectedScene // Scene Loaded");
            SceneManager.LoadScene("AmericanDinnerStylePreview");
        }

        if (data == "onStyleFullViewScene")
        {
            Debug.Log("+ onStyleFullViewScene // Scene Loaded");
            SceneManager.LoadScene("AmericanDinnerFullView");
        }

        if (data == "onPreviewCaptureScene")
        {
            Debug.Log("+ onPreviewCaptureScene // Scene Loaded");
            SceneManager.LoadScene("PreviewCapture");
        }

        if (data == "onCaptureGIF")
        {
            ProGifRecording_Popphy.ValueToStart = true;
            Debug.Log("+ onCaptureGIF // Capturing GIF");
        }
    }

}
