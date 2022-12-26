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
    public partial class EditPage : Form
    {
        public Pyramid<CCity> P;
        public EditPage()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar;
            if ((a < 'А' || a > 'я') && (a < 'A' || a > 'z') && a != '\b' && a != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar;
            if (a != '\b' && !Char.IsDigit(a))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char a = e.KeyChar;
            if (a != '\b' && !Char.IsDigit(a) && a != ',')
            {
                e.Handled = true;
            }
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
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
                for (int i = 0; i < P.HeapSize; i++)
                {
                    if (P.Arr[i].getName() == textBox1.Text)
                    {
                        D = P.Arr[i];
                        break;
                    }
                }
                if (D.name == " ")
                {
                    MessageBox.Show("Города с таким названием нет в списке. Пожалуйста, повторите ввод");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    checkBox1.Checked = false;
                }
                else
                {
                    if(textBox2.Text != "" && textBox3.Text != "")
                    {
                        D.SetCCity(Convert.ToInt32(textBox2.Text));
                        D.SetCCity(Convert.ToDouble(textBox3.Text));
                        D.SetCCity(checkBox1.Checked);
                    }
                    if (textBox2.Text != "" && textBox3.Text == "")
                    {
                        D.SetCCity(Convert.ToInt32(textBox2.Text));
                        D.SetCCity(checkBox1.Checked);
                    }
                    if (textBox2.Text == "" && textBox3.Text != "")
                    {
                        D.SetCCity(Convert.ToDouble(textBox3.Text));
                        D.SetCCity(checkBox1.Checked);
                    }
                    else
                    {
                        D.SetCCity(checkBox1.Checked);
                    }

                    MessageBox.Show("Параметры успешно изменены!");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    checkBox1.Checked = false;
                }
                
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var isValid = textBox3.Text.StartsWith(",");
            if (!isValid) return;
        }
    }
}
