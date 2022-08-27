using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//мои юзынги :)
using System.Runtime.InteropServices; //нужно для очистки корзины




namespace Cleaner
{
    public partial class Form1 : Form
    {
       //Небольше пояснение.Тут будут обьявлятся разные переменные то есть путь к файлам и папкам.Так вот путь начинается с такоого кода

       //    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)

       //    Это значит абсолютный путь то есть он будет искать нужнную папку в залогином пользователе на диске c:\

       //    Например C:\User\твое имя\App data\Local\ и тут идет далее нужная папка или файл





        //простая переменная cookies эта в папке Network , а cookies 2 просто в общей папке default это нужно для проверки где файл есть вообще
        string cookies = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Network\Cookies");
        string cookies2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Cookies");

        //dirinfo это переменная где хранится путь к папке cache 
        DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Cache"));

        //кэш хрома
        string indexDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\IndexedDB");
        string Code_Cache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Code Cache");

        //папка Temp
        string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Temp");


        //этот код нужен для очитски корзины

        enum RecycleFlags : int
	    {
        // No confirmation dialog when emptying the recycle bin
        SHERB_NOCONFIRMATION = 0x00000001,

	    // No progress tracking window during the emptying of the recycle bin

	    SHERB_NOPROGRESSUI = 0x00000001,

	    // No sound whent the emptying of the recycle bin is complete

	    SHERB_NOSOUND = 0x00000004

        }

	    [DllImport("Shell32.dll")]

	    // The signature of SHEmptyRecycleBin (located in Shell32.dll)
	    static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);





        public Form1()
        {
            InitializeComponent();
        }


        //Кнопка при нажатии которой будет происходит очистка хрома
        private void button1_Click(object sender, EventArgs e)
        {
            //этот код нужен для очитски буфер обмена
            if (checkBox6.Checked == true)
            {
                Clipboard.Clear();
                textBox1.Text += "Буфер обмена очищен" + "\r\n";
            }


            //этот код нужен для очитски корзины
            if (checkBox5.Checked == true)
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOSOUND | RecycleFlags.SHERB_NOCONFIRMATION);
                textBox1.Text += "Корзина очищена" + "\r\n";
            }

            //Очистить куки хрома
            if (checkBox1.Checked == true)
            {
                if (File.Exists(cookies))
                {
                    File.Delete($"{cookies}");
                    textBox1.Text += ($"Удален {cookies}" + "\r\n");
                }
                else if (File.Exists(cookies2))
                {
                    File.Delete($"{cookies2}");
                    textBox1.Text += ($"Удален {cookies2}" + "\r\n");
                }
                else
                {
                    textBox1.Text += ($"Файл куки не был найден" + "\r\n");
                }
                }



            //Очистить историю хрома
            if (checkBox2.Checked == true)
            {
                string History = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\History");
                string Favicons = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Favicons");
                string Shortcuts = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Shortcuts");
                string Shortcuts_journal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Shortcuts-journal");
                string Network_Action_Predictor = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Network Action Predictor");
                string Network_Action_Predictor_journal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Network Action Predictor-journal");
                string Visited_Links = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Visited Links");


                //тут я проверяю через if если файл существует то тогда выполняю удаления.Также я проверяю через && чтобы проверить несколько файлов так получается код короче
                if (File.Exists(History) && File.Exists(Favicons) && File.Exists(Shortcuts) && File.Exists(Shortcuts_journal) && File.Exists(Network_Action_Predictor) && File.Exists(Network_Action_Predictor_journal)  && File.Exists(Visited_Links))
                {
                    File.Delete($"{History}");
                    File.Delete($"{Favicons}");
                    File.Delete($"{Shortcuts}");
                    File.Delete($"{Shortcuts_journal}");
                    File.Delete($"{Network_Action_Predictor}");
                    File.Delete($"{Network_Action_Predictor_journal}");
                    File.Delete($"{Visited_Links}");

                    textBox1.Text += ($"Удален {History} , '\r\n'Удален {Favicons},'\r\n'Удален {Shortcuts},'\r\n'Удален {Shortcuts_journal},'\r\n'Удален {Network_Action_Predictor},'\r\n'Удален {Network_Action_Predictor_journal},'\r\n'Удален {Visited_Links} " + "\r\n");
                }
                //если файлов нету тут будет написано что уже удалено
                else
                {
                    textBox1.Text += ($"Уже удалено {History} , '\r\n'Уже удалено {Favicons},'\r\n'Уже удалено {Shortcuts},'\r\n'Уже удалено {Shortcuts_journal},'\r\n'Уже удалено {Network_Action_Predictor},'\r\n'Уже удалено {Network_Action_Predictor_journal},'\r\n'Уже удалено {Visited_Links} " + "\r\n");
                }


                
            }
            


            //Очистить кэш хрома Cache
            if (checkBox3.Checked == true)
            {
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();                   
                }
                textBox1.Text += ($"Папка очищена {dirInfo}" + "\r\n");

            }


            //Очистить кэш хрома indexDB
            if (checkBox3.Checked == true)
            {
                 Directory.Delete(indexDB, true); //true - если директория не пуста удаляем все ее содержимое
                 Directory.CreateDirectory(indexDB);
                 textBox1.Text += ($"Папка очищена {indexDB}" + "\r\n");                          
            }

            //Очистить кэш хрома Code_cach
            if (checkBox3.Checked == true)
            {
                Directory.Delete(Code_Cache, true); //true - если директория не пуста удаляем все ее содержимое
                Directory.CreateDirectory(Code_Cache);
                textBox1.Text += ($"Папка очищена {Code_Cache}" + "\r\n");
            }

            //Очистить папку Temp . Тут просто проверяется если она существует то удалить всю папку.Но если допустим она удалена или ее нет то напишет что "уже удалена"
            if (checkBox4.Checked == true)
            {
                if (File.Exists(temp))
                {
                    Directory.Delete(temp, true); //true - если директория не пуста удаляем все ее содержимое
                    Directory.CreateDirectory(temp);
                    textBox1.Text += ($"Папка очищена {temp}" + "\r\n");
                }
                else
                {
                    textBox1.Text += ($"Папка Temp уже удалена" + "\r\n");
                }
            }




            //после нажатия кнопки очистить  появиться всплавающее окно что очистка завершена
            DialogResult result = MessageBox.Show("Очистка завершена.", "Уведомление", MessageBoxButtons.OK);

        }





        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //эта кнопка "очитсить журнал удаления" нужна чтобы очистить список всего удаленного
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();

        }

        //просто выйди из проги через меню
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
