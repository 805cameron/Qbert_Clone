using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnableObjects
{
    Reds,
    Greens,
    Coilys,
    Uggs,
    Wrongways,
    Slicks   
}

public class EnemySpawner : MonoBehaviour
{
    GameManager gm;

    public GameObject red,
        green,
        coily,
        ugg,
        wrongway,
        slick;

    public Transform topLeftSpawn,
        topRightSpawn,
        uggSpawn,
        wrongwaySpawn;

    [Range(0f, 100f)]
    public int redRate = 30,
        greenRate = 2,
        coilyRate = 30,
        uggAndWrongwayRate = 30,
        slickRate = 5;

    float waitTime = 2.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        SetSpawnScheme();
        StartCoroutine(SpawnRoutine());
    }


    // Update is called once per frame
    void Update()
    {
        if (gm.coilyExists)
        {
            coilyRate = 0;
        }
        else
        {
            coilyRate = 30;
        }

        if (gm.cubesLeft == 0)
        {
            StopAllCoroutines();
        }
    }


    // Return the spawn rate of an object as a fraction of 1
    private float Rate(float rate)
    {
        float sum = redRate + greenRate + coilyRate + uggAndWrongwayRate + slickRate;

        float x = rate / sum;
        return x;
    }


    // Spawning patterns
    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            while (gm.timeFrozen)
            {
                yield return null;
            }

            float redRange = Rate(redRate);
            float greenRange = redRange + Rate(greenRate);
            float coilyRange = greenRange + Rate(coilyRate);
            float uwRange = coilyRange + Rate(uggAndWrongwayRate);
            float slickRange = uwRange + Rate(slickRate);

            float pct = Random.Range(0, 100) / 100f;

            if (pct >= 0 && pct < redRange) { Spawn(red); }
            else if (pct >= redRange && pct < greenRange) { Spawn(green); }
            else if (pct >= greenRange && pct < coilyRange) { Spawn(coily); }
            else if (pct >= coilyRange && pct < uwRange) { Spawn((Random.Range(0, 2) == 0) ? ugg : wrongway); }
            else if (pct >= uwRange && pct < 1.0f) { Spawn(slick); }
            
        }


    }

    void Spawn(GameObject thisObject)
    {
        Transform spawnPoint = this.transform;

        if (thisObject != ugg && thisObject != wrongway)
        {
            spawnPoint = (Random.Range(0, 2) == 0) ? topLeftSpawn : topRightSpawn;
        }
        else if (thisObject == ugg)
        {
            spawnPoint = uggSpawn;
        }
        else if (thisObject == wrongway)
        {
            spawnPoint = wrongwaySpawn;
        }

        Instantiate(thisObject, spawnPoint);

    }


    // Determine Spawn Scheme
    void SetSpawnScheme()
    {
        Vector2 lvl_rd = new Vector2(gm.level, gm.round);

        if(lvl_rd == new Vector2(0, 0))
        {
            redRate = 30;
            greenRate = 0;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 0;

            waitTime = 3f;
        }
        else if (lvl_rd == new Vector2(0, 1))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 0;

            waitTime = 3f;
        }
        else if (lvl_rd == new Vector2(0, 2))
        {
            redRate = 0;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 0;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(0, 3))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(1, 0))
        {
            redRate = 0;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 3f;
        }
        else if (lvl_rd == new Vector2(1, 1))
        {
            redRate = 0;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 3f;
        }
        else if (lvl_rd == new Vector2(1, 2))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 5;

            waitTime = 3f;
        }
        else if (lvl_rd == new Vector2(1, 3))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 50;
            slickRate = 15;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(2, 0))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 0;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(2, 1))
        {
            redRate = 0;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(2, 2))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(2, 3))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(3, 0))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(3, 1))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(3, 2))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 5;

            waitTime = 2f;
        }
        else if (lvl_rd == new Vector2(3, 3))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(4, 0))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 0;

            waitTime = 2f;
        }
        else if (lvl_rd == new Vector2(4, 1))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 0;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd == new Vector2(4, 2))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2f;
        }
        else if (lvl_rd == new Vector2(4, 3))
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2.5f;
        }
        else if (lvl_rd.x >= 7)
        {
            redRate = 30;
            greenRate = 0;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2f;
        }
        else
        {
            redRate = 30;
            greenRate = 2;
            coilyRate = 30;
            uggAndWrongwayRate = 30;
            slickRate = 5;

            waitTime = 2.5f;
        }
    }

}
