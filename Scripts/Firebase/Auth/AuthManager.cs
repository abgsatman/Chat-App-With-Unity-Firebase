/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of TicTacToe project.
*/

using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthManager : Singleton<AuthManager>
{
    public FirebaseAuth auth;

    public DBManager DB;

    public UserData user;
    
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        DB = DBManager.Instance;
        user = UserData.Instance;
    }

    public void Signup(string username, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                return;
            }
            if(task.IsFaulted)
            {
                return;
            }
            FirebaseUser newUser = task.Result;
            DB.user.userId = newUser.UserId;
            DB.CreateUser(username);
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                Debug.Log("canceled");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("faulted");
                return;
            }
            FirebaseUser newUser = task.Result;
            DB.user.userId = newUser.UserId;
            DB.GetUserInformation();
        });
    }

    public void AutoLogin(string userId)
    {
        Debug.Log("Auto Login...");
        FirebaseUser firebaseUser = auth.CurrentUser;
        user.userId = userId;

        DB.GetUserInformation();

        SceneManager.LoadScene("Lobby");
    }
}
