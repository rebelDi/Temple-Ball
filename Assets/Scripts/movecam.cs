#pragma warning disable CS0618
using UnityEngine;

public class movecam : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 tempTransformPosition = player.transform.position + offset;
        if (moveorb.isGrounded)
        {
            transform.position = new Vector3(transform.position.x, tempTransformPosition.y, tempTransformPosition.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, tempTransformPosition.z);
        }
    }
}
