using JetBrains.Annotations;
using UnityEngine;

public class DirectionalLook : MonoBehaviour {
    [CanBeNull] public Sprite upSprite;
    [CanBeNull] public Sprite downSprite;
    [CanBeNull] public Sprite leftSprite;

    [Tooltip("Ignores the right sprite and flips the left one instead")]
    public bool flipLeftSprite;

    [CanBeNull] public Sprite rightSprite;
    public GameObject lookingObject;

    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Update() {
        var mainPosition = transform.position;
        var lookingPosition = _camera.ScreenToWorldPoint(lookingObject.transform.position);

        // Gives us the angle... backwards
        var angle = Quaternion.FromToRotation(Vector3.up, lookingPosition - mainPosition).eulerAngles.z;

        // Debug.Log(angle);

        // TODO: Increase the amount of directions this allows for
        if (angle < 360 - 45 && angle <= 90 - 45) {
            // Down
            if (downSprite) {
                _spriteRenderer.sprite = downSprite;
                _spriteRenderer.flipX = false;
            }
        }
        else if (angle > 90 - 45 && angle <= 180 - 45) {
            // Left
            if (leftSprite) {
                _spriteRenderer.sprite = leftSprite;
                _spriteRenderer.flipX = false;
            }
        }
        else if (angle > 180 - 45 && angle <= 270 - 45) {
            // Up
            if (upSprite) {
                _spriteRenderer.sprite = upSprite;
                _spriteRenderer.flipX = false;
            }
        }
        else if (angle > 270 - 45 && angle <= 360 - 45) {
            // Right
            if (!flipLeftSprite) {
                if (rightSprite) {
                    _spriteRenderer.sprite = rightSprite;
                }
            }
            else {
                if (leftSprite) {
                    _spriteRenderer.sprite = leftSprite;
                    _spriteRenderer.flipX = true;
                }
            }
        }
    }
}