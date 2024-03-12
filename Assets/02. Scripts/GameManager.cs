using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SwordData swordData;

    public PlayerData playerData;
    string playerDataFilePath = "Assets/04. Resources/Json/PlayerData.json";

    public InventoryData inventoryData;
    string inventoryDataFilePath = "Assets/04. Resources/Json/InventoryData.json";

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        JsonParsing();
    }

    public void MoveScenes()
    {
        SceneManager.LoadScene("Game Scenes");
    }

    void JsonParsing()
    {
        string playerDataJson = File.ReadAllText(playerDataFilePath);
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        string inventoryDataJson = File.ReadAllText(inventoryDataFilePath);
        inventoryData = JsonUtility.FromJson<InventoryData>(inventoryDataJson);
    }
}
