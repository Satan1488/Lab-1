using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1
{
    class global
    {
        public static string[] files;
        public static StreamReader readbuild;
        public static string stroka;
        public static FileStream fs;
        public static bool vkl=false;
        public static string guide_path = ""; //The variable to store the path to the build of StarCraft.
    }
}
