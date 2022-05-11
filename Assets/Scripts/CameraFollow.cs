using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    //[SerializeField] private Vector3 positionOffset;
    //[SerializeField] private Vector3 rotationOffset;
    [ReadOnly] private Camera mainCamera;


    //private Vector3 newPosition;
    //private Vector3 newRotation;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //newPosition = new Vector3(  followTarget.position.x + positionOffset.x,
        //                            followTarget.position.y + positionOffset.y,
        //                            followTarget.position.z + positionOffset.z );
        //newRotation = new Vector3(  rotationOffset.x,
        //                            rotationOffset.y,
        //                            rotationOffset.z );

        //mainCamera.transform.position = followTarget.position;

        mainCamera.transform.LookAt(followTarget);
    }
}
