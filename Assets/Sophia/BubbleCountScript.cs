using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCountScript : MonoBehaviour
{
    [SerializeField] public static int numBubbles;

    void Start()
    {
        numBubbles = 3;
    }

    void Update()
    {
        if (numBubbles == 0)
        {
            Globals.lose();
        }
    }

    public static void addBubble()
    {
        numBubbles++;
    }

    public static void loseBubble()
    {
        numBubbles--;
    }
    public static int getNumBubbles()
    {
        return numBubbles;
    }
}
