using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GetPlayerID : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputField;
    public Button button;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerID()
    {
        if (inputField.text != null)
        {
            GameManager.Instance.playerID = int.Parse(inputField.text);
            SceneManager.LoadScene("ClientScene");  
        }

    }
}
