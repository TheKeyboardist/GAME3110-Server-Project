using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateRooms : MonoBehaviour
{
    // Start is called before the first frame update

    public int roomNumber;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.roomNumbers.Count == 4)
        {
            switch (roomNumber)
            {
                case 1:
                    gameObject.GetComponent<Text>().text = GameManager.Instance.roomNumbers[0].ToString();
                    break;
                case 2:
                    gameObject.GetComponent<Text>().text = GameManager.Instance.roomNumbers[1].ToString();
                    break;
                case 3:
                    gameObject.GetComponent<Text>().text = GameManager.Instance.roomNumbers[2].ToString();
                    break;
                case 4:
                    gameObject.GetComponent<Text>().text = GameManager.Instance.roomNumbers[3].ToString();
                    break;
            }
        }
    }
}
