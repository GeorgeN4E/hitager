
namespace Hitager
{
    partial class BMW_Remote
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.label_RSK_Hi = new System.Windows.Forms.Label();
            this.label_RSK_lo = new System.Windows.Forms.Label();
            this.ReadRemote_Button = new System.Windows.Forms.Button();
            this.label_RemoteID = new System.Windows.Forms.Label();
            this.label_Sync = new System.Windows.Forms.Label();
            this.label_Conf = new System.Windows.Forms.Label();
            this.button_WriteRemote = new System.Windows.Forms.Button();
            this.maskedTextBox_RemoteID = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_RSK_HI = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_RSK_LO = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_Sync = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_Conf = new System.Windows.Forms.MaskedTextBox();
            this.button_OpenCasDump = new System.Windows.Forms.Button();
            this.comboBox_bankSelect = new System.Windows.Forms.ComboBox();
            this.button_FetchRemoteData = new System.Windows.Forms.Button();
            this.groupBox_CasData = new System.Windows.Forms.GroupBox();
            this.label_CasMask = new System.Windows.Forms.Label();
            this.label_CasDumpMaskLabel = new System.Windows.Forms.Label();
            this.label_CasDumpStatus = new System.Windows.Forms.Label();
            this.label_CasDumpStatusLabel = new System.Windows.Forms.Label();
            this.groupBox_keyData = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox_CasData.SuspendLayout();
            this.groupBox_keyData.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_RSK_Hi
            // 
            this.label_RSK_Hi.AutoSize = true;
            this.label_RSK_Hi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RSK_Hi.Location = new System.Drawing.Point(16, 75);
            this.label_RSK_Hi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_RSK_Hi.Name = "label_RSK_Hi";
            this.label_RSK_Hi.Size = new System.Drawing.Size(76, 25);
            this.label_RSK_Hi.TabIndex = 21;
            this.label_RSK_Hi.Text = "RSK Hi";
            // 
            // label_RSK_lo
            // 
            this.label_RSK_lo.AutoSize = true;
            this.label_RSK_lo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RSK_lo.Location = new System.Drawing.Point(16, 120);
            this.label_RSK_lo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_RSK_lo.Name = "label_RSK_lo";
            this.label_RSK_lo.Size = new System.Drawing.Size(80, 25);
            this.label_RSK_lo.TabIndex = 22;
            this.label_RSK_lo.Text = "RSK Lo";
            // 
            // ReadRemote_Button
            // 
            this.ReadRemote_Button.Location = new System.Drawing.Point(15, 249);
            this.ReadRemote_Button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ReadRemote_Button.Name = "ReadRemote_Button";
            this.ReadRemote_Button.Size = new System.Drawing.Size(130, 55);
            this.ReadRemote_Button.TabIndex = 6;
            this.ReadRemote_Button.Text = "Read";
            this.ReadRemote_Button.UseVisualStyleBackColor = true;
            this.ReadRemote_Button.Click += new System.EventHandler(this.Read_Remote_Click);
            // 
            // label_RemoteID
            // 
            this.label_RemoteID.AutoSize = true;
            this.label_RemoteID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RemoteID.Location = new System.Drawing.Point(16, 31);
            this.label_RemoteID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_RemoteID.Name = "label_RemoteID";
            this.label_RemoteID.Size = new System.Drawing.Size(121, 25);
            this.label_RemoteID.TabIndex = 25;
            this.label_RemoteID.Text = "Key Number";
            // 
            // label_Sync
            // 
            this.label_Sync.AutoSize = true;
            this.label_Sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Sync.Location = new System.Drawing.Point(16, 165);
            this.label_Sync.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Sync.Name = "label_Sync";
            this.label_Sync.Size = new System.Drawing.Size(57, 25);
            this.label_Sync.TabIndex = 27;
            this.label_Sync.Text = "Sync";
            // 
            // label_Conf
            // 
            this.label_Conf.AutoSize = true;
            this.label_Conf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Conf.Location = new System.Drawing.Point(16, 209);
            this.label_Conf.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Conf.Name = "label_Conf";
            this.label_Conf.Size = new System.Drawing.Size(69, 25);
            this.label_Conf.TabIndex = 29;
            this.label_Conf.Text = "Config";
            // 
            // button_WriteRemote
            // 
            this.button_WriteRemote.Location = new System.Drawing.Point(186, 249);
            this.button_WriteRemote.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_WriteRemote.Name = "button_WriteRemote";
            this.button_WriteRemote.Size = new System.Drawing.Size(130, 55);
            this.button_WriteRemote.TabIndex = 7;
            this.button_WriteRemote.Text = "Write";
            this.button_WriteRemote.UseVisualStyleBackColor = true;
            this.button_WriteRemote.Click += new System.EventHandler(this.button_WriteRemote_Click);
            // 
            // maskedTextBox_RemoteID
            // 
            this.maskedTextBox_RemoteID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBox_RemoteID.Location = new System.Drawing.Point(248, 26);
            this.maskedTextBox_RemoteID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.maskedTextBox_RemoteID.Mask = "&& &&";
            this.maskedTextBox_RemoteID.Name = "maskedTextBox_RemoteID";
            this.maskedTextBox_RemoteID.Size = new System.Drawing.Size(67, 30);
            this.maskedTextBox_RemoteID.TabIndex = 1;
            this.maskedTextBox_RemoteID.Text = "0000";
            this.maskedTextBox_RemoteID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox_RemoteID.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // maskedTextBox_RSK_HI
            // 
            this.maskedTextBox_RSK_HI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBox_RSK_HI.Location = new System.Drawing.Point(248, 71);
            this.maskedTextBox_RSK_HI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.maskedTextBox_RSK_HI.Mask = "&& &&";
            this.maskedTextBox_RSK_HI.Name = "maskedTextBox_RSK_HI";
            this.maskedTextBox_RSK_HI.Size = new System.Drawing.Size(67, 30);
            this.maskedTextBox_RSK_HI.TabIndex = 2;
            this.maskedTextBox_RSK_HI.Text = "0000";
            this.maskedTextBox_RSK_HI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox_RSK_HI.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // maskedTextBox_RSK_LO
            // 
            this.maskedTextBox_RSK_LO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBox_RSK_LO.Location = new System.Drawing.Point(195, 115);
            this.maskedTextBox_RSK_LO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.maskedTextBox_RSK_LO.Mask = "&& && && &&";
            this.maskedTextBox_RSK_LO.Name = "maskedTextBox_RSK_LO";
            this.maskedTextBox_RSK_LO.Size = new System.Drawing.Size(120, 30);
            this.maskedTextBox_RSK_LO.TabIndex = 3;
            this.maskedTextBox_RSK_LO.Text = "00000000";
            this.maskedTextBox_RSK_LO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox_RSK_LO.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // maskedTextBox_Sync
            // 
            this.maskedTextBox_Sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBox_Sync.Location = new System.Drawing.Point(195, 160);
            this.maskedTextBox_Sync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.maskedTextBox_Sync.Mask = "&& && && &&";
            this.maskedTextBox_Sync.Name = "maskedTextBox_Sync";
            this.maskedTextBox_Sync.Size = new System.Drawing.Size(120, 30);
            this.maskedTextBox_Sync.TabIndex = 4;
            this.maskedTextBox_Sync.Text = "00000000";
            this.maskedTextBox_Sync.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox_Sync.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // maskedTextBox_Conf
            // 
            this.maskedTextBox_Conf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBox_Conf.Location = new System.Drawing.Point(195, 205);
            this.maskedTextBox_Conf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.maskedTextBox_Conf.Mask = "&& && && &&";
            this.maskedTextBox_Conf.Name = "maskedTextBox_Conf";
            this.maskedTextBox_Conf.Size = new System.Drawing.Size(120, 30);
            this.maskedTextBox_Conf.TabIndex = 5;
            this.maskedTextBox_Conf.Text = "00000000";
            this.maskedTextBox_Conf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedTextBox_Conf.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // button_OpenCasDump
            // 
            this.button_OpenCasDump.Location = new System.Drawing.Point(15, 29);
            this.button_OpenCasDump.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_OpenCasDump.Name = "button_OpenCasDump";
            this.button_OpenCasDump.Size = new System.Drawing.Size(156, 32);
            this.button_OpenCasDump.TabIndex = 8;
            this.button_OpenCasDump.Text = "Open CAS Dump";
            this.button_OpenCasDump.UseVisualStyleBackColor = true;
            this.button_OpenCasDump.Click += new System.EventHandler(this.button_OpenCasDump_Click);
            // 
            // comboBox_bankSelect
            // 
            this.comboBox_bankSelect.FormattingEnabled = true;
            this.comboBox_bankSelect.Location = new System.Drawing.Point(15, 149);
            this.comboBox_bankSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_bankSelect.Name = "comboBox_bankSelect";
            this.comboBox_bankSelect.Size = new System.Drawing.Size(193, 28);
            this.comboBox_bankSelect.TabIndex = 9;
            // 
            // button_FetchRemoteData
            // 
            this.button_FetchRemoteData.Location = new System.Drawing.Point(219, 148);
            this.button_FetchRemoteData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_FetchRemoteData.Name = "button_FetchRemoteData";
            this.button_FetchRemoteData.Size = new System.Drawing.Size(110, 32);
            this.button_FetchRemoteData.TabIndex = 10;
            this.button_FetchRemoteData.Text = "Get Data";
            this.button_FetchRemoteData.UseVisualStyleBackColor = true;
            this.button_FetchRemoteData.Click += new System.EventHandler(this.button_FetchRemoteData_Click);
            // 
            // groupBox_CasData
            // 
            this.groupBox_CasData.Controls.Add(this.label_CasMask);
            this.groupBox_CasData.Controls.Add(this.label_CasDumpMaskLabel);
            this.groupBox_CasData.Controls.Add(this.label_CasDumpStatus);
            this.groupBox_CasData.Controls.Add(this.label_CasDumpStatusLabel);
            this.groupBox_CasData.Controls.Add(this.comboBox_bankSelect);
            this.groupBox_CasData.Controls.Add(this.button_FetchRemoteData);
            this.groupBox_CasData.Controls.Add(this.button_OpenCasDump);
            this.groupBox_CasData.Location = new System.Drawing.Point(18, 323);
            this.groupBox_CasData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_CasData.Name = "groupBox_CasData";
            this.groupBox_CasData.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_CasData.Size = new System.Drawing.Size(471, 205);
            this.groupBox_CasData.TabIndex = 39;
            this.groupBox_CasData.TabStop = false;
            this.groupBox_CasData.Text = "CAS Data";
            // 
            // label_CasMask
            // 
            this.label_CasMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CasMask.Location = new System.Drawing.Point(224, 102);
            this.label_CasMask.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CasMask.Name = "label_CasMask";
            this.label_CasMask.Size = new System.Drawing.Size(105, 26);
            this.label_CasMask.TabIndex = 42;
            this.label_CasMask.Text = "N/A";
            this.label_CasMask.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_CasDumpMaskLabel
            // 
            this.label_CasDumpMaskLabel.AutoSize = true;
            this.label_CasDumpMaskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CasDumpMaskLabel.Location = new System.Drawing.Point(16, 102);
            this.label_CasDumpMaskLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CasDumpMaskLabel.Name = "label_CasDumpMaskLabel";
            this.label_CasDumpMaskLabel.Size = new System.Drawing.Size(169, 25);
            this.label_CasDumpMaskLabel.TabIndex = 41;
            this.label_CasDumpMaskLabel.Text = "µC Mask Version:";
            // 
            // label_CasDumpStatus
            // 
            this.label_CasDumpStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_CasDumpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CasDumpStatus.Location = new System.Drawing.Point(293, 66);
            this.label_CasDumpStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CasDumpStatus.Name = "label_CasDumpStatus";
            this.label_CasDumpStatus.Size = new System.Drawing.Size(168, 26);
            this.label_CasDumpStatus.TabIndex = 40;
            this.label_CasDumpStatus.Text = "No dump loaded";
            this.label_CasDumpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_CasDumpStatusLabel
            // 
            this.label_CasDumpStatusLabel.AutoSize = true;
            this.label_CasDumpStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CasDumpStatusLabel.Location = new System.Drawing.Point(16, 66);
            this.label_CasDumpStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_CasDumpStatusLabel.Name = "label_CasDumpStatusLabel";
            this.label_CasDumpStatusLabel.Size = new System.Drawing.Size(131, 25);
            this.label_CasDumpStatusLabel.TabIndex = 39;
            this.label_CasDumpStatusLabel.Text = "Dump Status:";
            // 
            // groupBox_keyData
            // 
            this.groupBox_keyData.Controls.Add(this.button1);
            this.groupBox_keyData.Controls.Add(this.label_RemoteID);
            this.groupBox_keyData.Controls.Add(this.label_RSK_Hi);
            this.groupBox_keyData.Controls.Add(this.ReadRemote_Button);
            this.groupBox_keyData.Controls.Add(this.button_WriteRemote);
            this.groupBox_keyData.Controls.Add(this.maskedTextBox_Conf);
            this.groupBox_keyData.Controls.Add(this.label_RSK_lo);
            this.groupBox_keyData.Controls.Add(this.maskedTextBox_Sync);
            this.groupBox_keyData.Controls.Add(this.label_Sync);
            this.groupBox_keyData.Controls.Add(this.maskedTextBox_RSK_LO);
            this.groupBox_keyData.Controls.Add(this.label_Conf);
            this.groupBox_keyData.Controls.Add(this.maskedTextBox_RSK_HI);
            this.groupBox_keyData.Controls.Add(this.maskedTextBox_RemoteID);
            this.groupBox_keyData.Location = new System.Drawing.Point(18, 3);
            this.groupBox_keyData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_keyData.Name = "groupBox_keyData";
            this.groupBox_keyData.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox_keyData.Size = new System.Drawing.Size(471, 314);
            this.groupBox_keyData.TabIndex = 40;
            this.groupBox_keyData.TabStop = false;
            this.groupBox_keyData.Text = "Key Data";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 53);
            this.button1.TabIndex = 30;
            this.button1.Text = "GET FROM EEPROM\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BMW_Remote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 546);
            this.Controls.Add(this.groupBox_keyData);
            this.Controls.Add(this.groupBox_CasData);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BMW_Remote";
            this.Text = "BMW Remote";
            this.groupBox_CasData.ResumeLayout(false);
            this.groupBox_CasData.PerformLayout();
            this.groupBox_keyData.ResumeLayout(false);
            this.groupBox_keyData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_RSK_Hi;
        private System.Windows.Forms.Label label_RSK_lo;
        private System.Windows.Forms.Button ReadRemote_Button;
        private System.Windows.Forms.Label label_RemoteID;
        private System.Windows.Forms.Label label_Sync;
        private System.Windows.Forms.Label label_Conf;
        private System.Windows.Forms.Button button_WriteRemote;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_RemoteID;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_RSK_HI;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_RSK_LO;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_Sync;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_Conf;
        private System.Windows.Forms.Button button_OpenCasDump;
        private System.Windows.Forms.ComboBox comboBox_bankSelect;
        private System.Windows.Forms.Button button_FetchRemoteData;
        private System.Windows.Forms.GroupBox groupBox_CasData;
        private System.Windows.Forms.Label label_CasDumpStatusLabel;
        private System.Windows.Forms.GroupBox groupBox_keyData;
        private System.Windows.Forms.Label label_CasDumpStatus;
        private System.Windows.Forms.Label label_CasMask;
        private System.Windows.Forms.Label label_CasDumpMaskLabel;
        private System.Windows.Forms.Button button1;
    }
}