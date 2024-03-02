using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }

    class Player
    {
        public int PosY { get; set; }
        public int PosX { get; set; }
        Random _random = new Random();
        Board _board;

        enum Dir
        { 
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)
        {
            PosY = posY;
            PosX = posX;
            _board = board;

            // 방향에 따라서 가야될 좌표들을 미리 들고있는다.
            // up, left, down, right 순으로 미리 좌표들을 들고있는다.
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };

            // 내가 바라보는 방향을 기준으로 오른쪽 좌표 표시.
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (PosY != board.DestY || PosX != board.DestX)
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인.
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;

                    // 앞으로 한 보 전진.
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));
                }
                // 2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인.
                else if (_board.Tile[PosY+frontY[_dir], PosX+frontX[_dir]] == Board.TileType.Empty)
                {
                    // 앞으로 한 보 전진.
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    // 왼쪽 방향으로 90도 회전.
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
