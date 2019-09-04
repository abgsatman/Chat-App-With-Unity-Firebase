/*
* Unity C#, Firebase: Multiplayer Oyun Altyapısı Geliştirme Udemy Eğitimi
* Copyright (C) 2019 A.Gokhan SATMAN <abgsatman@gmail.com>
* This file is a part of CHAT project.
*/

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBManager : Singleton<DBManager>
{
    public AuthManager auth;

    public UserData user;
    public RoomData room;

    public DatabaseReference usersDatabase;
    public DatabaseReference roomsDatabase;

    public string FirebaseDBURL = "https://training-tg.firebaseio.com/";

    void Start()
    {
        auth = AuthManager.Instance;

        user = UserData.Instance;
        room = RoomData.Instance;

        Initialization();
    }

    void Initialization()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(FirebaseDBURL);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                usersDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");
                roomsDatabase = FirebaseDatabase.DefaultInstance.GetReference("ChatRooms");

                if(auth.auth.CurrentUser != null)
                {
                    auth.AutoLogin(auth.auth.CurrentUser.UserId);
                }
                else
                {
                    SceneManager.LoadScene("Login");
                }
            }
            else
            {
                Debug.LogError(String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public void CreateUser(string username)
    {
        Dictionary<string, object> general = new Dictionary<string, object>();
        general["Username"] = username;

        usersDatabase.Child(user.userId).Child("General").UpdateChildrenAsync(general);
        user.username = username;

        Debug.Log("Kullanıcı başarıyla oluşturuldu, login sahnesine yönlendiriliyorsunuz...");

        SceneManager.LoadScene("Login");
    }

    public void GetUserInformation()
    {
        usersDatabase.Child(user.userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("faulted");
                return;
            }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                string username = snapshot.Child("General").Child("Username").Value.ToString();

                user.username = username;

                Debug.Log("Kullanıcı login oldu ve bilgileri çekildi, lobby sahnesine yönlendiriliyorsunuz...");

                //event listener açılıyor.
                OpenListenChatRoom();

                SceneManager.LoadScene("Lobby");
            }
        });
    }

    public void DeleteChatKey(string chatItemKey)
    {
        roomsDatabase.Child(room.roomId).Child("Messages").Child(chatItemKey).RemoveValueAsync();
    }

    public void OpenListenChatRoom()
    {
        roomsDatabase.Child(room.roomId).Child("Messages").ValueChanged += ListenChatRoom;
    }

    public void ListenChatRoom(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        var snapshot = args.Snapshot;

        //Debug.Log("Odaya mesaj gönderildi!");

        foreach (DataSnapshot mesajlar in snapshot.Children)
        {
            string mesaj = snapshot.Child(mesajlar.Key).Child("Mesaj").Value.ToString();

            if(!room.messages.Contains(mesajlar.Key))
            {
                room.messages.Add(mesajlar.Key);
            }
        }
    }

    public void CloseListenChatRoom()
    {
        roomsDatabase.Child(room.roomId).Child("Messages").ValueChanged -= ListenChatRoom;
    }
}
