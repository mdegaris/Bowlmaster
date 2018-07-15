using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    public static float xNudgeMax = 45f;
    public static float xNudgeMin = -45f;
    
    private Rigidbody ballRigidbody;
    private AudioSource ballAudio;
    private bool launched = false;


    // ===================================================================
    // Methods
    // ===================================================================

    // Performs the ball launching steps.
    public void Launch(Vector3 velocity)
    {
        this.launched = true;
        this.ballRigidbody.useGravity = true;
        this.ballRigidbody.velocity = velocity;
        this.ballAudio.Play();
    }

    public void Nudge(float xNudgeAmount)
    {
        if (! this.launched)
        {
            Vector3 currentPos = this.transform.position;
            float nudgedXPos = Mathf.Clamp((currentPos.x + xNudgeAmount), Ball.xNudgeMin, Ball.xNudgeMax);
            this.transform.position = new Vector3(nudgedXPos, currentPos.y, currentPos.z);            
        }
        else
        {
            Debug.Log("Cannot nudge, ball has already launched.");
        }
    }

    public bool InPlay()
    {
        return this.launched;
    }

    public void Reset()
    {
        Debug.Log("Reset the ball.");
    }

    // Use this for initialization
    private void Start()
    {
        this.ballRigidbody = GetComponent<Rigidbody>();
        this.ballAudio = GetComponent<AudioSource>();

        // We want the ball staying still until its launched.
        this.ballRigidbody.useGravity = false;
    }
}
