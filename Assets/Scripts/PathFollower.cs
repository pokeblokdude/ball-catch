using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed = 5;
    float distanceTravelled;
    Vector2 lastPos;

    // Update is called once per frame
    void Update() {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, end);
        if(transform.position == new Vector3(lastPos.x, lastPos.y, 0)) {
            //Debug.Log("stopped");
            this.enabled = false;
        }
        lastPos = transform.position;
        
    }
}
