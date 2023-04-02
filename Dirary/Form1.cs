using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Dirary
{
    public partial class Form1 : Form
    {
        // Объявление переменных и массивов.
        
        // Переменная индексации массивов.
        int indexcount = 3;

        // Переменная времени для пути файлов.
        DateTime dateTime;

        // Создание полного пути к файлам сохранения задач.
        static string path_program = System.Reflection.Assembly.GetExecutingAssembly().Location;
        // Получения полного пути к файлам сохранения задач.
        public static string path_directory = Path.GetDirectoryName(path_program);

        // Создать массивы, хранящих объекты для заполнения полей

        // Массив надписей, обозначающих номер задачи.
        public static Label[] labels = new Label[53];
        // Массив флажков для отметки выполненных задач.
        public static CheckBox[] checkBoxes = new CheckBox[53];
        // Массив текстовых полей для описания задачи.
        public static TextBox[] textBoxes = new TextBox[53];
        // Массив панелей для размещения флажков в таблице.
        public static Panel[] panels = new Panel[53];

        // Срабатывает сразу при запуске программы.
        public Form1()
        {
            InitializeComponent();
            InitializeTasks();
        }

        // Функциональные кнопки сохранения задач.

        // Кнопка удаления строки с задачей.
        private void buttonDelStr_Click(object sender, EventArgs e)
        {
            // Если остаётся две строки задачи, то вывести предупреждающее сообщение.
            if (tableLayoutPanel1.RowCount == 3 || indexcount >= 51)
            {
                MessageBox.Show("Достигнуто минимально допустимое количество строк!");
                return;
            }

            // Удалить задачи со строками.

            // Удалить надпись из первой ячейки таблицы.
            tableLayoutPanel1.Controls.Remove(labels[indexcount - 1]);
            // Удалить пенель флажка из второй ячейки таблицы.
            tableLayoutPanel1.Controls.Remove(panels[indexcount - 1]);
            // Удалить флажок из второй ячейки таблицы.
            tableLayoutPanel1.Controls.Remove(checkBoxes[indexcount - 1]);
            // Удалить поле для ввода текста из третьей ячейки таблицы.
            tableLayoutPanel1.Controls.Remove(textBoxes[indexcount - 1]);

            tableLayoutPanel1.RowCount--;
            indexcount--;
        }

        // Добавление при нажатии на кнопку новой строки.
        public void buttonAddStr_Click(object sender, EventArgs e)
        {
            // Если строк больше 50-ти, то вывести предупреждающее сообщение с 
            // прерыванием операции добавления новой строки с задачей.
            if (tableLayoutPanel1.RowCount > 50)
            {
                MessageBox.Show("Достигнуто максимальное количество допустимых задач!");
                return;
            }

            // Увеличить максимальное количество строк
            // для создания новой ячейки строки.
            tableLayoutPanel1.RowCount++;
            // Создать строку, придав ей стиль и установив высоту.
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27));

            // Добавление элементов в таблицу

            // Добавление надписи.
            // Создать надпись в массиве.
            labels[indexcount] = new Label();
            // Выдать параметы для надписи.
            labels[indexcount].Text = $"{indexcount}";
            labels[indexcount].Dock = DockStyle.Fill;
            labels[indexcount].AutoSize = false;
            labels[indexcount].TextAlign = ContentAlignment.MiddleCenter;
            labels[indexcount].Font = new Font(labels[indexcount].Font.Name, 8);
            // Добавить надпись в первую ячейку строки.
            tableLayoutPanel1.Controls.Add(labels[indexcount], 0, tableLayoutPanel1.RowCount - 2);
            // Изменить фоновый цвет ячейки с надписью.
            labels[indexcount].BackColor = Color.FromArgb(154, 144, 226);

            // Добавление панельки для флажка.
            // Создать панельку в массиве.
            panels[indexcount] = new Panel();
            // Заполнить панелью всю ячейку.
            panels[indexcount].Dock = DockStyle.Fill;
            // Разместить панель во вторую ячейку строки.
            tableLayoutPanel1.Controls.Add(panels[indexcount], 1, tableLayoutPanel1.RowCount - 2);

            // Добавление флажка в панельку.
            // Создать флажок в массиве.
            checkBoxes[indexcount] = new CheckBox();
            // Поместить флажок внутрь панельки второй ячейки.
            panels[indexcount].Controls.Add(checkBoxes[indexcount]);
            // Задать параметры для флажка.
            checkBoxes[indexcount].AutoSize = false;
            checkBoxes[indexcount].TextAlign = ContentAlignment.MiddleCenter;
            checkBoxes[indexcount].Location = new Point(42, 0);
            checkBoxes[indexcount].Cursor = Cursors.Hand;

            // Добавление текстового поля.
            // Создать текстовое поле в массиве
            textBoxes[indexcount] = new TextBox();
            // Прикрепить поле к верху ячейки.
            textBoxes[indexcount].Dock = DockStyle.Top;
            // Поместить поле в третью ячейку строки.
            tableLayoutPanel1.Controls.Add(textBoxes[indexcount], 2, tableLayoutPanel1.RowCount - 2);

            // Увеличить переменную индексации, чтобы добавить новую задачу при нажатии на кнопку.
            indexcount++;
        }

        // Кнопка сохранения задач на выбранный день.
        private void buttonSaveCase_Click(object sender, EventArgs e)
        {
            // Создать сохраняемый объект, поместив в него значения первых двух задач.
            ComponentsUI cUI = new ComponentsUI(this.indexcount, this.checkBox1.Checked,
                                                this.checkBox2.Checked, this.textBox1.Text,
                                                this.textBox2.Text, this.dateTimePicker);

            // Создать объект для сохранения данных в XML-файл.
            XmlSerializer xml = new XmlSerializer(typeof(ComponentsUI));

            // Удалить файл с сохранёнными задачами на выбранный день...
            if (File.Exists(cUI.current_file)) File.Delete(cUI.current_file);
            // ...и создать новый ради предотвращения ошибок сохранения.
            using (FileStream fs = new FileStream(cUI.current_file, FileMode.Create))
            {
                xml.Serialize(fs, cUI);
            }

            // Вывести пользователю уведомление о сохранении.
            MessageBox.Show("Задачи успешно сохранены!");
        }

        // Событие, срабатывающее при выборе другого дня.
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // Очистить значения на выбранный день.
            Clear();
            // Если есть сохранённые задачи на выбранные день, то отобразить их.
            InitializeTasks();
        }



        // Метод для возврата объекта, содержащего сохранённые данные из XML-файла.
        private ComponentsUI DeserializeTasks(string path)
        {
            // Создать сперва ссылку на объект.
            ComponentsUI cUI = null;

            // Создать объект, используемый для десериализации.
            XmlSerializer xml = new XmlSerializer(typeof(ComponentsUI));

            // Если файл существует, то извлечь объект с сохранёнными данными.
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    cUI = (ComponentsUI)xml.Deserialize(fs);
                    return cUI;
                }
            }
            // Если файл не существует, то вернуть пустую ссылку.
            else return cUI;
        }



        // Методы работы с отображением данных.

        // Метод очистки полей дня.
        private void Clear()
        {
            // Очистить значения первых двух полей.
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;

            // Удалить остальные строки.
            for (int i = 3; i < indexcount; i++)
            {
                if (tableLayoutPanel1.RowCount == 3) return;
                
                tableLayoutPanel1.Controls.Remove(labels[i]);
                tableLayoutPanel1.Controls.Remove(panels[i]);
                tableLayoutPanel1.Controls.Remove(checkBoxes[i]);
                tableLayoutPanel1.Controls.Remove(textBoxes[i]);
                tableLayoutPanel1.RowCount--;
            }
        }

        // Добавление в окно приложения сохранённых на выбранный день задач.
        private void InitializeTasks()
        {
            // Взять дату выбранного дня
            dateTime = dateTimePicker.Value.Date;
            // Создать путь к файлу XML, в котором будет храниться информация на выбранный день.
            string path = path_directory + "\\saved_tasks\\" + dateTime.Date.ToString("d").Replace('.', '_') + ".xml";

            // Извлечь данные из XML-файла.
            ComponentsUI componentsUI = DeserializeTasks(path);
            if (componentsUI == null) return;

            // Присвоить в первые две задачи сохранённые значения
            textBox1.Text = componentsUI.task1;
            textBox2.Text = componentsUI.task2;
            checkBox1.Checked = componentsUI.checked1;
            checkBox2.Checked = componentsUI.checked2;

            // Инициализировать индекс строк.
            indexcount = componentsUI.index_count;

            // Создать задачи и поместить в них сохранённые значения
            for (int i = 3; i < indexcount; i++)
            {
                // Добавление строк.
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27));
                // Добавление надписи c соответствующими для ежедневника параметрами.
                labels[i] = new Label();
                labels[i].Text = $"{i}";
                labels[i].Dock = DockStyle.Fill;
                labels[i].AutoSize = false;
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
                labels[i].Font = new Font(labels[i].Font.Name, 8);
                tableLayoutPanel1.Controls.Add(labels[i], 0, tableLayoutPanel1.RowCount - 2);
                // Изменить фоновый цвет ячейки с надписью.
                labels[i].BackColor = Color.FromArgb(154, 144, 226);

                // Добавление панельки для флажка c соответствующими для ежедневника параметрами.
                panels[i] = new Panel();
                panels[i].Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(panels[i], 1, tableLayoutPanel1.RowCount - 2);

                // Добавление флажка в панельку c соответствующими для ежедневника параметрами.
                checkBoxes[i] = new CheckBox();
                panels[i].Controls.Add(checkBoxes[i]);
                checkBoxes[i].AutoSize = false;
                checkBoxes[i].TextAlign = ContentAlignment.MiddleCenter;
                checkBoxes[i].Location = new Point(42, 0);
                checkBoxes[i].Cursor = Cursors.Hand;
                // Добавление значения флажка из сохранённого объекта.
                checkBoxes[i].Checked = componentsUI.checkeds[i];

                // Добавление текстового поля c соответствующими для ежедневника параметрами.
                textBoxes[i] = new TextBox();
                textBoxes[i].Dock = DockStyle.Top;
                // Добавление значения флажка из сохранённого объекта.
                textBoxes[i].Text = componentsUI.tasks[i];
                tableLayoutPanel1.Controls.Add(textBoxes[i], 2, tableLayoutPanel1.RowCount - 2);
            }
        }
    }
}
