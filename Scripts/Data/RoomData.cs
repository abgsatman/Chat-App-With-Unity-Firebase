using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : Singleton<RoomData>
{
    public string roomId = "Lobby";

    public List<string> messages = new List<string>();

    private int _index;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
            //buraya mesajları debuglıyoruz.
        }
    }
}