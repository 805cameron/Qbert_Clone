using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    GameManager gm;
    Sprite sprite;

    public int tier;

    public bool hasBeenTopTier = false;

    bool celebrationStarted = false;


    //Awake is called before the first frame update
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        tier = 0;
        ChangeSprite();
    }


    // Update is called once per frame
    void Update()
    {
        if (gm.cubesLeft == 0 && celebrationStarted == false)
        {
            StartCoroutine(RoundCelebration());
            celebrationStarted = true;
        }
        
    }


    // Changes sprite depending on level, round, cube tier
    public void ChangeSprite()
    {
        sprite = CubeStore.spriteArray[gm.level, gm.round, tier];
        GetComponent<SpriteRenderer>().sprite = sprite;
    }


    // Everybody Flash!
    public IEnumerator RoundCelebration()
    {
        yield return null;

        for (int i = 0; i < 24; i++)
        {
            if ((tier + 1 <= gm.topTier))
            {
                tier += 1;
                ChangeSprite();               
            }
            else
            { 
                tier = 0;
                ChangeSprite();                             
            }

            yield return new WaitForSecondsRealtime(0.08f);
        }

        tier = gm.topTier;
        ChangeSprite();
    }
}
