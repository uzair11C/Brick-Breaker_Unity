using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int hitPoints;

    [SerializeField]
    Sprite brokenSprite;

    public void BreakBrick()
    {
        hitPoints--;
        GetComponent<SpriteRenderer>().sprite = brokenSprite;
    }
}
