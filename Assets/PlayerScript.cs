using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
     public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float returnSpeed = 100f;
    public float maxTurnAngle = 17.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float linearVelo = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(linearVelo * moveSpeed, 0);
        rb.MoveRotation(rb.rotation + -linearVelo * 200f * Time.fixedDeltaTime);
        float currentRotation = NormalizeAngle(rb.rotation);
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f)
        {
            // Rotate based on input, then clamp to ±maxTurnAngle
            float targetRotation = currentRotation + -Input.GetAxisRaw("Horizontal") * turnSpeed * Time.fixedDeltaTime;
            float clampedRotation = Mathf.Clamp(targetRotation, -maxTurnAngle, maxTurnAngle);
            rb.MoveRotation(clampedRotation);
        }
        else
        {
            // No input — smoothly rotate back to 0
            float resetRotation = Mathf.MoveTowards(currentRotation, 0f, returnSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(resetRotation);
        }
    }
    
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }   
}
