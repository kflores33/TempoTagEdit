using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Metronome : MonoBehaviour
{
    public AudioSource metronome;

    public AudioSource loseSFX;

    public AudioSource countdownSFX;

    public AudioSource clap;

    public AudioClip metronomeSpeed1;
    public AudioClip metronomeSpeed2;
    public AudioClip metronomeSpeed3;
    public AudioClip metronomeSpeed4;

    public ShakeBehaviour shakeScript;
    public Camera mainCamera;

    public ParticleSystem playerParticles;

    public GameObject[] inputs;
    public GameObject[] enemyPoses;

    public List<string> enemyChoices;

    public float metronomeSpeed = 1;

    public float timer = 0f;
    public float playerTimer = 0f;

    public string inputString;

    public bool playerTurn = false;

    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;
    public GameObject empty;

    public GameObject[] enemySlots;

    public GameObject[] playerSlots;

    public GameObject enemySlotsObject;
    public GameObject playerSlotsObject;

    public bool gameActive;

    public GameObject restartButton;

    public TMP_Text scoreText;

    public TMP_Text finalScoreText;
    public GameObject finalScoreTextGameObject;
    public GameObject finalScoreHeader;

    public GameObject scoreHeader;
    public GameObject scoreTextGameObject;
    public GameObject scoreBG;

    public float score = 0;

    public GameObject restartScreen;

    public GameObject leftPose;
    public GameObject rightPose;
    public GameObject upPose;
    public GameObject downPose;
    public GameObject idlePose;

    public GameObject losePose;

    public GameObject enemyIdle;

    public GameObject countdown3;
    public GameObject countdown2;
    public GameObject countdown1;

    public GameObject countdownGo;

    public GameObject speedup;

    public bool speedup1Shown = false;
    public bool speedup2Shown = false;
    public bool speedup3Shown = false;

    public GameObject flash;

    public bool holding = false;

    // Start is called before the first frame update
    void Start()
    {
        idlePose.SetActive(true);
        metronome = GetComponent<AudioSource>();
        metronomeSpeed = metronomeSpeed1.length;
        StartCoroutine(gameLoop());
        shakeScript = mainCamera.GetComponent<ShakeBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                inputString = "left(Clone)";

                idlePose.SetActive(false);

                leftPose.SetActive(true);
                rightPose.SetActive(false);
                upPose.SetActive(false);
                downPose.SetActive(false);

                if (!holding)
                {
                    clap.Play();
                    playerParticles.Play();
                    shakeScript.TriggerShake();
                    holding = true;
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                inputString = "up(Clone)";

                idlePose.SetActive(false);

                leftPose.SetActive(false);
                rightPose.SetActive(false);
                upPose.SetActive(true);
                downPose.SetActive(false);
                
                if (!holding)
                {
                    clap.Play();
                    playerParticles.Play();
                    shakeScript.TriggerShake();
                    holding = true;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                inputString = "right(Clone)";

                idlePose.SetActive(false);

                leftPose.SetActive(false);
                rightPose.SetActive(true);
                upPose.SetActive(false);
                downPose.SetActive(false);

                if (!holding)
                {
                    clap.Play();
                    playerParticles.Play();
                    shakeScript.TriggerShake();
                    holding = true;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                inputString = "down(Clone)";

                idlePose.SetActive(false);

                leftPose.SetActive(false);
                rightPose.SetActive(false);
                upPose.SetActive(false);
                downPose.SetActive(true);

                if (!holding)
                {
                    clap.Play();
                    playerParticles.Play();
                    shakeScript.TriggerShake();
                    holding = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                holding = false;
            }
        }
        else if (!playerTurn && gameActive)
        {
            leftPose.SetActive(false);
            rightPose.SetActive(false);
            upPose.SetActive(false);
            downPose.SetActive(false);

            idlePose.SetActive(true);
        }
        
        scoreText.text = score.ToString();
    }

    public IEnumerator makeEnemyChoices() 
    {
        for (int i = 0; i < 4; i++)
        {
            int slotSelection = i;
            GameObject slotObject = enemySlots[slotSelection];
            GameObject newSelection = inputs[Random.Range(0, inputs.Length)];
            GameObject newChoice = Instantiate(newSelection, enemySlotsObject.transform);
            newChoice.transform.position = slotObject.transform.position;
            newChoice.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            enemyChoices.Insert(i, newChoice.name);

            enemyIdle.SetActive(false);
            foreach (GameObject pose in enemyPoses)
            {
                if (pose.name + "(Clone)" == newChoice.name)
                {
                    enemyIdle.SetActive(false);
                    pose.SetActive(true);
                }
                else if (pose.name != newChoice.name)
                {
                    enemyIdle.SetActive(false);
                    pose.SetActive(false);
                    if (newChoice.name == "idle(Clone)")
                    {
                        enemyIdle.SetActive(true);
                    }
                }
            }

            yield return new WaitForSeconds(metronomeSpeed / 4);
        }
        foreach (Transform child in enemySlotsObject.transform)
        {
            if (child.gameObject.tag == "choice")
            {
                Destroy(child.gameObject);
            }
        }
        foreach (GameObject pose in enemyPoses)
        {
            pose.SetActive(false);
        }
        enemyIdle.SetActive(true);
        playerTurn = true;
    }

    public IEnumerator falloffTime(string enemySelection)
    {
        yield return new WaitForSeconds(metronomeSpeed / 16);
        if (inputString != enemySelection)
        {
            if (enemySelection != "idle(Clone)")
            {
                Debug.Log(inputString);
                Debug.Log(enemySelection);

                if (!loseSFX.isPlaying)
                {
                    loseSFX.Play();
                }

                gameActive = false;

                leftPose.SetActive(false);
                rightPose.SetActive(false);
                upPose.SetActive(false);
                downPose.SetActive(false);
                idlePose.SetActive(false);

                losePose.SetActive(true);

                metronome.Stop();
                timer = 5;
                yield return new WaitForSeconds(1.5f);
                restartScreen.SetActive(true);
                restartButton.SetActive(true);
                finalScoreText.text = score.ToString();
                finalScoreTextGameObject.SetActive(true);
                finalScoreHeader.SetActive(true);
                scoreHeader.SetActive(false);
                scoreTextGameObject.SetActive(false);
                scoreBG.SetActive(false);
            }
        }
    }

    public IEnumerator playerChoices()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                clap.pitch = 1;
            }
            else if (i >= 1)
            {
                clap.pitch = 1 + (i/5);
            }
            int slotSelection = i;
            GameObject slotObject = playerSlots[slotSelection];
            string enemySelection = enemyChoices[i];
            GameObject enemyInput = null;
            foreach (GameObject input in inputs)
            {
                if (input.name + "(Clone)" == enemySelection)
                {
                    enemyInput = input;
                }
            }
            GameObject newChoice = Instantiate(enemyInput, playerSlotsObject.transform);
            newChoice.transform.position = slotObject.transform.position;
            newChoice.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            StartCoroutine(falloffTime(enemySelection));

            if (!gameActive)
            {
                break;
            }

            yield return new WaitForSeconds(metronomeSpeed / 4);
        }
        
        foreach (Transform child in playerSlotsObject.transform)
        {
            if (child.gameObject.tag == "choice")
            {
                Destroy(child.gameObject);
            }
        }
        playerTurn = false;
        inputString = "empty";
    }

    public IEnumerator gameLoop()
    {
        yield return new WaitForSeconds(0.5f);
        countdownSFX.Play();
        flash.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        flash.SetActive(false);
        countdown3.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdown3.SetActive(false);
        flash.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        flash.SetActive(false);
        countdown2.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdown2.SetActive(false);
        flash.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        flash.SetActive(false);
        countdown1.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdown1.SetActive(false);
        flash.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        flash.SetActive(false);
        countdownGo.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownGo.SetActive(false);
        gameActive = true;

        metronomeSpeed = metronomeSpeed1.length;
        metronome.clip = metronomeSpeed1;
        metronome.Play();
        while (gameActive)
        {
            playerTurn = false;

            StartCoroutine(makeEnemyChoices());

            yield return new WaitForSeconds(metronomeSpeed);

            StartCoroutine(playerChoices());

            yield return new WaitForSeconds(metronomeSpeed);
            enemyChoices.Clear();

            score++;

            if (timer >= 0 && timer < 1 && gameActive)
            {
                timer += 1f;
            }
            else if (timer >= 1 && timer < 2 && gameActive)
            {
                if (!speedup1Shown)
                {
                    flash.SetActive(true);
                    yield return new WaitForSeconds(0.01f);
                    flash.SetActive(false);
                    speedup.SetActive(true);
                }
                if (!speedup1Shown)
                {
                    metronomeSpeed = metronomeSpeed2.length;
                    metronome.clip = metronomeSpeed2;
                    metronome.Play();
                    yield return new WaitForSeconds(metronomeSpeed);
                    speedup.SetActive(false);
                    speedup1Shown = true;
                }
                timer += 0.2f;
            }
            else if (timer >= 2 && timer < 3 && gameActive)
            {
                if (!speedup2Shown)
                {
                    flash.SetActive(true);
                    yield return new WaitForSeconds(0.01f);
                    flash.SetActive(false);
                    speedup.SetActive(true);
                }
                if (!speedup2Shown)
                {
                    metronomeSpeed = metronomeSpeed3.length;
                    metronome.clip = metronomeSpeed3;
                    metronome.Play();
                    yield return new WaitForSeconds(metronomeSpeed * 2);
                    speedup.SetActive(false);
                    speedup2Shown = true;
                }
                timer += 0.1f;
            }
            else if (timer >= 3 && gameActive)
            {
                if (!speedup3Shown)
                {
                    flash.SetActive(true);
                    yield return new WaitForSeconds(0.01f);
                    flash.SetActive(false);
                    speedup.SetActive(true);
                }
                if (!speedup3Shown)
                {
                    metronomeSpeed = metronomeSpeed4.length;
                    metronome.clip = metronomeSpeed4;
                    metronome.Play();
                    yield return new WaitForSeconds(metronomeSpeed * 4);
                    speedup.SetActive(false);
                    speedup3Shown = true;
                }
                timer += 0.1f;
            }
        }
    }
}
