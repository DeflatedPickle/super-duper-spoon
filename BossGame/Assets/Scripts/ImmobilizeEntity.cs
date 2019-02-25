using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobilizeEntity : MonoBehaviour {
    public GameObject entity;
    public Stats entityStats;

    public void StickEntity() {
        if (entity != null) {
            Debug.Log("Stick");
            entityStats.canMove = false;
            entityStats.isStuck = true;
        }
    }

    public void UnstickEntity() {
        if (entity != null) {
            Debug.Log("Unstick");
            entityStats.canMove = true;
            entityStats.isStuck = false;

            entityStats.stuckTo = null;
            
            entityStats = null;
            entity = null;
        }
    }
}