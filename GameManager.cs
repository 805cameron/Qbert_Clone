using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject playerPrefab;

    public Text scoreText,
        levelText,
        roundText,
        bonusText,
        bonusScoreText;

    public int level = 1,
        round = 0,
        score = 0,
        currentBonus = 1000,
        lives = 2,
        pointsUntilLife = 6000,
        topTier = 1,
        cubesLeft = 28;

    public float timeScaleMultiplier = 1f;
    public float tsmIncrements = 0.075f;

    public bool colorLooping = false,
        colorSubtracting = false,
        coilyExists = false,
        timeFrozen = false;

    bool alreadyWinning = false,
        isPaused = false;    

    public Transform respawnPoint;


    // Awake is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (cubesLeft == 0)
        {
            if (alreadyWinning == false)
            {
                alreadyWinning = true;
                StartCoroutine(EndofRoundRoutine());
            }           
        }

        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            PauseGame();
        }

        if (pointsUntilLife == 0)
        {
            GiveLife();
        }

    }


    // Gives the player another life and resets the countdown to next life
    void GiveLife()
    {
        if(lives >= 9)
        {
            lives++;
        }
        pointsUntilLife = 12000;
    }


    // Freezes time excpet player for 7s
    IEnumerator FreezeTheTime()
    {
        timeFrozen = true;
        yield return new WaitForSecondsRealtime(7f);
        timeFrozen = false;
        print(player.transform.position);
    }
    // so other classes can call on it
    public void FreezeTime() { StartCoroutine(FreezeTheTime()); }


    // Increase round by 1, move to next level if needed, reset the scene
    IEnumerator EndofRoundRoutine()
    {
        yield return new WaitForSeconds(1.92f);

        Destroy(player);
        yield return StartCoroutine(KillTheNonPlayers());

        GiveBonus();

        yield return new WaitForSeconds(1.5f);

        while (GameObject.FindGameObjectWithTag("disc") != null)
        {             
            GameObject disc = GameObject.FindGameObjectWithTag("disc");             
            score += 50;
            pointsUntilLife -= 50;
            Destroy(disc);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        // prepare next round
        round++;
        if(currentBonus < 5000)
        {
            currentBonus += 250;
        }
        if (round == 4)
        {
            round = 0;
            NewLevel();
        }

        ResetVariables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Increase level by 1 and change rules accordingly
    public void NewLevel()
    {
        level++;

        switch (level)
        {
            case 1: //Level 2
                topTier = 2;
                colorLooping = false;
                break;
            case 2: //Level 3
                topTier = 1;
                colorLooping = true;
                break;
            case 3: //Level 4
                topTier = 1;
                colorSubtracting = true;
                break;
            case int n when (n >= 4 && n <= 8) : //Level 5-9
                topTier = 2;
                colorSubtracting = false;
                colorLooping = true;
                break;
            case 9: //beat level 9
                //congratulations panel goes here
                break;
        }

        timeScaleMultiplier += tsmIncrements;
        Time.timeScale = timeScaleMultiplier;
    }


    // Kills everything except the player
    IEnumerator KillTheNonPlayers()
    {
        GameObject killableThing;

        while (GameObject.FindGameObjectWithTag("enemy") != null)
        {
            killableThing = GameObject.FindGameObjectWithTag("enemy");
            Destroy(killableThing);
            yield return null;
        }
        //killableThing = GameObject.FindGameObjectWithTag("green");
        while (GameObject.FindGameObjectWithTag("green") != null)
        {
            killableThing = GameObject.FindGameObjectWithTag("green");
            Destroy(killableThing);           
            yield return null;
        }

        coilyExists = false;
    }
    // So other classes can call it
    public void KillNonPlayers() { StartCoroutine(KillTheNonPlayers()); }


    // Give Bonus Pts at end of round
    void GiveBonus()
    {
        bonusText.text = "BONUS";
        bonusScoreText.text = currentBonus.ToString();
        score += currentBonus;
        pointsUntilLife -= currentBonus;

        bonusText.gameObject.SetActive(true);
        bonusScoreText.gameObject.SetActive(true);

    }


    // Reset the Player when they die
    IEnumerator ResetThePlayer()
    {
        yield return StartCoroutine(KillTheNonPlayers());
        yield return new WaitForSeconds(1f);
        lives--;
        timeScaleMultiplier = 1f;

        if (lives >= 0)
        {
            GameObject newPlayer = Instantiate(playerPrefab, GameObject.Find("GameBoard").transform);
            newPlayer.transform.position = respawnPoint.position;
        }
        else
        {
            //GameOverPanel
        }

    }
    // So other classes can call it
    public void ResetPlayer() { StartCoroutine(ResetThePlayer()); }


    // Set all necessary variables to their Start-of-Level state
    void ResetVariables()
    {
        cubesLeft = 28;
        alreadyWinning = false;
        coilyExists = false;
        bonusText.text = "";
        bonusScoreText.text = "";
    }


    // Pause
    void PauseGame()
    {      
        if(isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = timeScaleMultiplier;
            isPaused = false;
        }       
    }


    // Update UI elements
    void UpdateUI()
    {
        scoreText.text = score.ToString();
        levelText.text = (level + 1).ToString();
        roundText.text = (round + 1).ToString();
    }
}
