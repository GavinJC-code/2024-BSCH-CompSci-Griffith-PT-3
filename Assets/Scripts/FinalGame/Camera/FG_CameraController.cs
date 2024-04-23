using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FG_CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float verticalOffset;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    // Camera bounds
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Update()
    {
        // Room camera
        Vector3 targetPosition = new Vector3(currentPosX, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);

        // Follow player
        float targetY = transform.position.y;
        if (player.position.y > transform.position.y + maxY)
        {
            targetY = player.position.y - maxY;
        }
        else if (player.position.y < transform.position.y + minY)
        {
            targetY = player.position.y - minY;
        }

        Vector3 targetPos = new Vector3(player.position.x + lookAhead, targetY + verticalOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * cameraSpeed);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        // debug log the new room position
        Debug.Log($"New room position is {_newRoom.position}");
        currentPosX = _newRoom.position.x;
    }
}
