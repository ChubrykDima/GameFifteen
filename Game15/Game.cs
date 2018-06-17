using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game15.Model;

namespace Game15
{
    class Game
    {
        int _size;
        int[,] _map;
        int _spaceX, _spaceY;
        static Random _random = new Random();

        public Game(int size)
        {
            if (size < 2) size = 2;
            if (size > 5) size = 5;
            this._size = size;
            _map = new int[size, size];
        }

        public void Start()
        {
            for (int x = 0; x < _size; x++)
                for (int y = 0; y < _size; y++)
                    _map[x, y] = CoordsToPosition(x, y) + 1;
            _spaceX = _size - 1;
            _spaceY = _size - 1;
            _map[_spaceX, _spaceY] = 0;
        }

        public void Shift(int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            if (Math.Abs(_spaceX - x) + Math.Abs(_spaceY - y) != 1) return;
            _map[_spaceX, _spaceY] = _map[x, y];
            _map[x, y] = 0;
            _spaceX = x;
            _spaceY = y;
        }

        public void ShiftRandom()
        {
            int a = _random.Next(0, 4);
            int x = _spaceX;
            int y = _spaceY;
            switch (a)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;
            }
            Shift(CoordsToPosition(x, y));
        }

        public int GetNumber(int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            if (x < 0 || x >= _size) return 0;
            if (y < 0 || y >= _size) return 0;
            return _map[x, y];
        }

        private int CoordsToPosition(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > _size - 1) x = _size - 1;
            if (y < 0) x = 0;
            if (y > _size - 1) y = _size - 1;
            return y * _size + x;
        }

        private void PositionToCoords(int position, out int x, out int y)
        {
            if (position < 0) position = 0;
            if (position > _size * _size - 1) position = _size * _size - 1;
            x = position % _size;
            y = position / _size;
        }

        public bool CheckNumbers()
        {
            if (!(_spaceX == _size - 1 && _spaceY == _size - 1))
                return false;
            for (int x = 0; x < _size; x++)
                for (int y = 0; y < _size; y++)
                    if (!(x == _size - 1 && y == _size - 1))
                        if (_map[x, y] != CoordsToPosition(x, y) + 1)
                            return false;
            return true;
        }

        public string CheckStep(int count)
        {
            var response = "шагов";
            var value = count.ToString();
            var lastvalue = value[value.Length - 1].ToString();

            if (value.Length >= 2)
            {
                var postvalue = value.Substring(value.Length - 2);
                if (postvalue.Contains("11") || postvalue.Contains("12") || postvalue.Contains("13") || value.Contains("14"))
                    response = "шагов";
                else
                {
                    if (lastvalue.Contains("2") || lastvalue.Contains("3") || lastvalue.Contains("4"))
                        response = "шага";
                    if (lastvalue.Contains("1"))
                        response = "шаг";
                }
            }
            else
            {
                if (lastvalue.Contains("2") || lastvalue.Contains("3") || lastvalue.Contains("4"))
                    response = "шага";
                if (lastvalue.Contains("1"))
                    response = "шаг";
            }
            return response;
        }
    }
}

