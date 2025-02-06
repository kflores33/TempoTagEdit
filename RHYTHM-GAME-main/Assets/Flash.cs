using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public Color black;
    public Color white;
    public Color transparent;

    public SpriteRenderer renderer;

    public GameObject logo;

    public bool trans1 = false;
    public bool trans2;

    public float transitionTime = 2f;
    public float timePassed;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(introSequence());
    }

    private void Update()
    {
        if (trans1 && timePassed < transitionTime)
        {
            timePassed += Time.deltaTime;
            renderer.color = Color.Lerp(renderer.color, white, timePassed/transitionTime);
        }
        else if (trans2 && timePassed < transitionTime)
        {
            timePassed += Time.deltaTime;
            renderer.color = Color.Lerp(renderer.color, transparent, timePassed / transitionTime);
        }
    }

    public IEnumerator introSequence()
    {
        yield return new WaitForSeconds(0.6f);
        timePassed = 0f;
        trans1 = true;
        yield return new WaitForSeconds(0.2f);
        trans1 = false;
        timePassed = 0f;
        logo.SetActive(false);
        trans2 = true;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
