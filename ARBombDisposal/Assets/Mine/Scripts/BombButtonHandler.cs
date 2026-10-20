using UnityEngine;

public class BombButtonHandler : MonoBehaviour
{
    [Range(0, 7)]
    [SerializeField] private int buttonNumber;
    public int ButtonNum { get { return buttonNumber; } set { buttonNumber = value; } }

    private int lastTriggeredFrame = -1;

    private void OnMouseDown()
    {
        if (Time.frameCount == lastTriggeredFrame) return;
        lastTriggeredFrame = Time.frameCount;

        GameManager.Instance.ButtonPress(buttonNumber);
    }
}
