using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SwordData swordData;

    public PlayerData playerData;
    string playerDataFilePath = "PlayerData.json";

    public InventoryData inventoryData;
    string inventoryDataFilePath = "InventoryData.json";

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
        playerDataFilePath = Path.Combine(Application.streamingAssetsPath, playerDataFilePath);
        inventoryDataFilePath = Path.Combine(Application.streamingAssetsPath, inventoryDataFilePath);

        string playerDataJson = File.ReadAllText(playerDataFilePath);
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        string inventoryDataJson = File.ReadAllText(inventoryDataFilePath);
        inventoryData = JsonUtility.FromJson<InventoryData>(inventoryDataJson);
    }

    void JsonSerialization()
    {
        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(playerDataFilePath, playerDataJson);
        string inventoryDataJson = JsonUtility.ToJson(inventoryData);
        File.WriteAllText(inventoryDataFilePath, inventoryDataJson);
    }

    void OnApplicationQuit()
    {
        JsonSerialization();
    }
}
