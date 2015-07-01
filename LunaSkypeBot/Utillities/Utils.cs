using System.IO;
using System.Threading.Tasks;

namespace LunaSkypeBot.Utillities
{
    public static class Utils
    {
        public static async Task<string> GetDropboxLinkForFilePath(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var pathOnly = filePath.Replace(fileName, "");

            var dropboxPath = "G:\\Dropbox\\Public";

            var dropboxInternalPath = pathOnly.Replace(dropboxPath, "");
            

            var baseDropboxURL = "https://dl.dropboxusercontent.com/u/69140190";
            // /Pics/pony/Luna/5%20Star/

            var dropboxURL = baseDropboxURL + dropboxInternalPath + fileName;

            dropboxURL = dropboxURL.Replace(" ", "%20");
            dropboxURL = dropboxURL.Replace(@"\", @"/");

            return dropboxURL;
        }
    }
}
