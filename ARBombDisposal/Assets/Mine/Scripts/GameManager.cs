using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text debugText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject resetButton;

    private BombController bomb;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetBomb(BombController bc)
    {
        bomb = bc;
    }

    public void ButtonPress(int buttonPressed)
    {
        bomb.NextButton(buttonPressed);
    }

    public void DebugText(string text)
    {
        debugText.text = text;
    }

    public void UpdateTimer(string text)
    {
        timerText.text = text;
    }

    public void BlowUp()
    {
        debugText.text = "YOU LOSE!";
        timerText.text = "BOOOOOOM!";
        resetButton.SetActive(true);
    }

    public void Defused()
    {
        resetButton.SetActive(true);
    }

    public void ResetButton()
    {
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
        SceneManager.LoadScene(0);
    }
}
