using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Controllers
{

    public class NavigationLink
    {
        public string Text;
        public string Controller;
        public string Action;
        public int RouteID;
    }

    class StringClass
    {
        public static string sanitiseDateTimeStringForFilename(string Input)
        {
            Input = Input.Replace(":", "_");
            Input = Input.Replace("-", "_");
            Input = Input.Replace("Z", "");
            return Input;
        }

        public static string removeZFromEndOfString(string Input)
        {
            return Input.TrimEnd('Z');
            //return strInput.Replace("Z", "");
        }

        public static byte[] GetByteArrayFromString(string str)
        {
            var imageArray = str.ToCharArray();
            var validator = imageArray.Select(c => (byte)c).ToArray();
            return validator;
        }
    }

}
