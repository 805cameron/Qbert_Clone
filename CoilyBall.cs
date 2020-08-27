using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyBall : MonoBehaviour
{
    GameManager gm;
    Animator animator;
    public GameObject coily;

    public float airTime = 0.4f,
        moveDelay = 0.3f;

    bool readyToMove,       
        readyToEvolve;


    // Awake is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        readyToEvolve = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.coilyExists = true;
        StartCoroutine(Fall());
    }


    // Update is called once per frame
    void Update()
    {
        if (readyToMove == true)
        {
            StartCoroutine(Move(RandomDirection()));
        }

        if (gm.cubesLeft == 0)
        {
            StopAllCoroutines();
            readyToMove = false;
        }
    }


    // Move.
    IEnumerator Move(Direction dir)
    {
        readyToMove = false;

        switch (dir)
        {
            case Direction.DownLeft:
                yield return StartCoroutine(Jump(dir));

                break;
            case Direction.DownRight:
                yield return StartCoroutine(Jump(dir));

                break;
        }

        yield return new WaitForSeconds(moveDelay);

        if (readyToEvolve == false)
        {
            readyToMove = true;
        }
    }


    // Jump to nearby tile using Bezier curve to calculate position
    IEnumerator Jump(Direction dir)
    {
        bool grounded = false;
        animator.SetBool("isGrounded", false);
        float timeStart = Time.time;

        Vector2 p0 = transform.position, p1, p2 = Vector2.zero, p01, p12, p012;


        while (grounded == false)
        {
            float u = (Time.time - timeStart) / airTime;

            if (u >= 1)
            {
                u = 1;
                grounded = true;
            }

            switch (dir)
            {
                case Direction.DownLeft:
                    p2 = p0 + new Vector2(-1.6f, -2.4f);
                    break;
                case Direction.DownRight:
                    p2 = p0 + new Vector2(1.6f, -2.4f);
                    break;
            }
            p1.x = (p0.x + p2.x) / 2;
            p1.y = ((p0.y + p2.y) / 2) + 2.4f;

            p01 = (1 - u) * p0 + u * p1;
            p12 = (1 - u) * p1 + u * p2;
            p012 = (1 - u) * p01 + u * p12;
            transform.position = p012;
            while (gm.timeFrozen)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
        }
    }


    // Random Direction
    Direction RandomDirection()
    {
        Direction dir = Direction.DownRight;

        int n = Random.Range(0, 2);

        switch (n)
        {
            case 0:
                dir = Direction.DownLeft;
                break;
            case 1:
                dir = Direction.DownRight;
                break;
        }

        return dir;
    }


    // Coily Ball falls from Spawn
    IEnumerator Fall()
    {
        bool falling = true;
        readyToMove = false;
        float timeStart = Time.time;

        Vector2 startingPos = transform.position;

        Vector2 p0 = transform.position;
        Vector2 p1 = transform.position + new Vector3(0, -7.2f);

        while (falling == true)
        {
            float u = (Time.time - timeStart) / airTime;

            if (u >= 1)
            {
                u = 1;
                falling = false;
            }

            Vector2 p01 = (1 - u) * p0 + u * p1;

            transform.position = p01;
            while (gm.timeFrozen)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(moveDelay);

        readyToMove = true;
    }


    // Transform into a Coily Snake
    IEnumerator Evolve()
    {
        readyToMove = false;

        yield return new WaitForSeconds(2f);

        Instantiate(coily, this.transform);
        Destroy(gameObject);
    }


    // Collision events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "cube")
        {
            animator.SetBool("isGrounded", true);
        }

        if(collision.gameObject.tag == "bottomRow")
        {
                readyToEvolve = true;
                readyToMove = false;
                StartCoroutine(Evolve());
        }
    }
}
