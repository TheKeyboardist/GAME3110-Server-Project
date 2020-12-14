using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumGen : MonoBehaviour
{

    private Text dispText;
    public int givenNumber;
    public int buttonID;
    // Start is called before the first frame update
    void Start()
    {
        dispText = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomNumber()
    {
        givenNumber = Random.Range(1, 10);
        dispText.text = givenNumber.ToString();
        gameObject.GetComponent<Button>().interactable = false;
        switch(buttonID)
        {
            case 1:
                GameManager.Instance.firstNum = givenNumber;
                break;
            case 2:
                GameManager.Instance.secondNum = givenNumber;
                break;
            case 3:
                GameManager.Instance.thirdNum = givenNumber;
                break;

        }
    }
}
