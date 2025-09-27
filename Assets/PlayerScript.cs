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
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public ParticleSystem explode1;
    public ParticleSystem explode2;

    public int health = 3;
    public int mental = 2;
    public int gas = 1;

    public GameObject[] gass;
    public GameObject[] mentals;
    public GameObject[] healths;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateUI();
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
            if (rb.linearVelocity.x != 0)
            {
                velo = rb.linearVelocity;
                freeze = true;
                CameraShake.Instance.ShakeOnce(0.2f, 0.15f);
            }
        }
        if (other.CompareTag("tree"))
        {
            velo = new Vector2(0, 0);
            freeze = true;
            GameObject.Find("LevelGenerator").GetComponent<LevelScript>().freeze();
            ps1.Stop();
            ps2.Stop();
            explode2.Play();
            explode1.Play();
            CameraShake.Instance.ShakeOnce(0.2f, 0.15f);
        }
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    public void updateUI()
    {
        for (int x = 0; x < 3; x++)
        {
            healths[x].SetActive(false);
        }
        for (int x = 0; x < health; x++)
        {
            healths[x].SetActive(true);
        }
        for (int x = 0; x < 3; x++)
        {
            gass[x].SetActive(false);
        }
        for (int x = 0; x < gas; x++)
        {
            gass[x].SetActive(true);
        }
        for (int x=0; x<3; x++) {
            mentals[x].SetActive(false);
        }
        for (int x = 0; x < mental; x++)
        {
            mentals[x].SetActive(true);
        }
    }
}
