using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TwitchInterface : MonoBehaviour {

    static string SERVER = "irc.twitch.tv";
    static int PORT = 6667;

    /// <summary>
    /// twitch username
    /// </summary>
    public string username = "username";
    /// <summary>
    /// twitch oauth login token
    /// go to https://twitchapps.com/tmi/ to generate a token
    /// </summary>
    public string oauthToken = "oauth:";
    /// <summary>
    /// channel to connect to: #username (all lowercase)
    /// </summary>
    public string channel = "#bobross";

    public InputField usernameField;
    public InputField oauthField; 

    //recieved messages delegate
    public delegate void RecievedMessage(string message);
    public RecievedMessage messageReciever;

    private TcpClient tcpClient;
    private NetworkStream nStream;
    private StreamReader sReader;
    private StreamWriter sWriter;

    bool connected = false;
    public GameObject connectObject;

    public struct TwitchChatMessage
    {
        public TwitchChatMessage(string msg, string send) : this()
        {
            message = msg;
            sender = send;
        }
        public string message;
        public string sender;
    }

    // Use this for initialization
    void Start () {
        connectObject.SetActive(false);

    }

    public void Connect()
    {
        if (connected)
            return;

        tcpClient = new TcpClient(SERVER, PORT);
        nStream = tcpClient.GetStream();
        sReader = new StreamReader(nStream);
        sWriter = new StreamWriter(nStream);

        username = usernameField.text.ToLower();
        oauthToken = oauthField.text;
        channel = "#" + username;

        Write("USER " + username + "tmi twitch :" + username);
        Write("PASS " + oauthToken);
        Write("NICK " + username);
    }

    public void Write(string message)
    {
        sWriter.WriteLine(message);
        sWriter.Flush();
    }
	
	// Update is called once per frame
	void Update () {
        //check for new data
        while (nStream != null && nStream.DataAvailable)
        {
            string data = sReader.ReadLine();
            if (data != null)
                ReadData(data);
        }
	}

    void ReadData(string data)
    {
        string[] splitData = data.Split(' ');
        switch(splitData[1])
        {
            case "PING":
                //respond to pings
                SendMessage("PONG " + splitData[2]);
                break;
            case "PRIVMSG":
                //find the second colon, get the string from after that
                int colonIndex = data.IndexOf(':');
                int secondColonIndex = data.IndexOf(':', colonIndex + 1);
                messageReciever(data.Substring(secondColonIndex + 1));
                break;
            case "001":
                Write(("MODE " + username + " +B"));
                Write("JOIN " + channel);
                Write("PRIVMSG " + channel + " :Game connected!");
                connected = true;
                connectObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
