using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using Microsoft.Win32;
namespace AntiVirus
{
    class FileWatcher : ISubject
    {
        // --------------------------------Наблюдатель------------------------------------------------------
        private List<IObserver> _observers = new List<IObserver>();

        // Методы управления подпиской.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("FileWatcher: Добавлен наблюдатель.");
            this._observers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("FileWatcher: Удаление наблюдателя.");
        }
        public void Notify()
        {
            Console.WriteLine("FileWatcher: Уведомление наблюдателей...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
        // --------------------------------Наблюдатель------------------------------------------------------
        FileSystemWatcher FileMonitor = new FileSystemWatcher();
        RegistryKey key_create = Registry.CurrentUser.CreateSubKey(@"Software\Antivirus");
        public string Put {get;set;}
        private string Filter = "*.*";
        private bool IncludeSubs = true;
        private System.ComponentModel.Container components = null;
        public string FullPath_Create { get; set; }
        public string ShortPath_Create { get; set; }
        public bool BoolPath_Create { get; set; }

        public string FullPath_Deleted { get; set; }
        public string ShortPath_Deleted { get; set; }
        public bool BoolPath_Deleted { get; set; }

        // Изменение названия 
        private void FileMonitor_OnRenamed(object source, RenamedEventArgs e)
        {
            //Старое имя
            string originalname = e.OldFullPath; 
            //Новое имя
            string renamed = e.FullPath;
            //Console.WriteLine("File: " + originalname + " renamed to " + renamed, e.OldName + " Renamed");
            string line;
            line = String.Format("File: {0} renamed to {1}, {2} Renamed", originalname, renamed, e.OldName);
            Program.fm1.Filewatcher_listbox(line);
            
        }

        //Выбор изменений произошедших с файлом 
        private void FileMonitor_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            string ChangeType = e.ChangeType.ToString();
            //Добавлен
            if (ChangeType == "Created")
            {
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType, e.Name + " Created");
                FullPath_Create = e.FullPath;
                ShortPath_Create = e.Name;
                BoolPath_Create = true;
                this.Notify();
                BoolPath_Create = false;
            }//Удален
            else if (ChangeType == "Deleted")
            {
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType, e.Name + " Deleted");
                FullPath_Deleted = e.FullPath;
                ShortPath_Deleted = e.Name;
                BoolPath_Deleted = true;
                this.Notify();
                BoolPath_Deleted = false;
            }//
            else if (ChangeType == "Changed")
            {
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType, e.Name + " Changed");
            }
        }

        //Начало слежения
        public void FileMonitor_Start()
        {            
            FileMonitor.Deleted += new System.IO.FileSystemEventHandler(FileMonitor_Changed);
            FileMonitor.Renamed += new System.IO.RenamedEventHandler(FileMonitor_OnRenamed);
            FileMonitor.Changed += new System.IO.FileSystemEventHandler(FileMonitor_Changed);
            FileMonitor.Created += new System.IO.FileSystemEventHandler(FileMonitor_Changed);

            Put = key_create.GetValue("filewatcher").ToString();

            //Путь до папки из textbox
            if (Put == "")
            {
                Console.WriteLine("Директория не указана!");
                return;
            }
           
            //Настройка параметров для монитора
            //Путь до папки
            //FileMonitor.Path = Path.ToString();
            FileMonitor.Path = Put.ToString();
            //Фильтр
            FileMonitor.Filter = Filter.ToString();
            //Вкл/Выкл поддирикторий
            FileMonitor.IncludeSubdirectories = IncludeSubs;

            FileMonitor.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            //Начать мониторингь
            FileMonitor.EnableRaisingEvents = true;
            Program.fm1.Filewatcher_listbox("FileWatcher Работает");
        }

        // Остановка слежения
        public void FileMonitor_Stop()
        {
            FileMonitor.EnableRaisingEvents = false;
            Program.fm1.Filewatcher_listbox("FileWatcher Отключен");
        }



    }
}
