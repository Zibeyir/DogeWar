(function webpackUniversalModuleDefinition(root, factory) {
	if(typeof exports === 'object' && typeof module === 'object')
		module.exports = factory();
	else if(typeof define === 'function' && define.amd)
		define([], factory);
	else {
		var a = factory();
		for(var i in a) (typeof exports === 'object' ? exports : root)[i] = a[i];
	}
})(this, () => {
return /******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ 913:
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.GAME_ALLOWANCE = exports.GAME_PRICE = exports.PAYMENT_TOKEN_ADDRESS = exports.GAME_ADDRESS = void 0;
exports.GAME_ADDRESS = window.dogWarConfig?.gameAddress ||
    '0x75f1bE481b17107d9376009687F2CBF1F6D46DcA';
exports.PAYMENT_TOKEN_ADDRESS = window.dogWarConfig?.paymentToken ||
    '0x385aeA348536a0Db02314047Bb9cd9Feac6507EB';
exports.GAME_PRICE = window.dogWarConfig?.gamePrice || '10000000000000000000000000';
exports.GAME_ALLOWANCE = window.dogWarConfig?.gameAllowance || '1000000000000000000000000000';


/***/ }),

/***/ 97:
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.approveGame = exports.isGameApproved = exports.claimGame = exports.startGame = exports.parseRoundId = exports.getGasLimit = exports.currentAccount = exports.formatError = void 0;
const DogWarGame_json_1 = __importDefault(__webpack_require__(290));
const IERC20_json_1 = __importDefault(__webpack_require__(478));
const config_1 = __webpack_require__(913);
const web3_1 = __webpack_require__(793);
function formatError(err) {
    const msgError = err?.error?.message ||
        err?.data?.message ||
        err?.reason ||
        err?.message ||
        'An unknown error occured';
    return msgError;
}
exports.formatError = formatError;
function currentAccount() {
    return window.web3Unity.currentAccount;
}
exports.currentAccount = currentAccount;
function getGasLimit(gas, ratio = 1.1) {
    const limit = (0, web3_1.toBN)(gas)
        .mul((0, web3_1.toBN)((ratio * 100).toFixed(0)))
        .div((0, web3_1.toBN)('100'));
    return limit;
}
exports.getGasLimit = getGasLimit;
function parseRoundId(contract, receipt) {
    return receipt?.events?.GameStarted?.returnValues?.roundId || '0';
}
exports.parseRoundId = parseRoundId;
async function startGame(onHashReceived) {
    const contract = (0, web3_1.getContract)(DogWarGame_json_1.default, config_1.GAME_ADDRESS);
    const from = currentAccount();
    const method = contract.methods.startGame();
    const gasLimit = await method.estimateGas({ from });
    const tx = await method
        .send({
        from,
        gasLimit: getGasLimit(gasLimit),
    })
        .on('transactionHash', (transactionHash) => {
        onHashReceived(transactionHash);
    });
    const receipt = tx;
    const roundId = parseRoundId(contract, receipt);
    return {
        receipt,
        success: receipt?.status === true,
        roundId,
    };
}
exports.startGame = startGame;
async function claimGame(signature, onHashReceived) {
    const parts = signature.split(',');
    if (parts.length > 1) {
        for (const part of parts) {
            try {
                const r = await claimGame(part, onHashReceived);
                if (r?.success)
                    return r;
            }
            catch { }
        }
    }
    const contract = (0, web3_1.getContract)(DogWarGame_json_1.default, config_1.GAME_ADDRESS);
    const from = currentAccount();
    const args = [signature];
    const method = contract.methods.claim(...args);
    const gasLimit = await method.estimateGas({ from });
    const tx = await method
        .send({
        from,
        gasLimit: getGasLimit(gasLimit),
    })
        .on('transactionHash', (transactionHash) => {
        onHashReceived(transactionHash);
    });
    const receipt = tx;
    return {
        receipt,
        success: receipt?.status === true,
        amount: receipt?.events?.Claimed?.returnValues?.amount || '0',
    };
}
exports.claimGame = claimGame;
async function isGameApproved() {
    const contract = (0, web3_1.getContract)(IERC20_json_1.default, config_1.PAYMENT_TOKEN_ADDRESS);
    const args = [currentAccount(), config_1.GAME_ADDRESS];
    const allowance = await contract.methods.allowance(...args).call();
    return (0, web3_1.toBN)(allowance).gte((0, web3_1.toBN)(config_1.GAME_PRICE));
}
exports.isGameApproved = isGameApproved;
async function approveGame(onHashReceived) {
    const contract = (0, web3_1.getContract)(IERC20_json_1.default, config_1.PAYMENT_TOKEN_ADDRESS);
    const from = currentAccount();
    const args = [config_1.GAME_ADDRESS, config_1.GAME_ALLOWANCE];
    const method = contract.methods.approve(...args);
    const gasLimit = await method.estimateGas({ from });
    const tx = await method
        .send({
        from,
        gasLimit: getGasLimit(gasLimit),
    })
        .on('transactionHash', (transactionHash) => {
        onHashReceived(transactionHash);
    });
    const receipt = tx;
    return {
        receipt,
        success: receipt?.status === true,
    };
}
exports.approveGame = approveGame;


/***/ }),

/***/ 793:
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.toBN = exports.getContract = exports.addEthereumChain = exports.disconnectWallet = exports.connectWallet = void 0;
let web3;
let provider;
async function connectWallet() {
    console.log('connectWallet');
    const wWindow = window;
    const providerOptions = {};
    const web3Modal = new wWindow.Web3Modal.default({
        providerOptions,
    });
    web3Modal.clearCachedProvider();
    provider = await web3Modal.connect();
    web3 = new Web3(provider);
    wWindow.web3Unity.networkId = parseInt(provider.chainId);
    if (wWindow.web3Unity.networkId != wWindow.web3ChainId) {
        try {
            await wWindow.ethereum.request({
                method: 'wallet_switchEthereumChain',
                params: [{ chainId: `0x${wWindow.web3ChainId.toString(16)}` }],
            });
        }
        catch {
            await addEthereumChain();
        }
    }
    wWindow.web3Unity.currentAccount =
        provider.selectedAddress || provider.accounts[0];
    provider.on('accountsChanged', () => {
        window.location.reload();
    });
    provider.on('chainChanged', (chainId) => {
        wWindow.web3Unity.networkId = parseInt(chainId);
    });
}
exports.connectWallet = connectWallet;
async function disconnectWallet() {
    console.log('disconnectWallet');
}
exports.disconnectWallet = disconnectWallet;
async function addEthereumChain() {
    const wWindow = window;
    const account = (await web3.eth.getAccounts())[0];
    const response = await fetch('https://chainid.network/chains.json');
    const chains = await response.json();
    const chain = chains.find((chain) => chain.chainId == wWindow.web3ChainId);
    const params = {
        chainId: '0x' + chain.chainId.toString(16),
        chainName: chain.name,
        nativeCurrency: {
            name: chain.nativeCurrency.name,
            symbol: chain.nativeCurrency.symbol,
            decimals: chain.nativeCurrency.decimals,
        },
        rpcUrls: chain.rpc,
        blockExplorerUrls: [
            chain.explorers && chain.explorers.length > 0 && chain.explorers[0].url
                ? chain.explorers[0].url
                : chain.infoURL,
        ],
    };
    await wWindow.ethereum
        .request({
        method: 'wallet_addEthereumChain',
        params: [params, account],
    })
        .catch(() => {
        window.location.reload();
    });
}
exports.addEthereumChain = addEthereumChain;
function getContract(abi, address) {
    return new web3.eth.Contract(abi, address, provider);
}
exports.getContract = getContract;
function toBN(value) {
    return new web3.utils.BN(value.toString());
}
exports.toBN = toBN;


/***/ }),

/***/ 290:
/***/ ((module) => {

module.exports = JSON.parse('[{"inputs":[{"internalType":"contract IERC20","name":"_paymentToken","type":"address"},{"internalType":"contract IERC20","name":"_rewardToken","type":"address"},{"internalType":"uint256","name":"_paymentAmount","type":"uint256"},{"internalType":"uint256","name":"_rewardAmount","type":"uint256"},{"internalType":"address","name":"_treasury","type":"address"},{"internalType":"address","name":"_authorizer","type":"address"}],"stateMutability":"nonpayable","type":"constructor"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"account","type":"address"},{"indexed":false,"internalType":"uint256","name":"roundId","type":"uint256"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"Claimed","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"EthRecovery","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"account","type":"address"},{"indexed":false,"internalType":"uint256","name":"roundId","type":"uint256"}],"name":"GameStarted","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"token","type":"address"},{"indexed":false,"internalType":"uint256","name":"tokenId","type":"uint256"}],"name":"NonFungibleTokenRecovery","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"previousOwner","type":"address"},{"indexed":true,"internalType":"address","name":"newOwner","type":"address"}],"name":"OwnershipTransferred","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"account","type":"address"}],"name":"Paused","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"token","type":"address"},{"indexed":false,"internalType":"uint256","name":"amount","type":"uint256"}],"name":"TokenRecovery","type":"event"},{"anonymous":false,"inputs":[{"indexed":false,"internalType":"address","name":"account","type":"address"}],"name":"Unpaused","type":"event"},{"inputs":[{"internalType":"bytes","name":"signature","type":"bytes"}],"name":"claim","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"owner","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"pause","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"paused","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"paymentAmount","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"paymentToken","outputs":[{"internalType":"contract IERC20","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address payable","name":"_to","type":"address"}],"name":"recoverEth","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_token","type":"address"},{"internalType":"uint256","name":"_tokenId","type":"uint256"}],"name":"recoverNonFungibleToken","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_token","type":"address"}],"name":"recoverToken","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"renounceOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"rewardAmount","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"rewardToken","outputs":[{"internalType":"contract IERC20","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_authorizer","type":"address"}],"name":"setAuthorizer","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_amount","type":"uint256"}],"name":"setPaymentAmount","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"contract IERC20","name":"_paymentToken","type":"address"}],"name":"setPaymentToken","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"_amount","type":"uint256"}],"name":"setRewardAmount","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"contract IERC20","name":"_rewardToken","type":"address"}],"name":"setRewardToken","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_treasury","type":"address"}],"name":"setTreasury","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"startGame","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"newOwner","type":"address"}],"name":"transferOwnership","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"treasury","outputs":[{"internalType":"address","name":"","type":"address"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"unpause","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"uint256","name":"amount","type":"uint256"}],"name":"withdraw","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[],"name":"withdrawAll","outputs":[],"stateMutability":"nonpayable","type":"function"}]');

/***/ }),

/***/ 478:
/***/ ((module) => {

module.exports = JSON.parse('[{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"owner","type":"address"},{"indexed":true,"internalType":"address","name":"spender","type":"address"},{"indexed":false,"internalType":"uint256","name":"value","type":"uint256"}],"name":"Approval","type":"event"},{"anonymous":false,"inputs":[{"indexed":true,"internalType":"address","name":"from","type":"address"},{"indexed":true,"internalType":"address","name":"to","type":"address"},{"indexed":false,"internalType":"uint256","name":"value","type":"uint256"}],"name":"Transfer","type":"event"},{"inputs":[{"internalType":"address","name":"owner","type":"address"},{"internalType":"address","name":"spender","type":"address"}],"name":"allowance","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"spender","type":"address"},{"internalType":"uint256","name":"amount","type":"uint256"}],"name":"approve","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"account","type":"address"}],"name":"balanceOf","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[],"name":"totalSupply","outputs":[{"internalType":"uint256","name":"","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"to","type":"address"},{"internalType":"uint256","name":"amount","type":"uint256"}],"name":"transfer","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"from","type":"address"},{"internalType":"address","name":"to","type":"address"},{"internalType":"uint256","name":"amount","type":"uint256"}],"name":"transferFrom","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"nonpayable","type":"function"}]');

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// This entry need to be wrapped in an IIFE because it need to be isolated against other modules in the chunk.
(() => {
var exports = __webpack_exports__;
var __webpack_unused_export__;

__webpack_unused_export__ = ({ value: true });
const dogwar_1 = __webpack_require__(97);
const web3_1 = __webpack_require__(793);
(function (global) {
    global.web3Unity = global.web3Unity || {
        connectWallet: web3_1.connectWallet,
        disconnectWallet: web3_1.disconnectWallet,
        startGame: dogwar_1.startGame,
        approveGame: dogwar_1.approveGame,
        isGameApproved: dogwar_1.isGameApproved,
        claimGame: dogwar_1.claimGame,
        formatError: dogwar_1.formatError,
    };
})(window);

})();

__webpack_exports__ = __webpack_exports__["default"];
/******/ 	return __webpack_exports__;
/******/ })()
;
});