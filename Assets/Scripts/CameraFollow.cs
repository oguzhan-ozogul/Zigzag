using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FollowPlayer;
    private Vector3 _cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;


    void Start()
    {
        _cameraOffset = transform.position - FollowPlayer.position;

    }


    void LateUpdate()
    {
        if(BallMove.fall == false)
        {
            Vector3 newPos = FollowPlayer.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }
        

    }

}
