using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int m_roomID, m_currNumPlayers, m_maxNumPlayers;
    // Start is called before the first frame update
    public Room(int roomID, int currentPlayers, int maxPlayers)
    {
        m_roomID = roomID;
        m_currNumPlayers = currentPlayers;
        m_maxNumPlayers = maxPlayers;
    }
}
