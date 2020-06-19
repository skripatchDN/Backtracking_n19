/*
 19. Найти кратчайший по количеству ходов путь ладьи из одной клетки в другую.
Известно расположение фигур двух цветов на доске.
Фигуры противоположного цвета можно бить
L - белая ладья F - точка назначения B - чёрные фигуры (можно бить) W - белые фигуры (нельзя бить)

Выполнил Дюжев Никита 2 курс 10 группа.
*/
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
using System.Windows.Forms.VisualStyles;

namespace Backtracking_n19
{
    public partial class FormMain : Form
    {
        string[,] tablet = new string[8, 8];
        int Ls = 0, Fs = 0, Bs = 0, Ws = 0;
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            labelHint.Text = "Расставьте белые и черные фигуры, выставьте ладью \nи пометьте финальную точку. \n " +
                          "W - белая фигура\n B - черная фигура\n L - ладья\n F - финальная точка";
            labelStatus.Text = "Ладья L (стартовая позиция): " + Ls + " шт.\nФинальная позиция: " + Fs + " шт.\nБелые фигуры: " + Ws + " шт.\nЧёрные фигуры: " + Bs + " шт.\n";
        }
        private void richTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((number != 66) && (number != 87) && (number != 76) && (number != 70) && (number != 8))
            {
                e.Handled = true;
            }
        }
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            //Все 64 текстбокса в дизайнере прописаны как имеющие эту функцию при изменении текста. Изменяемая клетка определяется по имени изменённого текстбокса (см. ниже).
            string name = (sender as RichTextBox).Name;
            name = name.Substring(name.Length - 2);
            int i = Convert.ToInt32(name) % 8;
            int j = Convert.ToInt32(name) / 8;
            tablet[i, j] = (sender as RichTextBox).Text;
            ChessUnit chessUnit = new ChessUnit();
            chessUnit.GetNums(tablet, out Ls, out Fs, out Bs, out Ws);
            labelStatus.Text = "Ладья L (стартовая позиция): " + Ls + " шт.\nФинальная позиция: " + Fs + " шт.\nБелые фигуры: " + Ws + " шт.\nЧёрные фигуры: " + Bs + " шт.\n";
            if ((Ls == 1) && (Fs == 1))
            {
                int?[,] paths = new int?[8, 8];
                chessUnit.FindL(out int X, out int Y, tablet);
                int path = chessUnit.FindPath(50, 0, X, Y, ref tablet, ref paths, Direction.None);
                if (path < 50)
                {
                    labelStatus.Text += "Кратчайший путь от L до F равен " + path;
                }
                else
                {
                    labelStatus.Text += "Путь от L до F не найден";
                }
            }
        }
    }
}