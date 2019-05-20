using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using Microsoft.Win32;
namespace AntiVirus
{
    public partial class addSign : Form
    {
        RegistryKey key_create = Registry.CurrentUser.CreateSubKey(@"Software\Antivirus");
        ScanEngine F2 = new ScanEngine();
        string sign_byte;

        public addSign()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(addSign_MouseDown);
        }

        #region button
        private void addSign_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion button

        #region component
        private Label label1;
        private Label label2;
        private Label label3;
        private Button changeSignPath;
        private Button add;
        private TextBox path;
        private TextBox length;
        private TextBox name;
        private TextBox offset;
        private Label exeSection;
        private IContainer components;
        private Label label4;
        #endregion component

        /// <summary>
        ///+ Путь к файлу
        /// </summary>
        private void changeSignPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OBD = new System.Windows.Forms.OpenFileDialog();
            if (OBD.ShowDialog() == DialogResult.OK)
            {
                path.Text = OBD.FileName;
            }

            // читаем файл
            FileStream fs = new FileStream(path.Text, FileMode.Open, FileAccess.Read);      
            // узнаем смещение от начала исп секции и длину секции,
            // чтобы пользователь знал, какую длину ему можно 
            // максимально задать
            int[] data = exeSect(fs);

            // идем на исполняемую секцию
            fs.Seek(data[1], SeekOrigin.Begin);
            // узнаем ее длину
            if (data[0] == 0) System.Windows.MessageBox.Show("Кажется, это неисполняемый файл...");
            exeSection.Text = Convert.ToString(data[0]);
            fs.Close();
        }

        /// <summary>
        /// Считывание сигнатуры из файла
        /// </summary>
        /// <param name="file">файл</param>
        /// <param name="len">длина</param>
        /// <returns>строка байтов</returns>
        public string read(FileStream file, int len)
        {
            byte[] bytes = new byte[len];
            // пишем в bytes от смещения файла до смещения + длина
            file.Read(bytes, 0, len);

            // конвертируем байты в строковый тип
            string str = "";
            foreach (byte symb in bytes)
            {
                // перевод из массива байтов в строку
                // Х2 - 16ричный формат
                str += symb.ToString("X2");
            }

            return str;
        }

        //Получение 8 байт из заголовка
        private string byte_id_header()
        {
            string str;
            using (var fs = new FileStream(path.Text, FileMode.Open))
            {
                var buffer = new byte[2];
                fs.Read(buffer, 0, buffer.Length);
                str = string.Join(" ", buffer.Select(b => b.ToString("X2")));
            }
            return str;
        }
        //Получение 8 байт из исполняемой облости
        private string byte_id_body()
        {
            string str;
            using(FileStream file = new FileStream(path.Text, FileMode.Open))  
            {
                // узнаем длину исп секции и смещение исп секции от начала файла
                int[] data = exeSect(file);
                file.Seek(data[1], SeekOrigin.Begin);

                var buffer = new byte[8];
                file.Read(buffer, 0, buffer.Length);
                str = string.Join(" ", buffer.Select(b => b.ToString("X2")));
            }
            return str;
        }
        /// <summary>
        /// Хэширование (контрольная сумма сигнатуры)
        /// </summary>
        /// <param name="sign">что хешировать</param>
        /// <returns>хеш</returns>
        public string hash(string sign)
        {
            uint crc = 0;
            byte[] data;
            using (MD5 hash = MD5.Create())
            {
                data = hash.ComputeHash(Encoding.ASCII.GetBytes(sign));
                for (int i = 0; i < data.Length; i++)
                {
                    crc += data[i];
                }
            }
            if (crc == 0)
            {
                System.Windows.MessageBox.Show("Ошибка хеширования");
                return null;
            }
            // конвертируем байты в строковый тип
            string str = "";
            foreach (byte symb in data)
            {
                // перевод из массива байтов в строку
                // Х2 - 16ричный формат
                str += symb.ToString("X2");
            }
            return str;
        }

        /// <summary>
        /// Получить длину исп секции файла и смещение исп секции от начала
        /// </summary>
        /// <param name="f">поток файла</param>
        /// <returns>0 - длина исп секции, 1 - смещение от начала исп файла</returns>
        public int[] exeSect(FileStream f)
        {
            int[] data = new int[2];

            // читаем смещение заголовка, 
            // идем к e_lfanew
            //f.Seek(60, SeekOrigin.Begin);

            f.Seek(60, SeekOrigin.Begin);

            byte[] tempArray2 = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                tempArray2[i] = (byte)f.ReadByte();
            }
            // заносим байты в строку
            string tempBytes2 = "";
            for (int i = 0; i < 4; i++)
            {
                // перевод из массива байтов в строку
                // Х2 - 16ричный формат
                tempBytes2 += tempArray2[i].ToString("X2");
            }
            int test = Convert.ToInt32(tempBytes2, 16);

          
            f.Seek(60, SeekOrigin.Begin);
            int offset = test;
            // смещаем на offset и встаем
            // перед PE-заголовком
            f.Seek(offset, SeekOrigin.Begin);

            // количество секций, пропускаем
            // сигнатуру (4 байта) и архитектуру
            // процессора (2 байта)
            f.Seek(6, SeekOrigin.Current);
            int numOfSections = f.ReadByte();

            // размер дополнительного заголовка
            f.Seek(13, SeekOrigin.Current);
            int addition = f.ReadByte();
            // прыгаем к заголовкам секций
            f.Seek(3 + addition, SeekOrigin.Current);

            // проверяем все секции
            while (numOfSections > 0)
            {
                // тип секции
                f.Seek(36, SeekOrigin.Current);
                int temp = f.ReadByte();
                // если исполняемая, заходим
                if (temp == 32)
                {

                    // считываем длину секции
                    // надо считать 4 байта
                    f.Seek(-21, SeekOrigin.Current);
                    // байты расположены наоборот
                    byte[] tempArray = new byte[4];
                    for (int i = 3; i >= 0; i--)
                    {
                        tempArray[i] = (byte)f.ReadByte();
                    }
                    // заносим байты в строку
                    string tempBytes = "";
                    for (int i = 0; i < 4; i++)
                    {
                        // перевод из массива байтов в строку
                        // Х2 - 16ричный формат
                        tempBytes += tempArray[i].ToString("X2");
                    }
                    data[0] = Convert.ToInt32(tempBytes, 16);

                    // считываем смещение секции
                    // от начала файла
                    // переворачиваем и заносим в строку
                    for (int i = 3; i >= 0; i--)
                    {
                        tempArray[i] = (byte)f.ReadByte();
                    }
                    tempBytes = "";
                    for (int i = 0; i < 4; i++)
                    {
                        tempBytes += tempArray[i].ToString("X2");
                    }
                    data[1] = Convert.ToInt32(tempBytes, 16);

                    break;
                }
                else
                {
                    //если секция не исполняемая
                    f.Seek(3, SeekOrigin.Current);
                    numOfSections--;
                }
            }
            return data;
        }

        //Присвоение номера сигнатуре
        //private int Sign_number()
        //{
        //    int number;
        //    List<Node_Write.Virus> sign = F2.getVirusDB();
        //    return number = sign.Count + 1;
        //}
        //---
        /// <summary>
        /// +Добавить сигнатуру
        /// </summary>
        private void add_Click(object sender, EventArgs e)
        {
            if (path.Text == "")
            {
                System.Windows.MessageBox.Show("Путь до сигнатуры не указан.");
                return;
            }

            if (name.Text != "" && offset.Text != "" && length.Text != "")
            {
                // заполняем структуру
                Node_Write.Virus sign;
                // открываем файл, чтобы считать код и захешировать
                FileStream file = new FileStream(path.Text, FileMode.Open, FileAccess.ReadWrite);
                // узнаем длину исп секции и смещение исп секции от начала файла
                int[] data = exeSect(file);
                // смещаем файл на начало исп секции + смещение от нее
                file.Seek(data[1] + Convert.ToInt32(offset.Text), SeekOrigin.Begin);
                // считываем сигнатуру из файла от смещения до смещения + длина
                string virusText = read(file, Convert.ToInt32(length.Text));
                // хэшируем
                sign.hash = hash(virusText);
                // смещаем файл на начало исп секции + смещение от нее
                file.Seek(data[1] + Convert.ToInt32(offset.Text), SeekOrigin.Begin);
                // считываем сигнатуру из файла от смещения до смещения + длина
                string virusText8 = read(file, 8);
                // хэшируем
                sign.hash8 = hash(virusText8);
                // имя вируса
                sign.name = name.Text;
                // длина вируса
                sign.length = Convert.ToInt32(length.Text);
                // кол-во обнаружений
                sign.detect = 0;
                // Присвоение номера каждой сигнатуре
               // sign.number = Sign_number();
                //Начало исполняемой секции
                sign.first_sec = data[1];
                //Длина исполняемой секции
                sign.lenght_sec = data[0];             
                //Последовательность байт для идентификации
                file.Close();
                //8 байт идентификации расширения
                sign.byts_header = byte_id_header();
                //8 байт идентификации тела
                sign.byts_body = byte_id_body();
                // Путь к файлу базы сигнатур
                string line = key_create.GetValue("sign").ToString();

                // сериализуем структуру (превращаем в бинарную форму)
                FileStream VDB = new FileStream(line, FileMode.Append, FileAccess.Write);
                BinaryFormatter binForm = new BinaryFormatter();
                // добавляем в VDB (файл базы сигнатур) объект sign
                // оно добавится туда в 2ой форме и сложно будет что-то понять
                // чтобы что-то понять, надо будет десериализовывать
                binForm.Serialize(VDB, sign);
                VDB.Close();

                this.Close();
            }
            else
                System.Windows.MessageBox.Show("Вы что-то не заполнили.");
        }


        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.changeSignPath = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.path = new System.Windows.Forms.TextBox();
            this.length = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.offset = new System.Windows.Forms.TextBox();
            this.exeSection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(163, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(111, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 57);
            this.label2.TabIndex = 0;
            this.label2.Text = "Смещение от начала \r\nисполняемой секции";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(134, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Длина вируса";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(111, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Длина исполняемой секции";
            // 
            // changeSignPath
            // 
            this.changeSignPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.changeSignPath.FlatAppearance.BorderSize = 0;
            this.changeSignPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.changeSignPath.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeSignPath.ForeColor = System.Drawing.Color.Black;
            this.changeSignPath.Location = new System.Drawing.Point(148, 258);
            this.changeSignPath.Name = "changeSignPath";
            this.changeSignPath.Size = new System.Drawing.Size(160, 37);
            this.changeSignPath.TabIndex = 1;
            this.changeSignPath.Text = "Путь к файлу";
            this.changeSignPath.UseVisualStyleBackColor = true;
            this.changeSignPath.Click += new System.EventHandler(this.changeSignPath_Click);
            // 
            // add
            // 
            this.add.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.add.FlatAppearance.BorderSize = 0;
            this.add.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.add.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add.ForeColor = System.Drawing.Color.Black;
            this.add.Location = new System.Drawing.Point(138, 394);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(160, 40);
            this.add.TabIndex = 2;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // path
            // 
            this.path.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.path.Location = new System.Drawing.Point(4, 301);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(412, 28);
            this.path.TabIndex = 3;
            // 
            // length
            // 
            this.length.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.length.Location = new System.Drawing.Point(4, 224);
            this.length.Name = "length";
            this.length.Size = new System.Drawing.Size(412, 28);
            this.length.TabIndex = 4;
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(4, 63);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(412, 28);
            this.name.TabIndex = 5;
            // 
            // offset
            // 
            this.offset.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.offset.Location = new System.Drawing.Point(4, 154);
            this.offset.Name = "offset";
            this.offset.Size = new System.Drawing.Size(412, 28);
            this.offset.TabIndex = 6;
            // 
            // exeSection
            // 
            this.exeSection.AutoSize = true;
            this.exeSection.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exeSection.ForeColor = System.Drawing.Color.Black;
            this.exeSection.Location = new System.Drawing.Point(211, 370);
            this.exeSection.Name = "exeSection";
            this.exeSection.Size = new System.Drawing.Size(19, 21);
            this.exeSection.TabIndex = 7;
            this.exeSection.Text = "0";
            // 
            // addSign
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(428, 480);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.path);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.offset);
            this.Controls.Add(this.changeSignPath);
            this.Controls.Add(this.name);
            this.Controls.Add(this.exeSection);
            this.Controls.Add(this.length);
            this.Controls.Add(this.add);
            this.Name = "addSign";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}

