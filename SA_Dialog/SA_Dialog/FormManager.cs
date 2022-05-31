using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using LitJson;

namespace SA_Dialog
{
    public class FormManager
    {
        public delegate void ExitHandler(Locale lc);
        public event ExitHandler OnExit;
        [STAThread]
        public void OpenSearchForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SearchForm(this));
        }

        static private string rkey = "";

        public void OnExitForm(Locale lc)
        {
            Console.WriteLine(lc.name);
            OnExit?.Invoke(lc);
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

            string json = reader.ReadToEnd();


            stream.Close();


            JsonData jsonData = JsonToObject(json);

            JsonData docs = jsonData["documents"];

            List<Locale> localeList = new List<Locale>();

            for (int i = 0; i < docs.Count; i++)
            {
                Locale locale = new Locale((string)docs[i]["place_name"], float.Parse((string)docs[i]["y"]), float.Parse((string)docs[i]["x"]));
                string lname = (string)docs[i]["place_name"];

                string x = (string)docs[i]["y"];

                string y = (string)docs[i]["x"];

                //Debug.Log("{0},{1},{2}", lname, x, y);
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

            string json = reader.ReadToEnd();


            stream.Close();

            JsonData jsonData = JsonToObject(json);

            JsonData docs = jsonData["documents"];

            List<Locale> localeList = new List<Locale>();

            for (int i = 0; i < docs.Count; i++)
            {
                Locale locale = new Locale((string)docs[i]["address_name"], float.Parse((string)docs[i]["y"]), float.Parse((string)docs[i]["x"]));
                string lname = (string)docs[i]["address_name"];


                string x = (string)docs[i]["y"];

                string y = (string)docs[i]["x"];

                //Debug.Log("{0},{1},{2}", lname, x, y);
                localeList.Add(locale);

            }
            return localeList;
        }
        JsonData JsonToObject(string json)
        {
            return JsonMapper.ToObject(json);
        }
    

    }
}
