using SteamKit2;
using System;
using System.Threading.Tasks;
using static CSGOBot.Utils;
using System.IO;
using SteamKit2.Internal;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Generic;
using SteamKit2.GC;
using SteamKit2.GC.CSGO.Internal;
using System.Text;

namespace CSGOBot
{
    public class CSGOClient
    {
        public static Form1 form;
        public static CallbackManager callbackManager;
        public static SteamClient steamClient;
        public static SteamUser steamUser;
        public static SteamFriends steamFriends;
        public static SteamGameCoordinator steamGameCoordinator;
        public static bool isRunning, isConnected;
        private static string sentryFileName, loginKeyFileName;

        static Handler cHandler;

        //Steam Informations
        private static string accountUsername, accountPassword;

        //CS:GO Informations
        public static uint CSGO_APPID = 730;
        public static uint CSGO_VERSION = 910; // 27-04-19
        public static uint CSGO_PVERSION = 13690;  // 27-04-19

        //Properties
        public static uint accountId { get; set; } = 0;
        public static ulong steamId { get; set; } = 0; //steamId - 76561197960265728 ??? YES!
        public static uint publicIp { get; set; } = 0;
        public static uint cellId { get; set; } = 0;
        public static ulong lobbyId { get; set; } = 0; //lobbyId - 109775240917155840 ????????? YES!!!!!
        public static ulong matchId { get; set; } = 0;
        public static RankName rankId { get; set; } = 0;
        public static int playerLvl { get; set; } = 0;

        public class MyCallback : CallbackMsg
        {
            public EResult Result { get; private set; }

            internal MyCallback(EResult res)
            {
                Result = res;
            }
        }

        public CSGOClient(Form1 formI, string steamUsername, string steamPassword)
        {
            form = formI;
            accountUsername = steamUsername;
            accountPassword = steamPassword;

            form.AddLog("Initialization CSGOClient...");

            loginKeyFileName = "data/sentry/" + accountUsername + ".key";
            sentryFileName = "data/sentry/" + accountUsername + ".sentry";

            steamClient = new SteamClient();
            steamClient.DebugNetworkListener = new NetHookNetworkListener("data/debug");

            steamUser = steamClient.GetHandler<SteamUser>();
            steamFriends = steamClient.GetHandler<SteamFriends>();
            steamGameCoordinator = steamClient.GetHandler<SteamGameCoordinator>();

            steamClient.AddHandler(new Handler(formI));
            cHandler = steamClient.GetHandler<Handler>();

            callbackManager = new CallbackManager(steamClient);

            //Steam Events
            callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            callbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);
            callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            callbackManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            callbackManager.Subscribe<SteamUser.AccountInfoCallback>(OnAccountInfo);
            callbackManager.Subscribe<SteamUser.LoginKeyCallback>(OnLoginKey);
            callbackManager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth);

            //CS:GO Events
            callbackManager.Subscribe<SteamGameCoordinator.MessageCallback>(OnGCMessage);
            callbackManager.Subscribe<Handler.MyCallback>(OnMyCallback);
        }

        void OnMyCallback(Handler.MyCallback callback)
        {
            // this will be called when our custom callback gets posted
            Console.WriteLine("OnMyCallback: {0}", callback.Result);
        }

        public void Connect()
        {
            steamClient.Connect();
            isRunning = true;

            while (isRunning)
                callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
        }

        public void Disconnect()
        {
            isConnected = false;
            isRunning = false;

            if (steamFriends.GetPersonaState() != EPersonaState.Offline)
                steamFriends.SetPersonaState(EPersonaState.Offline);

            steamUser.LogOff();
            steamClient.Disconnect();
        }

        #region Steam Events
        void OnConnected(SteamClient.ConnectedCallback callback)
        {
            if (callback == null)
                return;

            form.AddLog(String.Format("Connected to Steam servers! Logging '{0}' into Steam...", accountUsername));

            steamUser.LogOn(new SteamUser.LogOnDetails
            {
                Username = accountUsername,
                Password = accountPassword
            });
        }

        void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            if (callback == null)
                return;

            isConnected = false;
            isRunning = false;
            form.AddLog("[ERROR] Disconnecting from steam! Reconnecting...");

            Thread.Sleep(TimeSpan.FromSeconds(5));
            Connect();
        }

        async void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback == null)
                return;

            switch (callback.Result)
            {
                case EResult.TwoFactorCodeMismatch:
                case EResult.AccountLoginDeniedNeedTwoFactor:
                case EResult.AccountLogonDenied:
                    form.AddLog("[ERROR] SteamGuard needed for this account!");
                    return;
                case EResult.InvalidPassword:
                    form.AddLog("[ERROR] Unable to login to Steam: " + callback.Result);
                    return;
                case EResult.OK:
                    form.AddLog("Successfully connected!");

                    isConnected = true;

                    if (steamFriends.GetPersonaState() != EPersonaState.Online)
                        steamFriends.SetPersonaState(EPersonaState.Online);

                    var gamePlayed = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayed);
                    gamePlayed.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
                    {
                        game_id = new GameID(CSGO_APPID)
                    });

                    steamClient.Send(gamePlayed);

                    Thread.Sleep(TimeSpan.FromSeconds(5));

                    //Send ClientToGC GCClientHello to valve servers
                    var clientHello = new ClientGCMsgProtobuf<CMsgClientHello>((uint)EGCBaseClientMsg.k_EMsgGCClientHello);
                    clientHello.Body.version = CSGO_VERSION;
                    clientHello.Body.client_session_need = 0;
                    clientHello.Body.client_launcher = 0;
                    steamGameCoordinator.Send(clientHello, CSGO_APPID);

                    var clientCertRequest = new ClientMsgProtobuf<CMsgClientNetworkingCertRequest>(EMsg.ClientNetworkingCertRequest);
                    clientCertRequest.Body.key_data = new byte[] {  }; //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX4
                    clientCertRequest.Body.app_id = CSGO_APPID;
                    steamClient.Send(clientCertRequest);
                    break;
                default:
                    form.AddLog(String.Format("[ERROR] Can't connect to steam: {0} / {1}", callback.Result, callback.ExtendedResult));
                    return;
            }

            await Task.Delay(5000).ConfigureAwait(false);
        }

        void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            if (callback == null)
                return;

            isConnected = false;
            isRunning = false;
            form.AddLog(String.Format("[ERROR] Disconnected from steam: {0}", callback.Result));
            form.ToggleAll(false);
        }

        void OnAccountInfo(SteamUser.AccountInfoCallback callback)
        {
            if (callback == null)
                return;
        }

        void OnLoginKey(SteamUser.LoginKeyCallback callback)
        {
            if (callback == null)
                return;

            File.WriteAllText(loginKeyFileName, callback.LoginKey);
            steamUser.AcceptNewLoginKey(callback);
        }

        void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
        {
            if (callback == null)
                return;

            int fileSize;
            byte[] sentryHash;
            using (var fs = File.Open(sentryFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fs.Seek(callback.Offset, SeekOrigin.Begin);
                fs.Write(callback.Data, 0, callback.BytesToWrite);
                fileSize = (int)fs.Length;

                fs.Seek(0, SeekOrigin.Begin);

                using (var sha = new SHA1CryptoServiceProvider())
                    sentryHash = sha.ComputeHash(fs);
            }

            steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                JobID = callback.JobID,
                FileName = callback.FileName,
                BytesWritten = callback.BytesToWrite,
                FileSize = fileSize,
                Offset = callback.Offset,
                Result = EResult.OK,
                LastError = 0,
                OneTimePassword = callback.OneTimePassword,
                SentryFileHash = sentryHash,
            });
        }
        #endregion

        #region CS:GO Events
        void OnGCMessage(SteamGameCoordinator.MessageCallback callback)
        {
            if (callback == null)
                return;

            Console.WriteLine(string.Format("OnGCMessage code : {0}", callback.EMsg));

            var messageMap = new Dictionary<uint, Action<IPacketGCMsg>>
            {
                { ( uint )EGCBaseClientMsg.k_EMsgGCClientWelcome, OnClientWelcome },
                { ( uint )ECsgoGCMsg.k_EMsgGCCStrike15_v2_MatchmakingGC2ClientHello, OnMatchmakingClientHello },
                //{ (uint)EMsg.ClientMMSJoinLobbyResponse, OnJoinLobbyResponse },
                //{ (uint)EMsg.ClientMMSCreateLobbyResponse, OnCreateLobbyResponse }
        	};

            Action<IPacketGCMsg> func;

            if (!messageMap.TryGetValue(callback.EMsg, out func))
                return;

            func(callback.Message);
        }

        async void OnClientWelcome(IPacketGCMsg packetMsg)
        {
            // in order to get at the contents of the message, we need to create a ClientGCMsgProtobuf from the packet message we recieve
            // note here the difference between ClientGCMsgProtobuf and the ClientMsgProtobuf used when sending ClientGamesPlayed
            // this message is used for the GC, while the other is used for general steam messages
            var msg = new ClientGCMsgProtobuf<CMsgClientWelcome>(packetMsg);

            form.AddLog(String.Format("OnClientWelcome. Location: {0}", msg.Body.location.country));

            var storeGetUser = new ClientGCMsgProtobuf<CMsgStoreGetUserData>((uint)EGCItemMsg.k_EMsgGCStoreGetUserData);
            storeGetUser.Body.currency = 2;
            steamGameCoordinator.Send(storeGetUser, CSGO_APPID);

            var requestCoPlays = new ClientGCMsgProtobuf<CMsgGCCStrike15_v2_Account_RequestCoPlays>((uint)ECsgoGCMsg.k_EMsgGCCStrike15_v2_Account_RequestCoPlays);
            steamGameCoordinator.Send(requestCoPlays, CSGO_APPID);

            steamGameCoordinator.Send(storeGetUser, CSGO_APPID);

            form.ToggleConnected(true);
            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
        }

        async void OnMatchmakingClientHello(IPacketGCMsg packetMsg)
        {
            var msg = new ClientGCMsgProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>(packetMsg);

            if (accountId != 0 || msg == null || msg.Body == null || msg.Body.ranking == null || msg.Body.commendation == null)
            {
                await Task.Delay(1000).ConfigureAwait(false);
                return;
            }

            if (msg.Body.vac_banned == 1)
                form.AddLog(string.Format("Account '{0}' banned !", accountUsername));

            accountId = msg.Body.ranking.account_id;
            rankId = (RankName)msg.Body.ranking.rank_id;
            playerLvl = msg.Body.player_level;
        }
        #endregion

        public bool inLobby()
        {
            if (lobbyId == 0)
                return false;
            else
                return true;
        }

        public bool inMatch()
        {
            if (matchId == 0)
                return false;
            else
                return true;
        }

        public static void UpdateRichPresence()
        {
            var richUpload = new ClientMsgProtobuf<CMsgClientRichPresenceUpload>(EMsg.ClientRichPresenceUpload);
            richUpload.ProtoHeader.routing_appid = CSGO_APPID;
            richUpload.Header.Proto.routing_appid = CSGO_APPID;
            richUpload.Body.rich_presence_kv = new byte[] { 0x00, 0x52, 0x50, 0x00, 0x01, 0x73, 0x74, 0x61, 0x74, 0x75, 0x73, 0x00, 0x50, 0x6C, 0x61, 0x79, 0x69, 0x6E, 0x67, 0x20, 0x43, 0x53, 0x3A, 0x47, 0x4F, 0x00, 0x01, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x00, 0x31, 0x33, 0x36, 0x39, 0x33, 0x00, 0x01, 0x74, 0x69, 0x6D, 0x65, 0x00, 0x37, 0x34, 0x2E, 0x35, 0x36, 0x34, 0x30, 0x38, 0x36, 0x00, 0x01, 0x67, 0x61, 0x6D, 0x65, 0x3A, 0x73, 0x74, 0x61, 0x74, 0x65, 0x00, 0x6C, 0x6F, 0x62, 0x62, 0x79, 0x00, 0x01, 0x73, 0x74, 0x65, 0x61, 0x6D, 0x5F, 0x64, 0x69, 0x73, 0x70, 0x6C, 0x61, 0x79, 0x00, 0x23, 0x64, 0x69, 0x73, 0x70, 0x6C, 0x61, 0x79, 0x5F, 0x4C, 0x6F, 0x62, 0x62, 0x79, 0x00, 0x01, 0x63, 0x6F, 0x6E, 0x6E, 0x65, 0x63, 0x74, 0x00, 0x2B, 0x67, 0x63, 0x63, 0x6F, 0x6E, 0x6E, 0x65, 0x63, 0x74, 0x47, 0x33, 0x36, 0x41, 0x37, 0x39, 0x46, 0x41, 0x42, 0x00, 0x01, 0x67, 0x61, 0x6D, 0x65, 0x3A, 0x6D, 0x6F, 0x64, 0x65, 0x00, 0x63, 0x6F, 0x6D, 0x70, 0x65, 0x74, 0x69, 0x74, 0x69, 0x76, 0x65, 0x00, 0x08, 0x08 };
            //richUpload.Body.steamid_broadcast = list;
            steamClient.Send(richUpload);
        }

        public void CreateLobby(string _username)
        {
            var createRequest = new ClientMsgProtobuf<CMsgClientMMSCreateLobby>(EMsg.ClientMMSCreateLobby);
            createRequest.Header.Proto.routing_appid = CSGO_APPID;
            createRequest.ProtoHeader.routing_appid = CSGO_APPID;
            createRequest.Body.app_id = CSGO_APPID;
            createRequest.Body.max_members = 1;
            createRequest.Body.lobby_type = 1;
            createRequest.Body.lobby_flags = 1;
            createRequest.Body.cell_id = cellId;
            createRequest.Body.public_ip = publicIp;
            createRequest.Body.persona_name_owner = _username;
            steamClient.Send(createRequest);

            form.AddLog(string.Format("Creating lobby with username : {0}", _username));
        }

        public void JoinLobby(ulong _lobbyId, string _username)
        {
            if (inLobby())
                LeaveLobby();

            lobbyId = _lobbyId;

            StopMatchmaking();
            Testt();

            var joinRequest = new ClientMsgProtobuf<CMsgClientMMSJoinLobby>(EMsg.ClientMMSJoinLobby);
            joinRequest.ProtoHeader.routing_appid = CSGO_APPID;
            joinRequest.Header.Proto.routing_appid = CSGO_APPID;
            joinRequest.Body.app_id = CSGO_APPID;
            joinRequest.Body.steam_id_lobby = _lobbyId;
            joinRequest.Body.persona_name = _username;
            steamClient.Send(joinRequest);

            form.AddLog(string.Format("Joining lobbyId : {0} with username : {1}", _lobbyId, _username));
        }

        public void Testt()
        {
            var sendMessageRequest = new ClientMsgProtobuf<CMsgClientMMSSendLobbyChatMsg>(EMsg.ClientMMSSendLobbyChatMsg);
            sendMessageRequest.ProtoHeader.routing_appid = CSGO_APPID;
            sendMessageRequest.Header.Proto.routing_appid = CSGO_APPID;
            sendMessageRequest.Body.app_id = CSGO_APPID;
            sendMessageRequest.Body.steam_id_lobby = lobbyId;
            sendMessageRequest.Body.steam_id_target = 0;
            sendMessageRequest.Body.lobby_message = new byte[] { }; //function call //SysSession::Command; SysSession::OnMachineUpdated; SysSession::OnPlayerKicked; SysSession::OnPlayerRemoved; SysSession::OnPlayerUpdated; SysSession::OnUpdate; SysSession::Quit; SysSession::ReplyJoinData; SysSession::RequestJoinData; SysSession::TeamReservation; SysSession::TeamReservationResult; SysSession::Voice; SysSession::VoiceMutelist; SysSession::VoiceStatus; SysSessionData; etc
            steamClient.Send(sendMessageRequest);
        }

        public void LeaveLobby()
        {
            if (inLobby())
            {
                var leaveRequest = new ClientMsgProtobuf<CMsgClientMMSLeaveLobby>(EMsg.ClientMMSLeaveLobby);
                leaveRequest.ProtoHeader.routing_appid = CSGO_APPID;
                leaveRequest.Header.Proto.routing_appid = CSGO_APPID;
                leaveRequest.Body.app_id = CSGO_APPID;
                leaveRequest.Body.steam_id_lobby = lobbyId;
                steamClient.Send(leaveRequest);
            }
        }

        public void InviteToLobby(ulong _steamId)
        {
            if (inLobby())
            {
                var inviteRequest = new ClientMsgProtobuf<CMsgClientMMSInviteToLobby>(EMsg.ClientMMSInviteToLobby);
                inviteRequest.ProtoHeader.routing_appid = CSGO_APPID;
                inviteRequest.Header.Proto.routing_appid = CSGO_APPID;
                inviteRequest.Body.app_id = CSGO_APPID;
                inviteRequest.Body.steam_id_lobby = lobbyId;
                inviteRequest.Body.steam_id_user_invited = _steamId;
                steamClient.Send(inviteRequest);

                string log = string.Format("Invite steamId : {0} to lobbyId : {1}", _steamId, lobbyId);

                form.AddLog(log);
                form.AddMessage(log);
            }
        }

        public void SendLobbyMessage(string _messageToSend, string _steamPersonnalName)
        {
            if (inLobby())
            {
                var sendMessageRequest = new ClientMsgProtobuf<CMsgClientMMSSendLobbyChatMsg>(EMsg.ClientMMSSendLobbyChatMsg);

                string messageHex = (BitConverter.ToString(Encoding.UTF8.GetBytes(_messageToSend))).ToUpper().Replace("-", "");
                string usernameHex = (BitConverter.ToString(Encoding.Default.GetBytes(_steamPersonnalName))).ToUpper().Replace("-", "");

                string messagesend = "0000357D0053797353657373696F6E3A3A436F6D6D616E64000047616D653A3A43686174000172756E00616C6C000778756964000110000100092746016E616D6500" + usernameHex + "00016368617400" + messageHex + "000B0B0B"; ;

                sendMessageRequest.ProtoHeader.routing_appid = CSGO_APPID;
                sendMessageRequest.Header.Proto.routing_appid = CSGO_APPID;
                sendMessageRequest.Body.app_id = CSGO_APPID;
                sendMessageRequest.Body.steam_id_lobby = lobbyId;
                sendMessageRequest.Body.steam_id_target = 0;

                try
                {
                    byte[] checkedmessage = StringToByteArray(messagesend);
                    sendMessageRequest.Body.lobby_message = checkedmessage;
                    steamClient.Send(sendMessageRequest);
                }
                catch (Exception)
                {
                    messagesend = "0" + messagesend;
                    byte[] checkedmessage = StringToByteArray(messagesend);
                    sendMessageRequest.Body.lobby_message = checkedmessage;
                    steamClient.Send(sendMessageRequest);
                }
            }
        }

        public void SendLobbyErrorMessage(string _messageToSend, string _steamPersonnalName)
        {
            var sendMessageRequest = new ClientMsgProtobuf<CMsgClientMMSSendLobbyChatMsg>(EMsg.ClientMMSSendLobbyChatMsg);

            string messageHex = (BitConverter.ToString(Encoding.UTF8.GetBytes(_messageToSend))).ToUpper().Replace("-", "");
            string usernameHex = (BitConverter.ToString(Encoding.Default.GetBytes(_steamPersonnalName))).ToUpper().Replace("-", "");

            string messagesend = "0000357D0053797353657373696F6E3A3A436F6D6D616E64000047616D653A3A436861745265706F72744572726F72000172756E00616C6C000778756964000110000100092746016E616D6500" + usernameHex + "00016368617400" + messageHex + "000B0B0B"; ;

            sendMessageRequest.ProtoHeader.routing_appid = CSGO_APPID;
            sendMessageRequest.Header.Proto.routing_appid = CSGO_APPID;
            sendMessageRequest.Body.app_id = CSGO_APPID;
            sendMessageRequest.Body.steam_id_lobby = lobbyId;
            sendMessageRequest.Body.steam_id_target = 0;

            try
            {
                byte[] checkedmessage = StringToByteArray(messagesend);
                sendMessageRequest.Body.lobby_message = checkedmessage;
                steamClient.Send(sendMessageRequest);
            }
            catch (Exception)
            {
                messagesend = "0" + messagesend;
                byte[] checkedmessage = StringToByteArray(messagesend);
                sendMessageRequest.Body.lobby_message = checkedmessage;
                steamClient.Send(sendMessageRequest);
            }
        }

        public void SendLobbyInviteMessage(string _messageToSend, string _steamPersonnalName)
        {
            var sendMessageRequest = new ClientMsgProtobuf<CMsgClientMMSSendLobbyChatMsg>(EMsg.ClientMMSSendLobbyChatMsg);

            string messageHex = (BitConverter.ToString(Encoding.UTF8.GetBytes(_messageToSend))).ToUpper().Replace("-", "");
            string usernameHex = (BitConverter.ToString(Encoding.Default.GetBytes(_steamPersonnalName))).ToUpper().Replace("-", "");

            string messagesend = "0000357D0053797353657373696F6E3A3A436F6D6D616E64000047616D653A3A43686174496E766974654D657373616765000172756E00616C6C000778756964000110000100092746016E616D6500" + usernameHex + "07667269656E6400011000010000000101667269656E644E616D6500" + messageHex + "000B0B0B"; ;

            sendMessageRequest.ProtoHeader.routing_appid = CSGO_APPID;
            sendMessageRequest.Header.Proto.routing_appid = CSGO_APPID;
            sendMessageRequest.Body.app_id = CSGO_APPID;
            sendMessageRequest.Body.steam_id_lobby = lobbyId;
            sendMessageRequest.Body.steam_id_target = 0;

            try
            {
                byte[] checkedmessage = StringToByteArray(messagesend);
                sendMessageRequest.Body.lobby_message = checkedmessage;
                steamClient.Send(sendMessageRequest);
            }
            catch (Exception)
            {
                messagesend = "0" + messagesend;
                byte[] checkedmessage = StringToByteArray(messagesend);
                sendMessageRequest.Body.lobby_message = checkedmessage;
                steamClient.Send(sendMessageRequest);
            }
        }

        void StopMatchmaking()
        {
            var abandonMsg = new ClientGCMsgProtobuf<CMsgGCCStrike15_v2_MatchmakingStop>(
                (uint)ECsgoGCMsg.k_EMsgGCCStrike15_v2_MatchmakingStop
            );
            abandonMsg.Body.abandon = 0;
            steamGameCoordinator.Send(abandonMsg, CSGO_APPID);
        }

        void AbandonCompetitiveGame()
        {
            var abandonMsg = new ClientGCMsgProtobuf<CMsgGCCStrike15_v2_MatchmakingStop>(
                (uint)ECsgoGCMsg.k_EMsgGCCStrike15_v2_MatchmakingStop
            );
            abandonMsg.Body.abandon = 13563;
            steamGameCoordinator.Send(abandonMsg, CSGO_APPID);
        }
    }
}