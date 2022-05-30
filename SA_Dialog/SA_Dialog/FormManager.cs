using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
namespace SA_Dialog
{
    public class FormManager
    {
        static private string rkey = "";

        [STAThread]
        static public void OpenSearchForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static public void SetAPIKey(string key)
        {
            rkey = key;
        }
        public List<Locale> kewordSearchRequest(string keword)
        {

            string input = keword;

            string site = "https://dapi.kakao.com/v2/local/search/keyword.json";

            string query = string.Format("{0}?query={1}", site, input);

            WebRequest request = WebRequest.Create(query);



            string header = "KakaoAK " + rkey;




            request.Headers.Add("Authorization", header);

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();


            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            String json = reader.ReadToEnd();


            stream.Close();




            JavaScriptSerializer js = new JavaScriptSerializer();

            dynamic dob = js.Deserialize<dynamic>(json);

            dynamic docs = dob["documents"];

            object[] buf = docs;

            int length =  buf.Length;
            List<Locale> localeList = new List<Locale>();
            
            for (int i = 0; i < length; i++)
            {
                Locale locale = new Locale(docs[i]["place_name"], float.Parse( docs[i]["y"]),float.Parse(docs[i]["x"]));
                string lname = docs[i]["place_name"];

                string x = docs[i]["x"];

                string y = docs[i]["y"];

                Console.WriteLine("{0},{1},{2}", lname, x, y);
                localeList.Add(locale);
            }
            return localeList;

        }
        public List<Locale> AddressSearchRequest(string address)
        {


            string input = address;

            string site = "https://dapi.kakao.com/v2/local/search/address.json";

            string query = string.Format("{0}?query={1}", site, input);

            WebRequest request = WebRequest.Create(query);




            string header = "KakaoAK " + rkey;




            request.Headers.Add("Authorization", header);

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();


            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            String json = reader.ReadToEnd();


            stream.Close();


            JavaScriptSerializer js = new JavaScriptSerializer();

            dynamic dob = js.Deserialize<dynamic>(json);

            dynamic docs = dob["documents"];

            object[] buf = docs;

            int length = buf.Length;

            List<Locale> localeList = new List<Locale>();

            for (int i = 0; i < length; i++)
            {
                Locale locale = new Locale(docs[i]["address_name"], float.Parse(docs[i]["y"]), float.Parse(docs[i]["x"]));
                string lname = docs[i]["address_name"];

                string x = docs[i]["x"];

                string y = docs[i]["y"];


                Console.WriteLine("{0},{1},{2}", lname, x, y);
                localeList.Add(locale);
            }
            return localeList;
        }

    }
}
