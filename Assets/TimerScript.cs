using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x-0.00012f, transform.position.y, 0);
    }
}
