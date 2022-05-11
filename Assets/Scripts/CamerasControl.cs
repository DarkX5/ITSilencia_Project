using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasControl : MonoBehaviour
{
    // [SerializeField] private Transform[] cameras;
    // [SerializeField] private Vector2 zoomMinMax = new Vector2(-5f, 10f);
    // [SerializeField] private float zoomStep = 1f;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     if (cameras == null) {
    //         var temp = GetComponentsInChildren<Camera>();
    //         cameras = new Transform[temp.Length];
    //         for (int i = 0; i < temp.Length; i += 1) {
    //             cameras[i] = temp[i].transform;
    //         }
    //     }

    //     MoveController.onZoomIn += ZoomIn;
    //     MoveController.onZoomOut += ZoomOut;
    // }
    // private void OnDestroy() {
    //     MoveController.onZoomIn -= ZoomIn;
    //     MoveController.onZoomOut -= ZoomOut;
    // }

    // /* TODO - finish zoom in functionality */
    // private void ZoomIn() {
    //     if (cameras == null) { return; }
    //     for (int i = 0; i < cameras.Length; i += 1) {
    //         cameras[i].position = new Vector3(cameras[i].position.x, cameras[i].position.y, cameras[i].position.z);
    //     }
    // }

    // /* TODO - finish zoom out functionality */
    // private void ZoomOut() {
    //     if (cameras == null) { return; }
    //     for (int i = 0; i < cameras.Length; i += 1) {
    //         cameras[i].position = new Vector3(cameras[i].position.x, cameras[i].position.y, cameras[i].position.z);
    //     }
    // }
}
