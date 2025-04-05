namespace App
{
    partial class Add
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
            this.textTitle = new System.Windows.Forms.TextBox();
            this.textCategory = new System.Windows.Forms.TextBox();
            this.textTime = new System.Windows.Forms.TextBox();
            this.textDate = new System.Windows.Forms.TextBox();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.textParticipant = new System.Windows.Forms.TextBox();
            this.btnAddParticipant = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddEvent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridParticipant = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParticipant)).BeginInit();
            this.SuspendLayout();
            // 
            // textTitle
            // 
            this.textTitle.Location = new System.Drawing.Point(28, 40);
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(114, 22);
            this.textTitle.TabIndex = 0;
            // 
            // textCategory
            // 
            this.textCategory.Location = new System.Drawing.Point(28, 265);
            this.textCategory.Name = "textCategory";
            this.textCategory.Size = new System.Drawing.Size(114, 22);
            this.textCategory.TabIndex = 1;
            // 
            // textTime
            // 
            this.textTime.Location = new System.Drawing.Point(28, 206);
            this.textTime.Name = "textTime";
            this.textTime.Size = new System.Drawing.Size(114, 22);
            this.textTime.TabIndex = 2;
            // 
            // textDate
            // 
            this.textDate.Location = new System.Drawing.Point(28, 146);
            this.textDate.Name = "textDate";
            this.textDate.Size = new System.Drawing.Size(114, 22);
            this.textDate.TabIndex = 3;
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(28, 91);
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(114, 22);
            this.textDescription.TabIndex = 4;
            // 
            // textParticipant
            // 
            this.textParticipant.Location = new System.Drawing.Point(28, 326);
            this.textParticipant.Name = "textParticipant";
            this.textParticipant.Size = new System.Drawing.Size(114, 22);
            this.textParticipant.TabIndex = 5;
            // 
            // btnAddParticipant
            // 
            this.btnAddParticipant.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddParticipant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParticipant.Location = new System.Drawing.Point(28, 393);
            this.btnAddParticipant.Name = "btnAddParticipant";
            this.btnAddParticipant.Size = new System.Drawing.Size(155, 28);
            this.btnAddParticipant.TabIndex = 6;
            this.btnAddParticipant.Text = "Добавить участника";
            this.btnAddParticipant.UseVisualStyleBackColor = false;
            this.btnAddParticipant.Click += new System.EventHandler(this.btnAddParticipant_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(28, 444);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(155, 28);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Удалить участника";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddEvent
            // 
            this.btnAddEvent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEvent.Location = new System.Drawing.Point(373, 444);
            this.btnAddEvent.Name = "btnAddEvent";
            this.btnAddEvent.Size = new System.Drawing.Size(157, 28);
            this.btnAddEvent.TabIndex = 8;
            this.btnAddEvent.Text = "Добавить событие";
            this.btnAddEvent.UseVisualStyleBackColor = false;
            this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Описание";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Дата";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Время";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Категория";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Участник";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(234, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Список участников";
            // 
            // dataGridParticipant
            // 
            this.dataGridParticipant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridParticipant.Location = new System.Drawing.Point(237, 40);
            this.dataGridParticipant.Name = "dataGridParticipant";
            this.dataGridParticipant.RowHeadersWidth = 51;
            this.dataGridParticipant.RowTemplate.Height = 24;
            this.dataGridParticipant.Size = new System.Drawing.Size(293, 388);
            this.dataGridParticipant.TabIndex = 17;
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 527);
            this.Controls.Add(this.dataGridParticipant);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddEvent);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddParticipant);
            this.Controls.Add(this.textParticipant);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.textDate);
            this.Controls.Add(this.textTime);
            this.Controls.Add(this.textCategory);
            this.Controls.Add(this.textTitle);
            this.Name = "Add";
            this.Text = "Добавление";
            this.Load += new System.EventHandler(this.Add_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParticipant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.TextBox textCategory;
        private System.Windows.Forms.TextBox textTime;
        private System.Windows.Forms.TextBox textDate;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.TextBox textParticipant;
        private System.Windows.Forms.Button btnAddParticipant;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridParticipant;
    }
}