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

        public string text;

        public int Offset;

        public DateTime Week = DateTime.Now.AddDays(-7);

        //List<ParserResultData> ParserResult = new List<ParserResultData>();


        public class ParserResult
        {
            public int id { get; set; }
            public string plate { get; set; }
            public DateTime timestamp { get; set; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            Offset = 0;

            int done = 0;

            //Запускаем цикл до того момента, пока не будут получены более ранние записи

            while (done <= 1)
            {
                //Создание web-запроса

                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://xn--80aaahbralm5bfdcfjcdqpf.xn--p1ai:45555/api/v1/vehicles?offset=" + Offset + "&limit=15");

                myWebRequest.Accept = "application/json";

                myWebRequest.Method = "GET";

                //Отправка запроса 

                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();

                var serializer = JsonSerializer.CreateDefault();

                var listEnteringAuto = new ParserResult();

                //Обработка ответа

                using (StreamReader stream = new StreamReader(myWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    using (var jsonReader = new JsonTextReader(stream))
                    {
                        //Подготовка к парсингу json ответа
                        JObject jsonParser = JObject.Parse(stream.ReadToEnd());

                        List<JToken> results = jsonParser["entries"].Children().ToList();

                        List<ParserResult> ParserResult = new List<ParserResult>();

                        foreach (JToken result in results)
                        {
                            //Десериализация ответа и передача полученных значений

                            ParserResult searchResult = JsonConvert.DeserializeObject<ParserResult>(result.ToString());

                            ParserResult.Add(searchResult);
                        }

                        //Проверка даты, среди полученных данных

                        foreach (ParserResult result in ParserResult)
                        {
                            //Тестирование результата и передача его в richTextBox, для анализа

                            text = text + "ID: " + result.id + "; Timestamp: "
                                 + result.timestamp + "; Number: " + result.plate;

                            richTextBox1.Text = text;

                            //Если получаем дату, которая была раньше, чем 7 дней назад, то возвращаем done = 1

                            if (Week.CompareTo(result.timestamp) == 1)
                            {
                                done++;
                            }
                        }
                    }
                }

                //Увеличиваем значение Offset, что бы пропустить записи, которые уже есть

                Offset = Offset + 15;
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {

        }
    }
}
