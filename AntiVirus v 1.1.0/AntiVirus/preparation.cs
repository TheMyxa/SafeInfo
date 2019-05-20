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

namespace AntiVirus
{
    public class Preparation_scan
    {
        /// <summary>
        /// Распаковка архивов
        /// </summary>
        /// <param name="path">Дириктория</param>
        /// <returns>void</returns>
        public void preparation(string path)
        {

            string file_no_raz = (Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + ".zip");
            if (path == file_no_raz)
            {
                string path_file2 = vlog1(Path.GetDirectoryName(path), path, Path.GetFileNameWithoutExtension(path), Path.GetDirectoryName(path));
                return;
            }

            string path_file="";
            foreach (string f in Directory.GetFiles(path, "*.zip", SearchOption.AllDirectories))
            {
                if (!GetDirectori(Path.GetFileNameWithoutExtension(f), Path.GetDirectoryName(f)))
                    path_file = vlog1(path, f,Path.GetFileNameWithoutExtension(f), Path.GetDirectoryName(f));
                else
                    Program.fm1.listbox_scan_listBox(String.Format("Данный архив уже распакован: {0}", Path.GetDirectoryName(f)));
            }
        }


        /// <summary>
        /// Узнаем есть ли такая дириктория.
        /// </summary>
        /// <param name="name_file">Имя файла</param>
        /// <param name="path_file_folder">Имя директории</param>
        /// <returns>true-есть такая / false- нету</returns>
        private bool GetDirectori(string name_file, string path_file_folder)
        {
            string extractPath = path_file_folder + @"\" + name_file;
            if (Directory.Exists(extractPath))
                return true;            
            return false;
        }

        /// <summary>
        /// Распаковка архива
        /// </summary>
        /// <param name="root_path">Путь корневой папки</param>
        /// <param name="path_file_name">Путь с именем файла</param>
        /// /// <param name="name_file">Имя файла</param>
        /// /// <param name="path_file_folder">Имя директории</param>
        /// <returns>path_file_name</returns>
        public string vlog1(string root_path, string path_file_name, string name_file, string path_file_folder)
        { 
            string extractPath = path_file_folder + @"\" + name_file; // куда распокавать   
            try
            {
                ZipFile.ExtractToDirectory(path_file_name, extractPath);
                //File.Delete(path_file);
                vlog2(extractPath);
            }
            catch (Exception)
            {              
                Program.fm1.listbox_scan_listBox(String.Format("Данный архив уже распакован: {0}", path_file_name));
                return path_file_name;
            }
            return path_file_name;
        }
        
        /// <summary>
        /// Проверка папки на архивы
        /// </summary>
        /// <param name="path">Путь Обнаружения</param>
        /// <returns>void</returns>
        public void vlog2(string path)//если внутри есть архив распаковать
        {
            foreach (string f in Directory.GetFiles(path, "*.zip", SearchOption.AllDirectories))
            {
                vlog1(path, f, Path.GetFileNameWithoutExtension(f),Path.GetDirectoryName(f));
            }
        }
    }
}
