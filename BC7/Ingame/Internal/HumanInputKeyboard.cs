namespace BC7
{
    internal class HumanInputKeyboard : IHumanInput
    {
        private readonly KeyInput keys;
        private readonly MouseInput mouse;

        public HumanInputKeyboard(KeyInput keys, MouseInput mouse)
        {
            this.keys = keys;
            this.mouse = mouse;
        }

        public bool GetKick()
        {
            return mouse.Left.Pressed;
        }
    }
}