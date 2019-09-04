/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of CHAT project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour
{
    private DBManager DB;

    private UserData user;
    private RoomData room;

    void Start()
    {
        DB = DBManager.Instance;

        user = UserData.Instance;
        room = RoomData.Instance;

        DB.OpenListenChatRoom();
    }
}
