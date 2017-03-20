namespace ParserAPI
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this._dgvResult = new System.Windows.Forms.DataGridView();
            this._lblResult = new System.Windows.Forms.Label();
            this._btnRequest = new System.Windows.Forms.Button();
            this._btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._dgvResult);
            this.panel1.Controls.Add(this._lblResult);
            this.panel1.Controls.Add(this._btnRequest);
            this.panel1.Controls.Add(this._btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 342);
            this.panel1.TabIndex = 0;
            // 
            // _dgvResult
            // 
            this._dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvResult.Location = new System.Drawing.Point(4, 4);
            this._dgvResult.Name = "_dgvResult";
            this._dgvResult.Size = new System.Drawing.Size(300, 194);
            this._dgvResult.TabIndex = 5;
            // 
            // _lblResult
            // 
            this._lblResult.AutoSize = true;
            this._lblResult.Location = new System.Drawing.Point(12, 201);
            this._lblResult.Name = "_lblResult";
            this._lblResult.Size = new System.Drawing.Size(57, 13);
            this._lblResult.TabIndex = 4;
            this._lblResult.Text = "//results//";
            // 
            // _btnRequest
            // 
            this._btnRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRequest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._btnRequest.Location = new System.Drawing.Point(148, 316);
            this._btnRequest.Name = "_btnRequest";
            this._btnRequest.Size = new System.Drawing.Size(75, 23);
            this._btnRequest.TabIndex = 3;
            this._btnRequest.Text = "Запрос";
            this._btnRequest.UseVisualStyleBackColor = true;
            this._btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // _btnExit
            // 
            this._btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._btnExit.Location = new System.Drawing.Point(229, 316);
            this._btnExit.Name = "_btnExit";
            this._btnExit.Size = new System.Drawing.Size(75, 23);
            this._btnExit.TabIndex = 1;
            this._btnExit.Text = "Выход";
            this._btnExit.UseVisualStyleBackColor = true;
            this._btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(307, 342);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "ParserAPI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btnRequest;
        private System.Windows.Forms.Button _btnExit;
		private System.Windows.Forms.Label _lblResult;
        private System.Windows.Forms.DataGridView _dgvResult;
	}
}

