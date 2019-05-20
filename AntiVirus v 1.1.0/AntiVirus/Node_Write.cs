using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AntiVirus
{
    public partial class Node_Write
    {
        ScanEngine F2 = new ScanEngine();

        // Вирусы, для базы вирусов
        [Serializable]
        public struct Virus
        {
            public int      length;         //Длина сигнатуры
            public int      lenght_sec;     //Длина исполняемой секции
            public int      first_sec;      //Начало исполняемой секции
            public string   hash;           //Хеш
            public string   hash8;          //Хеш первых 8 символов из файла
            public string   name;           //Название вируса
            public int      detect;         //Кол-во обнаружений при сканировании
            public string   byts_header;    //Байты идентификации заголовка
            public string   byts_body;      //Байты идентификации исполняемой облости  
        }

        //вывести список сигнатур
        public void Signatures_List(ListBox listbox, Label files)
        {
            string line;

            List<Virus> sign = F2.getVirusDB();

            files.Text = "Кол-во записей в файле: " + sign.Count;

            listbox.Items.Clear();
            for (int i = 0; i < sign.Count; i++)
            {
                line = "Имя: " + sign[i].name +
                    ", хеш: " + sign[i].hash +
                    ", хеш8: " + sign[i].hash8 +
                    ", длина: " + sign[i].length +
                    ", byte header: " + sign[i].byts_header +
                    ", byte body: " + sign[i].byts_body +
                    ", начало исполняемой секции: " + sign[i].first_sec +
                    ", длина исполняемой секции: " + sign[i].lenght_sec +
                    ", кол-во заражений: " + sign[i].detect;

                listbox.Items.Add(line);
            }


        }
    }
}
