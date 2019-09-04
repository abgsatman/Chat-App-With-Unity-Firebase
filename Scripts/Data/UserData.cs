/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    #region Logic
    public GameState gameState;
    #endregion

    #region General
    public string userId;
    public string username;
    #endregion

    #region Progression
    public int score;
    #endregion
}
