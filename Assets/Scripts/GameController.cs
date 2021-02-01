using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class GameController : MonoBehaviour
{
    private static GameController instance;
    private Transform _spawnPoint;
    [SerializeField] private GameObject playerPrefab;

    public static Transform Spawn => instance._spawnPoint;

    public void Start()
    {
        instance = this;
        _spawnPoint = GameObject.FindWithTag(UnityTags.SPAWN_POINT).transform;
        Instantiate(playerPrefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
