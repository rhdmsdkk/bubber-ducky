using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHitScript : MonoBehaviour
{
    [SerializeField] BubbleCountScript bcs;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        bcs.addBubble();
        Destroy(gameObject);
    }
}
