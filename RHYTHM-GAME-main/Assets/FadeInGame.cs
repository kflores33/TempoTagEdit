using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInGame : MonoBehaviour
{
    public Image renderer;

    public Color transparent;

    public bool trans = false;

    public float transitionTime = 5f;
    public float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (trans && timePassed < transitionTime)
        {
            timePassed += Time.deltaTime;
            renderer.color = Color.Lerp(renderer.color, transparent, timePassed / transitionTime);
        }
    }

    public IEnumerator fadeIn()
    {
        trans = true;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
