using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int Lives;
    public int startLives = 10;

    public static int Money;
    public int startMoney = 500;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }
}
