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
        public Form1()
        {
            InitializeComponent();
        }

        // Добавление при нажатии на кнопку новой строки.
        private void buttonAddStr_Click(object sender, EventArgs e)
        {
            // Увелить максимальное количество строк
            // для создания новой ячейки строки.
            tableLayoutPanel1.RowCount++;
            // Создать строку, придав ей стиль.
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27));
        }
    }
}
