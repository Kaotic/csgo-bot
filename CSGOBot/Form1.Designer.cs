namespace CSGOBot
{
    public partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBoxAccounts = new System.Windows.Forms.ListBox();
            this.buttonReloadAccounts = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxLobbyLink = new System.Windows.Forms.TextBox();
            this.buttonJoinLobby = new System.Windows.Forms.Button();
            this.listBoxAppLogs = new System.Windows.Forms.ListBox();
            this.listBoxLobbyMessages = new System.Windows.Forms.ListBox();
            this.textBoxLobbyMessage = new System.Windows.Forms.TextBox();
            this.buttonSendLobbyMessage = new System.Windows.Forms.Button();
            this.groupBoxLobbyChat = new System.Windows.Forms.GroupBox();
            this.buttonSpamLobbyMessage = new System.Windows.Forms.Button();
            this.groupBoxLobbyPlayers = new System.Windows.Forms.GroupBox();
            this.buttonLobbyLvlPlayer = new System.Windows.Forms.Button();
            this.buttonLobbyRankPlayer = new System.Windows.Forms.Button();
            this.buttonLobbyKickPlayer = new System.Windows.Forms.Button();
            this.listBoxLobbyPlayers = new System.Windows.Forms.ListBox();
            this.groupBoxLobbyEvents = new System.Windows.Forms.GroupBox();
            this.buttonLeaveLobby = new System.Windows.Forms.Button();
            this.buttonCreateLobby = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.buttonNukeLobby = new System.Windows.Forms.Button();
            this.buttonCrashLobby = new System.Windows.Forms.Button();
            this.buttonInviteAllToLobby = new System.Windows.Forms.Button();
            this.buttonGetAllLobby = new System.Windows.Forms.Button();
            this.groupBoxPlayerInformations = new System.Windows.Forms.GroupBox();
            this.buttonChangeData = new System.Windows.Forms.Button();
            this.labelNOTMODIFIABLE4 = new System.Windows.Forms.Label();
            this.textBoxPlayerWins = new System.Windows.Forms.TextBox();
            this.labelNOTMODIFIABLE3 = new System.Windows.Forms.Label();
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.labelNOTMODIFIABLE2 = new System.Windows.Forms.Label();
            this.labelNOTMODIFIABLE1 = new System.Windows.Forms.Label();
            this.textBoxPlayerLevel = new System.Windows.Forms.TextBox();
            this.comboBoxPlayerRank = new System.Windows.Forms.ComboBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonInviteToLobby = new System.Windows.Forms.Button();
            this.groupBoxInvitePlayer = new System.Windows.Forms.GroupBox();
            this.labelSteamId = new System.Windows.Forms.Label();
            this.textBoxSteamIdInvite = new System.Windows.Forms.TextBox();
            this.buttonLobbyInject = new System.Windows.Forms.Button();
            this.groupBoxLobbyChat.SuspendLayout();
            this.groupBoxLobbyPlayers.SuspendLayout();
            this.groupBoxLobbyEvents.SuspendLayout();
            this.groupBoxPlayerInformations.SuspendLayout();
            this.groupBoxInvitePlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxAccounts
            // 
            this.listBoxAccounts.FormattingEnabled = true;
            this.listBoxAccounts.Items.AddRange(new object[] {
            "NO ACCOUNT..."});
            this.listBoxAccounts.Location = new System.Drawing.Point(12, 12);
            this.listBoxAccounts.Name = "listBoxAccounts";
            this.listBoxAccounts.Size = new System.Drawing.Size(120, 329);
            this.listBoxAccounts.TabIndex = 0;
            // 
            // buttonReloadAccounts
            // 
            this.buttonReloadAccounts.Location = new System.Drawing.Point(12, 349);
            this.buttonReloadAccounts.Name = "buttonReloadAccounts";
            this.buttonReloadAccounts.Size = new System.Drawing.Size(120, 23);
            this.buttonReloadAccounts.TabIndex = 1;
            this.buttonReloadAccounts.Text = "Reload Accounts";
            this.buttonReloadAccounts.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 378);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(120, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "START";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxLobbyLink
            // 
            this.textBoxLobbyLink.Location = new System.Drawing.Point(138, 12);
            this.textBoxLobbyLink.Name = "textBoxLobbyLink";
            this.textBoxLobbyLink.Size = new System.Drawing.Size(734, 20);
            this.textBoxLobbyLink.TabIndex = 3;
            this.textBoxLobbyLink.Text = "lobbyId";
            // 
            // buttonJoinLobby
            // 
            this.buttonJoinLobby.Location = new System.Drawing.Point(878, 10);
            this.buttonJoinLobby.Name = "buttonJoinLobby";
            this.buttonJoinLobby.Size = new System.Drawing.Size(75, 23);
            this.buttonJoinLobby.TabIndex = 4;
            this.buttonJoinLobby.Text = "Connect";
            this.buttonJoinLobby.UseVisualStyleBackColor = true;
            this.buttonJoinLobby.Click += new System.EventHandler(this.buttonJoinLobby_Click);
            // 
            // listBoxAppLogs
            // 
            this.listBoxAppLogs.FormattingEnabled = true;
            this.listBoxAppLogs.Items.AddRange(new object[] {
            "NO LOG.."});
            this.listBoxAppLogs.Location = new System.Drawing.Point(959, 10);
            this.listBoxAppLogs.Name = "listBoxAppLogs";
            this.listBoxAppLogs.Size = new System.Drawing.Size(619, 420);
            this.listBoxAppLogs.TabIndex = 5;
            // 
            // listBoxLobbyMessages
            // 
            this.listBoxLobbyMessages.FormattingEnabled = true;
            this.listBoxLobbyMessages.Items.AddRange(new object[] {
            "NO MESSAGE... CONNECT TO LOBBY OR CREATE LOBBY TO VIEW NEW MESSAGES..."});
            this.listBoxLobbyMessages.Location = new System.Drawing.Point(6, 19);
            this.listBoxLobbyMessages.Name = "listBoxLobbyMessages";
            this.listBoxLobbyMessages.Size = new System.Drawing.Size(615, 199);
            this.listBoxLobbyMessages.TabIndex = 6;
            // 
            // textBoxLobbyMessage
            // 
            this.textBoxLobbyMessage.Location = new System.Drawing.Point(6, 223);
            this.textBoxLobbyMessage.Name = "textBoxLobbyMessage";
            this.textBoxLobbyMessage.Size = new System.Drawing.Size(523, 20);
            this.textBoxLobbyMessage.TabIndex = 7;
            // 
            // buttonSendLobbyMessage
            // 
            this.buttonSendLobbyMessage.Location = new System.Drawing.Point(532, 221);
            this.buttonSendLobbyMessage.Name = "buttonSendLobbyMessage";
            this.buttonSendLobbyMessage.Size = new System.Drawing.Size(44, 23);
            this.buttonSendLobbyMessage.TabIndex = 8;
            this.buttonSendLobbyMessage.Text = "Send";
            this.buttonSendLobbyMessage.UseVisualStyleBackColor = true;
            this.buttonSendLobbyMessage.Click += new System.EventHandler(this.buttonSendLobbyMessage_Click);
            // 
            // groupBoxLobbyChat
            // 
            this.groupBoxLobbyChat.Controls.Add(this.buttonSpamLobbyMessage);
            this.groupBoxLobbyChat.Controls.Add(this.listBoxLobbyMessages);
            this.groupBoxLobbyChat.Controls.Add(this.buttonSendLobbyMessage);
            this.groupBoxLobbyChat.Controls.Add(this.textBoxLobbyMessage);
            this.groupBoxLobbyChat.Location = new System.Drawing.Point(328, 150);
            this.groupBoxLobbyChat.Name = "groupBoxLobbyChat";
            this.groupBoxLobbyChat.Size = new System.Drawing.Size(625, 251);
            this.groupBoxLobbyChat.TabIndex = 9;
            this.groupBoxLobbyChat.TabStop = false;
            this.groupBoxLobbyChat.Text = "Lobby Chat :";
            // 
            // buttonSpamLobbyMessage
            // 
            this.buttonSpamLobbyMessage.Location = new System.Drawing.Point(577, 221);
            this.buttonSpamLobbyMessage.Name = "buttonSpamLobbyMessage";
            this.buttonSpamLobbyMessage.Size = new System.Drawing.Size(44, 23);
            this.buttonSpamLobbyMessage.TabIndex = 9;
            this.buttonSpamLobbyMessage.Text = "Spam";
            this.buttonSpamLobbyMessage.UseVisualStyleBackColor = true;
            // 
            // groupBoxLobbyPlayers
            // 
            this.groupBoxLobbyPlayers.Controls.Add(this.buttonLobbyLvlPlayer);
            this.groupBoxLobbyPlayers.Controls.Add(this.buttonLobbyRankPlayer);
            this.groupBoxLobbyPlayers.Controls.Add(this.buttonLobbyKickPlayer);
            this.groupBoxLobbyPlayers.Controls.Add(this.listBoxLobbyPlayers);
            this.groupBoxLobbyPlayers.Location = new System.Drawing.Point(584, 39);
            this.groupBoxLobbyPlayers.Name = "groupBoxLobbyPlayers";
            this.groupBoxLobbyPlayers.Size = new System.Drawing.Size(369, 105);
            this.groupBoxLobbyPlayers.TabIndex = 10;
            this.groupBoxLobbyPlayers.TabStop = false;
            this.groupBoxLobbyPlayers.Text = "Lobby Players :";
            // 
            // buttonLobbyLvlPlayer
            // 
            this.buttonLobbyLvlPlayer.Location = new System.Drawing.Point(304, 75);
            this.buttonLobbyLvlPlayer.Name = "buttonLobbyLvlPlayer";
            this.buttonLobbyLvlPlayer.Size = new System.Drawing.Size(59, 23);
            this.buttonLobbyLvlPlayer.TabIndex = 11;
            this.buttonLobbyLvlPlayer.Text = "LVL";
            this.buttonLobbyLvlPlayer.UseVisualStyleBackColor = true;
            // 
            // buttonLobbyRankPlayer
            // 
            this.buttonLobbyRankPlayer.Location = new System.Drawing.Point(304, 46);
            this.buttonLobbyRankPlayer.Name = "buttonLobbyRankPlayer";
            this.buttonLobbyRankPlayer.Size = new System.Drawing.Size(59, 23);
            this.buttonLobbyRankPlayer.TabIndex = 10;
            this.buttonLobbyRankPlayer.Text = "RANK";
            this.buttonLobbyRankPlayer.UseVisualStyleBackColor = true;
            // 
            // buttonLobbyKickPlayer
            // 
            this.buttonLobbyKickPlayer.Location = new System.Drawing.Point(304, 17);
            this.buttonLobbyKickPlayer.Name = "buttonLobbyKickPlayer";
            this.buttonLobbyKickPlayer.Size = new System.Drawing.Size(59, 23);
            this.buttonLobbyKickPlayer.TabIndex = 9;
            this.buttonLobbyKickPlayer.Text = "KICK";
            this.buttonLobbyKickPlayer.UseVisualStyleBackColor = true;
            // 
            // listBoxLobbyPlayers
            // 
            this.listBoxLobbyPlayers.FormattingEnabled = true;
            this.listBoxLobbyPlayers.Items.AddRange(new object[] {
            "NO PLAYERS.."});
            this.listBoxLobbyPlayers.Location = new System.Drawing.Point(6, 17);
            this.listBoxLobbyPlayers.Name = "listBoxLobbyPlayers";
            this.listBoxLobbyPlayers.Size = new System.Drawing.Size(292, 82);
            this.listBoxLobbyPlayers.TabIndex = 0;
            // 
            // groupBoxLobbyEvents
            // 
            this.groupBoxLobbyEvents.Controls.Add(this.buttonLobbyInject);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonLeaveLobby);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonCreateLobby);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonStartGame);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonNukeLobby);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonCrashLobby);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonInviteAllToLobby);
            this.groupBoxLobbyEvents.Controls.Add(this.buttonGetAllLobby);
            this.groupBoxLobbyEvents.Location = new System.Drawing.Point(138, 39);
            this.groupBoxLobbyEvents.Name = "groupBoxLobbyEvents";
            this.groupBoxLobbyEvents.Size = new System.Drawing.Size(440, 106);
            this.groupBoxLobbyEvents.TabIndex = 11;
            this.groupBoxLobbyEvents.TabStop = false;
            this.groupBoxLobbyEvents.Text = "Lobby Events :";
            // 
            // buttonLeaveLobby
            // 
            this.buttonLeaveLobby.Location = new System.Drawing.Point(6, 46);
            this.buttonLeaveLobby.Name = "buttonLeaveLobby";
            this.buttonLeaveLobby.Size = new System.Drawing.Size(87, 23);
            this.buttonLeaveLobby.TabIndex = 6;
            this.buttonLeaveLobby.Text = "Leave Lobby";
            this.buttonLeaveLobby.UseVisualStyleBackColor = true;
            this.buttonLeaveLobby.Click += new System.EventHandler(this.buttonLeaveLobby_Click);
            // 
            // buttonCreateLobby
            // 
            this.buttonCreateLobby.Location = new System.Drawing.Point(6, 17);
            this.buttonCreateLobby.Name = "buttonCreateLobby";
            this.buttonCreateLobby.Size = new System.Drawing.Size(87, 23);
            this.buttonCreateLobby.TabIndex = 5;
            this.buttonCreateLobby.Text = "Create Lobby";
            this.buttonCreateLobby.UseVisualStyleBackColor = true;
            this.buttonCreateLobby.Click += new System.EventHandler(this.buttonCreateLobby_Click);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(347, 75);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(87, 23);
            this.buttonStartGame.TabIndex = 4;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            // 
            // buttonNukeLobby
            // 
            this.buttonNukeLobby.Location = new System.Drawing.Point(115, 75);
            this.buttonNukeLobby.Name = "buttonNukeLobby";
            this.buttonNukeLobby.Size = new System.Drawing.Size(87, 23);
            this.buttonNukeLobby.TabIndex = 3;
            this.buttonNukeLobby.Text = "Nuke Lobby";
            this.buttonNukeLobby.UseVisualStyleBackColor = true;
            // 
            // buttonCrashLobby
            // 
            this.buttonCrashLobby.Location = new System.Drawing.Point(208, 75);
            this.buttonCrashLobby.Name = "buttonCrashLobby";
            this.buttonCrashLobby.Size = new System.Drawing.Size(87, 23);
            this.buttonCrashLobby.TabIndex = 2;
            this.buttonCrashLobby.Text = "Crash Lobby";
            this.buttonCrashLobby.UseVisualStyleBackColor = true;
            // 
            // buttonInviteAllToLobby
            // 
            this.buttonInviteAllToLobby.Location = new System.Drawing.Point(192, 17);
            this.buttonInviteAllToLobby.Name = "buttonInviteAllToLobby";
            this.buttonInviteAllToLobby.Size = new System.Drawing.Size(103, 23);
            this.buttonInviteAllToLobby.TabIndex = 1;
            this.buttonInviteAllToLobby.Text = "Invite All to Lobby";
            this.buttonInviteAllToLobby.UseVisualStyleBackColor = true;
            // 
            // buttonGetAllLobby
            // 
            this.buttonGetAllLobby.Location = new System.Drawing.Point(99, 17);
            this.buttonGetAllLobby.Name = "buttonGetAllLobby";
            this.buttonGetAllLobby.Size = new System.Drawing.Size(87, 23);
            this.buttonGetAllLobby.TabIndex = 0;
            this.buttonGetAllLobby.Text = "Get All Lobby";
            this.buttonGetAllLobby.UseVisualStyleBackColor = true;
            // 
            // groupBoxPlayerInformations
            // 
            this.groupBoxPlayerInformations.Controls.Add(this.buttonChangeData);
            this.groupBoxPlayerInformations.Controls.Add(this.labelNOTMODIFIABLE4);
            this.groupBoxPlayerInformations.Controls.Add(this.textBoxPlayerWins);
            this.groupBoxPlayerInformations.Controls.Add(this.labelNOTMODIFIABLE3);
            this.groupBoxPlayerInformations.Controls.Add(this.textBoxPlayerName);
            this.groupBoxPlayerInformations.Controls.Add(this.labelNOTMODIFIABLE2);
            this.groupBoxPlayerInformations.Controls.Add(this.labelNOTMODIFIABLE1);
            this.groupBoxPlayerInformations.Controls.Add(this.textBoxPlayerLevel);
            this.groupBoxPlayerInformations.Controls.Add(this.comboBoxPlayerRank);
            this.groupBoxPlayerInformations.Location = new System.Drawing.Point(138, 150);
            this.groupBoxPlayerInformations.Name = "groupBoxPlayerInformations";
            this.groupBoxPlayerInformations.Size = new System.Drawing.Size(184, 152);
            this.groupBoxPlayerInformations.TabIndex = 12;
            this.groupBoxPlayerInformations.TabStop = false;
            this.groupBoxPlayerInformations.Text = "Player Informations";
            // 
            // buttonChangeData
            // 
            this.buttonChangeData.Location = new System.Drawing.Point(6, 124);
            this.buttonChangeData.Name = "buttonChangeData";
            this.buttonChangeData.Size = new System.Drawing.Size(172, 23);
            this.buttonChangeData.TabIndex = 8;
            this.buttonChangeData.Text = "Change Data for me";
            this.buttonChangeData.UseVisualStyleBackColor = true;
            // 
            // labelNOTMODIFIABLE4
            // 
            this.labelNOTMODIFIABLE4.AutoSize = true;
            this.labelNOTMODIFIABLE4.Location = new System.Drawing.Point(6, 101);
            this.labelNOTMODIFIABLE4.Name = "labelNOTMODIFIABLE4";
            this.labelNOTMODIFIABLE4.Size = new System.Drawing.Size(37, 13);
            this.labelNOTMODIFIABLE4.TabIndex = 7;
            this.labelNOTMODIFIABLE4.Text = "Wins :";
            // 
            // textBoxPlayerWins
            // 
            this.textBoxPlayerWins.Location = new System.Drawing.Point(51, 98);
            this.textBoxPlayerWins.Name = "textBoxPlayerWins";
            this.textBoxPlayerWins.Size = new System.Drawing.Size(127, 20);
            this.textBoxPlayerWins.TabIndex = 6;
            // 
            // labelNOTMODIFIABLE3
            // 
            this.labelNOTMODIFIABLE3.AutoSize = true;
            this.labelNOTMODIFIABLE3.Location = new System.Drawing.Point(6, 75);
            this.labelNOTMODIFIABLE3.Name = "labelNOTMODIFIABLE3";
            this.labelNOTMODIFIABLE3.Size = new System.Drawing.Size(41, 13);
            this.labelNOTMODIFIABLE3.TabIndex = 5;
            this.labelNOTMODIFIABLE3.Text = "Name :";
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Location = new System.Drawing.Point(51, 72);
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.Size = new System.Drawing.Size(127, 20);
            this.textBoxPlayerName.TabIndex = 4;
            this.textBoxPlayerName.Text = "XXXXXXXXXXXXX";
            // 
            // labelNOTMODIFIABLE2
            // 
            this.labelNOTMODIFIABLE2.AutoSize = true;
            this.labelNOTMODIFIABLE2.Location = new System.Drawing.Point(6, 49);
            this.labelNOTMODIFIABLE2.Name = "labelNOTMODIFIABLE2";
            this.labelNOTMODIFIABLE2.Size = new System.Drawing.Size(39, 13);
            this.labelNOTMODIFIABLE2.TabIndex = 3;
            this.labelNOTMODIFIABLE2.Text = "Level :";
            // 
            // labelNOTMODIFIABLE1
            // 
            this.labelNOTMODIFIABLE1.AutoSize = true;
            this.labelNOTMODIFIABLE1.Location = new System.Drawing.Point(6, 22);
            this.labelNOTMODIFIABLE1.Name = "labelNOTMODIFIABLE1";
            this.labelNOTMODIFIABLE1.Size = new System.Drawing.Size(39, 13);
            this.labelNOTMODIFIABLE1.TabIndex = 2;
            this.labelNOTMODIFIABLE1.Text = "Rank :";
            // 
            // textBoxPlayerLevel
            // 
            this.textBoxPlayerLevel.Location = new System.Drawing.Point(51, 46);
            this.textBoxPlayerLevel.Name = "textBoxPlayerLevel";
            this.textBoxPlayerLevel.Size = new System.Drawing.Size(127, 20);
            this.textBoxPlayerLevel.TabIndex = 1;
            // 
            // comboBoxPlayerRank
            // 
            this.comboBoxPlayerRank.FormattingEnabled = true;
            this.comboBoxPlayerRank.Items.AddRange(new object[] {
            "NotRanked",
            "SilverI",
            "SilverII",
            "SilverIII",
            "SilverIV",
            "SilverElite",
            "SilverEliteMaster",
            "GoldNovaI",
            "GoldNovaII",
            "GoldNovaIII",
            "GoldNovaMaster",
            "MasterGuardianI",
            "MasterGuardianII",
            "MasterGuardianElite",
            "DistinguishedMasterGuardian",
            "LegendaryEagle",
            "LegendaryEagleMaster",
            "SupremeMasterFirstClass",
            "TheGlobalElite"});
            this.comboBoxPlayerRank.Location = new System.Drawing.Point(51, 19);
            this.comboBoxPlayerRank.Name = "comboBoxPlayerRank";
            this.comboBoxPlayerRank.Size = new System.Drawing.Size(127, 21);
            this.comboBoxPlayerRank.TabIndex = 0;
            this.comboBoxPlayerRank.Text = "TheGlobalElite";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(12, 407);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(120, 23);
            this.buttonStop.TabIndex = 13;
            this.buttonStop.Text = "STOP";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonInviteToLobby
            // 
            this.buttonInviteToLobby.Location = new System.Drawing.Point(103, 66);
            this.buttonInviteToLobby.Name = "buttonInviteToLobby";
            this.buttonInviteToLobby.Size = new System.Drawing.Size(75, 23);
            this.buttonInviteToLobby.TabIndex = 14;
            this.buttonInviteToLobby.Text = "Invite";
            this.buttonInviteToLobby.UseVisualStyleBackColor = true;
            this.buttonInviteToLobby.Click += new System.EventHandler(this.buttonInviteToLobby_Click);
            // 
            // groupBoxInvitePlayer
            // 
            this.groupBoxInvitePlayer.Controls.Add(this.labelSteamId);
            this.groupBoxInvitePlayer.Controls.Add(this.textBoxSteamIdInvite);
            this.groupBoxInvitePlayer.Controls.Add(this.buttonInviteToLobby);
            this.groupBoxInvitePlayer.Location = new System.Drawing.Point(138, 308);
            this.groupBoxInvitePlayer.Name = "groupBoxInvitePlayer";
            this.groupBoxInvitePlayer.Size = new System.Drawing.Size(184, 100);
            this.groupBoxInvitePlayer.TabIndex = 15;
            this.groupBoxInvitePlayer.TabStop = false;
            this.groupBoxInvitePlayer.Text = "Invite Player";
            // 
            // labelSteamId
            // 
            this.labelSteamId.AutoSize = true;
            this.labelSteamId.Location = new System.Drawing.Point(8, 20);
            this.labelSteamId.Name = "labelSteamId";
            this.labelSteamId.Size = new System.Drawing.Size(52, 13);
            this.labelSteamId.TabIndex = 15;
            this.labelSteamId.Text = "SteamId :";
            // 
            // textBoxSteamIdInvite
            // 
            this.textBoxSteamIdInvite.Location = new System.Drawing.Point(9, 40);
            this.textBoxSteamIdInvite.Name = "textBoxSteamIdInvite";
            this.textBoxSteamIdInvite.Size = new System.Drawing.Size(169, 20);
            this.textBoxSteamIdInvite.TabIndex = 0;
            this.textBoxSteamIdInvite.Text = "765611979XXXXXXXX";
            // 
            // buttonLobbyInject
            // 
            this.buttonLobbyInject.Location = new System.Drawing.Point(22, 76);
            this.buttonLobbyInject.Name = "buttonLobbyInject";
            this.buttonLobbyInject.Size = new System.Drawing.Size(87, 23);
            this.buttonLobbyInject.TabIndex = 7;
            this.buttonLobbyInject.Text = "Inject";
            this.buttonLobbyInject.UseVisualStyleBackColor = true;
            this.buttonLobbyInject.Click += new System.EventHandler(this.buttonLobbyInject_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1590, 440);
            this.Controls.Add(this.groupBoxInvitePlayer);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.groupBoxPlayerInformations);
            this.Controls.Add(this.groupBoxLobbyEvents);
            this.Controls.Add(this.groupBoxLobbyPlayers);
            this.Controls.Add(this.groupBoxLobbyChat);
            this.Controls.Add(this.listBoxAppLogs);
            this.Controls.Add(this.buttonJoinLobby);
            this.Controls.Add(this.textBoxLobbyLink);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonReloadAccounts);
            this.Controls.Add(this.listBoxAccounts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CS:GO Bot | =)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxLobbyChat.ResumeLayout(false);
            this.groupBoxLobbyChat.PerformLayout();
            this.groupBoxLobbyPlayers.ResumeLayout(false);
            this.groupBoxLobbyEvents.ResumeLayout(false);
            this.groupBoxPlayerInformations.ResumeLayout(false);
            this.groupBoxPlayerInformations.PerformLayout();
            this.groupBoxInvitePlayer.ResumeLayout(false);
            this.groupBoxInvitePlayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxAccounts;
        public System.Windows.Forms.Button buttonReloadAccounts;
        public System.Windows.Forms.Button buttonStart;
        public System.Windows.Forms.TextBox textBoxLobbyLink;
        public System.Windows.Forms.Button buttonJoinLobby;
        public System.Windows.Forms.ListBox listBoxAppLogs;
        public System.Windows.Forms.ListBox listBoxLobbyMessages;
        public System.Windows.Forms.TextBox textBoxLobbyMessage;
        public System.Windows.Forms.Button buttonSendLobbyMessage;
        public System.Windows.Forms.GroupBox groupBoxLobbyChat;
        public System.Windows.Forms.GroupBox groupBoxLobbyPlayers;
        public System.Windows.Forms.Button buttonLobbyLvlPlayer;
        public System.Windows.Forms.Button buttonLobbyRankPlayer;
        public System.Windows.Forms.Button buttonLobbyKickPlayer;
        public System.Windows.Forms.ListBox listBoxLobbyPlayers;
        public System.Windows.Forms.GroupBox groupBoxLobbyEvents;
        public System.Windows.Forms.Button buttonStartGame;
        public System.Windows.Forms.Button buttonNukeLobby;
        public System.Windows.Forms.Button buttonCrashLobby;
        public System.Windows.Forms.Button buttonInviteAllToLobby;
        public System.Windows.Forms.Button buttonGetAllLobby;
        public System.Windows.Forms.Button buttonSpamLobbyMessage;
        public System.Windows.Forms.GroupBox groupBoxPlayerInformations;
        public System.Windows.Forms.ComboBox comboBoxPlayerRank;
        public System.Windows.Forms.Button buttonChangeData;
        public System.Windows.Forms.Label labelNOTMODIFIABLE4;
        public System.Windows.Forms.TextBox textBoxPlayerWins;
        public System.Windows.Forms.Label labelNOTMODIFIABLE3;
        public System.Windows.Forms.TextBox textBoxPlayerName;
        public System.Windows.Forms.Label labelNOTMODIFIABLE2;
        public System.Windows.Forms.Label labelNOTMODIFIABLE1;
        public System.Windows.Forms.TextBox textBoxPlayerLevel;
        public System.Windows.Forms.Button buttonStop;
        public System.Windows.Forms.Button buttonLeaveLobby;
        public System.Windows.Forms.Button buttonCreateLobby;
        private System.Windows.Forms.Button buttonInviteToLobby;
        private System.Windows.Forms.GroupBox groupBoxInvitePlayer;
        private System.Windows.Forms.TextBox textBoxSteamIdInvite;
        private System.Windows.Forms.Label labelSteamId;
        public System.Windows.Forms.Button buttonLobbyInject;
    }
}

