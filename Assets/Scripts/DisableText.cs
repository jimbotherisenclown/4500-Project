using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableText : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("disableText", 89);
    }

    // Update is called once per frame
    void disableText()
    {
        GetComponent<Text>().enabled = false;
    }
}
