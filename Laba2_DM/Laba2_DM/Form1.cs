using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba2_DM
{
    public partial class Form1 : Form
    {
        int[,] arr = new int[5, 5];
        
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        //Обработка данных, введеных во все текстбоксы
        public int TextboxGetInfo(TextBox t)
        {
            return (Convert.ToInt32(t.Text[0])-'0');
        }

        //Метод для определения рефлексивности
        //Меняет содержимое лейблов, по которому дальше ориентируется программа
        public void CheckIfReflexible(int[,] arr)
        {
            if (arr[0, 0] == 1 && arr[1, 1] == 1 && arr[2, 2] == 1 && arr[3, 3] == 1 && arr[4, 4] == 1)
            {
                label2.Text = "рефлексивное";
            }
            else if (arr[0, 0] == 0 && arr[1, 1] == 0 && arr[2, 2] == 0 && arr[3, 3] == 0 && arr[4, 4] == 0)
            {
                label2.Text = "антирефлексивное";
            }
            else
            {
                label2.Text = "не рефлексивное";
            }
        }

        //Метод для определения симметричности
        //Меняет содержимое лейблов, по которому дальше ориентируется программа
        public void CheckIfSimetric(int[,] arr)
        {
            bool s = true;
            int counter = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (arr[i, j] != arr[j, i] || (arr[i, j] == arr[j, i] && arr[i,j]==0 && i!=j))
                    {
                        s = false;
                        counter++;
                    }
                    
                }
            }
            if (s == false)
            {
                if (label2.Text == "антирефлексивное" && counter == 20)
                {
                    label3.Text = "асимметричное";
                }
                else if (counter == 20)
                {
                    label3.Text = "антисимметричное";
                }
                else
                {
                    label3.Text = "не симметричное";
                }
            }
            if (s == true)
            {
                label3.Text = "симметричное";
            }
        }

        //Метод для определение транзитивности
        //Меняет содержимое лейблов, по которому дальше ориентируется программа
        public void CheckIfTransitive(int[,] arr)
        {
            int[,] matrix = new int[5, 5];

            for (int i=0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k=0; k < 5; k++)
                    {
                        matrix[i, j] = matrix[i,j] | (arr[i, k] & arr[k, j]);
                    }
                }
            }
            bool transitive = true;
            for (int i = 0; i < 5; i++) 
            {
                for(int j=0; j < 5; j++)
                {
                    if (matrix[i, j] != arr[i, j])
                    {
                        transitive = false;
                    }
                }
            }
            if (transitive == true)
            {
                label4.Text = "транзитивное";
            }
            if (transitive == false)
            {
                label4.Text = "не транзитивное";
            }
        }

        //Итоговая проверка матрицы после определения всех её свойств
        public void CheckFinal(int[,] arr)
        {
            if (label2.Text == "рефлексивное" && label3.Text=="симметричное" && label4.Text=="транзитивное")
            {
                label6.Text = "эквивалентное";
            }
            if (label2.Text == "рефлексивное" && label3.Text == "антисимметричное" && label4.Text == "транзитивное")
            {
                label6.Text = "частичного порядка";
            }
            if (label2.Text == "антирефлексивное" && label3.Text == "асимметричное" && label4.Text == "транзитивное")
            {
                label6.Text = "строгого порядка";
            }

        }

        //Обработчик событий, в котором вызываются нужные методы и заполняется данными двумерный массив
        private void button1_Click(object sender, EventArgs e)
        {
            int counter = 1;
            groupBox2.Visible = true;
            List<TextBox> textboxes = this.Controls.OfType<GroupBox>().ToList()[3].Controls.OfType<TextBox>().ToList();

            for (int i=0; i < 5; i++)
            {
                for (int j=0; j < 5; j++)
                {
                    arr[i,j] = TextboxGetInfo(textboxes.Find(x => x.Name==("textBox"+counter.ToString())));
                    counter++;
                }
            }
            CheckIfReflexible(arr);
            CheckIfSimetric(arr);
            CheckIfTransitive(arr);
            CheckFinal(arr);
        }

        //Обработчик событий, который выводит матрицу после рефлексивного замыкания
        private void button2_Click(object sender, EventArgs e)
        {
            string str = "";
            if (label2.Text == "антирефлексивное" || label2.Text=="не рефлексивное" || label2.Text =="не симметричное")
            {
                for (int i = 0; i < 5; i++)
                {
                    arr[i, i] = 1;
                }
            }
           
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    str += arr[i, j].ToString() + " ";
                }
                str += "\n";
            }
            richTextBox1.Text = str;
        }

        //Обработчик событий, который выводит матрицу после симметричного замыкания
        private void button3_Click(object sender, EventArgs e)
        {
            string str = "";
            if (label3.Text=="антисимметричное" || label3.Text == "асимметричное" || label3.Text == "не симметричное")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (arr[i, j] != arr[j, i])
                        {
                            arr[i, j] = arr[j, i];
                        }

                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    str += arr[i, j].ToString() + " ";
                }
                str += "\n";
            }
            richTextBox2.Text = str;
        }

        //Обработчик событий, который выводит матрицу после транзитивного замыкания
        private void button4_Click(object sender, EventArgs e)
        {
            string str = "";

            if (label4.Text =="не транзитивное")
            {
                for (int k=0; k < 5; k++)
                {
                    for (int i=0; i < 5; i++)
                    {
                        for (int j=0; j < 5; j++)
                        {
                            arr[i, j] = arr[i, j] | (arr[i,k] & arr[k,j]);
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    str += arr[i, j].ToString() + " ";
                }
                str += "\n";
            }
            richTextBox3.Text = str;
        }

        //Обработчик событий, который выводит матрицу после возведения в квадратный степень
        private void button5_Click(object sender, EventArgs e)
        {
            string str = "";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        arr[i, j] = arr[i, j] | (arr[i, k] & arr[k, j]);
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    str += arr[i, j].ToString() + " ";
                }
                str += "\n";
            }
            richTextBox4.Text = str;
        }

        //Обработчик событий, который выводит матрицу после возведения в кубический степень
        private void button6_Click(object sender, EventArgs e)
        {
            string str = "";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        arr[i, j] = arr[i, j] | (arr[i, k] & arr[k, j]);
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        arr[i, j] = arr[i, j] | (arr[i, k] & arr[k, j]);
                    }
                }
            }


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    str += arr[i, j].ToString() + " ";
                }
                str += "\n";
            }
            richTextBox5.Text = str;
        }

        //Метод, который вызывают обработчики событий текстбоксов для валидации введённого значения
        //В случае, если пользователь ввёл что-то кроме 0 или 1, выскакивает диалоговое окно с предупреждением.
        //После чего очищается текст в выбранном текстбоксе
        public void CheckText(TextBox t)
        {
            if (t.Text != "1" && t.Text != "0")
            {
                t.Text = "";
                MessageBox.Show("Пожалуйста, введите 1 или 0");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox3);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox4);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox5);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox6);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox7);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox8);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox9);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox10);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox11);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox12);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox13);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox14);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox15);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox16);
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox17);
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox18);
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox19);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox20);
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox21);
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox22);
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox23);
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox24);
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            CheckText(textBox25);
        }
    }
}
