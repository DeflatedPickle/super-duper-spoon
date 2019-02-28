using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ItemSlot : MonoBehaviour {
    public Sprite sprite;

    private void Start() {
        var spriteObject = new GameObject();
        spriteObject.transform.parent = transform;
        spriteObject.transform.position = transform.position;
        
        var spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        spriteRenderer.sprite = sprite;
    }
}