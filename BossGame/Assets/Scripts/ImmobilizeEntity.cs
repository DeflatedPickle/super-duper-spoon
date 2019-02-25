using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmobilizeEntity : MonoBehaviour {
    public GameObject entity;
    public Stats entityStats;

    public void StickEntity() {
        //if (entityStats != null) {
            entityStats.canMove = false;
            entityStats.isStuck = true;

            entityStats.stuckTo = transform.GetChild(0).gameObject;
        //}
    }

    public void UnstickEntity() {
        //if (entityStats != null) {
            entityStats.canMove = true;
            entityStats.isStuck = false;

            entityStats.stuckTo = null;
        //}
    }
}