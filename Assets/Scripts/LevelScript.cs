using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    public void SetDifficulty(int difficult)
    {
        _gameManager.StartGame(difficult);
    }
}
