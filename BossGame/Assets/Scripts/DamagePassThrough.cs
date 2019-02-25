using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePassThrough : MonoBehaviour {
    public float damage;

    private GameObject _collider;
    
    public void SetDamage(float amount) {
        damage = amount;
    }

    public void StartDamaging() {
        Debug.Log("Start damaging");
        _collider = transform.GetChild(0).gameObject;
        _collider.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void StopDamaging() {
        Debug.Log("Stopped damaging");
        damage = 0;
        _collider.GetComponent<BoxCollider2D>().enabled = false;
    }
}
