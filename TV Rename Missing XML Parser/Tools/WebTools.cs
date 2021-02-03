using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Tools
{
    class WebTools
    {
        private static WebTools instance = null;

        public static WebTools Get()
        {
            return instance = instance == null ? new WebTools() : instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <note>
        /// May need to change order for linux and mac
        /// </note>
        /// <param name="url"></param>
        public void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    //TODO: BE BETTER!
                    throw;
                }
            }
        }
    }
}
