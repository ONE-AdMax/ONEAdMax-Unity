
namespace ONEAdMax
{
    public class OAMGravity
    {
        public const int TOP = (0x0002 | 0x0001) << 4; // 48
        public const int BOTTOM = (0x0004 | 0x0001) << 4; // 80
        public const int LEFT = (0x0002 | 0x0001) << 0; // 3
        public const int RIGHT = (0x0004 | 0x0001) << 0; // 5
        public const int CENTER_VERTICAL = 0x0001 << 4; // 16
        public const int CENTER_HORIZONTAL = 0x0001 << 0; // 1
        public const int CENTER = CENTER_VERTICAL | CENTER_HORIZONTAL; // 17
    }
}
