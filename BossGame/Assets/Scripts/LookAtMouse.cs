using UnityEngine;

public class LookAtMouse : MonoBehaviour {
    public bool shouldLook = true;
    
    private Camera _camera;

    private void Start() {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update() {
        if (shouldLook) {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            // ReSharper disable once Unity.InefficientPropertyAccess
            var direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.up = direction;
        }
    }
}
