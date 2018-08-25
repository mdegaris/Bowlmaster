using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bowl : MonoBehaviour
{
    public int bowlNumber;
    public Text pinsFallen;


    public void SetPinsFallen(string pinsFallen)
    {
        this.pinsFallen.text = pinsFallen.ToString();
    }

    public void Reset()
    {
        this.pinsFallen.text = "";
    }
}
