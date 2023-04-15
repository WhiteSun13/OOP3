using System;
using System.Windows.Forms;

namespace OOP2_Task4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int rows1, columns1;
            string str_row = textBox1.Text;
            string str_col = textBox2.Text;
            bool check1 = int.TryParse(str_row, out rows1);
            bool check2 = int.TryParse(str_col, out columns1);
            if (check1 && check2)
            {
                dataGridView1.RowCount = rows1;
                dataGridView1.ColumnCount = columns1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rows2, columns2;
            string str_row = textBox3.Text;
            string str_col = textBox4.Text;
            bool check1 = int.TryParse(str_row, out rows2);
            bool check2 = int.TryParse(str_col, out columns2);
            if (check1 && check2)
            {
                dataGridView2.RowCount = rows2;
                dataGridView2.ColumnCount = columns2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int ii = 0; ii < dataGridView1.ColumnCount; ii++)
                {
                    dataGridView1.Rows[i].Cells[ii].Value = Convert.ToDouble(rnd.Next(0, 10000)) / 100;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int ii = 0; ii < dataGridView2.ColumnCount; ii++)
                {
                    dataGridView2.Rows[i].Cells[ii].Value = Convert.ToDouble(rnd.Next(0, 10000)) / 100;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.RowCount = dataGridView1.RowCount;
                dataGridView3.ColumnCount = dataGridView2.ColumnCount;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    {
                        //try
                        //{
                        double element = 0;
                        for (int ii = 0; ii < dataGridView1.ColumnCount; ii++)
                        {
                            if (Convert.ToDouble(dataGridView1.Rows[i].Cells[ii].Value) < 0) throw new ArgumentException($"Matrix1 contains an invalid entry in cell[{i},{ii}]");
                            if (Convert.ToDouble(dataGridView2.Rows[ii].Cells[j].Value) < 0) throw new ArgumentException($"Matrix2 contains an invalid entry in cell[{ii},{j}]");

                            element +=
                                Convert.ToDouble(dataGridView1.Rows[i].Cells[ii].Value) *
                                Convert.ToDouble(dataGridView2.Rows[ii].Cells[j].Value);
                        }
                        dataGridView3.Rows[i].Cells[j].Value = element;
                        //}
                        //catch (ArgumentException a1) { MessageBox.Show(a1.Message.ToString()); }
                    }
                }
            }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Number of columns and rows should match"); }
            catch (ArgumentException a1) { MessageBox.Show(a1.Message.ToString()); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 0;
            dataGridView1.ColumnCount = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.RowCount = 0;
            dataGridView2.ColumnCount = 0;
        }
    }
}
