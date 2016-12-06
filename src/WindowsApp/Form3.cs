using Newtonsoft.Json;
using Shared;
using Shared.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsApp.Models;

namespace WindowsApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        WebClient client = new WebClient();

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var url = this.textBox1.Text.Trim();
                List<SecondPrice> list = new List<SecondPrice>();
                foreach (var line in client.DownloadString(url).Split('\r', '\n'))
                {
                    if (line.Length < 100)
                    {
                        continue;
                    }
                    list.Add(SecondPrice.NewStockPrice(line));
                }
                this.dataGridView1.DataSource = list;
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    db.Database.CreateIfNotExists();
            //}
            foreach (var path in Directory.GetFiles(@"D:\Work\Stock\samples\stock", "*.txt", SearchOption.TopDirectoryOnly))
            {

                FileInfo fi = new FileInfo(path);
                var lines = File.ReadAllLines(path, Encoding.Default);

                using (var db = new MongoDBContext())
                {
                    List<DailyPrice> list = new List<DailyPrice>();
                    for (int i = 2; i < lines.Length - 1; i++)
                    {
                        var item = DailyPrice.NewDailyPrice(lines[i]);
                        if (item == null)
                        {
                            continue;
                        }
                        item.Id = Guid.NewGuid().ToString("N");
                        item.Code = fi.Name.Replace(fi.Extension, "");
                        list.Add(item);
                    }
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            db.DataSet<DailyPrice>().Insert(item);
                        }
                    }
                }
                File.Move(fi.FullName, @"D:\Work\Stock\samples\OK\" + fi.Name);

            }
        }
    }
}