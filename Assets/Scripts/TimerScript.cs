using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float time = 0f;
    public bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.position = new Vector3(transform.position.x - 0.0005f, transform.position.y, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("timert"))
        {
            freeze();
            GameObject.Find("Player").GetComponent<PlayerScript>().winGame();
        }
    }

    public void freeze()
    {
        stop = true;
    }
}
