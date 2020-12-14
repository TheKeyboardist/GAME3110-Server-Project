using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public string playerID;
    public int fNum, sNum, tNum, totalNum, proomID;
    public bool b_ready;
    // Start is called before the first frame update
    public Player(string pID, int fN, int sN, int tN, int total, int roomID = 0, bool r = false)
    {
        playerID = pID;
        fNum = fN;
        sNum = sN;
        tNum = tN;
        totalNum = total;
        proomID = roomID;
        b_ready = r;
    }


}
