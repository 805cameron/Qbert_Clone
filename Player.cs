using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

public class Player : MonoBehaviour
{
    GameManager gm;
    Animator animator;
    public GameObject swearBubble;

    [SerializeField]
    bool readyToMove,
        falling,
        madeFirstMove;

    public bool onDisc;       

    public float airTime = 0.3f,
        moveDelay = 0.2f;

    public Vector2 discLocation;

    // Awake is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.player = this.gameObject;

        madeFirstMove = false;
        falling = false;
        readyToMove = true;
        onDisc = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(readyToMove == true && falling == false)
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) )// && !(gm.playerCoord.x + 1 == gm.playerCoord.y))
            {
                StartCoroutine(Move(Direction.UpRight));
            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) )// && !(gm.playerCoord.y == 0))
            {
                StartCoroutine(Move(Direction.UpLeft));
            }
            else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) )// && !(gm.playerCoord.x == 8))
            {
                StartCoroutine(Move(Direction.DownLeft));
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) )// && !(gm.playerCoord.x == 8))
            {
                StartCoroutine(Move(Direction.DownRight));
            }
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
        madeFirstMove = true;
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
        bool grounded = false;
        animator.SetBool("isGrounded", false);
        float timeStart = Time.time;

        Vector2 p0 = transform.position, p1, p2 = Vector2.zero, p01, p12, p012;
            

        while(grounded == false)
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
            p1.y = ((p0.y + p2.y) / 2) + 2.4f;

            p01 = (1 - u) * p0 + u * p1;
            p12 = (1 - u) * p1 + u * p2;
            p012 = (1 - u) * p01 + u * p12;
            transform.position = p012;

            yield return new WaitForEndOfFrame();
        }
    }


    // Qbert falls off (or onto) Pyramid
    IEnumerator Fall()
    {
        falling = true;
        readyToMove = false;
        if(onDisc == false)
        {
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }

        float timeStart = Time.time;

        Vector2 startingPos = transform.position;

        Vector2 p0 = transform.position;
        Vector2 p1 = transform.position + new Vector3(0, ((onDisc) ? -4f : -6f));

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

            yield return new WaitForEndOfFrame();
        }

        if(onDisc == false)
        {
            Time.timeScale = gm.timeScaleMultiplier;
            readyToMove = false;
            gm.ResetPlayer();
            Destroy(gameObject);
        }
        else
        {
            onDisc = false;
            yield return new WaitForSeconds(moveDelay);
            readyToMove = true;
        }

    }


    // Move towards the disc drop-off point at the top of the pyramid
    IEnumerator FollowDiscPath(GameObject disc)
    {
        bool discMoving = true;
        readyToMove = false;

        float timeStart = Time.time;

        Vector2 startingPos = transform.position;

        Vector2 p0 = transform.position;
        Vector2 p1 = new Vector2(0, 10.8f);

        while (discMoving == true)
        {
            float u = (Time.time - timeStart) / 1.75f;

            if (u >= 1)
            {
                u = 1;
                discMoving = false;
            }

            Vector2 p01 = (1 - u) * p0 + u * p1;

            transform.position = p01;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(disc);
        yield return StartCoroutine(Fall());
    }


    // Die if hit by enemy
    IEnumerator Die()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);

        swearBubble.SetActive(true);

        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = gm.timeScaleMultiplier;
        gm.ResetPlayer();
        Destroy(gameObject);
    }


    // Collider events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cube
        if(collision.gameObject.tag == "cube" && madeFirstMove == true && falling == false)
        {
            Cube cube = collision.gameObject.GetComponent<Cube>();
            if((cube.tier != gm.topTier))
            {
                cube.tier++;
                cube.ChangeSprite();
                if(!cube.hasBeenTopTier)
                {
                    gm.score += (cube.tier == gm.topTier) ? 25 : 15;
                    gm.pointsUntilLife -= (cube.tier == gm.topTier) ? 25 : 15;
                }
                if(cube.tier == gm.topTier)
                {
                    gm.cubesLeft--;
                    cube.hasBeenTopTier = true;
                }

            }
            else
            {
                if (gm.colorSubtracting == true)
                {
                    cube.tier--;
                    cube.ChangeSprite();
                    gm.cubesLeft++;
                }
                else if(gm.colorLooping == true)
                {
                    cube.tier = 0;
                    cube.ChangeSprite();
                    gm.cubesLeft++;
                }
            } 
            
            animator.SetBool("isGrounded", true);
        }           

        // Enemy
        if(collision.gameObject.tag == "enemy" && gm.timeFrozen == false)
        {
            StartCoroutine(Die());
        }

        // Gridpoint
        if((collision.gameObject.tag == "gridpoint" || collision.gameObject.tag == "bottomRow") && falling == false)
        {
            gm.respawnPoint = collision.GetComponent<Transform>();
        }

        // Disc
        if(collision.gameObject.tag == "disc")
        {
            collision.gameObject.transform.parent.GetComponent<DiskSpawner>().diskHere = false;
            discLocation = collision.transform.position;
            collision.gameObject.transform.parent = this.gameObject.transform;
            onDisc = true;            
            StartCoroutine(FollowDiscPath(collision.gameObject));           
        }

        if(collision.gameObject.tag == "pit")
        {
            if (collision.gameObject.GetComponent<DiskSpawner>().diskHere == false && onDisc == false)
            {
                gm.respawnPoint = GameObject.Find("(1.0, 1.0)").GetComponent<Transform>();
                print(gm.respawnPoint);
                StartCoroutine(Fall());
            }
            
        }
    }
}
