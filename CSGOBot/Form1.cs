using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGOBot
{
    public partial class Form1 : Form
    {
        private static CSGOClient Client;

        private string dirData = "data";
        private string dirDataSentry = "\\sentry";
        private string dirDataDebug = "\\debug";

        public Form1()
        {
            InitializeComponent();
            ToggleAll(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(dirData);
            Directory.CreateDirectory(dirData + dirDataSentry);
            Directory.CreateDirectory(dirData + dirDataDebug);

            Utils.DeletingFiles(dirData + dirDataDebug + "\\");

            Thread.Sleep(500);

            Client = new CSGOClient(this, "USERNAME", "PASSWORD");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.Disconnect();
        }

        private delegate void SafeToggleAll(bool enable);
        public void ToggleAll(bool enable)
        {
            if (textBoxLobbyLink.InvokeRequired)
            {
                var d = new SafeToggleAll(ToggleConnected);
                Invoke(d, new object[] { enable });
            }
            else
            {
                textBoxLobbyLink.Enabled = enable;
                buttonJoinLobby.Enabled = enable;
                buttonCreateLobby.Enabled = enable;
                buttonLeaveLobby.Enabled = enable;
                comboBoxPlayerRank.Enabled = enable;
                textBoxPlayerLevel.Enabled = enable;
                textBoxPlayerName.Enabled = enable;
                textBoxPlayerWins.Enabled = enable;
                buttonChangeData.Enabled = enable;
                buttonGetAllLobby.Enabled = enable;
                buttonInviteAllToLobby.Enabled = enable;
                buttonCrashLobby.Enabled = enable;
                buttonNukeLobby.Enabled = enable;
                buttonStartGame.Enabled = enable;
                groupBoxLobbyChat.Enabled = enable;
                textBoxLobbyMessage.Enabled = enable;
                buttonSendLobbyMessage.Enabled = enable;
                buttonSpamLobbyMessage.Enabled = enable;
                listBoxLobbyPlayers.Enabled = enable;
                buttonLobbyKickPlayer.Enabled = enable;
                buttonLobbyRankPlayer.Enabled = enable;
                buttonLobbyLvlPlayer.Enabled = enable;
                textBoxSteamIdInvite.Enabled = enable;
                buttonInviteToLobby.Enabled = enable;
                buttonStop.Enabled = enable;
            }
        }

        private delegate void SafeToggleConnected(bool enable);
        public void ToggleConnected(bool enable)
        {
            if (textBoxLobbyLink.InvokeRequired)
            {
                var d = new SafeToggleConnected(ToggleConnected);
                Invoke(d, new object[] { enable });
            }
            else
            {
                textBoxLobbyLink.Enabled = enable;
                buttonJoinLobby.Enabled = enable;
                buttonCreateLobby.Enabled = enable;
                buttonLeaveLobby.Enabled = enable;
                buttonGetAllLobby.Enabled = enable;
                comboBoxPlayerRank.Enabled = enable;
                textBoxPlayerLevel.Enabled = enable;
                textBoxPlayerName.Enabled = enable;
                textBoxPlayerWins.Enabled = enable;
                buttonChangeData.Enabled = enable;
                buttonStop.Enabled = enable;
            }
            
        }

        private delegate void SafeToggleLobby(bool enable);
        public void ToggleLobby(bool enable)
        {
            if (buttonInviteAllToLobby.InvokeRequired)
            {
                var d = new SafeToggleLobby(ToggleLobby);
                Invoke(d, new object[] { enable });
            }
            else
            {
                buttonLeaveLobby.Enabled = enable;
                buttonGetAllLobby.Enabled = enable;
                buttonInviteAllToLobby.Enabled = enable;
                buttonCrashLobby.Enabled = enable;
                buttonNukeLobby.Enabled = enable;
                buttonStartGame.Enabled = enable;
                groupBoxLobbyChat.Enabled = enable;
                textBoxLobbyMessage.Enabled = enable;
                buttonSendLobbyMessage.Enabled = enable;
                buttonSpamLobbyMessage.Enabled = enable;
                listBoxLobbyPlayers.Enabled = enable;
                buttonLobbyKickPlayer.Enabled = enable;
                buttonLobbyRankPlayer.Enabled = enable;
                buttonLobbyLvlPlayer.Enabled = enable;
                textBoxSteamIdInvite.Enabled = enable;
                buttonInviteToLobby.Enabled = enable;
            }
            
        }

        private delegate void SafeAddLog(string log);
        public void AddLog(string log)
        {
            if (listBoxAppLogs.InvokeRequired)
            {
                var d = new SafeAddLog(AddLog);
                Invoke(d, new object[] { log });
            }
            else
            {
                if (listBoxAppLogs.Items.Contains("NO LOG.."))
                    listBoxAppLogs.Items.Clear();

                listBoxAppLogs.Items.Add(log);
                Console.WriteLine(log);
            }
        }

        private delegate void SafeAddMessage(string message);
        public void AddMessage(string message)
        {
            if (listBoxLobbyMessages.InvokeRequired)
            {
                var d = new SafeAddMessage(AddMessage);
                Invoke(d, new object[] { message });
            }
            else
            {
                if (listBoxLobbyMessages.Items.Contains("NO MESSAGE... CONNECT TO LOBBY OR CREATE LOBBY TO VIEW NEW MESSAGES..."))
                    listBoxLobbyMessages.Items.Clear();

                listBoxLobbyMessages.Items.Add(message);
            }
        }

        private delegate void SafeDeleteMessage();
        public void DeleteMessage()
        {
            if (listBoxLobbyMessages.InvokeRequired)
            {
                var d = new SafeDeleteMessage(DeleteMessage);
                Invoke(d);
            }
            else
            {
                listBoxLobbyMessages.Items.Clear();
                listBoxLobbyMessages.Items.Add("NO MESSAGE... CONNECT TO LOBBY OR CREATE LOBBY TO VIEW NEW MESSAGES...");
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Task.Run(() => Client.Connect());
            //Client.Connect();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Client.Disconnect();
        }

        private void buttonCreateLobby_Click(object sender, EventArgs e)
        {
            Client.CreateLobby(textBoxPlayerName.Text);
        }

        private void buttonLeaveLobby_Click(object sender, EventArgs e)
        {
            Client.LeaveLobby();
        }

        private void buttonJoinLobby_Click(object sender, EventArgs e)
        {
            Client.JoinLobby(Convert.ToUInt64(textBoxLobbyLink.Text), textBoxPlayerName.Text);
        }

        private void buttonInviteToLobby_Click(object sender, EventArgs e)
        {
            Client.InviteToLobby(Convert.ToUInt64(textBoxSteamIdInvite.Text));
        }

        private void buttonSendLobbyMessage_Click(object sender, EventArgs e)
        {
            Client.SendLobbyMessage(textBoxLobbyMessage.Text, textBoxPlayerName.Text);
        }

        private void buttonLobbyInject_Click(object sender, EventArgs e)
        {
            Client.Testt();
        }
    }
}