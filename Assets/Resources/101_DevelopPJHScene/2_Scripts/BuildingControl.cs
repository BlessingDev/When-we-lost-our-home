﻿using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 건물을 관리하는 스크립트:
/// 외부와의 연결, 데이터 관리의 역할을 한다.
/// </summary>
public class BuildingControl : MonoBehaviour
{
    #region Variables
    public Transform mPlayerPos = null;
    public string mBuildingName = "";

    private float mMaxFloor = 0f;
    private List<GameObject> mFloors = new List<GameObject>();
    #endregion

    #region get/setter
    public float MaxFloor
    {
        get
        {
            return mMaxFloor;
        }
    }
    #endregion

    #region VirtualFuctions
    // Use this for initialization
    void Start ()
    {
        string dir = System.Environment.CurrentDirectory + "\\Assets\\Resources\\DataFiles\\MapData\\" + mBuildingName + ".wwmapdata";
	    if (File.Exists(dir))
        {
            ReadData(dir);
        }
        else
        {
            Debug.Log("Map Data doesn't exist");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnDestroy()
    {
        string dir = System.Environment.CurrentDirectory + "\\Assets\\Resources\\DataFiles\\MapData\\" + mBuildingName + ".wwmapdata";
        WriteData(dir);
    }
    #endregion

    #region CustomFunctions
    
    public void AddFloor(GameObject floor)
    {
        mMaxFloor += 1;
        mFloors.Add(floor);
    }

    public void SetPlayer()
    {
        GameDirector.Instance.mPlayer.transform.position = mPlayerPos.transform.position;
    }

    private void ReadData(string fPath)
    {
        Debug.Log("read data");
    }



    public void WriteData(string fPath)
    {
        Debug.Log("write data");
    }
    #endregion
}