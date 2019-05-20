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
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Win32;
namespace AntiVirus
{
    public partial class ScanEngine : ISubject
    {
        RegistryKey key_create = Registry.CurrentUser.CreateSubKey(@"Software\Antivirus");
            // --------------------------------Наблюдатель------------------------------------------------------
            public List<string> Observer_str = new List<string>();
            public string Observer_str2 {get;set;}
            //public int detected_virus { get; set; }
            private List<IObserver> _observers = new List<IObserver>();

            // Методы управления подпиской.
            public void Attach(IObserver observer)
            {
                Console.WriteLine("Антивирус: Добавлен наблюдатель.");
                this._observers.Add(observer);
            }
             public void Detach(IObserver observer)
            {
                this._observers.Remove(observer);
                Console.WriteLine("Антивирус: Удаление наблюдателя.");
            }
            public void Notify()
            {
                Console.WriteLine("Антивирус: Уведомление наблюдателей...");

                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }

         // --------------------------------Наблюдатель------------------------------------------------------

        string[] byte_identif = new string[] {"4D 5A"};
        public string Path_Last_Deleted { get; set; }

        /// <summary>
        /// Указать путь к файлу
        /// </summary>
        /// <returns>Путь к файлу</returns>
        public string pathScan()
        {
            String line = "";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //StreamWriter sw = new StreamWriter(key_create.GetValue("path").ToString());
                    //sw.WriteLine(FBD.SelectedPath);
                    //sw.Close();
                    key_create.SetValue("path", FBD.SelectedPath);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Exception: " + err.Message);
                }

                line = FBD.SelectedPath;
            }
            return line;
        }

        /// <summary>
        /// Получить список сигнатур
        /// </summary>
        /// <returns>Список сигнатур</returns>
        public List<Node_Write.Virus> getVirusDB()
        {
            Form1 f1 = new Form1();
            BinaryFormatter binForm = new BinaryFormatter();

            // узнать путь к файлу
            string line = key_create.GetValue("sign").ToString();

            FileStream virusDataBase = new FileStream(line, FileMode.Open);
            List<Node_Write.Virus> dataBase = new List<Node_Write.Virus>();
            while (virusDataBase.Position < virusDataBase.Length)
            {
                dataBase.Add((Node_Write.Virus)binForm.Deserialize(virusDataBase));    
            }
            virusDataBase.Close();
            return dataBase;
        }
    
        // ПРоверка первых 8 байтов заголовка на исполняемый файл
        private bool scan_Byte_header(string path)
        {
            string str;
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var buffer = new byte[2];
                fs.Read(buffer, 0, buffer.Length);
                str = string.Join(" ", buffer.Select(b => b.ToString("X2")));
            }
            if (str == byte_identif[0])
                return true;
            return false;
        }
        //Проверка первых 8 байт исполняемоей секции
        private bool scan_Byte_body(string path, string f, addSign aS, Balanced_binary_tree_search tree)
        {
            string str;
            Node_Write.Virus? virus;
            using (FileStream file = new FileStream(f, FileMode.Open))
            {
                // узнаем, где у него исп секция
                int[] data = aS.exeSect(file);
                // смещаем его на начало исп секции
                file.Seek(data[1], SeekOrigin.Begin);

                var buffer = new byte[8];
                file.Read(buffer, 0, buffer.Length);
                str = string.Join(" ", buffer.Select(b => b.ToString("X2")));
            }
            virus = tree.Find_body(str);
            if (virus != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сканировать директорию
        /// </summary>
        /// <param name="path">Путь к директории</param>
        /// <param name="sign">База сигнатур</param>
        public List<string> scan_List(string path)
        {
            // создаем АВЛ дерево
            List<Node_Write.Virus> sign = getVirusDB();
            Balanced_binary_tree_search tree = new Balanced_binary_tree_search();

            // Заполняем дерево сигнатурами
            for (int i = 0; i < sign.Count; i++)
                tree.Add(sign[i]);

            addSign aS = new addSign();
            List<string> str = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(path);


            
            foreach (string f in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Union(Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)))
            {
                if (!Program.fm1.backgroundWorker1.CancellationPending)
                {
                string full_path_file = Path.GetDirectoryName(f) +@"\"+ Path.GetFileNameWithoutExtension(f)+".zip";
                if (f == full_path_file)
                    continue;
                if (!scan_Byte_header(f))// Tru = exe || all False - другой мусор
                    continue;                           
                if (!scan_Byte_body(path,f,aS,tree))// Провекрка 8 байт исполняемой секции. True - скорее всего вирус False - исполняемая секция не равна 
                    continue;
            
                FileStream file;
                // Открываем файл
                try {file = new FileStream(f, FileMode.Open); }
                catch (Exception){continue;}

                Node_Write.Virus? virus;
                string signHash, signHash2;
                // узнаем, где у него исп секция
                int[] data = aS.exeSect(file);
                // смещаем его на начало исп секции
                file.Seek(data[1], SeekOrigin.Begin);

                // прогоняем для всей исп области, смещаясь
                // каждый раз на 1 байт и беря 8 байт как длину
                // возможного вируса
                while (file.Position + 8 < data[1] + data[0])
                {
                    // получаем блок кода из сканируемого файла
                    // на месте возможного вируса длиной 8 байт
                    signHash = aS.read(file, 8);
                    // хешируем его
                    signHash = aS.hash(signHash);
                    // сравниваем с помощью АВЛ по хешу
                    // если возвращаемое значение не null, то
                    // это, возможно, зараженный файл и нужно
                    // проверить полный хеш
                    virus = tree.Find(signHash);
                    if (virus != null)
                    {
                        // возвращаемся назад на 8 байт
                        file.Seek(-8, SeekOrigin.Current);
                        // читаем длину сигнатуры
                        signHash2 = aS.read(file, ((Node_Write.Virus)virus).length);
                        signHash2 = aS.hash(signHash2);
                        // если полные хеши равны, то это точно зараженный файл
                        if (signHash2 == ((Node_Write.Virus)virus).hash)
                        {
                            Console.WriteLine("\nАнтивирус: Обнаруженена угроза.");
                            this.Observer_str.Add(f);
                            this.Observer_str2 = f;
                            this.Notify();
                            //Program.fm1.Print_scan_listBox(f);
                            // добавляем в str путь к файлу
                            str.Add(f);
                            // ищем сигнатуру - источник заражения и
                            // прибавляем ей число обнаружений
                            for (int i = 0; i < sign.Count; i++)
                            {
                                if (sign[i].hash == ((Node_Write.Virus)virus).hash)
                                {
                                    //так делается, ибо это ошибка компилятора
                                    Node_Write.Virus vir = sign[i];
                                    vir.detect++;
                                    sign[i] = vir;
                                }
                            }
                            break;
                        }
                        // отменяем смещения
                        file.Seek(8 - ((Node_Write.Virus)virus).length, SeekOrigin.Current);
                    }
                    // идем назад на 8 байт и смещаем на 1 байт
                    file.Seek(-8 + 1, SeekOrigin.Current);
                }
                file.Close();

                }
            }

            // Путь к файлу базы сигнатур
            string line = key_create.GetValue("sign").ToString();

            // сериализуем структуру (превращаем в бинарную форму)
            FileStream VDB = new FileStream(line, FileMode.Create, FileAccess.Write);
            BinaryFormatter binForm = new BinaryFormatter();
            // добавляем в VDB (файл базы сигнатур) объект sign
            // оно добавится туда в 2ой форме и сложно будет что-то понять
            // чтобы что-то понять, надо будет десериализовывать

            for (int i = 0; i < sign.Count; i++)
            {
                binForm.Serialize(VDB, sign[i]);
            }
            VDB.Close();

            return str;
        }

        //Сканер для Наблюдателя
        public bool scan_String(string path, string f)
        {
            string full_path_file = Path.GetDirectoryName(f) + @"\" + Path.GetFileNameWithoutExtension(f) + ".zip";
            if (f == full_path_file)
                return false;
            if (!scan_Byte_header(f))// Tru = exe || all False - другой мусор
                return false;

            List<Node_Write.Virus> sign = getVirusDB();
            Balanced_binary_tree_search tree = new Balanced_binary_tree_search();

            // создаем АВЛ дерево из сигнатур
            for (int i = 0; i < sign.Count; i++)
            {
                tree.Add(sign[i]);
            }
            addSign aS = new addSign();

            if (!scan_Byte_body(path, f, aS, tree))// Провекрка 8 байт исполняемой секции. True - скорее всего вирус False - исполняемая секция не равна 
                return false;

            FileStream file;
            // Открываем файл
            try { file = new FileStream(f, FileMode.Open); }
            catch (Exception) { return false; }

            bool check = false;
            // создаем АВЛ дерево    
            List<string> str = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(path);

            Node_Write.Virus? virus;
            string signHash, signHash2;
            // узнаем, где у него исп секция
            int[] data = aS.exeSect(file);
            // смещаем его на начало исп секции
            file.Seek(data[1], SeekOrigin.Begin);

            // прогоняем для всей исп области, смещаясь
            // каждый раз на 1 байт и беря 8 байт как длину
            // возможного вируса
            while (file.Position + 8 < data[1] + data[0])
            {
                // получаем блок кода из сканируемого файла
                // на месте возможного вируса длиной 8 байт
                signHash = aS.read(file, 8);
                // хешируем его
                signHash = aS.hash(signHash);
                // сравниваем с помощью АВЛ по хешу
                // если возвращаемое значение не null, то
                // это, возможно, зараженный файл и нужно
                // проверить полный хеш
                virus = tree.Find(signHash);
                if (virus != null)
                {
                    // возвращаемся назад на 8 байт
                    file.Seek(-8, SeekOrigin.Current);
                    // читаем длину сигнатуры
                    signHash2 = aS.read(file, ((Node_Write.Virus)virus).length);
                    signHash2 = aS.hash(signHash2);
                    // если полные хеши равны, то это точно зараженный файл
                    if (signHash2 == ((Node_Write.Virus)virus).hash)
                    {
                        check = true;
                        // добавляем в str путь к файлу
                        str.Add(f);
                        // ищем сигнатуру - источник заражения и
                        // прибавляем ей число обнаружений
                        for (int i = 0; i < sign.Count; i++)
                        {
                            if (sign[i].hash == ((Node_Write.Virus)virus).hash)
                            {
                                //так делается, ибо это ошибка компилятора
                                Node_Write.Virus vir = sign[i];
                                vir.detect++;
                                sign[i] = vir;
                            }
                        }
                        break;
                    }
                    // отменяем смещения
                    file.Seek(8 - ((Node_Write.Virus)virus).length, SeekOrigin.Current);
                }
                // идем назад на 8 байт и смещаем на 1 байт
                file.Seek(-8 + 1, SeekOrigin.Current);
            }
            file.Close();         

            // Путь к файлу базы сигнатур
            string line = key_create.GetValue("sign").ToString();

            // сериализуем структуру (превращаем в бинарную форму)
            FileStream VDB = new FileStream(line, FileMode.Create, FileAccess.Write);
            BinaryFormatter binForm = new BinaryFormatter();
            // добавляем в VDB (файл базы сигнатур) объект sign
            // оно добавится туда в 2ой форме и сложно будет что-то понять
            // чтобы что-то понять, надо будет десериализовывать



            for (int i = 0; i < sign.Count; i++)
            {
                binForm.Serialize(VDB, sign[i]);
            }
            VDB.Close();

            return check;
        }



  
    }
}
