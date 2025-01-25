using UnityEngine;

public class CameraStabilizer : MonoBehaviour
{
    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, 0f);
    }
}
