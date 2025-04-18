namespace App
{
    partial class Participants
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewParticipant = new System.Windows.Forms.DataGridView();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParticipantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParticipationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipant)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Список участников";
            // 
            // dataGridViewParticipant
            // 
            this.dataGridViewParticipant.AllowUserToAddRows = false;
            this.dataGridViewParticipant.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewParticipant.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridViewParticipant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParticipant.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EventId,
            this.ParticipantName,
            this.ParticipationId});
            this.dataGridViewParticipant.Location = new System.Drawing.Point(32, 47);
            this.dataGridViewParticipant.Name = "dataGridViewParticipant";
            this.dataGridViewParticipant.RowHeadersWidth = 51;
            this.dataGridViewParticipant.RowTemplate.Height = 24;
            this.dataGridViewParticipant.Size = new System.Drawing.Size(330, 243);
            this.dataGridViewParticipant.TabIndex = 3;
            // 
            // EventId
            // 
            this.EventId.HeaderText = "EventId";
            this.EventId.MinimumWidth = 6;
            this.EventId.Name = "EventId";
            this.EventId.Visible = false;
            // 
            // ParticipantName
            // 
            this.ParticipantName.HeaderText = "Имя";
            this.ParticipantName.MinimumWidth = 6;
            this.ParticipantName.Name = "ParticipantName";
            // 
            // ParticipationId
            // 
            this.ParticipationId.HeaderText = "ParticipationId";
            this.ParticipationId.MinimumWidth = 6;
            this.ParticipationId.Name = "ParticipationId";
            this.ParticipationId.Visible = false;
            // 
            // textDescription
            // 
            this.textDescription.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.textDescription.Location = new System.Drawing.Point(32, 316);
            this.textDescription.Multiline = true;
            this.textDescription.Name = "textDescription";
            this.textDescription.ReadOnly = true;
            this.textDescription.Size = new System.Drawing.Size(330, 230);
            this.textDescription.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Описание";
            // 
            // Participants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(401, 569);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.dataGridViewParticipant);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Participants";
            this.Text = "Подробнее";
            this.Load += new System.EventHandler(this.Participants_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParticipant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewParticipant;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParticipationId;
    }
}