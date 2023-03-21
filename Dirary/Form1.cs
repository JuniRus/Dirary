using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Dirary
{
    public partial class Form1 : Form
    {
        // Переменная индексации массивов.
        int indexcount = 3;

        // Переменная, хранящая путь к файлу xml.
        public const string PATH = "tasks.xml";

        // Создать массивы, хранящих объекты для заполнения полей
        public static Label[] labels = new Label[53];
        public static CheckBox[] checkBoxes = new CheckBox[53];
        public static TextBox[] textBoxes = new TextBox[53];
        public static Panel[] panels = new Panel[53];

        public Form1()
        {
            InitializeComponent();
            InitializeTasks();
        }

        // Добавление в окно приложения сохранённых на этот день задач.
        private void InitializeTasks()
        {
            // Извлечь данные из XML-файла.
            ComponentsUI componentsUI = DeserializeTasks();
            if (componentsUI == null) return;

            textBox1.Text = componentsUI.task1;
            textBox2.Text = componentsUI.task2;
            checkBox1.Checked = componentsUI.checked1;
            checkBox2.Checked = componentsUI.checked2;

            for (int i = 3; i < componentsUI.index_count; i++)
            {
                // Добавление строк.
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27));
                // Добавление надписи.
                labels[i] = new Label();
                labels[i].Text = $"{i}";
                labels[i].Dock = DockStyle.Fill;
                labels[i].AutoSize = false;
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
                labels[i].Font = new Font(labels[i].Font.Name, 8);
                tableLayoutPanel1.Controls.Add(labels[i], 0, tableLayoutPanel1.RowCount - 2);
                // Изменить фоновый цвет ячейки с надписью.
                labels[i].BackColor = Color.FromArgb(154, 144, 226);

                // Добавление панельки для флажка.
                panels[i] = new Panel();
                panels[i].Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(panels[i], 1, tableLayoutPanel1.RowCount - 2);

                // Добавление флажка в панельку.
                checkBoxes[i] = new CheckBox();
                panels[i].Controls.Add(checkBoxes[i]);
                checkBoxes[i].AutoSize = false;
                checkBoxes[i].TextAlign = ContentAlignment.MiddleCenter;
                checkBoxes[i].Location = new Point(42, 0);
                checkBoxes[i].Cursor = Cursors.Hand;
                // Добавление значения флажка из сохранённого объекта.
                checkBoxes[i].Checked = componentsUI.checkeds[i];

                // Добавление текстового поля.
                textBoxes[i] = new TextBox();
                textBoxes[i].Dock = DockStyle.Top;
                // Добавление значения флажка из сохранённого объекта.
                textBoxes[i].Text = componentsUI.tasks[i];
                tableLayoutPanel1.Controls.Add(textBoxes[i], 2, tableLayoutPanel1.RowCount - 2);
            }
        }
        
        // Метод для возврата объекта, содержащего данные из XML-файла.
        private ComponentsUI DeserializeTasks()
        {
            ComponentsUI cUI = null;

            if (File.Exists(PATH))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ComponentsUI));

                using (FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate))
                {
                    cUI = (ComponentsUI)xml.Deserialize(fs);
                    return cUI;
                }
            }
            else return cUI;
        }

        // Добавление при нажатии на кнопку новой строки.
        public void buttonAddStr_Click(object sender, EventArgs e)
        {
            // Если строк больше 50-ти.
            if (tableLayoutPanel1.RowCount > 50)
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
            labels[indexcount] = new Label();
            labels[indexcount].Text = $"{indexcount}";
            labels[indexcount].Dock = DockStyle.Fill;
            labels[indexcount].AutoSize = false;
            labels[indexcount].TextAlign = ContentAlignment.MiddleCenter;
            labels[indexcount].Font = new Font(labels[indexcount].Font.Name, 8);
            tableLayoutPanel1.Controls.Add(labels[indexcount], 0, tableLayoutPanel1.RowCount - 2);
            // Изменить фоновый цвет ячейки с надписью.
            labels[indexcount].BackColor = Color.FromArgb(154, 144, 226);

            // Добавление панельки для флажка.
            panels[indexcount] = new Panel();
            panels[indexcount].Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(panels[indexcount], 1, tableLayoutPanel1.RowCount - 2);

            // Добавление флажка в панельку.
            checkBoxes[indexcount] = new CheckBox();
            panels[indexcount].Controls.Add(checkBoxes[indexcount]);
            checkBoxes[indexcount].AutoSize = false;
            checkBoxes[indexcount].TextAlign = ContentAlignment.MiddleCenter;
            checkBoxes[indexcount].Location = new Point(42, 0);
            checkBoxes[indexcount].Cursor = Cursors.Hand;

            // Добавление текстового поля.
            textBoxes[indexcount] = new TextBox();
            textBoxes[indexcount].Dock = DockStyle.Top;
            tableLayoutPanel1.Controls.Add(textBoxes[indexcount], 2, tableLayoutPanel1.RowCount - 2);

            indexcount++;
        }

        // Кнопка сохранения задач на выбранный день.
        private void buttonSaveCase_Click(object sender, EventArgs e)
        {
            ComponentsUI cUI = new ComponentsUI(this.indexcount, this.checkBox1.Checked,
                                                this.checkBox2.Checked, this.textBox1.Text,
                                                this.textBox2.Text);

            XmlSerializer xml = new XmlSerializer(typeof(ComponentsUI));

            using (FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, cUI);
            }

            MessageBox.Show("Данные успешно сохранены!");
        }
    }
}
