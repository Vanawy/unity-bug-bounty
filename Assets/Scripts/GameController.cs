using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperTiled2Unity;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private SuperObjectLayer _objects;

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

    void Awake()
    {
        PlayerController player = _objects.GetComponentInChildren<PlayerController>();
        _vcam.m_Follow = player.transform;

        PlayerDeath playerDeath = player.gameObject.GetComponent<PlayerDeath>();
        if (playerDeath != null) {
            playerDeath.OnRestart += RestartLevel;
        }

        _objectives = _objects.GetComponentsInChildren<ObjectiveController>();

        foreach (var objective in _objectives)
        {
            GameObject marker = Instantiate(_markerPrefab);
            MarkerController controller = marker.GetComponent<MarkerController>();
            controller.parent = player.transform;
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
    }

    private void UpdateCounters()
    {
        _yellowCounter.text = string.Format("{0}/{1}", _yellowCollected, _yellowTotal);
        _blueCounter.text = string.Format("{0}/{1}", _blueCollected, _blueTotal);
    }
    
    public void RestartLevel()
    {
        Debug.Log("Restart level here");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
