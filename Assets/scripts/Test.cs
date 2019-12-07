using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts;

public class Test : MonoBehaviour
{
    private NetworkProxy socket;

    // Start is called before the first frame update
    void Start()
    {
        socket = new TcpSocket();
        socket.StartSocket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
