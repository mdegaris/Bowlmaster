using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameScore : MonoBehaviour
{
    public Text score;

    public void SetScore(int score)
    {
        this.score.text = score.ToString();
    }

    public void Reset()
    {
        this.score.text = "";
    }
}
