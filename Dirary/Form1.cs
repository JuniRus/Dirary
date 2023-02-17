using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dirary
{
    public partial class Form1 : Form
    {
        int rows_count = 3;
        // Создать массивы, хранящих объекты для заполнения полей
        Label[] labels = new Label[53];
        CheckBox[] checkBoxes = new CheckBox[53];
        TextBox[] textBoxes = new TextBox[53];
        Panel[] panels = new Panel[53];

        public Form1()
        {
            InitializeComponent();
        }

        // Добавление при нажатии на кнопку новой строки.
        private void buttonAddStr_Click(object sender, EventArgs e)
        {
            // Если строк больше 50-ти.
            if(tableLayoutPanel1.RowCount > 50)
            {
                MessageBox.Show("Достигнуто максимальное количество допустимых задач!");
                return;
            }
            
            // Увелить максимальное количество строк
            // для создания новой ячейки строки.
            tableLayoutPanel1.RowCount++;
            // Создать строку, придав ей стиль.
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27));

            // Добавление элементов в таблицу

            // Добавление надписи.
            labels[rows_count] = new Label();
            labels[rows_count].Text = $"{rows_count}";
            labels[rows_count].Dock = DockStyle.Fill;
            labels[rows_count].AutoSize = false;
            labels[rows_count].TextAlign = ContentAlignment.MiddleCenter;
            labels[rows_count].Font = new Font(labels[rows_count].Font.Name, 8);
            tableLayoutPanel1.Controls.Add(labels[rows_count], 0, tableLayoutPanel1.RowCount - 2);

            // Изменить фоновый цвет ячейки с надписью.
            labels[rows_count].BackColor = Color.FromArgb(154, 144, 226);

            // Добавление панельки для флажка.
            panels[rows_count] = new Panel();
            panels[rows_count].Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(panels[rows_count], 1, tableLayoutPanel1.RowCount - 2);

            // Добавление флажка в панельку.
            checkBoxes[rows_count] = new CheckBox();
            panels[rows_count].Controls.Add(checkBoxes[rows_count]);
            checkBoxes[rows_count].AutoSize = false;
            checkBoxes[rows_count].TextAlign = ContentAlignment.MiddleCenter;
            checkBoxes[rows_count].Location = new Point(42, 0);
            checkBoxes[rows_count].Cursor = Cursors.Hand;

            // Добавление текстового поля.
            textBoxes[rows_count] = new TextBox();
            textBoxes[rows_count].Dock = DockStyle.Top;
            tableLayoutPanel1.Controls.Add(textBoxes[rows_count], 2, tableLayoutPanel1.RowCount - 2);

            rows_count++;
        }
    }
}
