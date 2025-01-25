using UnityEngine;

public class Slower : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float slowMultiplier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BubberController>(out BubberController bubber))
        {
            bubber.Slow(slowMultiplier);
        }
    }
}
