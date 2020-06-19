using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtracking_n19
{

    enum Direction
    {
        None, Up, Right, Down, Left
    }
    class ChessUnit
    {
        public void GetNums(string[,] tablet, out int Ls, out int Fs, out int Bs, out int Ws)
        {
            Ls = 0;
            Fs = 0;
            Bs = 0;
            Ws = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tablet[i, j] == "L")
                    {
                        Ls++;
                    }
                    else if (tablet[i, j] == "F")
                    {
                        Fs++;
                    }
                    else if (tablet[i, j] == "W")
                    {
                        Ws++;
                    }
                    else if (tablet[i, j] == "B")
                    {
                        Bs++;
                    }
                }
            }
        }
        public void FindL(out int X, out int Y, string[,] tablet)
        {
            X = 0;
            Y = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tablet[i, j] == "L")
                    {
                        X = i;
                        Y = j;
                    }
                }
            }
        }
        public int FindPath(int Shortest, int Current, int X, int Y, ref string[,] tablet, ref int?[,] paths, Direction dir)
        {
            //Shortest - кратчайший путь из найденных
            //Current - текущий пройденный путь
            //X, Y - координаты рассматриваемой в данный момент точки
            //tablet - поле с целевой точкой, чёрными и белыми фигурами
            //paths - копия таблицы с записями крайтчайших путей до каждой точки
            //dir - направление последнего перемещения
            if ((X >= 0) && (Y >= 0) && (X <= 7) && (Y <= 7))//Проверка, находится ли клетка на поле вообще
            {
                if (tablet[X, Y] == "F")//Если найдена целевая точка, то сравниваем пройденный путь и кратчайший, обновляя его если найденный короче
                {
                    if (Shortest > Current)
                    {
                        Shortest = Current;
                    }
                }
                else if (tablet[X, Y] != "W")//В противном случае, если на клетке нет белой фигуры...
                {
                    if ((paths[X, Y] == null) || (paths[X, Y] > Current))//...и если пройденный путь не уже длиннее кратчайшего к этой точке...
                    {
                        paths[X, Y] = Current;//...то записываем его как кратчайший к этой точке и переходим к следующим точкам
                        if (tablet[X, Y] == "B")//На клетке чёрная фигура
                        {
                            if (dir == Direction.Up)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current + 1, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current + 1, X - 1, Y, ref tablet, ref paths, Direction.Left);
                            }
                            else if (dir == Direction.Right)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current + 1, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current + 1, X, Y - 1, ref tablet, ref paths, Direction.Down);
                            }
                            else if (dir == Direction.Down)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current + 1, X, Y - 1, ref tablet, ref paths, Direction.Down);
                                Shortest = FindPath(Shortest, Current + 1, X - 1, Y, ref tablet, ref paths, Direction.Left);
                            }
                            else if (dir == Direction.Left)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current + 1, X, Y - 1, ref tablet, ref paths, Direction.Down);
                                Shortest = FindPath(Shortest, Current + 1, X - 1, Y, ref tablet, ref paths, Direction.Left);
                            }
                        }
                        else//На клетке нет чёрной фигуры
                        {
                            if (dir == Direction.None)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current + 1, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current + 1, X, Y - 1, ref tablet, ref paths, Direction.Down);
                                Shortest = FindPath(Shortest, Current + 1, X - 1, Y, ref tablet, ref paths, Direction.Left);

                            }
                            else if (dir == Direction.Up)
                            {
                                Shortest = FindPath(Shortest, Current, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current + 1, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current + 1, X - 1, Y, ref tablet, ref paths, Direction.Left);
                            }
                            else if (dir == Direction.Right)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current + 1, X, Y - 1, ref tablet, ref paths, Direction.Down);
                            }
                            else if (dir == Direction.Down)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X + 1, Y, ref tablet, ref paths, Direction.Right);
                                Shortest = FindPath(Shortest, Current, X, Y - 1, ref tablet, ref paths, Direction.Down);
                                Shortest = FindPath(Shortest, Current + 1, X - 1, Y, ref tablet, ref paths, Direction.Left);
                            }
                            else if (dir == Direction.Left)
                            {
                                Shortest = FindPath(Shortest, Current + 1, X, Y + 1, ref tablet, ref paths, Direction.Up);
                                Shortest = FindPath(Shortest, Current + 1, X, Y - 1, ref tablet, ref paths, Direction.Down);
                                Shortest = FindPath(Shortest, Current, X - 1, Y, ref tablet, ref paths, Direction.Left);
                            }
                        }
                    }
                }
            }
            return (Shortest);
        }
    }
}
