using System;
using System.Collections.Generic;
using System.Windows;

namespace HanoiTower
{
    public class RecHanoiTower
    {
        private Stack<int> _sourcePole;
        private Stack<int> _destinationPole;
        private Stack<int> _withPole;
        public int _movesCount;

        public RecHanoiTower(int numberOfDisks)
        {
            _sourcePole = new Stack<int>();
            _destinationPole = new Stack<int>();
            _withPole = new Stack<int>();

            for (int i = numberOfDisks; i > 0; i--)
            {
                _sourcePole.Push(i);
            }
        }

        public int Solve()
        {
            _movesCount = 0;
            MoveDisks(_sourcePole.Count, _sourcePole, _destinationPole, _withPole);
            return _movesCount;
        }

        private void MoveDisks(int numberOfDisks, Stack<int> sourcePole, Stack<int> destinationPole, Stack<int> withPole)
        {
            if (numberOfDisks == 1)
            {
                // Если количество дисков равно 1, переносим диск на целевую башню
                destinationPole.Push(sourcePole.Pop());
                _movesCount++;
                Console.WriteLine("Количество ходов: " + _movesCount);
            }
            else
            {
                // Рекурсивный вызов для переноса всех дисков кроме самого маленького
                MoveDisks(numberOfDisks - 1, sourcePole, withPole, destinationPole);

                // Перенос самого маленького диска на целевую башню
                destinationPole.Push(sourcePole.Pop());
                _movesCount++;
                Console.WriteLine("Количество ходов: " + _movesCount);

                // Рекурсивный вызов для переноса оставшихся дисков на целевую башню
                MoveDisks(numberOfDisks - 1, withPole, destinationPole, sourcePole);
            }
        }
    }
}