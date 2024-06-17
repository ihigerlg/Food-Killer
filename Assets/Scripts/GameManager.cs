using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targets;

    [SerializeField] private Text _pointsText;
    [SerializeField] private Text _scoreText;

    private int _index;
    private int _points;

    [SerializeField] private int _score;

    private float _spawnRate = 3f;

    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private GameObject _levelMenu;

    private bool _spawnActive;

    private void Start()
    {
        _pointsText.text = "Points: " + _points;

        _score = PlayerPrefs.GetInt("MaxScore", _score);
        _scoreText.text = "Score: " + _score;
    }

    IEnumerator SpawnTarget()
    {
        while (_spawnActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            _index = IndexOfRandomObject();

            Instantiate(_targets[_index]);
            AddPoints(1);
        }
    }

    private int IndexOfRandomObject()
    {
        return Random.Range(0, _targets.Count);
    }

    public void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        _pointsText.text = "Points: " + _points;
    }

    public void RemovePoints(int pointsToRemove)
    {
        _points -= pointsToRemove;
        _pointsText.text = "Points: " + _points;
    }

    public void StartGame(int difficulty)
    {
        _points = 0;
        _spawnActive = true;
          
        StartCoroutine(SpawnTarget());

        _spawnRate /= difficulty;
        _levelMenu.gameObject.SetActive(false);
    }

    private void SaveScore()
    {
        if (_points > _score)
            PlayerPrefs.SetInt("MaxScore", _points);
    }

    public void DeadMenu()
    {
        _spawnActive = false;
        _deadMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}