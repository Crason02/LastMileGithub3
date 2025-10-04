using UnityEngine;
using System.Collections.Generic;
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
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject endCut;
    public bool cutting = false;
    public bool yaCut = false;

    void Start()
    {
        Invoke("gasFunc", Random.Range(1f, 20f));
        Invoke("glassFunc", Random.Range(1f, 20f));
        Invoke("waterFunc", 15f);
        Invoke("turtFunc", Random.Range(1f, 20f));
        Invoke("beerFunc", Random.Range(1f, 20f));
        Invoke("medFunc", Random.Range(1f, 20f));
        endCut.SetActive(false);
    }
    void Update()
    {
        if (!stop)
        {
            road1.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            road2.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        }
        if (cutting)
        {
            if (endCut.transform.position.y > -91.01)
            {
                endCut.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
            else if(!yaCut)
            {
                yaCut = true;
                GameObject.Find("Player").GetComponent<PlayerScript>().win2();
            }
        }

        if (road1.position.y < -roadHeight && !cutting)
        {
            road1.position = new Vector3(road1.position.x, road2.position.y + roadHeight, road1.position.z);
        }
        if (road2.position.y < -roadHeight && !cutting)
        {
            road2.position = new Vector3(road2.position.x, road1.position.y + roadHeight, road2.position.z);
        }


    }

    public void glassFunc()
    {
        if (!stop)
        {
            obstacles.Add(Instantiate(glass));
            Invoke("glassFunc", Random.Range(10f, 20f));
        }
    }
    public void turtFunc()
    {
        if (!stop)
        {
            obstacles.Add(Instantiate(turtle));
            Invoke("turtFunc", Random.Range(10f, 20f));
        }
    }
    public void gasFunc()
    {
        if (!stop)
        {
            obstacles.Add(Instantiate(gas));
            Invoke("gasFunc", Random.Range(10f, 20f));
        }
    }

    public void waterFunc()
    {
        if (!stop)
        {
            obstacles.Add(Instantiate(water));
            Invoke("waterFunc", Random.Range(10f, 20f));
        }
    }
    public void beerFunc()
    {
        if (!stop)
        {
            obstacles.Add(Instantiate(beer));
            Invoke("beerFunc", Random.Range(10f, 20f));
        }
    }

    public void medFunc()
    {
        if (!stop)
        {
            obstacles.Add(Instantiate(med));
            Invoke("medFunc", Random.Range(10f, 20f));
        }
    }

    public void freeze()
    {
        stop = true;
        stopSpawn();
        foreach (GameObject obj in obstacles)
        {
            if (obj.GetComponent<BeerScript>() != null && obj != null)
            {
                obj.GetComponent<BeerScript>().freeze();
            }
        }
    }

    public void stopSpawn()
    {
        CancelInvoke();
        obstacles.RemoveAll(obj => obj == null);
    }

    public void cutscene()
    {
        if (road1.position.y > road2.position.y)
        {
            endCut.transform.position = new Vector3(road2.position.x, road1.position.y, road2.position.z);
        }
        endCut.SetActive(true);
        cutting = true;
    }
}
