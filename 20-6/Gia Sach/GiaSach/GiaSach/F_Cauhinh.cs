using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaSach
{
    public partial class F_Cauhinh : Form
    {
        public F_Cauhinh()
        {
            InitializeComponent();
        }


        public static int sokhoi, sotu;


        // đồng ý 
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.tb_cauhinh.Rows.Clear(); 
            if (tb_khoi.Text != "" && tb_tu.Text != "")
            {
                sokhoi = Convert.ToInt16(tb_khoi.Text);
                sotu = Convert.ToInt16(tb_tu.Text);
                Form1.tb_cauhinh.Rows.Add(sokhoi.ToString(), sotu.ToString());
            }
            else
            {
                tb_khoi.Focus();
                tb_tu.Focus();
            }

            //dataGridView1.DataSource = Form1.tb_cauhinh;
            tool_save_txt(Form1.tb_cauhinh, "tb_cauhinh.txt");
           
            this.Close();
        }

        // lưu cấu hình tổng quát cho mọi bảng txt, 

        public static void tool_save_txt(DataTable tb, string filepath )
        {
            string[] b = new string[tb.Rows.Count];
            //tb.Rows.Clear(); 
            // 
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                string a = "";
                for (int j = 0; j < tb.Columns.Count; j++)
                { 
                    a += tb.Rows[i][j].ToString() + " " +"\t" + "|";
                    b[i] = a;
                }
            }
            File.WriteAllLines(Application.StartupPath + @"\" + filepath, b);
         
        }


    }
}
