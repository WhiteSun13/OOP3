using System;
using System.Windows;

namespace OOP3_Task3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            int num1 = 2147483647;
            int umn = 2;
            try
            {
                checked {num1 *= umn;} //Проверка на ошибку
            }
            catch (OverflowException e1) //Поймана ошибка переполнения
            {
                MessageBox.Show(e1.Message.ToString());
            }
            textBox1.Text = num1.ToString(); //Вывод:2147483647

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            int num2 = 2147483647;
            int umn = 2;
            try
            {
                unchecked {num2 *= umn;} //Игнорирование ошибок
            }
            catch (OverflowException e1) //Не будет поймана ошибка
            {
                MessageBox.Show(e1.Message.ToString());
            }
            textBox2.Text = num2.ToString(); //Вывод:-2
        }
    }
}
