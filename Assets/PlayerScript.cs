using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject textyk;
    public GameObject winwin;
    public bool spin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("loseGas", 30f);
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            rb.linearVelocity = velo;
            if (spin) {
                transform.Rotate(0f, 0f, velo.x * 50f * Time.deltaTime);
            }
        }
    }

    public void loseGas()
    {
        gas--;
        Invoke("loseGas", 30f);
        updateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Glass"))
        {
            health -= 2;
            updateUI();
            CameraShake.Instance.ShakeOnce(0.2f, 0.1f);
            Destroy(other.gameObject);
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
            CameraShake.Instance.ShakeOnce(0.3f, 0.3f);
            updateUI();
            spin = false;
            CameraShake.Instance.stopShake();
            Invoke("gameOver", 4f);
        }
        if (other.CompareTag("Turtle"))
        {
            mental -= 2;
            health += 1;
            if (health > 3)
            {
                health = 3;
            }
            if (mental>0) {
                CameraShake.Instance.ShakeOnce(0.2f, 0.15f);
            }
            updateUI();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Gas"))
        {
            gas += 1;
            if (gas > 3)
            {
                gas = 3;
            }
            if (health > 3)
            {
                health = 3;
            }
            updateUI();
            CameraShake.Instance.ShakeOnce(0.2f, 0.05f);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Water"))
        {
            if (rb.linearVelocity.x != 0)
            {
                velo = new Vector2(rb.linearVelocity.x*1.1f, rb.linearVelocity.y*1.1f);
                freeze = true;
            }
            else
            {
                velo = new Vector2(6f, 0);
                freeze = true;
            }
            spin = true;
            
        }
        if (other.CompareTag("Beer"))
        {
            mental += 1;
            if (mental > 3)
            {
                mental = 3;
            }
            updateUI();
            CameraShake.Instance.ShakeOnce(0.2f, 0.05f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Med"))
        {
            health += 1;
            if (health > 3)
            {
                health = 3;
            }
            updateUI();
            CameraShake.Instance.ShakeOnce(0.2f, 0.05f);
            Destroy(other.gameObject);
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
        for (int x = 0; x < 3; x++)
        {
            mentals[x].SetActive(false);
        }
        for (int x = 0; x < mental; x++)
        {
            mentals[x].SetActive(true);
        }
        if (mental == 0 && health == 0 && gas == 0)
        {
            Invoke("gameOver", 5f);
            GameObject.Find("timer").GetComponent<TimerScript>().freeze();
            GameObject.Find("LevelGenerator").GetComponent<LevelScript>().freeze();
        }
        else if (mental <= 0)
        {
            CameraShake.Instance.startShake();
            if (rb.linearVelocity.x != 0)
            {
                velo = rb.linearVelocity * 2;
                freeze = true;
            }
            else
            {
                velo = new Vector2(10f, 0);
                freeze = true;
            }
            GameObject.Find("timer").GetComponent<TimerScript>().freeze();
            GameObject.Find("LevelGenerator").GetComponent<LevelScript>().freeze();
            
            
        }
        else if (health <= 0)
        {
            velo = new Vector2(0f, 0f);
            freeze = true;
            ps1.Stop();
            ps2.Stop();
            explode2.Play();
            explode1.Play();
            GameObject.Find("LevelGenerator").GetComponent<LevelScript>().freeze();
            GameObject.Find("timer").GetComponent<TimerScript>().freeze();
            Invoke("gameOver", 4f);
        }
        else if (gas <= 0)
        {
            velo = new Vector2(0f, 0f);
            freeze = true;
            ps1.Stop();
            ps2.Stop();
            GameObject.Find("LevelGenerator").GetComponent<LevelScript>().freeze();
            GameObject.Find("timer").GetComponent<TimerScript>().freeze();
            Invoke("gameOver", 4f);
        }
    }

    public void gameOver()
    {
        textyk.SetActive(true);
    }

    public void winGame()
    {
        GameObject.Find("LevelGenerator").GetComponent<LevelScript>().freeze();
        freeze = true;
        winwin.SetActive(true);
    }

}
