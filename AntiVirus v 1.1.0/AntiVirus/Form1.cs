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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Microsoft.Win32;
using System.Drawing.Drawing2D;

namespace AntiVirus
{

    public partial class Form1 : Form
    {
        ScanEngine wFile = new ScanEngine();
        Node_Write NW = new Node_Write();
        //List<string> InfFile = new List<string>();

        BackgroundWorker bw;
        BackgroundWorker bw2;
        BackgroundWorker bw3;

        public DateTime timer_nach;
        bool btimer = true;

        Observer_Virus_FileWatcher Observer_FileWatcher = new Observer_Virus_FileWatcher();
        RegistryKey key_create = Registry.CurrentUser.CreateSubKey(@"Software\Antivirus");
        public Form1()
        {
            InitializeComponent();
            paths();

            Proverka_Registry();
        }

        /// <summary>
        /// Вывод последних данных в окошки
        /// при загрузке окна
        /// </summary>
        public void paths()
        {
            // Последнее сканирование
            try{ scan_label.Text = "Последнее сканирование: " + key_create.GetValue("date"); }
            catch (Exception e) { scan_label.Text = e.Message; }

            // Путь сканирования
            try
            {scanPath.Text = "Путь сканирования: " + key_create.GetValue("path");}
            catch (Exception e){scanPath.Text = e.Message; }

            //Путь базы вирусов
            try{signPath.Text = "Путь к файлу базы вирусов: " + key_create.GetValue("sign");}
            catch (Exception e){signPath.Text = e.Message;}

            //Путь FileWatcher
            try{file_watcher_textbox.Text = "Путь к директории наблюдения: " + key_create.GetValue("filewatcher");}
            catch (Exception e){file_watcher_textbox.Text = e.Message;}
        }

        #region Other

        private void Close2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Other

       
        /// <summary>
        /// +Путь сканирования
        /// </summary>
        private void DirScan_Click(object sender, EventArgs e)
        {
            // Путь сканирования
            String line = wFile.pathScan();
            scanPath.Text = "Путь сканирования: " + line;

            // Вывести все pe файлы директории в listbox
            DirectoryInfo dir = new DirectoryInfo(line);
            listBox1.Items.Clear();
            files.Text = "PE файлы директории";
            //foreach (string f in Directory.GetFiles(line, "*.*", SearchOption.AllDirectories).Union(Directory.GetFiles(line, "*.dll", SearchOption.AllDirectories)))
            //{
            //    string[] splitpath = f.Split('\\');
            //    string name = splitpath[splitpath.Length - 1];
            //    scan_listBox.Items.Add(name);
            //}
            //scan_label_2.Text = "Кол-во файлов в данной директории: " + scan_listBox.Items.Count;
        }

        /// <summary>
        /// +Путь к файлу базы вирусов
        /// </summary>
        private void changeSignPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OBD = new System.Windows.Forms.OpenFileDialog();
            if (OBD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    key_create.SetValue("sign", OBD.FileName);
                }
                catch (Exception err)
                {
                    System.Windows.MessageBox.Show("Exception: " + err.Message);
                }
                signPath.Text = "Путь к файлу базы вирусов: " + OBD.FileName;
            }
        }
        public void Print_scan_listBox(String line)
        {
            if (scan_listBox.InvokeRequired)
            {
                listbox_scan_listBox(line);
            }
        }
        public void Print_scan_listBox(String line, List<string> InfFile)
        {
            if (scan_listBox.InvokeRequired)
            {
                //scan_listBox.Invoke(new Action(() => scan_listBox.Items.Clear()));               
                    //for (int j = 0; j < InfFile.Count; j++)
                    //{
                     //   line = InfFile[j];
                        //scan_listBox.Invoke(new Action(() => scan_listBox.Items.Add(line)));
                     //   listbox_scan_listBox(line);
                    //}

                    listbox_scan_listBox("Кол-во вирусом: "+InfFile.Count.ToString());
                    //this.Invoke(new System.Threading.ThreadStart(delegate
                    //{
                    //    progressBar1.Value = 0;
                    //    bunifuCircleProgressbar1.Value = 0;
                    //}));
            }
        }

       // void func_timer()
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime time;
            TimeSpan value;
            string date;
            while (btimer)
            {
                this.Invoke(new System.Threading.ThreadStart(delegate
                {
                    time = DateTime.Now;
                    value = time.Subtract(timer_nach);
                    date = value.ToString().Substring(0, 8);
                    scan_label_2.Text = String.Format("Время сканирования {0}", date);
                }));
            }          
        }
               
        //void Potok1(object sender, EventArgs e)
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String line = key_create.GetValue("path").ToString();
       
            var subject = new ScanEngine();
            var observerA = new Observer_Virus_detection();
            subject.Attach(observerA);
         
            this.Invoke(new System.Threading.ThreadStart(delegate
            {
                scanir.Enabled = false;
                //progressBar1.Style = ProgressBarStyle.Marquee;
                timer_nach = DateTime.Now;
                btimer = true;
            }));

            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }

            Print_scan_listBox("Идет распаковка архивов.");
            Preparation_scan pr = new Preparation_scan();
            pr.preparation(line);
            Print_scan_listBox("Распаковка архивов закончена.");


            List<string> InfFile = subject.scan_List(line);

            this.Invoke(new System.Threading.ThreadStart(delegate
            {
                //progressBar1.Style = ProgressBarStyle.Blocks;
                btimer = false;
            }));

            subject.Detach(observerA);

            Print_scan_listBox(line, InfFile);
            this.Invoke(new System.Threading.ThreadStart(delegate
            {
                scanir.Enabled = true;
            }));
        }

        /// <summary>
        ///+ Сканировать
        /// </summary>
        private void scanir_Click_1(object sender, EventArgs e)
        {     
            // Обновить последнее сканирование
            try{key_create.SetValue("date", DateTime.Now.ToString());}
            catch (Exception err){System.Windows.MessageBox.Show("Exception: " + err.Message);}

            scan_label.Text = "Последнее сканирование: " + DateTime.Now;
  
            //Thread.Sleep(20);
            // Считываем путь, по которому надо искать
            String line = key_create.GetValue("path").ToString();

            //bw = new BackgroundWorker();
            //bw.DoWork += (obj, ea) => Potok1(sender, e);
            //bw.RunWorkerAsync();

            //ProgresBar(line);
       
            scan_listBox.Invoke(new Action(() => scan_listBox.Items.Clear()));
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }        
        }

        /// <summary>
        ///+ Добавить сигнатуру
        /// </summary>
        private void addSign_Click(object sender, EventArgs e)
        {
            addSign addSign = new addSign();
            addSign.Show();
        }

        /// <summary>
        /// +вывести список сигнатур
        /// </summary>
        private void Basesign_Click(object sender, EventArgs e)
        {      
            NW.Signatures_List(listBox1, files);           
        }

        /// <summary>
        /// +Удалить зараженный файл
        /// </summary>
        private void deleteFile_Click_2(object sender, EventArgs e)
        {
            // ищем, что было выделено в listbox1
            for (int i = 0; i < scan_listBox.Items.Count; i++)
            {
                if (scan_listBox.Items[i] == scan_listBox.SelectedItem)
                {
                    // получаем путь к файлу через его структуру
                    string Path = scan_listBox.Items[i].ToString();
                   // Path = Path.Substring(6);
                    // удаляем файл
                    File.Delete(Path);

                    // после удаления файла надо удалить 
                    // запись о файле в listBox
                    scan_listBox.Items.RemoveAt(i);

                    break;
                }
            }
        }

        /// <summary>
        /// +Удалить сигнатуру из файла
        /// </summary>
        private void deleteInfFile_Click(object sender, EventArgs e)
        {
            List<Node_Write.Virus> buffer = new List<Node_Write.Virus>();

            // ищем, что было выделено в listbox1
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i] == listBox1.SelectedItem)
                {
                    string line = key_create.GetValue("sign").ToString();

                    FileStream InfectedFilesDB = new FileStream(line, FileMode.Open, FileAccess.Read);
                    BinaryFormatter binForm = new BinaryFormatter();

                    for (int k = 0; k < listBox1.Items.Count; k++)
                    {
                        Node_Write.Virus inf = (Node_Write.Virus)binForm.Deserialize(InfectedFilesDB);
                        if (k != i)
                        {
                            buffer.Add(inf);
                        }
                    }
                    InfectedFilesDB.Close();

                    // сериализуем buffer
                    InfectedFilesDB = new FileStream(line, FileMode.Create);
                    for (int j = 0; j < buffer.Count; j++)
                    {
                        binForm.Serialize(InfectedFilesDB, buffer[j]);
                    }

                    // надо удалить запись о сигнатурев listBox
                    listBox1.Items.RemoveAt(i);

                    InfectedFilesDB.Close();
                    break;
                }
            }
        }

        public void listbox_scan_listBox(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    scan_listBox.Items.Add(str);
                }));
            }
        }


        #region Проверка Реестра
        void Proverka_Registry()
        {
           using( RegistryKey key_open = Registry.CurrentUser.OpenSubKey(@"Software\Antivirus"))
           {
               // База сигнатур
               if (key_open.GetValue("sign", null) == null)
                   key_create.SetValue("sign", "", RegistryValueKind.String);
               //Путь е папке сканирования
               if (key_open.GetValue("path", null) == null)
                   key_create.SetValue("path", "", RegistryValueKind.String);
               //Путь сканирования FileWatcher'a
               if (key_open.GetValue("filewatcher", null) == null)
                   key_create.SetValue("filewatcher", "", RegistryValueKind.String);
               //Дата последнего сканирования
               if (key_open.GetValue("date", null) == null)
                   key_create.SetValue("date", "", RegistryValueKind.String);
           }      
        }
        #endregion  Проверка Реестра

        //  file_watcher_textbox    textbox
        //  file_watcher_listBox    listbox
        //Путь отслеживания
        private void file_watcher_path_Click(object sender, EventArgs e)
        {
            // Путь сканирования

            String line = "";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                try{key_create.SetValue("filewatcher", FBD.SelectedPath);}
                catch (Exception err){System.Windows.MessageBox.Show("Exception: " + err.Message);}

                line = FBD.SelectedPath;
            }

            file_watcher_textbox.Text = "Путь к директории наблюдения: " + line;

            // Вывести все pe файлы директории в listbox
            DirectoryInfo dir = new DirectoryInfo(line);

            file_watcher_listBox.Items.Clear();

            //foreach (string f in Directory.GetFiles(line, "*.*", SearchOption.AllDirectories).Union(Directory.GetFiles(line, "*.dll", SearchOption.AllDirectories)))
            //{
            //    string[] splitpath = f.Split('\\');
            //    string name = splitpath[splitpath.Length - 1];
            //    file_watcher_listBox.Items.Add(name);
            //}
            //file_watcher_label.Text = "Кол-во файлов в данной директории: " + file_watcher_listBox.Items.Count;
        }

        //-------------------------------------------Кнопки FileWatcher-----------------------------------------
        #region Кнопки FileWatcher
        //Начать слежение
        FileWatcher watcher = new FileWatcher();
        private void file_watcher_start_Click(object sender, EventArgs e)//Кнопка старт вотчер
        {
            watcher.Attach(Observer_FileWatcher);
            file_watcher_start.Enabled = false;
            watcher.FileMonitor_Start();
        }
        //Отменить слежение
        private void file_watcher_stop_Click(object sender, EventArgs e)//Кнопка конец вотчер
        {
            watcher.Detach(Observer_FileWatcher);
            watcher.FileMonitor_Stop();
            file_watcher_start.Enabled = true;
        }
        private void file_watcher_clear_Click(object sender, EventArgs e)//Кнопка очистить листбокс вотчера
        {
            file_watcher_listBox.Items.Clear();
        }
        #endregion Кнопки FileWatcher
        //-------------------------------------------Кнопки FileWatcher-----------------------------------------

        //----------------------------------------Листбокс FileWatcher-----------------------------------------  
        #region Листбокс FileWatcher
        public void Filewatcher_listbox(string str)//Листбокс вотчера добавить
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    file_watcher_listBox.Items.Add(str);
                }));
            }
        }
        public void Filewatcher_listbox_Clear()//Листбокс вотчера очистить
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    file_watcher_listBox.Items.Clear();
                }));
            }
        }
        #endregion Листбокс FileWatcher
        //----------------------------------------Листбокс FileWatcher-----------------------------------------

      
        //Drag and Drop

        void Potok3(string obj, List<string> paths, ScanEngine Seng, Preparation_scan pr)
        {
            var observerDragandDrop = new Observer_Virus_detection();
            Seng.Attach(observerDragandDrop);


            Print_scan_listBox("Идет распаковка архивов.");
            pr.preparation(obj);
            Print_scan_listBox("Распаковка архивов закончена.");


            paths = Seng.scan_List(obj);           
            Print_scan_listBox("Кол-во вирусом: " + paths.Count.ToString());
            Seng.Detach(observerDragandDrop);
        }

        void Potok4(string line)
        {

            Print_scan_listBox("Идет распаковка архивов.");
            Preparation_scan pr = new Preparation_scan();
            pr.preparation(line);
            Print_scan_listBox("Распаковка архивов закончена.");

            ScanEngine Seng = new ScanEngine();
            var Observer_Virus_detection_DaD = new Observer_Virus_detection();
            Seng.Attach(Observer_Virus_detection_DaD);
            string full_path_file = Path.GetDirectoryName(line) + @"\" + Path.GetFileNameWithoutExtension(line);

            this.Invoke(new System.Threading.ThreadStart(delegate
            {
   
                scanir.Enabled = false;
                timer_nach = DateTime.Now;
                btimer = true;
            }));
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }

            List<string> InfFile = Seng.scan_List(full_path_file);
            Print_scan_listBox(full_path_file, InfFile);
            Seng.Detach(Observer_Virus_detection_DaD);
            this.Invoke(new System.Threading.ThreadStart(delegate
            {
                btimer = false;
            }));
        }
        void DragAndDrop_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
           List<string> paths = new List<string>();
             ScanEngine Seng = new ScanEngine();
             Preparation_scan pr = new Preparation_scan();

           foreach (string obj in (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop))
               if (Directory.Exists(obj))
               {
                   bw = new BackgroundWorker();
                   bw.DoWork += (obj2, ea) => Potok3(obj, paths, Seng, pr);
                   bw.RunWorkerAsync();
               }
               else
               {
                   string file_no_raz = (Path.GetDirectoryName(obj) + @"\" + Path.GetFileNameWithoutExtension(obj) + ".zip");
                   if (file_no_raz== obj)
                   {
                       bw2 = new BackgroundWorker();
                       bw2.DoWork += (obj2, ea) => Potok4(obj);
                       bw2.RunWorkerAsync();
                       continue;
                   }

                   if (Seng.scan_String(Path.GetDirectoryName(obj), obj))
                   {
                       //string line = String.Format("Файл является вирусом! {0}", obj);
                       scan_listBox.Items.Add("Файл является вирусом!");
                       scan_listBox.Items.Add(obj);
                   }
                   else
                   {
                       string line  = String.Format("Файл не является вирусом {0}", obj);
                       scan_listBox.Items.Add(line);
                   }
               }
        }


        //-----------------------Drag and Drop Путь к сигнатурам-------------------------------------
        string text;
        private void signPath_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                text = signPath.Text;
                signPath.Text = "Перетащите файл с базой сигнатур";
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
        }

        private void signPath_DragLeave(object sender, EventArgs e)
        {
            signPath.Text = text;
        }

        private void signPath_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //foreach (string obj in (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop))
            //{
            //    signPath.Text = "Путь сканирования: " + obj;
            //}
            string[] files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
            signPath.Text = "Путь сканирования: " + files[0];

            key_create.SetValue("sign", files[0]);
        }

        //-----------------------Drag and Drop Путь к сигнатурам-------------------------------------
    }
}

