using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCounter : MonoBehaviour
{
    public static TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        moneyText = text;
        moneyText.text = GestureRepo.counter.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        moneyText.text = GestureRepo.counter.ToString();
    }
}
