using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnBackButtonClicked()
    {
        GameManager.Instance.total = 0;
        GameManager.Instance.firstNum = 0;
        GameManager.Instance.secondNum = 0;
        GameManager.Instance.thirdNum = 0;
        GameManager.Instance.gameRoomID = 0;
        GameManager.Instance.ready = false;
        SceneManager.LoadScene("ClientScene");
    }

    void Update()
    {
        
    }


}
