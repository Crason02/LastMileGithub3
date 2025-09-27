using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
     public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float returnSpeed = 100f;
    public float maxTurnAngle = 17.6f;
    public bool freeze = false;
    public Vector2 velo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            float linearVelo = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(linearVelo * moveSpeed, 0);
            rb.MoveRotation(rb.rotation + -linearVelo * 200f * Time.fixedDeltaTime);
            float currentRotation = NormalizeAngle(rb.rotation);
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f)
            {
                float targetRotation = currentRotation + -Input.GetAxisRaw("Horizontal") * turnSpeed * Time.fixedDeltaTime;
                float clampedRotation = Mathf.Clamp(targetRotation, -maxTurnAngle, maxTurnAngle);
                rb.MoveRotation(clampedRotation);
            }
            else
            {
                float resetRotation = Mathf.MoveTowards(currentRotation, 0f, returnSpeed * Time.fixedDeltaTime);
                rb.MoveRotation(resetRotation);
            }
        }
        else
        {
            rb.linearVelocity = velo;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Glass"))
        {
            velo = rb.linearVelocity;
            freeze = true;
        }
        if (other.CompareTag("tree"))
        {
            velo = new Vector2(0, 0);
            freeze = true;
        }
    }
    
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }   
}
