namespace Pong
{
    public sealed class BatControlOther : IBatControl
    {
        private readonly Bat _bat;
        
        public BatControlOther(Bat bat)
        {
            _bat = bat;
        }

        public void Update() {}
    }
}