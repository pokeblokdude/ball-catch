using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMoveable : MonoBehaviour
{

    [SerializeField] Vector3 targetLocation = new Vector3(0, 0, -10);
    [SerializeField] float targetOrthoSize = 10;
    [SerializeField] float moveDuration = 1f;
    [SerializeField] Ease easeType;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(transform.position != targetLocation) {
            transform.DOMove(targetLocation, moveDuration);
        }
        if(cam.orthographicSize != targetOrthoSize) {
            cam.DOOrthoSize(targetOrthoSize, moveDuration);
        }
    }
}
