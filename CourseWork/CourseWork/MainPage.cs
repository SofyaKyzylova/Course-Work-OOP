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
    public partial class MainPage : Form
    {
		public Pyramid<CCity> P = new Pyramid<CCity>();
		public MainPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			ToolTip t = new ToolTip();
			t.SetToolTip(button1, "Добавить город в пирамиду");

			ToolTip t1 = new ToolTip();
			t1.SetToolTip(button5, "Обновить пирамиду");

			ToolTip t2 = new ToolTip();
			t2.SetToolTip(button4, "Показать вершину пирамиды и удалить ее");

			ToolTip t3 = new ToolTip();
			t3.SetToolTip(button2, "Посмотреть плотность населения города");

			ToolTip t4 = new ToolTip();
			t4.SetToolTip(button3, "Изменить характеристики города");

			ToolTip t5 = new ToolTip();
			t5.SetToolTip(button6, "Сравнить два города по населению");
		}
		
		private void button1_Click(object sender, EventArgs e)
        {
			if(textBox2.Text =="" || textBox3.Text == "" || textBox1.Text == "")
            {
				MessageBox.Show("Пожалуйста, заполните все поля ввода");
			}
            else
			{
				CCity C = new CCity(Convert.ToInt32(textBox2.Text), Convert.ToDouble(textBox3.Text), textBox1.Text, checkBox1.Checked);
				bool flag = true;
				for(int i=0; i < P.HeapSize; i++)
                {
					if(C.name == P.Arr[i].name)
                    {
						flag = false;
						break;
					}
				}
				if (flag)
                {
					dataGridView1.Rows.Clear();
					P.pushHeap(C);
					P.heapSort();
					C.setData(dataGridView1);
					P.outHeap();

					textBox4.Text = Convert.ToString(P.PSize());
					CCity.count = 0;
					for (int i = 0; i < P.HeapSize; i++)
					{
						if (P.Arr[i].airport == true)
							P.Arr[i].countCCity(P.Arr[i]);
					}
					int t = CCity.getCount();
					textBox5.Text = Convert.ToString(t);

					textBox1.Clear();
					textBox2.Clear();
					textBox3.Clear();
					checkBox1.Checked = false;
					dataGridView2.Rows.Clear();
				}
				else
                {
					MessageBox.Show("Город с таким названием уже есть в списке!");
					textBox1.Clear();
					textBox2.Clear();
					textBox3.Clear();
					checkBox1.Checked = false;
				}
			}
		}

		private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
		{
			char a = e.KeyChar;
			if (a != '\b' && !Char.IsDigit(a) && a !=',')
			{
				e.Handled = true;
			}
			if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
			{
				e.Handled = true;
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

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
			char a = e.KeyChar;
			if (a != '\b' && !Char.IsDigit(a))
			{
				e.Handled = true;
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
			PopDensityForm newForm = new PopDensityForm();
			newForm.Show();
			newForm.P = this.P;
		}

        private void button3_Click(object sender, EventArgs e)
        {
			EditPage newForm = new EditPage();
			newForm.Show();
			newForm.P = this.P;
		}

        private void button4_Click(object sender, EventArgs e)
        {
			dataGridView2.Rows.Clear();
			if(P.isEmpty())
			{
				MessageBox.Show("Пирамида пуста");
			}
            else 
			{
				CCity C1 = new CCity();
				Pyramid<CCity> P1 = new Pyramid<CCity>();

				C1 = P.popHeap();
				P1.pushHeap(C1);
				C1.setData(dataGridView2);
				P1.outHeap();

				dataGridView1.Rows.Clear();
				P.heapSort();
				P.outHeap();
				textBox4.Text = Convert.ToString(P.PSize());

				CCity.count = 0;
				for (int i = 0; i < P.HeapSize; i++)
				{
					if (P.Arr[i].airport == true)
						P.Arr[i].countCCity(P.Arr[i]);
				}
				int t = CCity.getCount();
				textBox5.Text = Convert.ToString(t);
			}
		}

        private void button5_Click(object sender, EventArgs e)
        {
			dataGridView1.Rows.Clear();
			P.heapSort();
			P.outHeap();

			textBox4.Text = Convert.ToString(P.PSize());

			CCity.count = 0;
			for (int i=0;i<P.HeapSize;i++)
            {
				if (P.Arr[i].airport == true)
					P.Arr[i].countCCity(P.Arr[i]);
            }
			int t = CCity.getCount();
			textBox5.Text = Convert.ToString(t);
		}

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
			var isValid = textBox3.Text.StartsWith(",");
			if (!isValid) return;
		}

        private void button6_Click(object sender, EventArgs e)
        {
			CompareForm newForm = new CompareForm();
			newForm.Show();
			newForm.P = this.P;
		}
    }


    public class CCity : IComparable<CCity>
	{
		public static int count = 0;
		public void countCCity(CCity C) { count++; }
		public static int getCount() { return count; }

		public int population;
		public double square;
		public string name;
		public bool airport;
		public DataGridView dataGridView;

		public CCity() { population = 0; square = 0; name = " "; airport = false; }
		public CCity(int VPopulation, double VSquare, string VName, bool VAirport)
		{
			population = VPopulation;
			square = VSquare;
			name = VName;
			airport = VAirport;
		}
		public CCity(CCity C) { name = C.name; square = C.square; population = C.population; airport = C.airport; }

		public double getPopDen() { return (double)(population) / square; }
		public int getPopulation() { return population; }
		public double getSquare() { return square; }
		public string getName() { return name; }
		public bool getAirport() { return airport; }

		public string info()
		{
			return "Город " + name + ", площадь " + square.ToString() + ", население " + population.ToString() + ", аэропорт " + airport.ToString();
		}
		public void SetCCity(int pop) { population = pop; }
		public void SetCCity(double sqr) { square = sqr; }
		public void SetCCity(bool airp) { airport = airp; }
		public void SetCCity(string nm) { name = nm; }

		public int CompareTo(CCity C)
		{
			if (this.population > C.population)
				return 1;
			if (this.population < C.population)
				return -1;
			return 0;
		}

		public void setData(DataGridView d)
        {
			dataGridView = d;
        }
		public void set(DataGridView d)
		{
			d.Rows.Add(name, population, square, airport);
		}
		public override string ToString()
        {
			set(dataGridView);
			return name;
        }
        ~CCity() { }
	}

	public class Pyramid<T> where T : IComparable<T>
	{
		public int HeapSize = 0;
		public T[] Arr = new T[100];
		public Pyramid() { }

		public int father(int i) { return (i - 1) / 2; }
		public int left(int i) { return 2 * i + 1; }
		public int right(int i) { return 2 * i + 2; }

		public void buidHeap()
		{
			int i = 0, k = 1;
			while ((i < k) && i < HeapSize)
			{
				i++;
				k = left(k);
			}
		}
		public void pushHeap(T n) 
		{
			int parent, i = HeapSize;
			Arr[i] = n;
			parent = father(i);
			while (parent >= 0 && i > 0)
			{
				if (Arr[i].CompareTo(Arr[parent]) > 0) 
				{
					T temp = Arr[i];
					Arr[i] = Arr[parent];
					Arr[parent] = temp;
				}
				i = parent;
				parent = father(i);
			}
			HeapSize++;
		}
		public void heapify(int i)
		{
			T temp;
			int lft = left(i);
			int rght = right(i);
			if (lft < HeapSize)
			{
				if (Arr[i].CompareTo(Arr[lft]) < 0)  
				{
					temp = Arr[i];
					Arr[i] = Arr[lft];
					Arr[lft] = temp;
					heapify(lft);
				}
			}
			if (rght < HeapSize)
			{
				if (Arr[i].CompareTo(Arr[rght]) < 0)  
				{
					temp = Arr[i];
					Arr[i] = Arr[rght];
					Arr[rght] = temp;
					heapify(rght);
				}
			}
		}
		public T popHeap()
		{
			T x = Arr[0];
			Arr[0] = Arr[HeapSize - 1];
			HeapSize--;
			heapify(0);
			return x;
		}
		public T peekHeap() { return Arr[0]; }
		public static void swap(ref T a, ref T b)
		{
			T c = a;
			a = b;
			b = c;

		}
		public void heapSort()
		{
			for (int i = HeapSize / 2 - 1; i >= 0; i--)
				heapify(i);

			for (int i = HeapSize - 1; i >= 0; i--)
			{
				swap(ref Arr[0], ref Arr[i]);
				heapify(0);
			}
		}
		public void outHeap()
		{
			for(int i = 0; i < HeapSize; i++)
            {
				Console.WriteLine(Arr[i]);
			}
		}
		public bool isEmpty() { return HeapSize <= 0; }
		public int PSize() { return HeapSize; }
		~Pyramid() { }
	}
}
