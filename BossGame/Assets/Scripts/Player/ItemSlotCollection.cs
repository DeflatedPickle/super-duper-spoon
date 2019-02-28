using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ItemSlotCollection : MonoBehaviour {
    public GameObject parent;
    public bool slotsOpen;
    public int selectedSlot;

    public float rotation;

    public Sprite gildedSprite;
    public GameObject gildedObject;

    private void Start() {
        gildedObject = new GameObject();
        gildedObject.transform.parent = transform;
        
        var spriteRenderer = gildedObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = gildedSprite;
    }

    private void Update() {
        if (Input.GetKeyDown("i")) {
            slotsOpen = !slotsOpen;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (selectedSlot - 1 >= 0) {
                selectedSlot--;
            }
            else {
                selectedSlot = transform.GetChild(0).childCount - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (selectedSlot + 1 < transform.GetChild(0).childCount) {
                selectedSlot++;
            }
            else {
                selectedSlot = 0;
            }
        }

        // Show the slots
        if (slotsOpen) {
            var centre = parent.transform.position;

            foreach (var i in Enumerable.Range(0, transform.GetChild(0).childCount)) {
                var radius = 2.4f;
                rotation -= 0.0004f;
                if (rotation <= 0) rotation = 360;

                var t = 2 * Math.PI * i / transform.GetChild(0).childCount;

                if (selectedSlot == i) {
                    radius += 0.4f;
                }
                
                var x = (float) (centre.x + radius * Math.Cos(t + rotation));
                var y = (float) (centre.y + radius * Math.Sin(t + rotation));

                transform.GetChild(0).GetChild(i).position = new Vector3(x, y, -6);

                if (selectedSlot == i) {
                    gildedObject.transform.position = new Vector3(x, y, -7);
                }
            }

            transform.GetChild(0).gameObject.SetActive(true);
        }
        // Hide the slots
        else {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}