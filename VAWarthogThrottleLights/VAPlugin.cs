using System;
using System.Collections.Generic;
using WarthogLightControl;

namespace VAWarthogThrottleLights
{
    namespace VATestPlugin
    {
        public class VoiceAttackPlugin
        {
            public static string VA_DisplayName()
            {
                return "VoiceAttack Warthog Throttle Lights";
            }

            public static string VA_DisplayInfo()
            {
                return "VoiceAttack Warthog Throttle Lights plugin\r\n\r\nControl your lights through VoiceAttack.";
            }
            public static void VA_StopCommand()
            {

            }
            public static Guid VA_Id()
            {
                return new Guid( "{B9D106D0-DCD8-4F82-91EF-DF15C6224DC5}" );
            }

            public static void VA_Init1(dynamic vaProxy){

            }

            public static void VA_Exit1(dynamic vaProxy)
            {

            }

            public static void VA_Invoke1(dynamic vaProxy)
            {
                Controller.LightMask = LightMasks.None;
                if( vaProxy.GetBoolean( "ThrottleLight1") != null && vaProxy.GetBoolean("ThrottleLight1") == true )
                    Controller.LightMask |= LightMasks.First;

                if(vaProxy.GetBoolean( "ThrottleLight2") != null && vaProxy.GetBoolean("ThrottleLight2") == true )
                    Controller.LightMask |= LightMasks.Second;

                if(vaProxy.GetBoolean( "ThrottleLight3") != null && vaProxy.GetBoolean("ThrottleLight3") == true )
                    Controller.LightMask |= LightMasks.Third;

                if (vaProxy.GetBoolean("ThrottleLight4") != null && vaProxy.GetBoolean("ThrottleLight4") == true)
                    Controller.LightMask |= LightMasks.Fourth;

                if (vaProxy.GetBoolean("ThrottleLight5") != null && vaProxy.GetBoolean("ThrottleLight5") == true)
                    Controller.LightMask |= LightMasks.Fifth;
                if (vaProxy.GetBoolean("ThrottleBacklight") != null && vaProxy.GetBoolean("ThrottleBacklight") == true)
                    Controller.LightMask |= LightMasks.Backlight;

                 Controller.Brightness = vaProxy.GetInt("ThrottleIntensity") ?? 0;

                Controller.Update();
            }
        }
    }
}
