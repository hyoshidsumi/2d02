using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    string mode;                 // モード(ONLINE, OFFLINE)
    string dispStatus;           // 画面項目：状態
    string dispMessage;          // 画面項目：メッセージ
    string dispRoomName;         // 画面項目：ルーム名
    List<RoomInfo> roomDispList; // 画面項目：ルーム一覧

    public enum Status
    {
        ONLINE, OFFLINE,
    };

    private void Start()
    {
        initParam();
    }

    private void initParam()
    {
        dispRoomName = "";
        dispMessage = "";
        dispStatus = Status.OFFLINE.ToString();
        roomDispList = new List<RoomInfo>();

        ConnectPhoton(false);
    }

    public void ConnectPhoton(bool boolOffline)
    {
        if (boolOffline)
        {
            mode = Status.OFFLINE.ToString();
            PhotonNetwork.OfflineMode = true; // OnConnectedToMaster()が呼ばれる
            dispMessage = "OFFLINEモードで起動しました。";
            return;
        }
        mode = Status.ONLINE.ToString();
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log(transform.Find("map1").Find("Text").GetComponent<Text>().text);
    }

    // Photonサーバ切断処理
    public void DisConnectPhoton()
    {
        PhotonNetwork.Disconnect();
        // 変数初期化
        initParam();
    }

    // コールバック：Photonサーバ接続完了
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if (Status.ONLINE.ToString().Equals(mode))
        {
            dispStatus = Status.ONLINE.ToString();
            dispMessage = "サーバに接続しました。";
            // ロビーに接続
            PhotonNetwork.JoinLobby();
        }
    }

    // コールバック：Photonサーバ接続失敗
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        dispMessage = "サーバから切断しました。";
        dispStatus = Status.OFFLINE.ToString();
    }

    // コールバック：ロビー入室完了
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    // ルーム一覧更新処理
    // (ロビーに入室した時、他のプレイヤーが更新した時のみ)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        // ルーム一覧更新
        foreach (var info in roomList)
        {
            if (!info.RemovedFromList)
            {
                // 更新データが削除でない場合
                roomDispList.Add(info);
            }
            else
            {
                // 更新データが削除の場合
                roomDispList.Remove(info);
            }
        }
    }

    // ルーム作成処理
    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    // ルーム入室処理
    public void ConnectToRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    // コールバック：ルーム作成完了
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        dispMessage = "ルームを作成しました。";
    }

    // コールバック：ルーム作成失敗
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        dispMessage = "ルーム作成に失敗しました。";
    }

    // コールバック：ルームに入室した時
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // 表示ルームリストに追加する
        roomDispList.Add(PhotonNetwork.CurrentRoom);
        dispMessage = "【" + PhotonNetwork.CurrentRoom.Name + "】" + "に入室しました。";
    }

    // ---------- 設定GUI ----------
    void OnGUI()
    {
        float scale = Screen.height / 480.0f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(
            Screen.width * 0.5f, Screen.height * 0.5f, 0),
            Quaternion.identity,
            new Vector3(scale, scale, 1.0f));

        GUI.Window(0, new Rect(-200, -200, 400, 400),
            NetworkSettingWindow, "Photon接続テスト");
    }

    Vector2 scrollPosition;
    void NetworkSettingWindow(int windowID)
    {
        // ステータス, メッセージの表示
        GUILayout.BeginHorizontal();
        GUILayout.Label("状態: " + dispStatus, GUILayout.Width(100));
        GUILayout.FlexibleSpace();
        if (Status.ONLINE.ToString().Equals(dispStatus))
        {
            // サーバ接続時のみ表示
            if (GUILayout.Button("切断"))
                DisConnectPhoton();
        }
        GUILayout.EndHorizontal();
        GUILayout.Label(dispMessage);
        GUILayout.Space(20);

        if (!Status.ONLINE.ToString().Equals(dispStatus))
        {
            // --- 初期表示時、OFFLINEモードのみ表示
            // マスターサーバに接続する
            GUILayout.Label("【モード選択】");
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("ONLINE Mode"))
                ConnectPhoton(false);
            if (GUILayout.Button("OFFLINE Mode"))
                ConnectPhoton(true);
            GUILayout.EndHorizontal();
        }
        else if (Status.ONLINE.ToString().Equals(dispStatus))
        {
            // --- ONLINEモードのみ表示
            if (!(PhotonNetwork.CurrentRoom != null))
            {
                // ルーム作成
                GUILayout.Label("【ルーム作成】");
                GUILayout.BeginHorizontal();
                GUILayout.Label("　ルーム名: ");
                dispRoomName = GUILayout.TextField(dispRoomName, GUILayout.Width(150));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                // 作成ボタン
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("作成 & 入室"))
                {
                    CreateRoom(dispRoomName);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(20);

                // ルーム一覧
                GUILayout.Label("【ルーム一覧 (クリックで入室)】");
                // 一覧表示
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(380), GUILayout.Height(100));
                if (roomDispList != null && roomDispList.Count > 0)
                {
                    // 更新ボタン
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("更新"))
                    {
                        // ロビーに入り直す
                        roomDispList = new List<RoomInfo>();
                        PhotonNetwork.LeaveLobby();
                        PhotonNetwork.JoinLobby();
                    }
                    // ルーム一覧
                    GUILayout.EndHorizontal();
                    foreach (RoomInfo roomInfo in roomDispList)
                        if (GUILayout.Button(roomInfo.Name, GUI.skin.box, GUILayout.Width(360)))
                            ConnectToRoom(roomInfo.Name);
                }
                GUILayout.EndScrollView();
            }
        }
    }
}