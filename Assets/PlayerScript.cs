using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
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
        float targetRotation = rb.rotation + -linearVelo * 200f * Time.fixedDeltaTime;
        float clampedRotation = Mathf.Clamp(NormalizeAngle(targetRotation), -17.6f, 17.6f);
        rb.MoveRotation(clampedRotation);
    }
    
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }   
}
