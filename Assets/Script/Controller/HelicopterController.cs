using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [Header("Option")]
    public float rotateTime;
    public bool isFlying = true;
    //value
    private Quaternion StartingPos;
    private Vector2[] touchStartPos = new Vector2[2];
    private float initialDistance;
    private Vector3 initialScale;
    private Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.rotation;
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2 && !isFlying)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            float touch0PosX = touch0.position.x;
            float touch1PosX = touch1.position.x;

            switch (touch0.phase)
            {
                case TouchPhase.Began:
                    touchStartPos[0] = new Vector2(touch0PosX, touch0.position.y);
                    break;
            }

            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    touchStartPos[1] = new Vector2(touch1PosX, touch1.position.y);
                    initialDistance = Vector2.Distance(touchStartPos[0], touchStartPos[1]);
                    break;

                case TouchPhase.Moved:
                    float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                    float scaleFactor = currentDistance / initialDistance;

                    // Scale the GameObject
                    currentScale = initialScale * scaleFactor;
                    transform.localScale = currentScale;
                    break;
            }
        }
        else if (Input.touchCount == 1 && !isFlying)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos[0] = touch.position;
                    break;

                case TouchPhase.Moved:
                    float swipeDelta = touch.position.x - touchStartPos[0].x;
                    float rotationAngle = swipeDelta * rotateTime * Time.deltaTime;

                    // Rotate the GameObject along its up vector (Y-axis)
                    transform.Rotate(Vector3.up, rotationAngle);
                    break;
            }
        }
    }
    public void ResetHeliRotation()
    {
        AudioController.PlayMusic("ButtonClicked");
        transform.localScale = initialScale;
        transform.rotation = StartingPos;
    }
}
