using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Web.Common.Utils
{
    public static class ScriptExtensions
    {
        public const string ScriptBaseRelativeUrl = "~/ClientSide/Scripts/";
        public const string LibScriptBaseRelativeUrl = "~/ClientSide/Scripts/Lib/";

        public static string AppendJSLibFolder(this string script)
        {
            return LibScriptBaseRelativeUrl + script;
        }

        public static string AppendJSFolder(this string script)
        {
            return ScriptBaseRelativeUrl + script;
        }
    }
}
