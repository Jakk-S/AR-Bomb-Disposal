using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    private BombButtonHandler[] bombButtons;
    
    private int currentButton = 0;

    [SerializeField] private float bombTimer = 60f;
    private float countdown;

    private bool defused = false;
    
    private void Awake()
    {
        GameManager.Instance.SetBomb(this);
        countdown = bombTimer;
    }

    private void Start()
    {
        bombButtons = buttons.GetComponentsInChildren<BombButtonHandler>();
        List<int> usedNums = new List<int>();
        Debug.Log(usedNums);
        foreach (var bombButton in bombButtons)
        {
            Debug.Log($"DEF ORDER:{bombButton.gameObject.name}");
            int temp;
            do
            {
                temp = Random.Range(0, bombButtons.Length);
            } while (usedNums.Contains(temp));
            usedNums.Add(temp);
            bombButton.ButtonNum = temp;
        }
        Array.Sort(bombButtons, (x, y) => x.ButtonNum.CompareTo(y.ButtonNum));
        foreach (var bombButton in bombButtons) Debug.Log($"SORTED/RAND ORDER:{bombButton.gameObject.name}");

        GameManager.Instance.DebugText($"Next Button:\n{bombButtons[0].name}");
    }

    private void Update()
    {
        if (!defused)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                GameManager.Instance.BlowUp();
                Destroy(gameObject);
            }
            else GameManager.Instance.UpdateTimer($"Timer: {countdown:F2}");
        }
        else
        {
            GameManager.Instance.UpdateTimer($"Bomb Defused with {countdown:F2} Seconds left");
            GameManager.Instance.Defused();
        }
    }

    public void NextButton(int button)
    {
        Debug.Log($"{currentButton}:{button}");
        if (currentButton == button)
        {
            if (currentButton == 7)
            {
                Debug.Log("YOU WIN!");
                GameManager.Instance.DebugText("YOU WIN!");
                defused = true;
            }
            else
            {
                currentButton++;
                GameManager.Instance.DebugText($"Next Button:\n{bombButtons[currentButton].name}");
            }
        }
        else
        {
            Debug.Log("YOU LOSE!");
            GameManager.Instance.BlowUp();
            Destroy(gameObject);
        }
    }
}
