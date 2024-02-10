using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO3;
using System;
using BestHTTP.SocketIO3.Events;

public class SocketManagerScript : MonoBehaviour
{
    private SocketManager manager;
    public String SocketValue;

    void Start()
    {
        Application.runInBackground = true;

        SocketOptions options = new SocketOptions();
        options.AutoConnect = false;

        this.manager = new SocketManager(new Uri("http://localhost:3000"), options);

        manager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, onConnected);
        manager.Socket.On<Error>(SocketIOEventTypes.Error, OnError);

        this.manager.Open();

        manager.Socket.On<int>("test", TestSocket);
    }

    void onConnected(ConnectResponse resp)
    {
        Debug.Log("Connected");
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

    private void TestSocket(int value)
    {
        Debug.Log(value);
        if (value == 1)
        {
            Debug.Log("Hello Socket!");
        }
    }

}
