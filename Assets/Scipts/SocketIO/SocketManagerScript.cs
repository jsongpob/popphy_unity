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

public class SocketManagerScript : MonoBehaviour
{
    private SocketManager manager;

    private void Awake()
    {

    }

    void Start()
    {
        Application.runInBackground = true;

        SocketOptions options = new SocketOptions();
        options.AutoConnect = false;
        this.manager = new SocketManager(new Uri("http://localhost:3000"), options);

        manager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
        manager.Socket.On<Error>(SocketIOEventTypes.Error, OnError);

        this.manager.Open();

        manager.Socket.On<string>("test", OnMethod);
        manager.Socket.Emit("channel_platfrom", "msg 1");
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

}
