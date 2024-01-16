using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public Text MoneyDisplay;
    public int MoneyValue;

    [SerializeField] private int decreaseAmount;
    [SerializeField] private float decreaseRate;
    private float decreaseTimer;

    private void Start()
    {
        instance = this;
        SetDisplayAmount(MoneyValue);
    }

    private void Update()
    {
        decreaseTimer += Time.deltaTime;
        if(decreaseTimer >= decreaseRate)
        {
            MoneyValue -= decreaseAmount;
            MoneyValue = Mathf.Clamp(MoneyValue, 0, 9999999);
            SetDisplayAmount(MoneyValue);
            decreaseTimer = 0;
        }
        if(MoneyValue <= 0)
        {
            GameOver();
        }
    }

    public void AddAmount(int amount)
    {
        MoneyValue += amount;
        SetDisplayAmount(MoneyValue);
    }

    private void SetDisplayAmount(int amount)
    {
        MoneyDisplay.text = $"Self worth: {amount}$";
    }

    private void GameOver()
    {

    }

}
