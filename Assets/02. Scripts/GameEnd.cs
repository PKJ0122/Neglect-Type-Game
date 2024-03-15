using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public Button endButton;
    public GameObject endPanel;

    void Start()
    {
        endButton.onClick.AddListener(GameEndButton);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            endPanel.SetActive(true);
    }

    void GameEndButton()
    {
        Application.Quit();
    }
}
