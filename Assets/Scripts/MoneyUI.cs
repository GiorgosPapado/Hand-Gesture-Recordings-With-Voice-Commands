using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public static TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        moneyText = text;
        moneyText.text = PlayerStats.Rounds.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        moneyText.text = PlayerStats.Rounds.ToString();
    }
}
