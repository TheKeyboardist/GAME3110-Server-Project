using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitResult : MonoBehaviour
{
    // Start is called before the first frame update



    public void OnSubmitButtonClicked()
    {
        GameManager.Instance.ready = true;
    }
}
