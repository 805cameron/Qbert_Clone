using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    GameManager gm;

    public GameObject disk;

    public bool diskHere = false;

    // Vector3(level, round, disk color); added in inspector
    public List<Vector3> validRounds = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;

        foreach(Vector3 round in validRounds)
        {
            if (new Vector2(round.x, round.y) == new Vector2(gm.level, gm.round))
            {
                GameObject disc = Instantiate(disk, this.transform);
                disc.GetComponentInChildren<Animator>().SetInteger("diskColor", Mathf.RoundToInt(round.z));
                diskHere = true;
            }
        }
    }
}
