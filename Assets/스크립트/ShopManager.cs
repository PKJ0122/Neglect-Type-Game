using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    SavaDataManager sM;

    [SerializeField] GameObject shopWindow;
    [SerializeField] Button shopOnBt;
    [SerializeField] Button shopOffBt;

    [SerializeField] GameObject playerUpgradeWindow;
    [SerializeField] Button playerUpgradeOnBt;
    [SerializeField] Button playerUpgradeOffBt;

    // Start is called before the first frame update
    void Start()
    {
        sM = GameObject.Find("SavaDataManager").GetComponent<SavaDataManager>();

        shopOnBt.onClick.AddListener(OnOffShop);
        shopOffBt.onClick.AddListener(OnOffShop);
        playerUpgradeOnBt.onClick.AddListener(OnOffPlayerUpgrade);
        playerUpgradeOffBt.onClick.AddListener(OnOffPlayerUpgrade);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnOffShop()
    {
        shopWindow.SetActive(!shopWindow.activeSelf);
    }
    void OnOffPlayerUpgrade()
    {
        shopWindow.SetActive(false);
        playerUpgradeWindow.SetActive(!playerUpgradeWindow.activeSelf);
    }
}
