namespace Algotrithm
{
    internal class Program
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
                #region 프레임 관리
                //FPS 계산.. 정밀도는 떨어짐
                int currentTick = Environment.TickCount;

                if ((currentTick - lastTick) < WAIT_TICK)
                    continue;
                lastTick = currentTick; 
                #endregion


                //렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();

            }
        }
    }
}