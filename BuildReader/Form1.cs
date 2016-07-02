using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        private Hook _hook;

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        int i = 0;
        string PATH_STRING;
        const int FONT_SIZE = 35;
        const FontStyle FONT_STYLE = FontStyle.Bold;
        SizeF stringSize;
        public Form1()

        {
            InitializeComponent();

            

            keybd_event(0x20, 0x45, 0x1, (UIntPtr)0);

            _hook = new Hook(0x20); //Передаем код клавиши на которую ставим хук, тут это Space

            _hook.KeyPressed += new KeyPressEventHandler(_hook_KeyPressed);
            _hook.SetHook();
            //this.TopMost = true;
        }


        void _hook_KeyPressed(object sender, KeyPressEventArgs e) //Событие нажатия клавиш
        {          
            if (global.vkl == true)
            {
                global.stroka = global.readbuild.ReadLine();
                myMethod(global.stroka); //вызываем метод, который выводит строку на экран

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream bit = new FileStream(global.guide_path + textBox1.Text + ".build", FileMode.OpenOrCreate);
            byte[] array = System.Text.Encoding.Default.GetBytes(textBox2.Text);
            bit.Write(array, 0, array.Length);
            bit.Close();
        }
        private void Get_Path()
        {
            using (var dialog = new FolderBrowserDialog())
            {            
                if (dialog.ShowDialog() == DialogResult.OK)
                    global.guide_path = dialog.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            global.guide_path = Properties.Settings.Default.path;
            if (global.guide_path != "") {
                global.files = Directory.GetFiles(global.guide_path);
            }
            else
            {
                Get_Path();
            }
            listBox1.Items.Clear();
            string nf;
            for (int i = 0; i < global.files.Length; i++)
            {
                nf = global.files[i];
                nf = nf.Remove(0, 32);
                nf = nf.Replace(".build", "");
                listBox1.Items.Add(nf);
            }
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (global.guide_path != "")
            {
                global.files = Directory.GetFiles(global.guide_path, "*.build");
            }
            else
            {
                Get_Path();
            }
            listBox1.Items.Clear();
            string nf;
            for (int i = 0; i < global.files.Length; i++)
            {
                nf = global.files[i];
                while (nf.IndexOf(@"\") >= 0)
                {
                    nf=nf.Remove(0, 1);
                }
                nf = nf.Remove(nf.IndexOf("."), 6);
                listBox1.Items.Add(nf);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            String s;
            s = listBox1.SelectedItem.ToString();
            global.fs = new FileStream(global.guide_path+"/"+listBox1.Text+".build",FileMode.Open); 
            global.readbuild = new StreamReader(global.fs,System.Text.Encoding.Default);
            global.stroka = global.readbuild.ReadLine();
            myMethod(global.stroka);
            global.vkl = true;
        }
        void myMethod(string text)
        {
            if (text != "")
            {
                // вот здесь устанавливай нужное значение строки
                PATH_STRING = text;
                ++i;

                //Получаем оконные координаты верхней левой точки клиентской области
                Point origin = new Point(SystemInformation.Border3DSize.Width, SystemInformation.CaptionHeight);
                //При помощи GraphicsPath создаем Region в виде строки
                GraphicsPath path = new GraphicsPath();

                path.AddString(PATH_STRING, Font.FontFamily, (int)FONT_STYLE, FONT_SIZE, origin, StringFormat.GenericDefault);

                //Устанавливаем регион для формы
                Region = new Region(path);
                //Вычисляем размеры прямоугольника, занимаемого строкой
                stringSize = CreateGraphics().MeasureString(PATH_STRING, new Font(Font.FontFamily, FONT_SIZE, FONT_STYLE));
                Width = (int)Math.Ceiling(stringSize.Width);
            }
            else
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, Color.Blue, Color.Red, 10);
            e.Graphics.FillRectangle(brush, 0, 0, stringSize.Width, stringSize.Height);
        }

        //Чтобы наше окно можно было "тягать" мышью, "скажем" системе что вся его область является заголовком,
        //захватив который, как известно, можно перемещать окно
        const int WM_NCHITTEST = 0x0084;
        const int HTCAPTION = 2;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTCAPTION;
                return;
            }
            base.WndProc(ref m);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Get_Path();
            }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.path = global.guide_path;
        }
    }
    }
