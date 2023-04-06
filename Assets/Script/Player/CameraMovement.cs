using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float timeOffSet; // Speed of the camera
    [SerializeField] Vector3 offSetPos;

    [SerializeField] Vector3 boundMin;
    [SerializeField] Vector3 boundMax;

    private void LateUpdate()
    {
        if (player != null)
        {
            //Camera Location
            Vector3 startPos = transform.position;
            Vector3 targetPos = player.position;
                
            //placement of the camera
            targetPos.x += offSetPos.x;
            targetPos.y += offSetPos.y;
            targetPos.z = transform.position.z;

            //min and max boundX
            targetPos.x = Mathf.Clamp(targetPos.x, boundMin.x, boundMax.x);

            //min and max boundY
            targetPos.y = Mathf.Clamp(targetPos.y, boundMin.y, boundMax.y);

            //moving the camera
            float t = 1f - Mathf.Pow(1f - timeOffSet, Time.deltaTime * 30);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

        }
    }
}
