using System;
using HidSharp;

namespace WarthogLightControl
{
    [Flags]
    public enum LightMasks
    {
        None      = 0,
        First     = 64,
        Second    = 1,
        Third     = 16,
        Fourth    = 2,
        Fifth     = 4,
        Backlight = 8
    }

    public static class Controller
    {
        private static LightMasks _lightMask = LightMasks.Backlight;
        private static int _brightness = 5;
        private static LightByIndex _light = new LightByIndex();

        static LightMasks GetLightMaskByIndex( int pIndex )
        {
            switch( pIndex )
            {
                case 0  : return LightMasks.Backlight;
                case 1  : return LightMasks.First;
                case 2  : return LightMasks.Second;
                case 3  : return LightMasks.Third;
                case 4  : return LightMasks.Fourth;
                case 5  : return LightMasks.Fifth;
                default : return LightMasks.None;
            }
        }
        
        /// <summary>
        /// Send updated status over USB to the throttle.
        /// </summary>
        /// <returns></returns>
        public static bool Update()
        {
            HidDeviceLoader loader = new HidDeviceLoader();

            HidDevice throttle = null;

            // find Warthog Throttle
            foreach( var device in loader.GetDevices( 0x044F, 0x0404 ) )
            {
                throttle = device;
                break;
            }

            if( throttle == null )
                return false;

            HidStream stream;

            if( !throttle.TryOpen( out stream ) )
                return false;

            stream.Write( new byte[]
                {
                    1,
                    6,
                    (byte)_lightMask,
                    (byte)_brightness
                }
            );

            stream.Close();

            return true;
        }

        /// <summary>
        /// Bitmask of all lights.
        /// </summary>
        public static LightMasks LightMask
        {
            get { return _lightMask; }
            set { _lightMask = value; }
        }

        /// <summary>
        /// Brightness, where 0 = off, and 5 = maximum.
        /// </summary>
        public static int Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        public static LightByIndex Light
        {
            get { return _light; }
        }

        public class LightByIndex
        {
            /// <summary>
            /// Get/set the state of an individual light. 0 is the backlight, 1 is the light closest to the throttle, and 5 is the light closest to the edge.
            /// </summary>
            /// <param name="pLightIndex">Light index 0-5.</param>
            /// <returns>true if the light is on, false if it is off</returns>
            public bool this[ int pLightIndex ]
            {
                get { return ( Controller._lightMask & GetLightMaskByIndex( pLightIndex ) ) != 0; }
                set { Controller._lightMask |= GetLightMaskByIndex( pLightIndex ); }
            }
        }
    }
}
