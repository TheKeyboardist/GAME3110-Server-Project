using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion
    public int firstNum = 0, secondNum = 0, thirdNum = 0, total = 0;
    //Ivan's edits
    public bool ready = false;
    //
    public int playerID = 0;
    public List<Player> totalPlayers = new List<Player>();
    public string firstPlayer, secondPlayer, thirdPlayer;
    public int gameRoomID;
    public List<int> roomNumbers;
    // Update is called once per frame


    public void Update()
    {

        total = firstNum + secondNum + thirdNum;
    }
}
