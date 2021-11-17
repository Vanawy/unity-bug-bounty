using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperTiled2Unity;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    [Header("Level")]
    [SerializeField]
    private SuperObjectLayer _objects;
    private PlayerController _player;
    [SerializeField]
    private OnDeath _deathAnimation;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _vcam;
    [Header("Markers Settings")]
    [SerializeField]
    private GameObject _markerPrefab;
    [SerializeField]
    private Sprite[] _markerSprites;

    private ObjectiveController[] _objectives;

    private int _yellowTotal = 0;
    private int _yellowCollected = 0;
    private int _blueTotal = 0;
    private int _blueCollected = 0;

    [Header("Objectives")]
    [SerializeField]
    private Text _yellowCounter;
    [SerializeField]
    private Text _blueCounter;
    [SerializeField]
    private Text _timer;
    private float _startLevelTime;
    private float _endLevelTime = -1f;

    [Header("Audio")]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _collectedSound;


    void Awake()
    {
        _player = _objects.GetComponentInChildren<PlayerController>();
        _vcam.m_Follow = _player.transform;

        PlayerDeath playerDeath = _player.gameObject.GetComponent<PlayerDeath>();
        playerDeath.OnRestart += RestartLevel;

        ObjectiveCollector collector = _player.gameObject.GetComponent<ObjectiveCollector>(); 
        collector.OnCollect += TakeObjective;
        _audio = GetComponent<AudioSource>();

        _startLevelTime = Time.time;
        _objectives = _objects.GetComponentsInChildren<ObjectiveController>();

        foreach (var objective in _objectives)
        {
            GameObject marker = Instantiate(_markerPrefab);
            MarkerController controller = marker.GetComponent<MarkerController>();
            controller.parent = _player.transform;
            controller.target = objective.transform;
            switch (objective.type)
            {
                case ObjectiveType.YELLOW:
                    controller.SetSprite(_markerSprites[0]);
                    _yellowTotal++;
                    break;
                case ObjectiveType.BLUE:
                    controller.SetSprite(_markerSprites[1]);
                    _blueTotal++;
                    break;
                default:
                    controller.SetSprite(_markerSprites[0]);
                    _yellowTotal++;
                    break;
            }
        }
    }

    void Update()
    {
        UpdateCounters();
        UpdateTimer();
    }

    private void UpdateCounters()
    {
        _yellowCounter.text = string.Format("{0}/{1}", _yellowCollected, _yellowTotal);
        _blueCounter.text = string.Format("{0}/{1}", _blueCollected, _blueTotal);
    }
    
    private void RestartLevel()
    {
        if (!_player) {
            ReloadScene();
            return;
        }
        OnDeath death = Instantiate<OnDeath>(_deathAnimation, _player.transform.position, _player.transform.rotation);
        death.gameController = this;
        Destroy(_player.gameObject);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TakeObjective(ObjectiveController objective)
    {
        switch (objective.type)
        {
            case ObjectiveType.YELLOW:
                _yellowCollected++;
                break;
            case ObjectiveType.BLUE:
                _blueCollected++;
                break;
            default:
                _yellowCollected++;
                break;
        }
        Destroy(objective.gameObject);
        
        _audio.clip = _collectedSound;
        _audio.Play();

        if (_blueCollected == _blueTotal && _yellowCollected == _yellowTotal) {
            AllObjectivesCollected();
        }
    }

    private void UpdateTimer()
    {
        float currentTime;
        if (_endLevelTime > 0) {
            currentTime = _endLevelTime;
        } else {
            currentTime = Time.time;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime - _startLevelTime);
        _timer.text = FormatHelper.FormatTimeSpan(timeSpan);
    }

    private void AllObjectivesCollected()
    {
        _endLevelTime = Time.time;
        TimeSpan time = TimeSpan.FromSeconds(_endLevelTime - _startLevelTime);
        Debug.Log(string.Format("All objectives collected in {0}", FormatHelper.FormatTimeSpan(time)));
    }
}
