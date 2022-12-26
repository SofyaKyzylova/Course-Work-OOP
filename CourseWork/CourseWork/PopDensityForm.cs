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
    public partial class PopDensityForm : Form
    {
        public Pyramid<CCity> P;
        public PopDensityForm()
        {
            InitializeComponent();
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
                for(int i = 0; i < P.HeapSize; i++)
                {
                   if(P.Arr[i].getName() == textBox1.Text)
                    {
                        D = P.Arr[i];
                        break;
                    }
                }
                if(D.name == " ")
                {
                    MessageBox.Show("Города с таким названием нет в списке. Пожалуйста, повторите ввод");
                    textBox1.Clear();
                    textBox2.Clear();
                }
                   
                else 
                    textBox2.Text = Convert.ToString(D.getPopDen());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar;
            if ((a < 'А' || a > 'я') && (a < 'A' || a > 'z') && a != '\b' && a != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }
    }
}
