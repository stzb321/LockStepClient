using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts;
using UnityEngine.UI;

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
        string s = text.text;
        socket.SendTo(s);
    }
}
