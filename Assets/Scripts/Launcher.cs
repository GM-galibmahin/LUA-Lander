using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{   
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBefourForce;

    private Rigidbody2D landerRigidBody2D;

    private void Awake()
    {
        landerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnBefourForce?.Invoke(this, EventArgs.Empty);
        if (Keyboard.current.upArrowKey.isPressed)
        {
            float force = 700f;
            landerRigidBody2D.AddForce(force * transform.up * Time.deltaTime);
            OnUpForce?.Invoke(this, EventArgs.Empty);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            float turnSpeed = -100f;
            landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            float turnSpeed = +100f;
            landerRigidBody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       // Checks if the collision is with a LandingPad
       if (!collision.gameObject.TryGetComponent(out LandingPad landingpad))
       {
        Debug.Log("Noo! You Landed on Terrain");
        return; 
       }
       
       // Checks if the collision is with a soft landing pad
       float softLandingChecker = 2.5f;
       float relativeVelocityMagnitue = collision.relativeVelocity.magnitude;
       if (relativeVelocityMagnitue > softLandingChecker)
       {
        Debug.Log("Opps The Ship Crashed"); 
        return;
       }

        // Checks if the Launcher is landing at the correct angle
        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = .95f;

        if (dotVector < minDotVector)
        {
            Debug.Log("Ahh the Truster broke");
            return;
        }

        Debug.Log("Yeaaaaa Success!");

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;
        
        float maxScoreAmountLandingSpeed = 100;
        float landingSpeedScore = (softLandingChecker - relativeVelocityMagnitue) * maxScoreAmountLandingSpeed;

       Debug.Log("Landing Angle Score: " + landingAngleScore);
       Debug.Log("Landing Speed Score: " + landingSpeedScore);

        int socre = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingpad.GetScoreMultiplier());

        Debug.Log("Total Score: " + socre);


    }

}
