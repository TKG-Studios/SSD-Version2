using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{


    private PlayerMovementExtension playerMovement;
    private PlayerAnimator playerAnimator;

    public enum Axis
    {
        None = 0,
        Horizontal,
        Vertical
    }

    public enum TapVelocity
    {
        None = 0,
        Positive,
        Negative
    }

    /// <summary>
    /// The number of taps required before the method 'OnTapCountMet' is called.
    /// </summary>
    public int tapsRequired = 2;

    // The time between taps. Once this time elapses the tap count is reset.
    public float timeBetweenTaps = 0.4f;

    // Used to make sure that the axis is reset (i.e. the joystick is re-centred) before we count a new tap.
    private bool wasLastFrameInput = false;

    // Stores the previous tap velocity. Makes sure the player taps the joystick in the same direction twice.
    private TapVelocity prevVelocity = TapVelocity.None;

    // Stores the previous tap axis. Makes sure the player taps the joystick on the same axis twice.
    private Axis prevAxis = Axis.None;

    private int currentTapCount;
    private float lastTapTime;
    private bool isRunning;

    void Start()
    {
       
        playerMovement = GetComponent<PlayerMovementExtension>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();

        // If you don't want to listen for taps at start then remove this line.
        StartChecking();

    }

    /// <summary>
    /// Starts checking for double taps (if not already running).
    /// </summary>
    public void StartChecking()
    {
        isRunning = true;
    }

    /// <summary>
    /// Stops checking for double taps.
    /// </summary>
    public void StopChecking()
    {
        isRunning = false;
    }

    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        float horiAxisVal = Input.GetAxis("Horizontal");
        float vertAxisVal = Input.GetAxis("Vertical");

        float horiAxisAbs = Mathf.Abs(horiAxisVal);
        float vertAxisAbs = Mathf.Abs(vertAxisVal);

        if (horiAxisAbs > 0f || vertAxisAbs > 0f) // We've moved the joystick in either a vertical or horizontal direction.
        {
            Axis currentMove = horiAxisAbs > vertAxisAbs ? Axis.Horizontal : Axis.Vertical;

            if (IsTapOnSameAxis(currentMove)) // Make sure that this tap is on the same axis as the previous tap.
            {
                float currentVelocity = currentMove == Axis.Horizontal ? horiAxisVal : vertAxisVal;

                if (IsTapInSameDirection(currentVelocity)) // Ensures that the player tapped in the same direction as the last tap.
                {
                  
                    if (!wasLastFrameInput)
                    {
                        
                        Vector2 velocity = new Vector2(horiAxisVal, vertAxisVal).normalized;
                      

                        // We've registered a tap on the correct axis and direction.
                        ProcessTap(currentMove, currentVelocity, velocity);
                     
                    }
                }
            }
        }
        else
        {
            wasLastFrameInput = false; // No axis input registered this frame.

            if (Time.time - lastTapTime > timeBetweenTaps)
            {
                // Reset tap count if timeBetweenTaps has elapsed.
                playerAnimator.Walk();
                playerAnimator.Idle();
                Reset();
            }
        }


    }

    private void Reset()
    {
        currentTapCount = 0;
        prevVelocity = TapVelocity.None;
        prevAxis = Axis.None;


    }

    private bool IsTapOnSameAxis(Axis currentMove)
    {
        if (prevAxis == Axis.None)
        {
            return true;
        }

        return prevAxis == currentMove;
    }

    private bool IsTapInSameDirection(float currentVal)
    {

        return (prevVelocity == TapVelocity.None) ||
                    (currentVal < 0f && prevVelocity == TapVelocity.Negative) ||
                    (currentVal > 0f && prevVelocity == TapVelocity.Positive);
    }

    private void ProcessTap(Axis axis, float axisVal, Vector2 tapVelocity)
    {
        lastTapTime = Time.time;
        wasLastFrameInput = true;

        if (++currentTapCount == tapsRequired)
        {
            //Debug.Log(currentTapCount + " taps on axis: " + axis + " with direction: " + tapVelocity);

            OnTapCountMet(tapVelocity);

            Reset();
        }
        else
        {
            prevAxis = axis;
            prevVelocity = axisVal > 0f ? TapVelocity.Positive : TapVelocity.Negative;
            playerMovement.walkSpeed = 3;

        }
    }

    private void OnTapCountMet(Vector2 tapDirection)
    {
        
        Vector2 right = new Vector2(1f, 0);
        Vector2 left = new Vector2(-1f, 0);
       
        float horiAxisVal = Input.GetAxis("Horizontal");
        float horiAxisAbs = Mathf.Abs(horiAxisVal);
        if (horiAxisAbs > 0f)
        {

            GameObject dustEffect = Resources.Load<GameObject>("DustEffect");
            Instantiate(dustEffect, transform.position, Quaternion.identity);
            ParticleSystem.VelocityOverLifetimeModule dustVelocity = dustEffect.GetComponent<ParticleSystem>().velocityOverLifetime;
            ParticleSystem.MinMaxCurve vel = new ParticleSystem.MinMaxCurve();

            if (tapDirection == right)
            {
                vel.constantMax = 1;
                dustVelocity.x = vel;
            }

            if (tapDirection == left)
            {

                vel.constantMax = -1;
                dustVelocity.x = vel;
            }

            playerMovement.walkSpeed = playerMovement.runSpeed;
        }
        else
        {
            playerMovement.walkSpeed = playerMovement.defaultWalkSpeed;
        }
    }


}

