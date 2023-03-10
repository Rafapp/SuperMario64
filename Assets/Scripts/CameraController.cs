using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float followSpeed = 10f;

    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - player.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * followSpeed);
        transform.LookAt(player.position);
    }
}
