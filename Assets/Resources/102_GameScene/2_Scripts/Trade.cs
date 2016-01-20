﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trade : MonoBehaviour
{
    private List<string> NPC_List = new List<string>(); // 아이템 리스트
    private List<string> Player_List = new List<string>(); // 아이템 리스트
    private List<string> NPC_Trade = new List<string>(); // 교환할 아이템 리스트
    private List<string> Player_Trade = new List<string>(); // 교환할 아이템 리스트  
    public InvenData NPC_Data;
    public InvenData Player_Data;

	// Use this for initialization
	void Start ()
    {

        NPC_List = NPC_Data.GetList();
        Player_List = Player_Data.GetList();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
        
    public void accept()// 거래 함
    {
        NPC_Data.SetList(Player_Trade);
        Player_Data.SetList(NPC_Trade);
    }

    public void refuse()//거래 안함
    {

       Player_Data.SetList(Player_Trade);
        NPC_Data.SetList(NPC_Trade);
    }


}