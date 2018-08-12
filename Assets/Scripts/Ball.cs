using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static float xNudgeMax = 45f;
    private static float xNudgeMin = -45f;

    private Vector3 startingPosition;
    private Rigidbody ballRigidbody;
    private AudioSource ballAudio;
    // Indicates if the ball has been launched by the player.
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


    public void SetLaunched(bool launched)
    {
        this.launched = launched;
    }


    public bool Launched()
    {
        return this.launched;
    }


    public void Reset()
    {
        Debug.Log("Reset the ball.");

        // Rigidbody reset
        this.ballRigidbody.useGravity = false;
        this.ballRigidbody.angularVelocity = Vector3.zero;
        this.ballRigidbody.velocity = Vector3.zero;

        this.transform.position = this.startingPosition;
        this.launched = false;
    }

    // Use this for initialization
    private void Start()
    {
        this.ballRigidbody = GetComponent<Rigidbody>();
        this.ballAudio = GetComponent<AudioSource>();

        // Capture starting position
        this.startingPosition = this.transform.position;

        // Apply reset logic
        this.Reset();        
    }
}
