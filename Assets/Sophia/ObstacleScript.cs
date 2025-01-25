using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BubberController>(out BubberController bubber))
        {
            BubbleCountScript.loseBubble();
            gameObject.SetActive(false);
        }
    }
}
