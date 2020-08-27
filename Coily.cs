using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coily : MonoBehaviour
{
    GameManager gm;
    GameObject player;
    Animator animator;

    public float airTime = 0.4f,
        moveDelay = 0.3f;

    bool grounded = true,
        falling,
        readyToMove;

    public Vector2 target;

    // Awake is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        transform.parent = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        player = GameObject.FindGameObjectWithTag("Player");
        readyToMove = true;

    }


    // Update is called once per frame
    void Update()
    {
        if(readyToMove == true && falling == false)
        {
            StartCoroutine(Move(ShortestRoute()));
        }

        if(gm.cubesLeft == 0)
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
            case Direction.UpRight:
                animator.SetInteger("direction", 0);
                yield return StartCoroutine(Jump(dir));
                break;
            case Direction.UpLeft:
                animator.SetInteger("direction", 1);
                yield return StartCoroutine(Jump(dir));
                break;
            case Direction.DownLeft:
                animator.SetInteger("direction", 2);
                yield return StartCoroutine(Jump(dir));
                break;
            case Direction.DownRight:
                animator.SetInteger("direction", 3);
                yield return StartCoroutine(Jump(dir));
                break;
        }
        yield return new WaitForSeconds(moveDelay);

        readyToMove = true;
    }


    // Jump to nearby tile using Bezier curve to calculate position
    IEnumerator Jump(Direction dir)
    {
        grounded = false;
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
                case Direction.UpRight:
                    p2 = p0 + new Vector2(1.6f, 2.4f);
                    break;
                case Direction.UpLeft:
                    p2 = p0 + new Vector2(-1.6f, 2.4f);
                    break;
                case Direction.DownLeft:
                    p2 = p0 + new Vector2(-1.6f, -2.4f);
                    break;
                case Direction.DownRight:
                    p2 = p0 + new Vector2(1.6f, -2.4f);
                    break;
            }
            p1.x = (p0.x + p2.x) / 2;
            p1.y = ((p0.y + p2.y) / 2) + 3.6f;

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


    // Returns shortest route to player out of his 4 movement options
    Direction ShortestRoute()
    {
        Direction dir = Direction.UpRight;

        if (player.GetComponent<Player>().onDisc == true)
        {
            target = player.GetComponent<Player>().discLocation;
        }
        else
        {
            target = player.transform.position;
        }

        float UpRightDis = Distance(transform.position + new Vector3(1.6f, 2.4f), target);
        float UpLeftDis = Distance(transform.position + new Vector3(-1.6f, 2.4f), target);
        float DownLeftDis = Distance(transform.position + new Vector3(-1.6f, -2.4f), target);
        float DownRightDis = Distance(transform.position + new Vector3(1.6f, -2.4f), target);

        float min = Mathf.Min(UpRightDis, UpLeftDis, DownLeftDis, DownRightDis);
        switch (min)
        {
            case float n when n == UpRightDis:
                dir = Direction.UpRight;
                break;
            case float n when n == UpLeftDis:
                dir = Direction.UpLeft;
                break;
            case float n when n == DownLeftDis:
                dir = Direction.DownLeft;                
                break;
            case float n when n == DownRightDis: 
                dir = Direction.DownRight;               
                break;
        }

        return dir;
    }


    // Coily falls off Pyramid
    IEnumerator Fall()
    {
        falling = true;
        GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
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

        Die();
    }


    // Die.
    void Die()
    {
        gm.KillNonPlayers();
        gm.score += 500;
        gm.pointsUntilLife -= 500;
        Destroy(gameObject);
    }


    // Distance Formula
    float Distance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt(Mathf.Pow((p2.x - p1.x), 2) + Mathf.Pow((p2.y - p1.y), 2));
    }


    // Collider events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "cube")
        {
            animator.SetBool("isGrounded", true);
        }


        if(collision.gameObject.tag == "pit")
        {
            StopAllCoroutines();
            StartCoroutine(Fall());
        }
    }
}
