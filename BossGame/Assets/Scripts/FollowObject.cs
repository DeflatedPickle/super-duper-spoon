using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
    public bool copyPositionX;
    public bool copyPositionY;
    public bool copyPositionZ;

    public bool copyRotation;

    public GameObject parent;

    void Update() {
        var position = transform.position;
        if (copyPositionX) {
            position.x = parent.transform.position.x;
        }

        if (copyPositionY) {
            position.y = parent.transform.position.y;
        }

        if (copyPositionZ) {
            position.z = parent.transform.position.z;
        }

        transform.position = position;

        if (copyRotation) {
            transform.rotation = parent.transform.rotation;
        }
    }
}