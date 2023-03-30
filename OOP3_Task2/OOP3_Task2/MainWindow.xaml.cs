using System;
using System.Windows;
using System.Windows.Controls;

namespace OOP3_Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double[,] matrix1 = new double[2, 2];
        double[,] matrix2 = new double[2, 2];
        double[,] result = new double[2, 2];
        private double[,] getRandomValuesFromGrid(Grid grid, double[,] matrix)
        {
            int columns = grid.ColumnDefinitions.Count;
            int rows = grid.RowDefinitions.Count;
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c];
                Random rnd = new Random();
                int row = Grid.GetRow(t);
                int column = Grid.GetColumn(t);
                matrix[row, column] = rnd.NextDouble();
            }
            return matrix;
        }
        private double[,] getValuesFromGrid(Grid grid, double[,] matrix)
        {
            int columns = grid.ColumnDefinitions.Count;
            int rows = grid.RowDefinitions.Count;
            // Iterate over cells in Grid, copying to matrix array
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c];
                //Random rnd = new Random();
                int row = Grid.GetRow(t);
                int column = Grid.GetColumn(t);
                matrix[row, column] = double.Parse(t.Text);
            }
            return matrix;
        }
        private void initializeGrid(Grid grid, double[,] matrix)
        {
            if (grid != null)
            {
                // Reset the grid before doing anything
                grid.Children.Clear();
                grid.ColumnDefinitions.Clear();
                grid.RowDefinitions.Clear();
                // Get the dimensions
                int rows = matrix.GetLength(0);
                int columns = matrix.GetLength(1);
                // Add the correct number of coumns to the grid
                for (int x = 0; x < columns; x++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), });
                }
                for (int y = 0; y < rows; y++)
                {
                    // GridUnitType.Star - The value is expressed as a weighted proportion of available space
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star), });
                }
                // Fill each cell of the grid with an editable TextBox containing the value from the matrix 
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        double cell = (double)matrix[y, x];
                        TextBox t = new TextBox();
                        t.Text = cell.ToString();
                        t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        t.SetValue(Grid.RowProperty, y);
                        t.SetValue(Grid.ColumnProperty, x);
                        grid.Children.Add(t);
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Col1.Items.Add(new ComboBoxItem() { Content = "2" });
            Col1.Items.Add(new ComboBoxItem() { Content = "3" });
            Col1.Items.Add(new ComboBoxItem() { Content = "4" });

            Row1.Items.Add(new ComboBoxItem() { Content = "2" });
            Row1.Items.Add(new ComboBoxItem() { Content = "3" });
            Row1.Items.Add(new ComboBoxItem() { Content = "4" });

            Col2.Items.Add(new ComboBoxItem() { Content = "2" });
            Col2.Items.Add(new ComboBoxItem() { Content = "3" });
            Col2.Items.Add(new ComboBoxItem() { Content = "4" });
        }

        private void Col1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string col1 = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            matrix1 = new double[matrix1.GetLength(0), int.Parse(col1)];
        }

        private void Row1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Row1 = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            matrix1 = new double[int.Parse(Row1), matrix1.GetLength(1)];
        }

        private void Col2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string col2 = ((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
            matrix2 = new double[matrix1.GetLength(1), int.Parse(col2)];
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            initializeGrid(grid1, matrix1);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            initializeGrid(grid2, matrix2);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                matrix1 = getValuesFromGrid(grid1, matrix1);
                matrix2 = getValuesFromGrid(grid2, matrix2);
                result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
                int m1columns_m2rows, m1rows, m2columns, m2rows;
                m1columns_m2rows = matrix1.GetLength(1);
                m1rows = matrix1.GetLength(0);
                m2columns = matrix2.GetLength(1);
                m2rows = matrix2.GetLength(0);
                try
                {
                    if (m1columns_m2rows != m2rows) throw new ArgumentException("number of columns and rows should match.");
                }
                catch (ArgumentException a1)
                {
                    MessageBox.Show(a1.Message.ToString());
                }
                for (int row = 0; row < m1rows; row++)
                {
                    for (int column = 0; column < m2columns; column++)
                    {
                        double accumulator = 0;
                        for (int cell = 0; cell < m1columns_m2rows; cell++)
                        {
                            try
                            {
                                if (matrix1[row, cell] < 0) throw new ArgumentException($"Matrix1 contains an invalid entry in cell[{row},{cell}]");
                            }
                            catch (ArgumentException a1)
                            {
                                MessageBox.Show(a1.Message.ToString());
                            }
                            try
                            {
                                if (matrix2[cell, column] < 0) throw new ArgumentException($"Matrix2 contains an invalid entry in cell[{cell},{column}]");
                            }
                            catch (ArgumentException a2)
                            {
                                MessageBox.Show(a2.Message.ToString());
                            }
                            accumulator += matrix1[row, cell] * matrix2[cell, column];
                        }
                        result[row, column] = accumulator;
                    }
                }
            }
            catch (ArgumentException)
            {
                return;
            }
            initializeGrid(Grid3, result);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                matrix1 = getRandomValuesFromGrid(grid1, matrix1);
                matrix2 = getRandomValuesFromGrid(grid2, matrix2);
                result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
                int m1columns_m2rows, m1rows, m2columns, m2rows;
                m1columns_m2rows = matrix1.GetLength(1);
                m1rows = matrix1.GetLength(0);
                m2columns = matrix2.GetLength(1);
                m2rows = matrix2.GetLength(0);
                try
                {
                    if (m1columns_m2rows != m2rows) throw new ArgumentException("number of columns and rows should match.");
                }
                catch (ArgumentException a1)
                {
                    MessageBox.Show(a1.Message.ToString());
                    return;
                }
                for (int row = 0; row < m1rows; row++)
                {
                    for (int column = 0; column < m2columns; column++)
                    {
                        double accumulator = 0;
                        for (int cell = 0; cell < m1columns_m2rows; cell++)
                        {
                            try
                            {
                                if (matrix1[row, cell] < 0) throw new ArgumentException($"Matrix1 contains an invalid entry in cell[{row},{cell}]");
                            }
                            catch (ArgumentException a1)
                            {
                                MessageBox.Show(a1.Message.ToString());
                            }
                            try
                            {
                                if (matrix2[cell, column] < 0) throw new ArgumentException($"Matrix2 contains an invalid entry in cell[{cell},{column}]");
                            }
                            catch (ArgumentException a2)
                            {
                                MessageBox.Show(a2.Message.ToString());
                            }
                            accumulator += matrix1[row, cell] * matrix2[cell, column];
                        }
                        result[row, column] = accumulator;
                    }
                }
            }
            catch (ArgumentException)
            {
                return;
            }
            initializeGrid(grid1, matrix1);
            initializeGrid(grid2, matrix2);
            initializeGrid(Grid3, result);
        }
    }
}
