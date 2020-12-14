namespace Boogle
{
    class Pos
    {
        private int _x;
        private int _y;

        int X { get { return _x; } }
        int Y { get { return _y; } }

        public Pos(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}