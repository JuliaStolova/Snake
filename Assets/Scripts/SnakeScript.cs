using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeScript : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> lSegments;
    public Transform segmentPrefab;
    public int instantiateSize = 4;
    private int foodScore = 0;
    private int deathCounter = 0;
    public Text fruitsEatenScore;
    public Text diedScore;
    public Text gameOver;
    public Text superFruity;

    private void Start()
    {
        // creating a list and a snake itself
        lSegments = new List<Transform>();
        lSegments.Add(this.transform);

        for (int i = 1; i < this.instantiateSize; i++)
        {
            lSegments.Add(Instantiate(this.segmentPrefab));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        // Snake movement
        for (int i = lSegments.Count - 1; i > 0; i--)
        {
            lSegments[i].position = lSegments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0f);

        //text score
        fruitsEatenScore.text = $"Fruits eaten: {foodScore}";
        diedScore.text = $"Died times: {deathCounter}";
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = lSegments[lSegments.Count - 1].position;
        lSegments.Add(segment);
        foodScore += 1;
    }

    private void ResetState()
    {
        for (int i = 1; i < lSegments.Count; i++)
        {
            Destroy(lSegments[i].gameObject);
        }

        lSegments.Clear();
        lSegments.Add(this.transform);

        for (int i = 1; i < this.instantiateSize; i++)
        {
            lSegments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
        foodScore = 0;
        deathCounter += 1;
    }

    private void Damage()
    {
        if (lSegments.Count - 1 < 1)
        {
            ResetState();
        }
        else
        {
            Destroy(lSegments[lSegments.Count - 1].gameObject);
            lSegments.RemoveAt(lSegments.Count - 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.tag)
        {
            case "Food":
                Grow();
                break;

            case "Obstacle":
                ResetState();
                break;

            case "Spike":
                Damage();
                break;

            case "SuperFruit":
                for (int i = 0; i < 3; i++)
                {
                    Grow();
                }
                collision.gameObject.SetActive(false);
                break;
        }

    }
}
