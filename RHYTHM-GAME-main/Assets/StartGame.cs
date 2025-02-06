using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public GameObject Fade;
    public SpriteRenderer renderer;

    public Color black;

    public bool trans = false;

    public float transitionTime = 5f;
    public float timePassed;

    public AudioSource bgm;

    // Update is called once per frame
    void Update()
    {
        if (trans && timePassed < transitionTime)
        {
            timePassed += Time.deltaTime;
            renderer.color = Color.Lerp(renderer.color, black, timePassed / transitionTime);
            bgm.volume -= 0.01f;
        }
    }

    public IEnumerator startCoroutine()
    {
        Fade.SetActive(true);
        trans = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
    }

    public void startGame()
    {
        StartCoroutine(startCoroutine());
    }
}
