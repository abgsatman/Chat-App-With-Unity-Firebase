/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signup : MonoBehaviour
{
    public InputField usernameForm;
    public InputField emailForm;
    public InputField passwordForm;

    public Button signupButtonForm;

    private AuthManager auth;

    private UserData user;

    void Start()
    {
        auth = AuthManager.Instance;
        user = UserData.Instance;

        user.gameState = GameState.Signup;

        signupButtonForm.onClick.AddListener(DoSignup);
    }

    void DoSignup()
    {
        Debug.Log("signup süreci başladı..");

        string username = usernameForm.text;
        string email = emailForm.text;
        string password = passwordForm.text;

        auth.Signup(username, email, password);
    }
}
