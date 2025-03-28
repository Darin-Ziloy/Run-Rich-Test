using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameStartUI StartUI;
    public GameObject GameUI;
    public GameOverUI GameOverUI;
    public GameVictoryUI GameVictoryUI;
    public MoneyUI moneyUI;

    public static UI instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}