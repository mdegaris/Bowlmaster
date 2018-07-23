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
        Debug.Log("Start dragging.");

        if (! this.ball.InPlay())
        {
            this.dragStartPos = Input.mousePosition;
            this.dragStartTime = Time.realtimeSinceStartup;
            this.isDragging = true;
        }
    }

    // Launch the ball when the dragging has ended.
    public void DragEnd()
    {
        Debug.Log("Stop dragging.");

        if (this.isDragging)
        {
            Vector2 dragEndPos = Input.mousePosition;
            float dragEndTime = Time.realtimeSinceStartup;

            float dragDuration = Time.realtimeSinceStartup - this.dragStartTime;

            float launchSpeedX = ((dragEndPos.x - this.dragStartPos.x) / dragDuration);
            float launchSpeedY = ((dragEndPos.y - this.dragStartPos.y) / dragDuration);

            if ((launchSpeedX > BallDragLaunch.minimumMovement) || (launchSpeedY > BallDragLaunch.minimumMovement))
            {
                // Translate the 2D mouse swipe into a 3D vector, i.e. (3D) z = (2D) y;
                Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedY);

                Debug.Log(string.Format("launchVelocity: {0}", launchVelocity));
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
