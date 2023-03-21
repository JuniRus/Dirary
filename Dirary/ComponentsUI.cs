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
        // Строки программы.
        public int index_count;

        // Первые две строки программы.
        public bool checked1;
        public bool checked2;
        public string task1;
        public string task2;

        // Массивы для содержания сохранённых значений.
        public bool[] checkeds = new bool[53];
        public string[] tasks = new string[53];

        public ComponentsUI() { }

        public ComponentsUI(int indexcount, bool checked1, bool checked2, string task1, string task2)
        {
            // Получена информация о количестве строк программы.
            this.index_count = indexcount;
            this.checked1 = checked1;
            this.checked2 = checked2;
            this.task1 = task1;
            this.task2 = task2;

            // Сохранить состояния объектов.
            for (int i = 3; i < indexcount; i++)
            {
                checkeds[i] = Form1.checkBoxes[i].Checked;
                tasks[i] = Form1.textBoxes[i].Text;
            }
        }
    }
}
