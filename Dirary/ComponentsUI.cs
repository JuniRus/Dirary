using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dirary
{
    // Класс для содержания элементов UI и их использовании
    // в XML-файле.
    [Serializable]
    public class ComponentsUI
    {
        // Переменная индексации массивов.
        public int index_count;

        // Строка с текущим путём к файлу.
        public string current_file;

        // Переменные первых двух строк программы.
        public bool checked1;
        public bool checked2;
        public string task1;
        public string task2;

        // Переменная времени.
        public DateTime date;

        // Массивы для содержания сохранённых значений 
        // флажков и текстовых полей.
        public bool[] checkeds = new bool[53];
        public string[] tasks = new string[53];

        // Базовый конструктор, необходимый для сериализации объекта класса.
        public ComponentsUI() { }

        // Конструктор объекта для сериализации.
        public ComponentsUI(int indexcount, bool checked1, bool checked2, string task1, string task2, DateTimePicker dtp)
        {
            // Получена информация о количестве строк программы.
            this.index_count = indexcount;
            this.checked1 = checked1;
            this.checked2 = checked2;
            this.task1 = task1;
            this.task2 = task2;

            // Сохранить дату.
            date = dtp.Value;
            // Построить путь, по которому располагается файл с сохранёнными данными.
            current_file = Form1.path_directory + "\\saved_tasks\\" + date.Date.ToString("d").Replace('.', '_') + ".xml";

            // Сохранить состояния объектов.
            for (int i = 3; i < indexcount; i++)
            {
                // Сохранить состояния флажков.
                checkeds[i] = Form1.checkBoxes[i].Checked;
                // Сохранить значения текстовых полей.
                tasks[i] = Form1.textBoxes[i].Text;
            }
        }
    }
}
