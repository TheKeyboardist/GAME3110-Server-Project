using UnityEngine;
using Unity.Collections;
using Unity.Networking.Transport;
using NetworkMessages;
using NetworkObjects;
using System;
using System.Text;
using UnityEngine.UI;

public class NetworkClient : MonoBehaviour
{
    //game pver stuff
    public Text gameOverText;
    //



    public NetworkDriver m_Driver;
    public NetworkConnection m_Connection;
    public string serverIP;
    public ushort serverPort;
    private bool sendInfo;
    //
  
    //
    void Start ()
    {
        //
        gameOverText.gameObject.SetActive(false);
        //

        m_Driver = NetworkDriver.Create();
        m_Connection = default(NetworkConnection);
        var endpoint = NetworkEndPoint.Parse(serverIP,serverPort);
        m_Connection = m_Driver.Connect(endpoint);
        sendInfo = false;

    }
    
    void SendToServer(string message){
        var writer = m_Driver.BeginSend(m_Connection);
        NativeArray<byte> bytes = new NativeArray<byte>(Encoding.ASCII.GetBytes(message),Allocator.Temp);
        writer.WriteBytes(bytes);
        m_Driver.EndSend(writer);
    }

    void OnConnect(){
        Debug.Log("We are now connected to the server");
        sendInfo = true;

        //// Example to send a handshake message:
        //HandshakeMsg m = new HandshakeMsg();

    }

    void OnData(DataStreamReader stream){
        NativeArray<byte> bytes = new NativeArray<byte>(stream.Length,Allocator.Temp);
        stream.ReadBytes(bytes);
        string recMsg = Encoding.ASCII.GetString(bytes.ToArray());
        NetworkHeader header = JsonUtility.FromJson<NetworkHeader>(recMsg);
        
        switch (header.cmd){
            case Commands.HANDSHAKE:
            HandshakeMsg hsMsg = JsonUtility.FromJson<HandshakeMsg>(recMsg);
            Debug.Log("Handshake message received!");
                break;
            case Commands.PLAYER_UPDATE:
            PlayerUpdateMsg puMsg = JsonUtility.FromJson<PlayerUpdateMsg>(recMsg);
                if (GameManager.Instance.gameRoomID == puMsg.player.roomID && GameManager.Instance.gameRoomID != 0 && puMsg.player.roomID != 0)
                {
                    if (puMsg.player.id != GameManager.Instance.playerID.ToString() && string.IsNullOrEmpty(GameManager.Instance.firstPlayer))
                    {
                        
                        GameManager.Instance.firstPlayer = puMsg.player.id;
                        
                        GameManager.Instance.totalPlayers.Add(new Player(puMsg.player.id, puMsg.player.firstNum, puMsg.player.secondNum, puMsg.player.thirdNum, puMsg.player.totalNum, puMsg.player.roomID, puMsg.player.ready));
       
                        
                    }
                    if (puMsg.player.id != GameManager.Instance.playerID.ToString() && puMsg.player.id != GameManager.Instance.firstPlayer && !string.IsNullOrEmpty(GameManager.Instance.firstPlayer) && string.IsNullOrEmpty(GameManager.Instance.secondPlayer))
                    {
                        GameManager.Instance.secondPlayer = puMsg.player.id;
                      
                        GameManager.Instance.totalPlayers.Add(new Player(puMsg.player.id, puMsg.player.firstNum, puMsg.player.secondNum, puMsg.player.thirdNum, puMsg.player.totalNum, puMsg.player.roomID, puMsg.player.ready));
               
                    }

                    if (GameManager.Instance.totalPlayers.Count > 0 )
                    {
                        UpdatePlayerInfos(GameManager.Instance.totalPlayers[0], puMsg);
                    }

                    if (GameManager.Instance.totalPlayers.Count > 1 )
                    {
                        UpdatePlayerInfos(GameManager.Instance.totalPlayers[1], puMsg);
                    }

                    if (GameManager.Instance.totalPlayers.Count > 2 )
                    {
                        UpdatePlayerInfos(GameManager.Instance.totalPlayers[2], puMsg);
                    }
                }
                GameManager.Instance.roomNumbers = puMsg.player.matchedRooms;
                //Debug.Log("PlayerID : " + puMsg.player.id + " : " + puMsg.player.totalNum);
                break;
            case Commands.SERVER_UPDATE:
            ServerUpdateMsg suMsg = JsonUtility.FromJson<ServerUpdateMsg>(recMsg);
            Debug.Log("Server update message received!");
            break;

            case Commands.PLAYERS_READY:
            PlayersReadyMsg prMsg = JsonUtility.FromJson<PlayersReadyMsg>(recMsg);
            break;
            default:
            Debug.Log("Unrecognized message received!");
            break;
        }
    }

    void Disconnect(){
        m_Connection.Disconnect(m_Driver);
        m_Connection = default(NetworkConnection);
    }

    void OnDisconnect(){
        Debug.Log("Client got disconnected from server");
        m_Connection = default(NetworkConnection);
        //Destroy(t);
        sendInfo = false;
    }

    public void OnDestroy()
    {
        m_Driver.Dispose();
    }
    void Update()
    {

        m_Driver.ScheduleUpdate().Complete();

        if (!m_Connection.IsCreated)
        {
            return;
        }

        DataStreamReader stream;
        NetworkEvent.Type cmd;
        cmd = m_Connection.PopEvent(m_Driver, out stream);
        while (cmd != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                OnConnect();
                //x = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                OnData(stream);
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                OnDisconnect();
            }

            cmd = m_Connection.PopEvent(m_Driver, out stream);

        }
 

        if(sendInfo)
        {
            PlayerUpdateMsg m = new PlayerUpdateMsg();
            m.player.id =        GameManager.Instance.playerID.ToString();
            m.player.totalNum =  GameManager.Instance.total;
            m.player.firstNum =  GameManager.Instance.firstNum;
            m.player.secondNum = GameManager.Instance.secondNum;
            m.player.thirdNum =  GameManager.Instance.thirdNum;
            m.player.roomID =    GameManager.Instance.gameRoomID;
            m.player.ready =     GameManager.Instance.ready;
            SendToServer(JsonUtility.ToJson(m));
        }
        
    }

    void UpdatePlayerInfos(Player tempP, PlayerUpdateMsg puMsg)
    {


        if (puMsg.player.id == tempP.playerID)
        {
            tempP.b_ready = puMsg.player.ready;
        }


        if(GameManager.Instance.totalPlayers.Count == 1)
        {
            if (GameManager.Instance.ready && GameManager.Instance.totalPlayers[0].b_ready)
            {
                if (puMsg.player.id == tempP.playerID)
                {
                    tempP.totalNum = puMsg.player.totalNum;
                    gameOverText.gameObject.SetActive(true);
                    FindWinner();
                }
            }
        }


        if (GameManager.Instance.totalPlayers.Count == 2)
        {
            if (GameManager.Instance.ready && GameManager.Instance.totalPlayers[0].b_ready && GameManager.Instance.totalPlayers[1].b_ready)
            {
                if (puMsg.player.id == tempP.playerID)
                {
                    tempP.totalNum = puMsg.player.totalNum;
                    gameOverText.gameObject.SetActive(true);
                    FindWinner();
                }
            }
        }

        if (GameManager.Instance.totalPlayers.Count == 3)
        {
            if (GameManager.Instance.ready && GameManager.Instance.totalPlayers[0].b_ready && GameManager.Instance.totalPlayers[1].b_ready && GameManager.Instance.totalPlayers[2].b_ready)
            {
                if (puMsg.player.id == tempP.playerID)
                {
                    tempP.totalNum = puMsg.player.totalNum;
                    gameOverText.gameObject.SetActive(true);
                    FindWinner();
                }
            }
        }
    }


    void FindWinner()
    {
       // int tempMe = GameManager.Instance.total;
       // int temp0 = GameManager.Instance.totalPlayers[0].totalNum;
       // int temp1 = GameManager.Instance.totalPlayers[1].totalNum;
       // int temp2 = GameManager.Instance.totalPlayers[2].totalNum;
        Player tempWinner = null;
        bool moreThan1winner = false;

        if (GameManager.Instance.totalPlayers.Count < 2)
        {
            if (GameManager.Instance.total > GameManager.Instance.totalPlayers[0].totalNum)     
            {
                moreThan1winner = false;
               // gameOverText.text = "Winner is: " + GameManager.Instance.playerID.ToString();
                gameOverText.text = GameManager.Instance.playerID.ToString();
            }
            else if (GameManager.Instance.total == GameManager.Instance.totalPlayers[0].totalNum)
            {
                gameOverText.text = "It's a tie";
                moreThan1winner = true;
            }
            else
            {
                moreThan1winner = false;
               // gameOverText.text = "Winner is: " + GameManager.Instance.totalPlayers[0].playerID.ToString();
                gameOverText.text =GameManager.Instance.totalPlayers[0].playerID;
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.totalPlayers.Count; i++)
            {
                if(i == 0)
                {
                    if (GameManager.Instance.totalPlayers[i].totalNum > GameManager.Instance.totalPlayers[i + 1].totalNum)
                    {
                        tempWinner = GameManager.Instance.totalPlayers[i];
                        moreThan1winner = false;
                    }
                    else if(GameManager.Instance.totalPlayers[i].totalNum < GameManager.Instance.totalPlayers[i + 1].totalNum)
                    {
                        tempWinner = GameManager.Instance.totalPlayers[i + 1];
                        moreThan1winner = false;
                    }
                    else
                    {
                        tempWinner = GameManager.Instance.totalPlayers[i];
                        moreThan1winner = true;
                    }
                }
                else if (tempWinner.totalNum < GameManager.Instance.totalPlayers[i].totalNum)
                {
                    tempWinner = GameManager.Instance.totalPlayers[i];
                    moreThan1winner = false;
                }
            }
            if (GameManager.Instance.total > tempWinner.totalNum)
            {
                moreThan1winner = false;
                gameOverText.text = "Winner is: " + GameManager.Instance.playerID.ToString();
            }
            else if(GameManager.Instance.total < tempWinner.totalNum)
            {
                moreThan1winner = false;
                gameOverText.text = "Winner is: " + tempWinner.playerID.ToString();
            }
            else if (moreThan1winner)
            {
                gameOverText.text = "It's more than one winner";
            }

        }

    }
}