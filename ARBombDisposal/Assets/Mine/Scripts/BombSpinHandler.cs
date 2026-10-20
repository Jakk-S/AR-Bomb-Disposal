using UnityEngine;
using UnityEngine.InputSystem;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BombSpinHandler : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;

    private void Update()
    {
#if UNITY_EDITOR
        CheckMouseInput();
#else
        CheckTouchInput();
#endif
    }

    private void CheckMouseInput()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            float rotate = mouseDelta.x * rotateSpeed * Time.deltaTime * -1f;

            transform.Rotate(Vector3.up, rotate, Space.World);
        }
    }

    private void CheckTouchInput()
    {
        if (Touch.activeTouches.Count == 0) return;

        Touch touch = Touch.activeTouches[0];

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            float rotate = touch.delta.x * rotateSpeed * Time.deltaTime * -1;

            transform.Rotate(Vector3.up, rotate, Space.World);
        }
    }
}
