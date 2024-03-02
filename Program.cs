﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize(25);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            

            int lastTick = 0;
            while (true)
            {
                #region 프레임 계산
                int currentTick = System.Environment.TickCount;
                int elapsedTick = currentTick - lastTick;

                // 만약에 경과한 시간이 1/30초보다 작다면
                if (elapsedTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;
                #endregion

                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}