using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParserAPI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _lblResult.Text = string.Empty;
        }

        public int Offset;

        public DateTime Week = DateTime.Now.AddDays(-7);

        public class ParserResult
        {
            [JsonProperty("timestamp")]
            public DateTime timestamp { get; set; }
            [JsonProperty("id")]
            public string id { get; set; }
        }

        public class DataList
        {
            public DataList(int date, int valueCount)
            {
                Date = date;
                ValueCount = valueCount;
            }

            public DataList()
            {

            }
            public int Date { get; set; }
            public int ValueCount { get; set; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnRequest_Click(object sender, EventArgs e)
        {
            _lblResult.Text = "Loading...";

            _dgvResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "date",
                HeaderText = "Дата"
            });

            _dgvResult.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "valueCount",
                HeaderText = "Кол-во"
            });

            List<ParserResult> parserResults = new List<ParserResult>();

            List<DataList> Datalist = new List<DataList>();

            Offset = 0;

            int Done = 0;

            //Запускаем цикл до того момента, пока не будут получены более ранние записи
            while (Done < 1)
            {
                //Создание web-запроса
                var myWebRequest = (HttpWebRequest)WebRequest.Create("http://xn--80aaahbralm5bfdcfjcdqpf.xn--p1ai:45555/api/v1/vehicles?offset=" + Offset + "&limit=15");

                myWebRequest.Accept = "application/json";

                myWebRequest.Method = "GET";

                //Отправка запроса 
                var myWebResponse = (HttpWebResponse)await myWebRequest.GetResponseAsync();

                //Обработка ответа
                using (var stream = new StreamReader(myWebResponse.GetResponseStream(), Encoding.UTF8))
                {
                    using (var jsonReader = new JsonTextReader(stream))
                    {
                        //Подготовка к парсингу json ответа
                        var jsonParser = JObject.Parse(await stream.ReadToEndAsync());

                        var results = jsonParser["entries"].Children().ToList();

                        foreach (var result in results)
                        {
                            //Десериализация ответа и передача полученных значений
                            var searchResult = await JsonConvert.DeserializeObjectAsync<ParserResult>(result.ToString());

                            parserResults.Add(searchResult);
                        }

                        IEnumerable<IGrouping<int, string>> query = parserResults.GroupBy(group => group.timestamp.Day, group => group.id);

                        foreach (IGrouping<int, string> Group in query)
                        {
                            int GroupDate = Group.Key;

                            foreach (string name in Group)
                            {
                                int countValue = name.Count();

                                Datalist.Add(new DataList() { Date = GroupDate, ValueCount = countValue });
                            }
                        }

                        foreach (var result in parserResults)
                        {
                            //Если получаем дату, которая была раньше, чем 7 дней назад, то возвращаем done = 1
                            if (Week.CompareTo(result.timestamp) == 1)
                            {
                                Done++;
                            }
                        }

                        foreach (var result in Datalist)
                        {
                            var data = new List<DataList>{
                                new DataList ( result.Date, result.ValueCount )
                            };

                            _dgvResult.DataSource = data;
                        }
                    }
                }
                //Увеличиваем значение Offset, что бы пропустить записи, которые уже есть
                Offset = Offset + 15;
            }

            _lblResult.Text = "Было обработано " + Offset.ToString() + " записей";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
