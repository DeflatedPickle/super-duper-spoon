using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {
    private Camera _camera;
    private RectTransform _rectTransform;

    private void Start() {
        _camera = FindObjectOfType<Camera>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update() {
        var mousePosition = Input.mousePosition;

        _rectTransform.position = mousePosition;

        _rectTransform.Rotate(0f, 0f, -0.5f);

        var angle = Math.PI / 180f * (_rectTransform.rotation.z * -1);
        var sine = (float) Math.Sin(angle); // Ranges from -1 to 1
        // Credit: https://forum.unity.com/threads/mapping-or-scaling-values-to-a-new-range.180090/#post-1231294
        // Maps the range of the sine value from -1:1 to -0.04:0.04, to make the scaling more gradual
        var result = Mathf.Lerp(-0.06f, 0.06f, Mathf.InverseLerp(-1, 1, sine));

        _rectTransform.localScale += new Vector3(result, result, 0f);
    }
}