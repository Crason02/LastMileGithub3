using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Transform road1;
    public Transform road2;
    public float moveSpeed = 5f;
    public float roadHeight = 10f;
    public bool stop = false;
    public GameObject glass;

    void Start()
    {
        glassFunc();
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
        Instantiate(glass);
        Invoke("glassFunc", Random.Range(2f, 10f));
    }

    public void freeze()
    {
        stop = true;
    }
}
