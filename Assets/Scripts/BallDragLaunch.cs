using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallDragLaunch : MonoBehaviour
{
    // ===================================================================
    // Variables
    // ===================================================================

    private static float minimumMovement = 100;

    private Ball ball;
    private Vector2 dragStartPos;
    private float dragStartTime;
    private bool isDragging;


    // ===================================================================
    // Methods
    // ===================================================================

    // Capture the time and position of a drag start.
    public void DragStart()
    {
        if (! this.ball.Launched())
        {
            Debug.Log("Start dragging.");

            this.dragStartPos = Input.mousePosition;
            this.dragStartTime = Time.realtimeSinceStartup;
            this.isDragging = true;
        }
    }

    // Launch the ball when the dragging has ended.
    public void DragEnd()
    {        
        if (this.isDragging)
        {
            Debug.Log("Stop dragging.");

            Vector2 dragEndPos = Input.mousePosition;
            float dragEndTime = Time.realtimeSinceStartup;

            float dragDuration = (Time.realtimeSinceStartup - this.dragStartTime);

            // Debug Testing
            float launchSpeedX = ((dragEndPos.x - this.dragStartPos.x) / dragDuration);
            launchSpeedX = launchSpeedX / 1.5f;
            // float launchSpeedX = 0;
            float launchSpeedY = ((dragEndPos.y - this.dragStartPos.y) / dragDuration);

            if ((launchSpeedX > BallDragLaunch.minimumMovement) || (launchSpeedY > BallDragLaunch.minimumMovement))
            {
                // Translate the 2D mouse swipe into a 3D vector, i.e. (3D) z = (2D) y;
                Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedY);
                this.ball.Launch(launchVelocity);
            }

            this.isDragging = false;
        }
    }


    private void Start()
    { 
        this.ball = this.gameObject.GetComponent<Ball>();
        this.isDragging = false;
    }
}
