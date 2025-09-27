using UnityEngine;

public class GlassScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(Random.Range(-1.22f, 7.75f), transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.down * 10f * Time.deltaTime);
    }
}
