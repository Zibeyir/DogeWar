mergeInto(LibraryManager.library, {
  ConnectWallet: async function () {
    try {
      await window.web3Unity.connectWallet();
      if (window.web3Unity.currentAccount) {
        gameInstance.SendMessage(
          "Web3Connector",
          "OnAccountChanged",
          window.web3Unity.currentAccount
        );
      }
    } catch (e) {
      gameInstance.SendMessage("Web3Connector", "OnWeb3Error", window.web3Unity.formatError(e));
    } finally {
      gameInstance.SendMessage("Web3Connector", "OnConnectComplete");
    }
  },

  DisconnectWallet: function () {
    try {
      window.web3Unity.disconnectWallet();
      gameInstance.SendMessage("Web3Connector", "OnAccountChanged", "");
    }
    catch (e) {
      gameInstance.SendMessage("Web3Connector", "OnWeb3Error", window.web3Unity.formatError(e));
    } 
  },

  StartGame: async function () {
    console.log("StartGame");
    try {
      var result = await window.web3Unity.startGame(function (transactionHash) {
        gameInstance.SendMessage("Web3Connector", "OnPendingTransaction", transactionHash);
      });

      gameInstance.SendMessage(
        "Web3Connector",
        "OnTransactionComplete",
        result && result.receipt ? result.receipt.transactionHash : null,
        // result && result.receipt && result.receipt.status === true ? 1 : 0
      );
      gameInstance.SendMessage("Web3Connector", "OnGameStarted", result && result.roundId);
    } catch (e) {
      gameInstance.SendMessage("Web3Connector", "OnWeb3Error", window.web3Unity.formatError(e));
    } finally {
      gameInstance.SendMessage("Web3Connector", "OnStartGameComplete");
    }
  },

  ClaimGame: async function (signaturePtr) {
    console.log('ClaimGame')
    var success = false;
    try {
      var signature = UTF8ToString(signaturePtr)
      var result = await window.web3Unity.claimGame(signature, function (transactionHash) {
        gameInstance.SendMessage("Web3Connector", "OnPendingTransaction", transactionHash);
      });

      success = result && result.receipt && result.receipt.status === true
      gameInstance.SendMessage(
        "Web3Connector",
        "OnTransactionComplete",
       result && result.receipt ? result.receipt.transactionHash : null,
       // result && result.receipt && result.receipt.status === true ? 1 : 0
      );
      gameInstance.SendMessage("Web3Connector", "OnClaimed", result.amount);
    } catch (e) {
      gameInstance.SendMessage("Web3Connector", "OnWeb3Error", window.web3Unity.formatError(e));
    } finally {
      gameInstance.SendMessage("Web3Connector", "OnClaimComplete", success ? 1 : 0);
    }
  },

  ApproveGame: async function () {
    console.log('ApproveGame')
    try {
      var result = await window.web3Unity.approveGame(function (transactionHash) {
        gameInstance.SendMessage("Web3Connector", "OnPendingTransaction", transactionHash);
      });

      gameInstance.SendMessage(
        "Web3Connector",
        "OnTransactionComplete",
        result && result.receipt ? result.receipt.transactionHash : null,
        // result && result.receipt && result.receipt.status === true ? 1 : 0
      );
      gameInstance.SendMessage("Web3Connector", "OnGameApproved", result.success ? 1 : 0);
    } catch (e) {
      gameInstance.SendMessage("Web3Connector", "OnWeb3Error", window.web3Unity.formatError(e));
    } finally {
      gameInstance.SendMessage("Web3Connector", "OnApproveGameComplete");
    }
  },

  LoadApproval: async function () {
    console.log('LoadApproval')
    var isApproved = false
    try {
      isApproved = await window.web3Unity.isGameApproved();
    } catch (e) {
      gameInstance.SendMessage("Web3Connector", "OnWeb3Error", window.web3Unity.formatError(e));
    } finally {
      gameInstance.SendMessage("Web3Connector", "OnApprovalLoaded", isApproved ? 1 : 0);
    }
  }
});
