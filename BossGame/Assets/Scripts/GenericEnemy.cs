﻿using UnityEngine;

public class GenericEnemy : MonoBehaviour {
    private LivingStats _stats;

    private void Awake() {
        _stats = GetComponent<LivingStats>();
    }

    void Update() {
        if (_stats.isStuck) {
            if (_stats.stuckTo != null) {
                var dragObject = _stats.stuckTo.transform.GetChild(0);
                
                // FIXME: This is a hacky solution, work something else out
                if (dragObject.name == "Edge") {
                    if (Vector3.Distance(transform.position, dragObject.position) > 0.01f) {
                        transform.position = Vector3.MoveTowards(transform.position, dragObject.position, 
                            0.6f * Time.deltaTime);
                    }
                }

                // transform.position = _stats.stuckTo.transform.position - transform.position;
            }
        }
    }
}