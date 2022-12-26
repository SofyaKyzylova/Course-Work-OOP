using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class CompareForm : Form
    {
        public Pyramid<CCity> P;
        public CompareForm()
        {
            InitializeComponent();
        }

        private void CompareForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar;
            if ((a < 'А' || a > 'я') && (a < 'A' || a > 'z') && a != '\b' && a != ' ')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Вы ничего не ввели! Пожалуйста, введите название города");
            }
            else
            {
                CCity D = new CCity();
                CCity D1 = new CCity();
                for (int i = 0; i < P.HeapSize; i++)
                {
                    if (P.Arr[i].getName() == textBox1.Text)
                    {
                        D = P.Arr[i];
                        break;
                    }
                }
                for (int i = 0; i < P.HeapSize; i++)
                {
                    if (P.Arr[i].getName() == textBox2.Text)
                    {
                        D1 = P.Arr[i];
                        break;
                    }
                }
                if (D.name == " " || D1.name == " ")
                {
                    MessageBox.Show("Города с таким названием нет в списке. Пожалуйста, повторите ввод");
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else
                {
                   int t = D.CompareTo(D1);
                    if(t == 1)
                        textBox3.Text = "больше";
                    if (t == -1)
                        textBox3.Text = "меньше";
                    if (t == 0)
                        textBox3.Text = "равно";
                }
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }
    }
}
