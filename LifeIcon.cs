using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon : MonoBehaviour
{
    SpriteRenderer sr;
    GameManager gm;

    public int lifeNo;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.lives >= lifeNo)
        {
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
    }
}
