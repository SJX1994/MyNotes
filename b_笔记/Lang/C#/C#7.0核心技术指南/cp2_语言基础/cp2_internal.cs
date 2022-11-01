using System;

namespace cp2_internal
{
    internal class internal_BaseClass // 内部类无法被外部访问
    {
        public static int intMember = 0;
        
    }

    public class BaseClass 
    {
        internal static int intInternalMember = 0; // 内部变量无法被外部访问
        public static int intPublicMember = 33;
        public int notStatic = 10;
    }
}