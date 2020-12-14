using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecondPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(GameManager.Instance.secondPlayer))
        {
            gameObject.GetComponent<Text>().text = "None";
        }
        else
        {
            gameObject.GetComponent<Text>().text = GameManager.Instance.totalPlayers[1].playerID;
        }
    }
}
