using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public int frameNumber;

    private List<Bowl> bowls;
    private FrameScore frameScore;


    public void SetScore(int score)
    {
        this.frameScore.SetScore(score);
    }

    public List<Bowl> GetBowls()
    {
        return this.bowls;
    }

    private void Start()
    {
        this.frameScore = this.gameObject.GetComponentInChildren<FrameScore>();
        this.bowls = this.gameObject.GetComponentsInChildren<Bowl>().ToList<Bowl>();        
        this.bowls = this.bowls.OrderBy(b => b.bowlNumber).ToList<Bowl>();
    }

    public void Reset()
    {
        this.frameScore.Reset();
    }
}

