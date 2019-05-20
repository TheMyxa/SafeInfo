namespace AntiVirus
{
    partial class Form1
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
            this.Basesign = new System.Windows.Forms.Button();
            this.addSign = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.DirScan = new System.Windows.Forms.Button();
            this.signPath = new System.Windows.Forms.TextBox();
            this.changeSignPath = new System.Windows.Forms.Button();
            this.scanPath = new System.Windows.Forms.TextBox();
            this.deleteInfFile = new System.Windows.Forms.Button();
            this.scan_listBox = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.file_watcher_path = new System.Windows.Forms.Button();
            this.file_watcher_textbox = new System.Windows.Forms.TextBox();
            this.file_watcher_stop = new System.Windows.Forms.Button();
            this.file_watcher_clear = new System.Windows.Forms.Button();
            this.file_watcher_start = new System.Windows.Forms.Button();
            this.file_watcher_listBox = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scanir = new System.Windows.Forms.Button();
            this.scan_label = new System.Windows.Forms.Label();
            this.deleteFile = new System.Windows.Forms.Button();
            this.scan_label_2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.files = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Basesign
            // 
            this.Basesign.BackColor = System.Drawing.Color.White;
            this.Basesign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Basesign.FlatAppearance.BorderSize = 0;
            this.Basesign.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Basesign.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Basesign.ForeColor = System.Drawing.Color.Black;
            this.Basesign.Location = new System.Drawing.Point(518, 360);
            this.Basesign.Margin = new System.Windows.Forms.Padding(4);
            this.Basesign.Name = "Basesign";
            this.Basesign.Size = new System.Drawing.Size(257, 73);
            this.Basesign.TabIndex = 10;
            this.Basesign.Text = "База сигнатур";
            this.Basesign.UseVisualStyleBackColor = false;
            this.Basesign.Click += new System.EventHandler(this.Basesign_Click);
            // 
            // addSign
            // 
            this.addSign.BackColor = System.Drawing.Color.White;
            this.addSign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.addSign.FlatAppearance.BorderSize = 0;
            this.addSign.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addSign.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addSign.ForeColor = System.Drawing.Color.Black;
            this.addSign.Location = new System.Drawing.Point(253, 358);
            this.addSign.Margin = new System.Windows.Forms.Padding(4);
            this.addSign.Name = "addSign";
            this.addSign.Size = new System.Drawing.Size(257, 75);
            this.addSign.TabIndex = 8;
            this.addSign.Text = "Добавить сигнатуру";
            this.addSign.UseVisualStyleBackColor = false;
            this.addSign.Click += new System.EventHandler(this.addSign_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(253, 47);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(898, 304);
            this.listBox1.TabIndex = 1;
            // 
            // DirScan
            // 
            this.DirScan.BackColor = System.Drawing.Color.White;
            this.DirScan.FlatAppearance.BorderSize = 0;
            this.DirScan.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DirScan.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DirScan.ForeColor = System.Drawing.Color.Black;
            this.DirScan.Location = new System.Drawing.Point(7, 373);
            this.DirScan.Margin = new System.Windows.Forms.Padding(4);
            this.DirScan.Name = "DirScan";
            this.DirScan.Size = new System.Drawing.Size(238, 75);
            this.DirScan.TabIndex = 12;
            this.DirScan.Text = "Путь";
            this.DirScan.UseVisualStyleBackColor = false;
            this.DirScan.Click += new System.EventHandler(this.DirScan_Click);
            // 
            // signPath
            // 
            this.signPath.AllowDrop = true;
            this.signPath.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.signPath.Location = new System.Drawing.Point(7, 46);
            this.signPath.Margin = new System.Windows.Forms.Padding(4);
            this.signPath.Multiline = true;
            this.signPath.Name = "signPath";
            this.signPath.Size = new System.Drawing.Size(238, 305);
            this.signPath.TabIndex = 0;
            this.signPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.signPath_DragDrop);
            this.signPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.signPath_DragEnter);
            this.signPath.DragLeave += new System.EventHandler(this.signPath_DragLeave);
            // 
            // changeSignPath
            // 
            this.changeSignPath.BackColor = System.Drawing.Color.White;
            this.changeSignPath.FlatAppearance.BorderSize = 0;
            this.changeSignPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.changeSignPath.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeSignPath.ForeColor = System.Drawing.Color.Black;
            this.changeSignPath.Location = new System.Drawing.Point(7, 359);
            this.changeSignPath.Margin = new System.Windows.Forms.Padding(4);
            this.changeSignPath.Name = "changeSignPath";
            this.changeSignPath.Size = new System.Drawing.Size(238, 75);
            this.changeSignPath.TabIndex = 6;
            this.changeSignPath.Text = "Путь";
            this.changeSignPath.UseVisualStyleBackColor = false;
            this.changeSignPath.Click += new System.EventHandler(this.changeSignPath_Click);
            // 
            // scanPath
            // 
            this.scanPath.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scanPath.Location = new System.Drawing.Point(7, 47);
            this.scanPath.Margin = new System.Windows.Forms.Padding(4);
            this.scanPath.Multiline = true;
            this.scanPath.Name = "scanPath";
            this.scanPath.Size = new System.Drawing.Size(238, 304);
            this.scanPath.TabIndex = 0;
            this.scanPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // deleteInfFile
            // 
            this.deleteInfFile.BackColor = System.Drawing.Color.Transparent;
            this.deleteInfFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.deleteInfFile.FlatAppearance.BorderSize = 0;
            this.deleteInfFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deleteInfFile.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteInfFile.ForeColor = System.Drawing.Color.Black;
            this.deleteInfFile.Location = new System.Drawing.Point(783, 359);
            this.deleteInfFile.Margin = new System.Windows.Forms.Padding(4);
            this.deleteInfFile.Name = "deleteInfFile";
            this.deleteInfFile.Size = new System.Drawing.Size(257, 75);
            this.deleteInfFile.TabIndex = 11;
            this.deleteInfFile.Text = "Удалить запись";
            this.deleteInfFile.UseVisualStyleBackColor = false;
            this.deleteInfFile.Click += new System.EventHandler(this.deleteInfFile_Click);
            // 
            // scan_listBox
            // 
            this.scan_listBox.ItemHeight = 20;
            this.scan_listBox.Location = new System.Drawing.Point(252, 47);
            this.scan_listBox.Name = "scan_listBox";
            this.scan_listBox.Size = new System.Drawing.Size(900, 304);
            this.scan_listBox.TabIndex = 20;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // file_watcher_path
            // 
            this.file_watcher_path.BackColor = System.Drawing.Color.White;
            this.file_watcher_path.FlatAppearance.BorderSize = 0;
            this.file_watcher_path.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.file_watcher_path.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.file_watcher_path.ForeColor = System.Drawing.Color.Black;
            this.file_watcher_path.Location = new System.Drawing.Point(9, 364);
            this.file_watcher_path.Margin = new System.Windows.Forms.Padding(4);
            this.file_watcher_path.Name = "file_watcher_path";
            this.file_watcher_path.Size = new System.Drawing.Size(238, 75);
            this.file_watcher_path.TabIndex = 6;
            this.file_watcher_path.Text = "Путь";
            this.file_watcher_path.UseVisualStyleBackColor = false;
            this.file_watcher_path.Click += new System.EventHandler(this.file_watcher_path_Click);
            // 
            // file_watcher_textbox
            // 
            this.file_watcher_textbox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.file_watcher_textbox.Location = new System.Drawing.Point(7, 7);
            this.file_watcher_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.file_watcher_textbox.Multiline = true;
            this.file_watcher_textbox.Name = "file_watcher_textbox";
            this.file_watcher_textbox.Size = new System.Drawing.Size(238, 349);
            this.file_watcher_textbox.TabIndex = 0;
            // 
            // file_watcher_stop
            // 
            this.file_watcher_stop.BackColor = System.Drawing.Color.White;
            this.file_watcher_stop.FlatAppearance.BorderSize = 0;
            this.file_watcher_stop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.file_watcher_stop.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.file_watcher_stop.ForeColor = System.Drawing.Color.Black;
            this.file_watcher_stop.Location = new System.Drawing.Point(894, 364);
            this.file_watcher_stop.Name = "file_watcher_stop";
            this.file_watcher_stop.Size = new System.Drawing.Size(238, 75);
            this.file_watcher_stop.TabIndex = 17;
            this.file_watcher_stop.Text = "Стоп";
            this.file_watcher_stop.UseVisualStyleBackColor = false;
            this.file_watcher_stop.Click += new System.EventHandler(this.file_watcher_stop_Click);
            // 
            // file_watcher_clear
            // 
            this.file_watcher_clear.BackColor = System.Drawing.Color.White;
            this.file_watcher_clear.FlatAppearance.BorderSize = 0;
            this.file_watcher_clear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.file_watcher_clear.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.file_watcher_clear.ForeColor = System.Drawing.Color.Black;
            this.file_watcher_clear.Location = new System.Drawing.Point(565, 364);
            this.file_watcher_clear.Name = "file_watcher_clear";
            this.file_watcher_clear.Size = new System.Drawing.Size(238, 75);
            this.file_watcher_clear.TabIndex = 16;
            this.file_watcher_clear.Text = "Очистить окно";
            this.file_watcher_clear.UseVisualStyleBackColor = false;
            this.file_watcher_clear.Click += new System.EventHandler(this.file_watcher_clear_Click);
            // 
            // file_watcher_start
            // 
            this.file_watcher_start.BackColor = System.Drawing.Color.White;
            this.file_watcher_start.FlatAppearance.BorderSize = 0;
            this.file_watcher_start.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.file_watcher_start.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.file_watcher_start.ForeColor = System.Drawing.Color.Black;
            this.file_watcher_start.Location = new System.Drawing.Point(252, 364);
            this.file_watcher_start.Name = "file_watcher_start";
            this.file_watcher_start.Size = new System.Drawing.Size(238, 75);
            this.file_watcher_start.TabIndex = 16;
            this.file_watcher_start.Text = "Следить";
            this.file_watcher_start.UseVisualStyleBackColor = false;
            this.file_watcher_start.Click += new System.EventHandler(this.file_watcher_start_Click);
            // 
            // file_watcher_listBox
            // 
            this.file_watcher_listBox.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.file_watcher_listBox.ForeColor = System.Drawing.Color.Black;
            this.file_watcher_listBox.FormattingEnabled = true;
            this.file_watcher_listBox.HorizontalScrollbar = true;
            this.file_watcher_listBox.ItemHeight = 23;
            this.file_watcher_listBox.Location = new System.Drawing.Point(252, 7);
            this.file_watcher_listBox.Name = "file_watcher_listBox";
            this.file_watcher_listBox.Size = new System.Drawing.Size(880, 349);
            this.file_watcher_listBox.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1184, 490);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.files);
            this.tabPage1.Controls.Add(this.changeSignPath);
            this.tabPage1.Controls.Add(this.addSign);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.Basesign);
            this.tabPage1.Controls.Add(this.deleteInfFile);
            this.tabPage1.Controls.Add(this.signPath);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1176, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "База сигнатур";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scanPath);
            this.tabPage2.Controls.Add(this.DirScan);
            this.tabPage2.Controls.Add(this.scan_listBox);
            this.tabPage2.Controls.Add(this.scan_label_2);
            this.tabPage2.Controls.Add(this.scan_label);
            this.tabPage2.Controls.Add(this.deleteFile);
            this.tabPage2.Controls.Add(this.scanir);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1176, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сканирование";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // scanir
            // 
            this.scanir.BackColor = System.Drawing.Color.White;
            this.scanir.FlatAppearance.BorderSize = 0;
            this.scanir.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.scanir.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scanir.ForeColor = System.Drawing.Color.Black;
            this.scanir.Location = new System.Drawing.Point(252, 371);
            this.scanir.Margin = new System.Windows.Forms.Padding(4);
            this.scanir.Name = "scanir";
            this.scanir.Size = new System.Drawing.Size(238, 77);
            this.scanir.TabIndex = 7;
            this.scanir.Text = "Сканировать";
            this.scanir.UseVisualStyleBackColor = false;
            this.scanir.Click += new System.EventHandler(this.scanir_Click_1);
            // 
            // scan_label
            // 
            this.scan_label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scan_label.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.scan_label.ForeColor = System.Drawing.Color.Black;
            this.scan_label.Location = new System.Drawing.Point(3, 3);
            this.scan_label.Name = "scan_label";
            this.scan_label.Size = new System.Drawing.Size(1173, 23);
            this.scan_label.TabIndex = 15;
            this.scan_label.Text = "Дата";
            this.scan_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deleteFile
            // 
            this.deleteFile.BackColor = System.Drawing.Color.White;
            this.deleteFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.deleteFile.FlatAppearance.BorderSize = 0;
            this.deleteFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deleteFile.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.deleteFile.ForeColor = System.Drawing.Color.Black;
            this.deleteFile.Location = new System.Drawing.Point(498, 371);
            this.deleteFile.Margin = new System.Windows.Forms.Padding(4);
            this.deleteFile.Name = "deleteFile";
            this.deleteFile.Size = new System.Drawing.Size(257, 77);
            this.deleteFile.TabIndex = 9;
            this.deleteFile.Text = "Удалить зараженный файл";
            this.deleteFile.UseVisualStyleBackColor = false;
            this.deleteFile.Click += new System.EventHandler(this.deleteFile_Click_2);
            // 
            // scan_label_2
            // 
            this.scan_label_2.AutoSize = true;
            this.scan_label_2.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.scan_label_2.ForeColor = System.Drawing.Color.Black;
            this.scan_label_2.Location = new System.Drawing.Point(845, 371);
            this.scan_label_2.Name = "scan_label_2";
            this.scan_label_2.Size = new System.Drawing.Size(307, 23);
            this.scan_label_2.TabIndex = 17;
            this.scan_label_2.Text = "Время сканирования 00:00:00\r\n";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.file_watcher_stop);
            this.tabPage3.Controls.Add(this.file_watcher_path);
            this.tabPage3.Controls.Add(this.file_watcher_clear);
            this.tabPage3.Controls.Add(this.file_watcher_textbox);
            this.tabPage3.Controls.Add(this.file_watcher_start);
            this.tabPage3.Controls.Add(this.file_watcher_listBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1176, 457);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Слежение";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // files
            // 
            this.files.AutoSize = true;
            this.files.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.files.ForeColor = System.Drawing.Color.Black;
            this.files.Location = new System.Drawing.Point(389, 20);
            this.files.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.files.Name = "files";
            this.files.Size = new System.Drawing.Size(0, 23);
            this.files.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 490);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Антивирус";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox scanPath;
        private System.Windows.Forms.TextBox signPath;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button scanir_Click;
        private System.Windows.Forms.Button deleteFile_Click;
        private System.Windows.Forms.Button changeSignPath;
        private System.Windows.Forms.Button addSign;
        private System.Windows.Forms.Button Basesign;
        private System.Windows.Forms.Button DirScan;
        private System.Windows.Forms.Button deleteInfFile;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.ComponentModel.BackgroundWorker backgroundWorker2;
        public System.Windows.Forms.ListBox scan_listBox;
        private System.Windows.Forms.Button file_watcher_path;
        private System.Windows.Forms.TextBox file_watcher_textbox;
        private System.Windows.Forms.ListBox file_watcher_listBox;
        private System.Windows.Forms.Button file_watcher_stop;
        private System.Windows.Forms.Button file_watcher_start;
        private System.Windows.Forms.Button file_watcher_clear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label scan_label_2;
        private System.Windows.Forms.Label scan_label;
        private System.Windows.Forms.Button deleteFile;
        private System.Windows.Forms.Button scanir;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label files;
    }
    
}

