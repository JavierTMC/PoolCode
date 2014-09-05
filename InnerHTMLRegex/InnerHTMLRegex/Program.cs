using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace InnerHTMLRegex
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader file = new StreamReader("C:\\Users\\JTMC\\Downloads\\ml-100k\\ml-100k\\u.item");
            string linea;
            List<string> titulos = new List<string>();
            int counter = 0;
            while (counter <= 3)
            {
                linea = file.ReadLine();
                string[] palabras = linea.Split('|');

                string urlAddress = palabras[4];
                string codeHtml = HtmlCode(urlAddress);
                if (codeHtml != null)
                {
                    //get director by regex
                    titulos.Add(string.Format("Titulo: {0} \t Director: {1}",palabras[1],GetDirectorByRegex(codeHtml)));
                }
                counter++;
            }

            foreach (var item in titulos)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey(true);
        }

        static string HtmlCode(string urlAddress)
        {
            string data = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }

            return data;
        }

        static string GetDirectorByRegex(string dom)
        {
            string pattern = "(<div.*itemprop=\"director\"(?:.|\n|\r)+?</div>)";

            Match m = Regex.Match(dom, pattern);
            if (m.Success)
            {
                string divTag =  m.Value;
                return GetDirectorsByRegex(divTag);
            }
            else
                return null;
        }

        static string GetDirectorsByRegex(string dom)
        {
            string directors = string.Empty;
            //string pattern = "<.*Directors?:.*\\s.*\\s*.*itemprop=\"name\">(?<Director>.*)</span>";
            string pattern = "itemprop=\"name\">(?<Director>.*)</span>";
            RegexOptions options = RegexOptions.IgnorePatternWhitespace;
            MatchCollection matches = Regex.Matches(dom, pattern, options);

            foreach (Match match in matches)
            {
                directors += match.Groups[1].Value + "\n";
            }

            return directors;
        }
    }
}
