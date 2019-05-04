using Gameloop.Vdf;
using SteamKit2;
using SteamKit2.GC;
using SteamKit2.GC.CSGO.Internal;
using SteamKit2.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGOBot
{
    class Handler : ClientMsgHandler
    {
        Random rnd = new Random();
        Form1 form;

        public class MyCallback : CallbackMsg
        {
            public EResult Result { get; private set; }

            internal MyCallback(EResult res)
            {
                Result = res;
            }
        }

        public Handler(Form1 formI)
        {
            form = formI;
        }

        public override void HandleMsg(IPacketMsg packetMsg)
        {
            Console.WriteLine(string.Format("{0}", packetMsg.MsgType));

            switch (packetMsg.MsgType)
            {
                case EMsg.ClientLogOnResponse:
                    HandleLogonResponse(packetMsg);
                    break;
                case EMsg.ClientFromGC:
                    HandleClientFromGC(packetMsg);
                    break;
                case EMsg.ClientMMSCreateLobbyResponse:
                    HandleCreateLobbyResponse(packetMsg);
                    break;
                case EMsg.ClientMMSJoinLobbyResponse:
                    HandleJoinLobbyResponse(packetMsg);
                    break;
                case EMsg.ClientMMSLeaveLobbyResponse:
                    HandleLeaveLobbyResponse(packetMsg);
                    break;
                case EMsg.ClientMMSUserJoinedLobby:
                    HandleUserJoinedLobby(packetMsg);
                    break;
                case EMsg.ClientMMSUserLeftLobby:
                    HandleUserLeftLobby(packetMsg);
                    break;
                case EMsg.ClientMMSLobbyChatMsg:
                    HandleLobbyChatMsg(packetMsg);
                    break;
            }
        }

        private void HandleClientFromGC(IPacketMsg packetMsg)
        {
            form.AddLog(string.Format("HandleClientFromGC code : {0}", packetMsg.MsgType));
        }

        private void HandleCreateLobbyResponse(IPacketMsg packetMsg)
        {
            var createResponse = new ClientMsgProtobuf<CMsgClientMMSCreateLobbyResponse>(packetMsg);

            EResult result = (EResult)createResponse.Body.eresult;

            if(result == EResult.OK)
            {
                ulong lobbyId = createResponse.Body.steam_id_lobby;
                CSGOClient.lobbyId = lobbyId;

                SetLobbyData(0, 1, 1, 1);
                SetLobbyData(0, 1, 2, 0);
                SetLobbyData(0, 1, 2, 0);
                SetLobbyOwner(CSGOClient.steamId);
                SetLobbyData(0, 10, 2, 0);
                SetLobbyData(0, 10, 2, 0);
                SetLobbyData(0, 10, 2, 0);

                RegisterLobbyParty();
                CSGOClient.UpdateRichPresence();
                //ClientGamesPlayedWithDataBlob(3);

                string log = string.Format("Lobby created with lobbyId : {0}", lobbyId);

                form.ToggleLobby(true);
                form.AddMessage(log);
                form.AddLog(log);
            }
        }

        private void SetLobbyData(ulong steamIdMember, int maxMembers, int lobbyType, int lobbyFlags)
        {
            var setLobbyDataRequest = new ClientMsgProtobuf<CMsgClientMMSSetLobbyData>(EMsg.ClientMMSSetLobbyData);
            setLobbyDataRequest.Header.Proto.routing_appid = CSGOClient.CSGO_APPID;
            setLobbyDataRequest.ProtoHeader.routing_appid = CSGOClient.CSGO_APPID;
            setLobbyDataRequest.Body.app_id = CSGOClient.CSGO_APPID;
            setLobbyDataRequest.Body.steam_id_lobby = CSGOClient.lobbyId;
            setLobbyDataRequest.Body.steam_id_member = steamIdMember;
            setLobbyDataRequest.Body.max_members = maxMembers;
            setLobbyDataRequest.Body.lobby_type = lobbyType;
            setLobbyDataRequest.Body.lobby_flags = lobbyFlags;
            setLobbyDataRequest.Body.metadata = File.ReadAllBytes(@"data/metadata_with_maps.bin");
            Client.Send(setLobbyDataRequest);
        }

        private void SetLobbyOwner(ulong steamIdNewOwner)
        {
            var setLobbyOwnerRequest = new ClientMsgProtobuf<CMsgClientMMSSetLobbyOwner>(EMsg.ClientMMSSetLobbyOwner);
            setLobbyOwnerRequest.Header.Proto.routing_appid = CSGOClient.CSGO_APPID;
            setLobbyOwnerRequest.ProtoHeader.routing_appid = CSGOClient.CSGO_APPID;
            setLobbyOwnerRequest.Body.app_id = CSGOClient.CSGO_APPID;
            setLobbyOwnerRequest.Body.steam_id_lobby = CSGOClient.lobbyId;
            setLobbyOwnerRequest.Body.steam_id_new_owner = steamIdNewOwner;
            Client.Send(setLobbyOwnerRequest);
        }

        private void RegisterLobbyParty()
        {
            var registerLobbyRequest = new ClientGCMsgProtobuf<CMsgGCCStrike15_v2_Party_Register>((uint)ECsgoGCMsg.k_EMsgGCCStrike15_v2_Party_Register);
            registerLobbyRequest.Body.id = Convert.ToUInt32(CSGOClient.lobbyId - 109775240917155840);
            registerLobbyRequest.Body.ver = CSGOClient.CSGO_PVERSION;
            registerLobbyRequest.Body.apr = 1;
            registerLobbyRequest.Body.ark = 110;
            registerLobbyRequest.Body.nby = 1;
            registerLobbyRequest.Body.slots = 4;
            registerLobbyRequest.Body.launcher = 0;
            registerLobbyRequest.Body.game_type = 33800;
            CSGOClient.steamGameCoordinator.Send(registerLobbyRequest, CSGOClient.CSGO_APPID);
        }

        private void ClientGamesPlayedWithDataBlob(uint gameFlags)
        {
            var changeDataBlobRequest = new ClientMsgProtobuf<CMsgClientGamesPlayed>(EMsg.ClientGamesPlayedWithDataBlob);
            changeDataBlobRequest.Body.games_played.Add(new CMsgClientGamesPlayed.GamePlayed
            {
                steam_id_gs = 0,
                game_id = new GameID(CSGOClient.CSGO_APPID),
                game_ip_address = 0,
                game_port = 0,
                is_secure = true,
                game_extra_info = "",
                game_data_blob = Utils.StringToByteArray("test"),
                process_id = 5364,
                streaming_provider_id = 0,
                game_flags = gameFlags,
                owner_id = Convert.ToUInt32(CSGOClient.steamId - 76561197960265728),
                launch_option_type = 0,
                launch_source = 0
            });
            changeDataBlobRequest.Body.client_os_type = 16;
        }

        private void HandleJoinLobbyResponse(IPacketMsg packetMsg)
        {
            var joinResponse = new ClientMsgProtobuf<CMsgClientMMSJoinLobbyResponse>(packetMsg);

            ulong lobbyId = joinResponse.Body.steam_id_lobby;
            int joiningResult = joinResponse.Body.chat_room_enter_response;

            string log = string.Format("Lobby joined with lobbyId : {0}", lobbyId);

            if (joiningResult == 1)
            {
                CSGOClient.lobbyId = lobbyId;

                Testt2();
            }else if(joiningResult == 2)
            {
                CSGOClient.lobbyId = 0;
                log = string.Format("Failed Lobby joined with lobbyId : {0}", lobbyId);
            }
           

            form.ToggleLobby(true);
            form.AddMessage(log);
            form.AddLog(log);
           
        }

        public void Testt2()
        {
            var sendMessageRequest = new ClientMsgProtobuf<CMsgClientMMSSendLobbyChatMsg>(EMsg.ClientMMSSendLobbyChatMsg);
            sendMessageRequest.ProtoHeader.routing_appid = CSGOClient.CSGO_APPID;
            sendMessageRequest.Header.Proto.routing_appid = CSGOClient.CSGO_APPID;
            sendMessageRequest.Body.app_id = CSGOClient.CSGO_APPID;
            sendMessageRequest.Body.steam_id_lobby = CSGOClient.lobbyId;
            sendMessageRequest.Body.steam_id_target = 0;
            sendMessageRequest.Body.lobby_message = File.ReadAllBytes(@"data/message_joining.bin");
            Client.Send(sendMessageRequest);
        }

        private void HandleLobbyChatMsg(IPacketMsg packetMsg)
        {
            var joinResponse = new ClientMsgProtobuf<CMsgClientMMSLobbyChatMsg>(packetMsg);

            ulong steamIdSender = joinResponse.Body.steam_id_sender;
            byte[] message = joinResponse.Body.lobby_message;

            Console.WriteLine(string.Format("LobbyChatMsg sended by steamId : {0}", steamIdSender));
            Console.WriteLine(string.Format("Message Encoded : {0}", BitConverter.ToString(message).Replace("-", "")));
            Console.WriteLine(string.Format("Message Decoded : {0}", Encoding.UTF8.GetString(message, 0, message.Length)));
        }

        private void HandleLeaveLobbyResponse(IPacketMsg packetMsg)
        {
            var leaveResponse = new ClientMsgProtobuf<CMsgClientMMSJoinLobbyResponse>(packetMsg);

            form.AddLog(string.Format("Lobby left with lobbyId : {0}", CSGOClient.lobbyId));

            CSGOClient.lobbyId = 0;
            form.ToggleLobby(false);
            form.DeleteMessage();
        }

        private void HandleUserJoinedLobby(IPacketMsg packetMsg)
        {
            var joinedResponse = new ClientMsgProtobuf<CMsgClientMMSUserJoinedLobby>(packetMsg);

            ulong steamIdUserJoined = joinedResponse.Body.steam_id_user;
            string usernameUserJoined = joinedResponse.Body.persona_name;

            string log = string.Format("User '{0}' #{1} joined the lobby!", usernameUserJoined, steamIdUserJoined);

            form.AddMessage(log);
            form.AddLog(log);
        }

        private void HandleUserLeftLobby(IPacketMsg packetMsg)
        {
            var leftResponse = new ClientMsgProtobuf<CMsgClientMMSUserLeftLobby>(packetMsg);

            ulong steamIdUserJoined = leftResponse.Body.steam_id_user;

            string log = string.Format("User #{0} left the lobby!", steamIdUserJoined);

            form.AddMessage(log);
            form.AddLog(log);
        }

        void HandleLogonResponse(IPacketMsg packetMsg)
        {
            // in order to get at the message contents, we need to wrap the packet message
            // in an object that gives us access to the message body
            var logonResponse = new ClientMsgProtobuf<CMsgClientLogonResponse>(packetMsg);

            // the raw body of the message often doesn't make use of useful types, so we need to
            // cast them to types that are prettier for the user to handle
            EResult result = (EResult)logonResponse.Body.eresult;
            CSGOClient.cellId = logonResponse.Body.cell_id;
            CSGOClient.publicIp = logonResponse.Body.public_ip;
            CSGOClient.steamId = logonResponse.Body.client_supplied_steamid;

            // post the callback to be consumed by user code
            Client.PostCallback(new MyCallback(result));
        }
    }
}