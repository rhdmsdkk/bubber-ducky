using UnityEngine;

public class BubbleHitScript : MonoBehaviour
{
    private Pulser bubbleCounter;
    [SerializeField] private GameObject popParticleSystem;

    private void Start()
    {
        bubbleCounter = FindAnyObjectByType<Pulser>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BubberController>(out BubberController bubber))
        {
            BubbleCountScript.addBubble();
            bubbleCounter?.Pulse();
            gameObject.SetActive(false);
        }
    }
}
