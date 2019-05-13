using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    private static CoinSystem instance;
    private static int coin;
    private static int essence;
    private static int upgradeCount;
    private delegate void UpdateSomething();
    private static UpdateSomething UpdateValue;

    [SerializeField]
    private Button UpgradeButton;
    [SerializeField]
    private Image UpgradeButtonEffect;
    [SerializeField]
    private Text CoinText;
    [SerializeField]
    private Text PoisonText;
    [SerializeField, Range(1, 23)]
    private int Stage = 0;
    [SerializeField]
    private int[] MaxCoin = new int[] { 20, 40, 60 };
    [SerializeField]
    private int[] GainCoin = new int[] { 1, 2, 3 };
    [SerializeField]
    private int[] UpgradeCoin = new int[] { 10, 30, -1 };
    [SerializeField]
    private string[] UpgradeText = new string[] { "1단계", "2단계", "MAX" };

    #region Property

    public static CoinSystem Instance
    {
        get
        {
            if(instance)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }

        private set
        {
            instance = value;
        }
    }
    public static int Coin
    {
        get
        {
            return coin;
        }
        private set
        {
            coin = value;
            UpdateValue();
        }
    }

    public static int Essence
    {
        get
        {
            return essence;
        }
        private set
        {
            essence = value;
            UpdateValue();
        }
        
    }
    public static int UpgradeCount
    {
        get
        {
            return upgradeCount;
        }
        private set
        {
            upgradeCount = value;
        }
        
    }

    #endregion

    public static void SpawnObject(int Cost)
    {
        Coin -= Cost;
    }

    private void OnEnable()
    {
        Instance = this;
        UpdateValue = null;
        UpdateValue += UpdateText;
    }

    private void Start()
    {
        InvokeRepeating("GainCoinFunc", 0, 1.0f);
    }

    private void GainCoinFunc()
    {
        if(MaxCoin[UpgradeCount] <= Coin + GainCoin[UpgradeCount])
        {
            Coin = MaxCoin[UpgradeCount];

            return;
        }
        Coin += GainCoin[UpgradeCount];

        UpdateText();
    }

    private void UpdateText()
    {
        CoinText.text = Coin.ToString();
        PoisonText.text = Essence.ToString();

        if (UpgradeCoin[UpgradeCount] != -1)
            UpgradeButtonEffect.color = new Color(1, 1, 1, (float)Coin / UpgradeCoin[UpgradeCount]);
        else
            UpgradeButtonEffect.color = Color.white;
    }

    public void Upgrade(Text text)
    {
        if (UpgradeCoin[UpgradeCount] <= Coin)
        {
            UpgradeCount += 1;
            Coin -= UpgradeCoin[UpgradeCount - 1];

            if(UpgradeCoin[UpgradeCount] == -1)
            {
                UpgradeButton.interactable = false;
            }
            text.text = UpgradeText[UpgradeCount];
        }
    }
}
