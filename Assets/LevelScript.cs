using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Transform road1;
    public Transform road2;
    public float moveSpeed = 5f;
    public float roadHeight = 10f;
    public bool stop = false;
    public GameObject glass;
    public GameObject turtle;
    public GameObject gas;
    public GameObject water;
    public GameObject beer;
    public GameObject med;

    void Start()
    {
        Invoke("gasFunc", Random.Range(1f, 20f));
        Invoke("glassFunc", Random.Range(1f, 20f));
        Invoke("waterFunc", 15f);
        Invoke("turtFunc", Random.Range(1f, 20f));
        Invoke("beerFunc", Random.Range(1f, 20f));
        Invoke("medFunc", Random.Range(1f, 20f));
    }
    void Update()
    {
        if (!stop)
        {
            road1.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            road2.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        if (road1.position.y < -roadHeight)
        {
            road1.position = new Vector3(road1.position.x, road2.position.y + roadHeight, road1.position.z);
        }
        if (road2.position.y < -roadHeight)
        {
            road2.position = new Vector3(road2.position.x, road1.position.y + roadHeight, road2.position.z);
        }
        

    }

    public void glassFunc()
    {
        if (!stop) {
            Instantiate(glass);
            Invoke("glassFunc", Random.Range(10f, 20f));
        }
    }
    public void turtFunc()
    {
        if (!stop) {
            Instantiate(turtle);
            Invoke("turtFunc", Random.Range(10f, 20f));
        }
    }
    public void gasFunc()
    {
        if (!stop) {
            Instantiate(gas);
            Invoke("gasFunc", Random.Range(10f, 20f));
        }
    }

    public void waterFunc()
    {
        if (!stop) {
            Instantiate(water);
            Invoke("waterFunc", Random.Range(10f, 20f));
        }
    }
    public void beerFunc()
    {
        if (!stop) {
            Instantiate(beer);
            Invoke("beerFunc", Random.Range(10f, 20f));
        }
    }

    public void medFunc()
    {
        if (!stop) {
            Instantiate(med);
            Invoke("medFunc", Random.Range(10f, 20f));
        }
    }

    public void freeze()
    {
        stop = true;
    }
}
