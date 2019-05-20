using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiVirus
{
    public interface IObserver
    {
        // Получает обновление от издателя
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        // Присоединяет наблюдателя к издателю.
        void Attach(IObserver observer);

        // Отсоединяет наблюдателя от издателя.
        void Detach(IObserver observer);

        // Уведомляет всех наблюдателей о событии.
        void Notify();
    }
    //Обнаружение вирусов
    class Observer_Virus_detection : IObserver
    {
        public async void Update(ISubject subject)
        {

            if ((subject as ScanEngine).Observer_str.Count > 0)
            {
                Program.fm1.Print_scan_listBox((subject as ScanEngine).Observer_str2);
            }
        }
    }

    //Слежение за изменениями в папке
    class Observer_Virus_FileWatcher : IObserver
    {
        public async void Update(ISubject subject)
        {
            string line;
            if ((subject as FileWatcher).BoolPath_Create)
            {
                line = String.Format("FileWatcher: Обнаружен новый файл, передаю его сканеру:{0}", (subject as FileWatcher).FullPath_Create.ToString());
                Program.fm1.Filewatcher_listbox(line);

                ScanEngine wf = new ScanEngine();
                bool virus = false;
                virus = wf.scan_String((subject as FileWatcher).Put, (subject as FileWatcher).FullPath_Create.ToString());

                if (virus)
                {
                    line = String.Format("FileWatcher: Новый файл является вирусом! {0}", (subject as FileWatcher).FullPath_Create.ToString());
                    Program.fm1.Filewatcher_listbox(line);
                }
                else
                {
                    line = String.Format("FileWatcher: Обнаруженный файл не является вирусом {0}", (subject as FileWatcher).FullPath_Create.ToString());
                    Program.fm1.Filewatcher_listbox(line);
                }
                
            }

            if ((subject as FileWatcher).BoolPath_Deleted)
            {
                line = String.Format("FileWatcher: Удален файл! {0}",(subject as FileWatcher).FullPath_Deleted.ToString());
                Program.fm1.Filewatcher_listbox(line);
            }
        }
    }


}
