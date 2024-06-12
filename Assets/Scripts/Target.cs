using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Rigidbody _rb;

    private int _minForce = 10;
    private int _maxForce = 17;

    private int _posX = 11;
    private int _randomTorque = 12;

    private GameManager _gameManager;
    [SerializeField] private ParticleSystem _boomEffect;
    [SerializeField] private bool _isDangerous;
    [SerializeField] private bool _isNegative;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPosition();

        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -4)
            Destroy(gameObject);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range( _minForce, _maxForce );
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-_posX, _posX), 0);
    }

    private float RandomTorque()
    {
        return Random.Range(-_randomTorque, _randomTorque);
    }

    private void OnMouseDown()
    {

        _gameManager.AddPoints(5);

        if (_isDangerous)
        {
            _gameManager.DeadMenu();
        }

        if (_isNegative)
        {
            _gameManager.RemovePoints(15);
        }

        Destroy(gameObject);
        Instantiate(_boomEffect, transform.position, _boomEffect.transform.rotation);
    }
}
