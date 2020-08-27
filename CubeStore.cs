using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStore : MonoBehaviour
{
    public static CubeStore instance;

    private Sprite[] _sprites = new Sprite[35];

    // (level, round, cube tier)
    public static Sprite[,,] spriteArray = new Sprite[9, 4, 3];
    // (level, round)
    public static Sprite[,] spriteIconArray = new Sprite[9, 4];


    // Awake is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _sprites = Resources.LoadAll<Sprite>("cube_sprite_sheet");

        InitializeArray();
    }


    // Gets sprite from array
    public Sprite GetSprite(string name)
    {
        Sprite s = null;

        for (int i = 0; i < _sprites.Length; i++)
        {
            if (_sprites[i].name == name)
            {
                s = _sprites[i];
            }

        }

        return s;
    }


    // Stores correct cube in every 3D array slot
    void InitializeArray()
    {
        //LEVEL 1
        //1-1
        spriteArray[0, 0, 0] = GetSprite("cube_sprite2");
        spriteArray[0, 0, 1] = GetSprite("cube_sprite1");
        spriteIconArray[0, 0] = GetSprite("cube_UI_sprite1");
        //1-2
        spriteArray[0, 1, 0] = GetSprite("cube_sprite4");
        spriteArray[0, 1, 1] = GetSprite("cube_sprite3");
        spriteIconArray[0, 1] = GetSprite("cube_UI_sprite3");
        //1-3
        spriteArray[0, 2, 0] = GetSprite("cube_sprite6");
        spriteArray[0, 2, 1] = GetSprite("cube_sprite7");
        spriteIconArray[0, 2] = GetSprite("cube_UI_sprite7");

        //1-4
        spriteArray[0, 3, 0] = GetSprite("cube_sprite10");
        spriteArray[0, 3, 1] = GetSprite("cube_sprite9");
        spriteIconArray[0, 3] = GetSprite("cube_UI_sprite9");


        //LEVEL 2
        //2-1
        spriteArray[1, 0, 0] = GetSprite("cube_sprite3");
        spriteArray[1, 0, 1] = GetSprite("cube_sprite4");
        spriteArray[1, 0, 2] = GetSprite("cube_sprite5");
        spriteIconArray[1, 0] = GetSprite("cube_UI_sprite5");

        //2-2
        spriteArray[1, 1, 0] = GetSprite("cube_sprite11");
        spriteArray[1, 1, 1] = GetSprite("cube_sprite10");
        spriteArray[1, 1, 2] = GetSprite("cube_sprite9");
        spriteIconArray[1, 1] = GetSprite("cube_UI_sprite9");

        //2-3
        spriteArray[1, 2, 0] = GetSprite("cube_sprite0");
        spriteArray[1, 2, 1] = GetSprite("cube_sprite2");
        spriteArray[1, 2, 2] = GetSprite("cube_sprite1");
        spriteIconArray[1, 2] = GetSprite("cube_UI_sprite1");

        //2-4
        spriteArray[1, 3, 0] = GetSprite("cube_sprite13");
        spriteArray[1, 3, 1] = GetSprite("cube_sprite12");
        spriteArray[1, 3, 2] = GetSprite("cube_sprite14");
        spriteIconArray[1, 3] = GetSprite("cube_UI_sprite14");


        //LEVEL 3
        //3-1
        spriteArray[2, 0, 0] = GetSprite("cube_sprite17");
        spriteArray[2, 0, 1] = GetSprite("cube_sprite16");
        spriteIconArray[2, 0] = GetSprite("cube_UI_sprite16");

        //3-2
        spriteArray[2, 1, 0] = GetSprite("cube_sprite7");
        spriteArray[2, 1, 1] = GetSprite("cube_sprite6");
        spriteIconArray[2, 1] = GetSprite("cube_UI_sprite6");

        //3-3
        spriteArray[2, 2, 0] = GetSprite("cube_sprite3");
        spriteArray[2, 2, 1] = GetSprite("cube_sprite4");
        spriteIconArray[2, 2] = GetSprite("cube_UI_sprite4");

        //3-4
        spriteArray[2, 3, 0] = GetSprite("cube_sprite1");
        spriteArray[2, 3, 1] = GetSprite("cube_sprite2");
        spriteIconArray[2, 3] = GetSprite("cube_UI_sprite2");


        //LEVEL 4
        //4-1
        spriteArray[3, 0, 0] = GetSprite("cube_sprite5");
        spriteArray[3, 0, 1] = GetSprite("cube_sprite4");
        spriteArray[3, 0, 2] = GetSprite("cube_sprite3");
        spriteIconArray[3, 0] = GetSprite("cube_UI_sprite3");

        //4-2
        spriteArray[3, 1, 0] = GetSprite("cube_sprite12");
        spriteArray[3, 1, 1] = GetSprite("cube_sprite14");
        spriteArray[3, 1, 2] = GetSprite("cube_sprite13");
        spriteIconArray[3, 1] = GetSprite("cube_UI_sprite13");

        //4-3
        spriteArray[3, 2, 0] = GetSprite("cube_sprite1");
        spriteArray[3, 2, 1] = GetSprite("cube_sprite0");
        spriteArray[3, 2, 2] = GetSprite("cube_sprite2");
        spriteIconArray[3, 2] = GetSprite("cube_UI_sprite2");

        //4-4
        spriteArray[3, 3, 0] = GetSprite("cube_sprite11");
        spriteArray[3, 3, 1] = GetSprite("cube_sprite10");
        spriteArray[3, 3, 2] = GetSprite("cube_sprite9");
        spriteIconArray[3, 3] = GetSprite("cube_UI_sprite9");


        //LEVEL 5
        //5-1
        spriteArray[4, 0, 0] = GetSprite("cube_sprite0");
        spriteArray[4, 0, 1] = GetSprite("cube_sprite1");
        spriteArray[4, 0, 2] = GetSprite("cube_sprite2");
        spriteIconArray[4, 0] = GetSprite("cube_UI_sprite2");

        //5-2
        spriteArray[4, 1, 0] = GetSprite("cube_sprite5");
        spriteArray[4, 1, 1] = GetSprite("cube_sprite3");
        spriteArray[4, 1, 2] = GetSprite("cube_sprite4");
        spriteIconArray[4, 1] = GetSprite("cube_UI_sprite4");

        //5-3
        spriteArray[4, 2, 0] = GetSprite("cube_sprite7");
        spriteArray[4, 2, 1] = GetSprite("cube_sprite8");
        spriteArray[4, 2, 2] = GetSprite("cube_sprite6");
        spriteIconArray[4, 2] = GetSprite("cube_UI_sprite6");
        //5-4
        spriteArray[4, 3, 0] = GetSprite("cube_sprite9");
        spriteArray[4, 3, 1] = GetSprite("cube_sprite10");
        spriteArray[4, 3, 2] = GetSprite("cube_sprite11");
        spriteIconArray[4, 3] = GetSprite("cube_UI_sprite11");

        //LEVEL 6
        //6-1
        spriteArray[5, 0, 0] = GetSprite("cube_sprite5");
        spriteArray[5, 0, 1] = GetSprite("cube_sprite3");
        spriteArray[5, 0, 2] = GetSprite("cube_sprite4");
        spriteIconArray[5, 0] = GetSprite("cube_UI_sprite4");
        //6-2
        spriteArray[5, 1, 0] = GetSprite("cube_sprite10");
        spriteArray[5, 1, 1] = GetSprite("cube_sprite11");
        spriteArray[5, 1, 2] = GetSprite("cube_sprite9");
        spriteIconArray[5, 1] = GetSprite("cube_UI_sprite9");
        //6-3
        spriteArray[5, 2, 0] = GetSprite("cube_sprite0");
        spriteArray[5, 2, 1] = GetSprite("cube_sprite1");
        spriteArray[5, 2, 2] = GetSprite("cube_sprite2");
        spriteIconArray[5, 2] = GetSprite("cube_UI_sprite2");
        //6-4
        spriteArray[5, 3, 0] = GetSprite("cube_sprite14");
        spriteArray[5, 3, 1] = GetSprite("cube_sprite12");
        spriteArray[5, 3, 2] = GetSprite("cube_sprite13");
        spriteIconArray[5, 3] = GetSprite("cube_UI_sprite13");

        //LEVEL 7
        //7-1
        spriteArray[6, 0, 0] = GetSprite("cube_sprite16");
        spriteArray[6, 0, 1] = GetSprite("cube_sprite17");
        spriteArray[6, 0, 2] = GetSprite("cube_sprite15");
        spriteIconArray[6, 0] = GetSprite("cube_UI_sprite15");
        //7-2
        spriteArray[6, 1, 0] = GetSprite("cube_sprite6");
        spriteArray[6, 1, 1] = GetSprite("cube_sprite7");
        spriteArray[6, 1, 2] = GetSprite("cube_sprite8");
        spriteIconArray[6, 1] = GetSprite("cube_UI_sprite8");
        //7-3
        spriteArray[6, 2, 0] = GetSprite("cube_sprite5");
        spriteArray[6, 2, 1] = GetSprite("cube_sprite3");
        spriteArray[6, 2, 2] = GetSprite("cube_sprite4");
        spriteIconArray[6, 2] = GetSprite("cube_UI_sprite4");
        //7-4
        spriteArray[6, 3, 0] = GetSprite("cube_sprite1");
        spriteArray[6, 3, 1] = GetSprite("cube_sprite2");
        spriteArray[6, 3, 2] = GetSprite("cube_sprite0");
        spriteIconArray[6, 3] = GetSprite("cube_UI_sprite0");

        //LEVEL 8
        //8-1
        spriteArray[7, 0, 0] = GetSprite("cube_sprite3");
        spriteArray[7, 0, 1] = GetSprite("cube_sprite4");
        spriteArray[7, 0, 2] = GetSprite("cube_sprite5");
        spriteIconArray[7, 0] = GetSprite("cube_UI_sprite5");
        //8-2
        spriteArray[7, 1, 0] = GetSprite("cube_sprite14");
        spriteArray[7, 1, 1] = GetSprite("cube_sprite12");
        spriteArray[7, 1, 2] = GetSprite("cube_sprite13");
        spriteIconArray[7, 1] = GetSprite("cube_UI_sprite13");
        //8-3
        spriteArray[7, 2, 0] = GetSprite("cube_sprite1");
        spriteArray[7, 2, 1] = GetSprite("cube_sprite2");
        spriteArray[7, 2, 2] = GetSprite("cube_sprite0");
        spriteIconArray[7, 2] = GetSprite("cube_UI_sprite0");
        //8-4
        spriteArray[7, 3, 0] = GetSprite("cube_sprite9");
        spriteArray[7, 3, 1] = GetSprite("cube_sprite10");
        spriteArray[7, 3, 2] = GetSprite("cube_sprite11");
        spriteIconArray[7, 3] = GetSprite("cube_UI_sprite11");

        //LEVEL 9
        //9-1
        spriteArray[8, 0, 0] = GetSprite("cube_sprite2");
        spriteArray[8, 0, 1] = GetSprite("cube_sprite0");
        spriteArray[8, 0, 2] = GetSprite("cube_sprite1");
        spriteIconArray[8, 0] = GetSprite("cube_UI_sprite1");
        //9-2
        spriteArray[8, 1, 0] = GetSprite("cube_sprite4");
        spriteArray[8, 1, 1] = GetSprite("cube_sprite5");
        spriteArray[8, 1, 2] = GetSprite("cube_sprite3");
        spriteIconArray[8, 1] = GetSprite("cube_UI_sprite3");
        //9-3
        spriteArray[8, 2, 0] = GetSprite("cube_sprite15");
        spriteArray[8, 2, 1] = GetSprite("cube_sprite16");
        spriteArray[8, 2, 2] = GetSprite("cube_sprite17");
        spriteIconArray[8, 1] = GetSprite("cube_UI_sprite17");
        //9-4
        spriteArray[8, 3, 0] = GetSprite("cube_sprite11");
        spriteArray[8, 3, 1] = GetSprite("cube_sprite9");
        spriteArray[8, 3, 2] = GetSprite("cube_sprite10");
        spriteIconArray[8, 3] = GetSprite("cube_UI_sprite10");
    }

}
