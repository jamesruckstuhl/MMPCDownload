using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace MMPCDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            parseAndDownload(getSourceCode());
        }

        public static string getSourceCode()
        {
            //Get HTML code of web page
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://billburr.libsyn.com");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            String sourceCode = sr.ReadToEnd();
            sr.Close();
            resp.Close();
            return sourceCode;             
        }

        public static void parseAndDownload(string sourceCode)
        {
            //Get the link within the HTML of the page
            int startIndex = sourceCode.IndexOf("\"postTitle\"");
            sourceCode = sourceCode.Substring(startIndex, sourceCode.Length - startIndex);
            startIndex = sourceCode.IndexOf("<A HREF=") + 9;
            int endIndex = sourceCode.IndexOf(".mp3",startIndex);
            string link = sourceCode.Substring(startIndex + 13, endIndex - startIndex - 13) + ".mp3";

            //Establish a connection, download the file, and archive it based on year and month
            WebClient wc = new WebClient();
            Uri realLink = new Uri(link);
            string fileName = link.Substring(35);
            string filePath = "C:\\Users\\James\\Desktop\\Podcasts\\Monday_Morning_Podcast_Bill_Burr\\" + DateTime.Today.Year + "\\" + DateTime.Today.Month + "\\" + fileName;
            (new FileInfo(filePath)).Directory.Create();
            wc.DownloadFileAsync(realLink,filePath);
        }
    }
}
