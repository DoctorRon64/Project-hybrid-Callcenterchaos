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
        MoneyDisplay.text = MoneyValue.ToString();
    }

    private void Update()
    {
        decreaseTimer += Time.deltaTime;
        if(decreaseTimer > decreaseRate)
        {
            MoneyValue -= decreaseAmount;
            MoneyValue = Mathf.Clamp(MoneyValue, 0, 9999999);
            MoneyDisplay.text = MoneyValue.ToString();
        }
        if(MoneyValue <= 0)
        {
            GameOver();
        }
    }

    public void AddAmount(int amount)
    {
        MoneyValue += amount;
        MoneyDisplay.text = amount.ToString();
    }

    private void GameOver()
    {

    }

}
