using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIcon : MonoBehaviour
{
    GameManager gm;
    Sprite sprite;


    //Awake is called before the first frame update
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        ChangeSprite();
    }


    public void ChangeSprite()
    {
        sprite = CubeStore.spriteIconArray[gm.level, gm.round];
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
