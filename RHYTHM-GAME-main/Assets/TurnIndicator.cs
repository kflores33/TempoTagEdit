using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public Sprite PlayerTurnSprite;
    public Sprite OppTurnSprite;

    public Metronome metronomeScript;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (metronomeScript != null)
        {
            if (metronomeScript.playerTurn)
            {
                SpriteRenderer.enabled = true;
                SpriteRenderer.sprite = PlayerTurnSprite;
            }
            else if (metronomeScript.betweenTurns) // if between turns is true, disable sprite renderer
            {
                SpriteRenderer.enabled = false;
            }
            else if (!metronomeScript.playerTurn)
            {
                SpriteRenderer.enabled = true;
                SpriteRenderer.sprite = OppTurnSprite;
            }
        }
    }
}
