using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(GameManager.Instance.firstPlayer))
        {
            gameObject.GetComponent<Text>().text = "None";
        }
        else
        {
            Player tempP = GameManager.Instance.totalPlayers[0];
            gameObject.GetComponent<Text>().text = tempP.playerID + " : " + tempP.totalNum;
        }
    }
}
