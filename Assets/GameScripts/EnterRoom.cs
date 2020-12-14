using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
public class EnterRoom : MonoBehaviour
{

    public int roomNumber;
    public GameObject gamePanel;
    public GameObject matchroomPanel;
    // Start is called before the first frame update
    public void setGameRoom()
    {
        switch (roomNumber)
        {
            case 1:
                GameManager.Instance.gameRoomID = GameManager.Instance.roomNumbers[0];
                gamePanel.SetActive(true);
                matchroomPanel.SetActive(false);

                break;
            case 2:
                GameManager.Instance.gameRoomID = GameManager.Instance.roomNumbers[1];
                gamePanel.SetActive(true);
                matchroomPanel.SetActive(false);
                break;
            case 3:
                GameManager.Instance.gameRoomID = GameManager.Instance.roomNumbers[2];
                gamePanel.SetActive(true);
                matchroomPanel.SetActive(false);
                break;
            case 4:
                GameManager.Instance.gameRoomID = GameManager.Instance.roomNumbers[3];
                gamePanel.SetActive(true);
                matchroomPanel.SetActive(false);
                break;
        }
    }
}
