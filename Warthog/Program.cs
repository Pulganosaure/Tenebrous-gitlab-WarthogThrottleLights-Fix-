using WarthogLightControl;

namespace Warthog
{
    class Program
    {
        static void Main( string[] args )
        {
            Controller.LightMask = LightMasks.None;
            Controller.Update();
        }
    }
}
