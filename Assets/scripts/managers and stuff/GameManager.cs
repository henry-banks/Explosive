﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject thingToPlace;
    public int thingIdx;
    public LevelManager levelManager;

    private void Awake()
    {
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        levelManager = FindObjectOfType <LevelManager>();
    }

    private void Update()
    {
        //if(levelManager)
        //{
        //    Debug.Log(levelManager.placeableObjects[thingIdx].remaining);
        //}
    }
}
