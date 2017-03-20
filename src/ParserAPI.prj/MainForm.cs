using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace ParserAPI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public int Offset;

        public DateTime Week = DateTime.Now.AddDays(-7);

        public class ParserResult
        {
            [JsonProperty("timestamp")]
            public DateTime timestamp { get; set; }
        }


        public class ParserResultLog
        {
            [JsonProperty("entries")]
            public List<ParserResult> ParserResults { get; set; }
            public ParserResultLog()
            {
                ParserResults = new List<ParserResult>();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            List<ParserResult> parserResults = new List<ParserResult>();

            Offset = 0;

            int Done = 0;

            //Запускаем цикл до того момента, пока не будут получены более ранние записи
            while (Done < 1)
            {
                //Создание web-запроса
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://xn--80aaahbralm5bfdcfjcdqpf.xn--p1ai:45555/api/v1/vehicles?offset=" + Offset + "&limit=15");

                myWebRequest.Accept = "application/json";

                myWebRequest.Method = "GET";

                //Отправка запроса 
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();

                //Обработка ответа
                using (StreamReader stream = new StreamReader(myWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    using (var jsonReader = new JsonTextReader(stream))
                    {
                        //Подготовка к парсингу json ответа
                        JObject jsonParser = JObject.Parse(stream.ReadToEnd());

                        List<JToken> results = jsonParser["entries"].Children().ToList();

                        foreach (JToken result in results)
                        {
                            //Десериализация ответа и передача полученных значений
                            ParserResult searchResult = JsonConvert.DeserializeObject<ParserResult>(result.ToString());
                            
                            parserResults.Add(searchResult);
                        }

                        foreach (ParserResult result in parserResults)
                        {
                            //Если получаем дату, которая была раньше, чем 7 дней назад, то возвращаем done = 1
                            if (Week.CompareTo(result.timestamp) == 1)
                            {
                                Done++;
                            }
                        }
                    }
                }
                //Увеличиваем значение Offset, что бы пропустить записи, которые уже есть
                Offset = Offset + 15;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
