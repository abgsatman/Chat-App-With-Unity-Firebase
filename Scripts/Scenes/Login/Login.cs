/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of CHAT project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField emailForm;
    public InputField passwordForm;

    public Button loginButtonForm;

    private AuthManager auth;

    private UserData user;
    
    void Start()
    {
        auth = AuthManager.Instance;
        user = UserData.Instance;

        user.gameState = GameState.Login;

        loginButtonForm.onClick.AddListener(DoLogin);
    }

    void DoLogin()
    {
        Debug.Log("Login süreci başladı..");

        string email = emailForm.text;
        string password = passwordForm.text;

        auth.Login(email, password);
    }
}
