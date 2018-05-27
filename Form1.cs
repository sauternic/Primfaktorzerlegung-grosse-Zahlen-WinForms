using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Primfaktorzerlegung
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            button1.Enabled = false;
            await Task.Run(new Action(Zerlegen));
            button1.Enabled = true;

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PotenzSchreibweise();
            button2.Enabled = false;
        }


        private void PotenzSchreibweise()
        {
            string str = textBox2.Text;
            string[] strArr = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //Textbox Leeren:
            textBox2.Text = "";


            bool flag = true;
            string temp = "";
            Stack<string> ausgabe = new Stack<string>();
            int zaehler = 0;
            foreach (string s in strArr)
            {
                if (flag)
                {
                    //Hier nur einmal vorbei!
                    temp = s;
                    flag = false;
                }

                if (temp == s)
                {
                    ++zaehler;
                    if (zaehler == 1)
                    {
                        ausgabe.Push(s);
                    }
                    else
                    {
                        ausgabe.Push(s + " hoch " + zaehler);
                    }
                }
                else
                {
                    //Ausgabe:
                    textBox2.Text += ausgabe.Pop() + "\r\n";

                    zaehler = 1;
                    if (zaehler == 1)
                    {
                        ausgabe.Push(s);
                    }
                    else
                    {
                        ausgabe.Push(s + " hoch " + zaehler);
                    }


                    temp = s;
                }


            }
            //Ausgabe:
            textBox2.Text += ausgabe.Pop() + "\r\n";

        }


    }
}
