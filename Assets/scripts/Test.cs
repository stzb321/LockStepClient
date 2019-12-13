using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts;
using UnityEngine.UI;
using LockStepFrameWork.NetMsg;

public class Test : MonoBehaviour
{
    public Button button;
    public InputField text;
    private NetworkProxy socket;

    // Start is called before the first frame update
    void Start()
    {
        socket = new TcpSocket();
        socket.StartSocket();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Msg_C2S_Ping msg = new Msg_C2S_Ping();
        msg.Tick = 2;
        Packet packet = new Packet(MsgType.C2S_Ping, msg);
        socket.SendTo(PacketParser.SerializeToByteArray(packet));
    }
}
