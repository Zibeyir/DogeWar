using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Numerics;

public class Web3 : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ConnectWallet();
    [DllImport("__Internal")]
    private static extern void DisconnectWallet();

    [DllImport("__Internal")]
    private static extern void StartGame();

    [DllImport("__Internal")]
    private static extern void ClaimGame(string signature);

    [DllImport("__Internal")]
    private static extern void LoadApproval();

    [DllImport("__Internal")]
    private static extern void ApproveGame();

    public UI UI;
    public Button ConnectButton;
    public GameObject ConnectButtonPanel;
    public Button DisconnectButton;
    public GameObject DisconnectButtonPanel;
    public Button StartButton;
    public GameObject StartButtonPanel;
    public Button ClaimButton;
    public GameObject ClaimButtonPanel;
    public Button ApproveButton;
    public GameObject ApproveButtonPanel;

    public string Account;
    public string CurrentRoundId;

    public bool IsConnecting;

    public bool IsConnected => !string.IsNullOrEmpty(Account);

    public bool IsApproveRequired;
    public bool IsApproving;
    public bool IsClaiming;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Connect()
    {
        print("Connect");
        this.IsConnecting = true;
        ConnectButton.enabled = false;
        ConnectWallet();
    }

    public void OnConnectComplete()
    {
        print("OnConnectComplete");
        this.IsConnecting = false;
        ConnectButton.enabled = true;
        print("OnConnectComplete -");

        if (this.IsConnected)
            LoadApproval();
    }

    public void Disconnect()
    {
        print("Disconnect");
        DisconnectWallet();
        print("Disconnect -");
    }

    void OnAccountChanged(string account)
    {
        this.Account = account;

        print(string.Format("OnAccountChanged: {0}, {1}", (account ?? string.Empty), IsConnected));

        // var txt = DisconnectButton.GetComponent<Text>();
        // if (txt != null)
        //     txt.text = (account ?? "Disconnect");
        ConnectButtonPanel.SetActive(!this.IsConnected);
        DisconnectButtonPanel.SetActive(this.IsConnected);
        if (!this.IsConnected)
        {
            StartButtonPanel.SetActive(false);
            ApproveButtonPanel.SetActive(false);
        }

        print("OnAccountChanged -");
    }

    public void Approve()
    {
        print("Approve");
        this.IsApproving = true;
        ApproveButton.enabled = false;
        ApproveGame();
        print("Approve -");
    }

    void OnGameApproved(int approved)
    {
        this.IsApproveRequired = approved != 1;
        StartButtonPanel.SetActive(!this.IsApproveRequired);
        ApproveButtonPanel.SetActive(this.IsApproveRequired);
        this.IsApproving = false;
        print(string.Format("OnGameApproved: IsApproveRequired: {0} / {1}", this.IsApproveRequired, approved));
    }

    void OnApproveGameComplete()
    {
        ApproveButton.enabled = true;
    }

    void OnApprovalLoaded(int approved)
    {
        this.IsApproveRequired = approved != 1;
        StartButtonPanel.SetActive(!this.IsApproveRequired);
        ApproveButtonPanel.SetActive(this.IsApproveRequired);
        print(string.Format("OnApprovalLoaded: IsApproveRequired: ${0}", this.IsApproveRequired));
    }

    public void NewGame()
    {
        if (!this.IsConnected)
        {
            OnWeb3Error("Connect your wallet to start the game");
            return;
        }

        StartButton.enabled = false;
        StartGame();
    }
    public void OnStartGameComplete()
    {
        StartButton.enabled = true;
    }

    public void OnGameStarted(string roundId)
    {
        this.CurrentRoundId = !string.IsNullOrEmpty(roundId) && !string.Equals(roundId, "0") ? roundId : null;
        if (!string.IsNullOrEmpty(this.CurrentRoundId))
        {
            UI.StartGame();

            // TODO: uncomment for debug
            // StartButtonPanel.SetActive(false);
            // ClaimButtonPanel.SetActive(true);
        }
    }

    private byte[] encode(BigInteger value, bool reverse)
    {
        byte[] bytes;
        if (reverse) // BitConverter.IsLittleEndian != littleEndian)
            bytes = value.ToByteArray().Reverse().ToArray();
        else
            bytes = value.ToByteArray().ToArray();
        return bytes;
    }

    private string LoadSignature()
    {
#if DEBUG
        // TODO: DO NOT USE THIS CODE IN PRODUCTION AND DO NOT KEEP THE PRIVATE KEY IN THE PROJECT !
        // Load from server and verify the claim (user must have finished the game for the current round !), 
        // Nethereum can be removed from the project

        var amount = "50000000000000000000000000";
        var key = new EthECKey("0x759e8116dc223cb5fa609b01bdd2da7ebcac4550dea16718614523166621cb80");
        var sha3 = new Nethereum.Util.Sha3Keccack();

        var signer = new EthereumMessageSigner();

        var hash = sha3.CalculateHashFromHex(new[]
        {
            this.Account.RemoveHexPrefix().PadLeft(64, '0'),
            encode(BigInteger.Parse(CurrentRoundId), true).ToHex().RemoveHexPrefix().PadLeft(64, '0'),
            encode(BigInteger.Parse(amount), true).ToHex().RemoveHexPrefix().PadLeft(64, '0'),
        });

        var signature = signer.Sign(hash.HexToByteArray(), key);

        // JS
        //   const signature = await wallet.signMessage(
        //     ethers.utils.arrayify(
        //       ethers.utils.keccak256(
        //         ethers.utils.defaultAbiCoder.encode(
        //           ['address', 'uint256', 'uint256'],
        //           [sender, roundId, amount],
        //         ),
        //       ),
        //     ),
        //   )

        return signature;
#endif

        return "0x";
    }

    public void Claim()
    {
        print("Claim");
        this.IsClaiming = true;
        ClaimButton.enabled = false;

        var signature = LoadSignature();
        if (!string.IsNullOrEmpty(signature))
        {
            ClaimGame(signature);
        }
    }
    public void OnClaimComplete(int success)
    {
        ClaimButton.enabled = true;
        this.CurrentRoundId = null;

        // TODO: Custom scene after claim ?
        if (success == 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnClaimed(string amount)
    {
        this.CurrentRoundId = null;
        // TODO: Show success popup
    }

    void OnWeb3Error(string message)
    {
        // TODO: Show Error
        print(string.Format("OnWeb3Error: {0}", message));
    }
    void OnPendingTransaction(string hash)
    {
        print(string.Format("OnPendingTransaction: {0}", hash));
    }
    void OnTransactionComplete(string hash)
    {
        print(string.Format("OnTransactionComplete: {0}", hash));
    }
}
