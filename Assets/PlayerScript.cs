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
        transform.Rotate(Vector3.forward * linearVelo * -2);
        if (transform.rotation.z > 20f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 20f);
        }
        if (transform.rotation.z < -20f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -20f);
        }
        Debug.Log(transform.rotation.z);
    }
}
