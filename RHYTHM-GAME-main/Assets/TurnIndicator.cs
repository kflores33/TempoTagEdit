using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public Sprite PlayerTurnSprite;
    public Sprite OppTurnSprite;

    public Metronome metronomeScript;

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
                SpriteRenderer.sprite = PlayerTurnSprite;
            }
            else
            {
                SpriteRenderer.sprite = OppTurnSprite;
            }
        }
    }
}
