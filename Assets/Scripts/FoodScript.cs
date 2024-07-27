using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public GameObject superFruit;

    private void Start()
    {
        RandomizePosition();
        SuperFruitSpawn();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RandomizePosition();
            if (superFruit.active == false)
            {
                SuperFruitSpawn();
            }
        }
        else if (collision.tag == "Obstacle" || collision.tag == "SuperFruit")
        {
            RandomizePosition();
        }
    }

    private void SuperFruitSpawn()
    {
        float randomNum = Mathf.Round(Random.Range(1f, 25f));
        Debug.Log(randomNum);
        if (randomNum == 24f)
        {
            superFruit.SetActive(true);
            Debug.Log("spawned");
        }
        else
        {
            superFruit.SetActive(false);
            Debug.Log("disapeared");
        }
    }
}
