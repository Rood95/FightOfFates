﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHeaderPanel : MonoBehaviour
{
    private readonly string connectionStatusMessage = "    Connection Status: ";

    [Header("UI References")]
    public Text ConnectionStatusText;

    #region UNITY

    public void Update() => ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;

    #endregion
}
