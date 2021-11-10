using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    [Range(0, 20)]
    private float camOffest = 15;

    [SerializeField]
    private float smoothing = 0.1f;

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y + camOffest, player.position.z);
    }

    void Update()
    {
        Vector3 playerPos = new Vector3(player.position.x, player.position.y + camOffest, player.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * smoothing);
    }
}
