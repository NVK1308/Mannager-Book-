using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaSach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            trToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            pnHome.Visible = true;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;
            pnCauhinh.Visible = false;
            pnBang1.Visible = true;
            pnDung.Visible = false;

            //dữ liệu
            tbs_columns();
            cauhinh();
            thong_tin(tb_hientai, lb_soluong_tong, lb_theloai_tong);
            dung_luong_tong.Value = dung_luong / (20 * Convert.ToInt32(tb_cauhinh.Rows[0][1])); 
        }


        //
        //----------------------------Bien can dung------------------------------------
        //
        public static DataTable tb_hientai = new DataTable();
        public static DataTable tb_danhmuc = new DataTable();
        public static DataTable tb_ls = new DataTable();
        public static DataTable tb_cauhinh = new DataTable();

        //
        //----------------------------Load du lieu------------------------------------
        //
        // add cot cho datatable 






        // import file txt 
        void data_tb(string filepath, DataTable tb)
        {
            string[] lines = File.ReadAllLines(Application.StartupPath + @"\" + filepath); // đọc tất cả các dòng lưu vào một mảng string
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split('|');
                string[] row = new string[values.Length - 1];
                for (int j = 0; j < values.Length - 1; j++)
                {
                    row[j] = values[j].Trim();
                    //MessageBox.Show(row.Length.ToString() + "\n" + tb.Columns.Count.ToString());
                }
                tb.Rows.Add(row);
            }
        }



        //
        //----------------------------Luc du lieu ------------------------------------
        //

        //
        //----------------------------Tap------------------------------------
        //


        #region: Tap

        // đổi màu cho tap 
        void tap_color()
        {
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                if (menuStrip1.Items[i].Selected == true)
                {
                    menuStrip1.Items[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));

                }
                else
                {
                    menuStrip1.Items[i].BackColor = System.Drawing.SystemColors.Window;
                }
            }

        }
        private void trToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tap_color();
            pnHome.Visible = true;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;
            pnCauhinh.Visible = false;

            pnHome.Visible = true;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;

            // cấu hình 

            pnBang1.Visible = true;
            pnDung.Visible = false;



        }


        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tap_color();
            //
            danhmucthongtin();
            // 


            show_grid(grid_danhmuc, tb_danhmuc);

            pnHome.Visible = false;
            pnDanhmuc.Visible = true;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;
            pnCauhinh.Visible = false;
            //
            pndanhm.Visible = true;
            pnThem.Visible = false;
            spnSearchHt.Visible = false;
            pnSearchDanhm.Visible = false;


            lbName.Text = "BẢNG DANH MỤC SÁCH";
            grid_hientai.Visible = false;
            grid_danhmuc.Visible = true;

            //
            radio(pnTheloai, l_theloai);
            radio(pnTacgia, l_tacgia);
            radio(pnNXB, l_nxb);

        }

        private void lịchSửToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tap_color();
            pnHome.Visible = false;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = true;
            pnCauhinh.Visible = false;

            //
             //data_tb("ls.txt", tb_ls);
             show_grid(grid_ls_nx, tb_ls);
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tap_color();
            pnHome.Visible = false;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = true;
            pnLs.Visible = false;

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tap_color();
            pnHome.Visible = false;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = true;
            pnThongke.Visible = false;
            pnLs.Visible = false;
            pnCauhinh.Visible = false;
        }

        #region: Cau hinh 
        //
        //----------------------------Cau hinh------------------------------------
        //

        private void cấuHìnhGiáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tap_color();
            pnHome.Visible = false;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;
            pnCauhinh.Visible = true;

            F_Cauhinh f = new F_Cauhinh();
            f.ShowDialog();
            tb_cauhinh.Rows.Clear();
            data_tb("tb_cauhinh.txt", tb_cauhinh);
            foreach (Panel panel in l_khoi)
            {
                pnBang1.Controls.Remove(panel);
            }

            cauhinh();
            pnHome.Visible = true;


        }

        // list cau hinh 
        List<Panel> l_khoi = new List<Panel>();
        List<Panel> l_tu = new List<Panel>();
        List<Button> l_bao = new List<Button>();
        List<Panel> l_o = new List<Panel>();

        // danh sách vị trí đã dùng còn trống
        public static List<string> l_vitri = new List<string>();

        public void cauhinh()
        {
            for (int i = 0; i < Convert.ToInt32(tb_cauhinh.Rows[0][0].ToString()); i++)
            {
                 // khối 
                Panel p = new Panel();
                p.Location = new Point(50, 300 * i + 50);
                p.Size = new Size(1380, 250);
                p.BackColor = System.Drawing.SystemColors.Window;
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Name = "B" + (i + 1).ToString();
                l_khoi.Add(p);


                for (int j = 0; j < Convert.ToInt32(tb_cauhinh.Rows[0][1].ToString()); j++)
                {
                    // tủ 
                    Panel p1 = new Panel();
                    p1.Location = new Point(100 * j, 0);
                    p1.Size = new Size(100, 248);
                    p1.BackColor = System.Drawing.SystemColors.Window;
                    p1.BackgroundImage = global::GiaSach.Properties.Resources.gs2;
                    p1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    p1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                    p1.Name = "G_" + (j + 1).ToString();
                    p1.Click += new System.EventHandler(panelke_Click);
                    p1.Name = p.Name + "_" + "G" + (j + 1).ToString();
                    p.Controls.Add(p1);
                   

                    // tên 
                    Label l = new Label();
                    l.AutoSize = true;
                    l.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(201)))), ((int)(((byte)(178)))));
                    l.Enabled = false;
                    l.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    l.Location = new System.Drawing.Point(33, 79);
                    l.Name = "label47";
                    l.Size = new System.Drawing.Size(30, 21);
                    l.Text = "G" + (j + 1).ToString();
                    l.Enabled = false;
                    p1.Controls.Add(l);


                    // đèn báo
                    Button b = new Button();
                    b.Size = new Size(80, 80);
                    b.Location = new Point(10, 10);
                    b.FlatAppearance.BorderSize = 0;
                    b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    b.Enabled = false;
                    b.Visible = false;
                    b.BackColor = Color.Red;
                    p1.Controls.Add(b);
                    l_bao.Add(b);
                    l_tu.Add(p1);
                }
                pnBang1.Controls.Add(p);
            }

            //List vị trí 
            for (int i = 0; i < l_tu.Count; i++)
            {
                for (int j = 1 ; j <= 10; j++)
                {
                    l_vitri.Add(l_tu[i].Name +"_R"+ j.ToString());
                    l_vitri.Add(l_tu[i].Name + "_L" + j.ToString());
                }
            }
           // MessageBox.Show(l_vitri.Count().ToString()); 
        }
        #endregion
        //
        //----------------------------Trở về trang chủ ------------------------------------
        //

        private void btTrangChu_Click(object sender, EventArgs e)

        {
            trToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            quảnLýToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            thốngKêToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            danhMụcToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            lịchSửToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;


            pnHome.Visible = true;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;

            pnHome.Visible = true;
            pnDanhmuc.Visible = false;
            pnQuanly.Visible = false;
            pnThongke.Visible = false;
            pnLs.Visible = false;
            pnCauhinh.Visible = true;
            // cấu hình 

            pnBang1.Visible = false;
            pnDung.Visible = true;
        }
        #endregion

        //
        //----------------------------HÀM THƯỜNG DÙNG ----------------------------------
        //
        #region: hàm thường dùng 
        //
        //----------------------------Hàm cần dùng------------------------------------
        //
        public void tbs_columns()
        {
            void tb_columns(DataGridView dg, DataTable tb)
            {
                tb.Columns.Clear();
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    tb.Columns.Add(dg.Columns[i].HeaderText);
                }
            }

            //
            tb_cauhinh.Columns.Add("Số khối");
            tb_cauhinh.Columns.Add("Số tủ/khối");

            //
            tb_columns(grid_hientai, tb_hientai);
            tb_columns(grid_danhmuc, tb_danhmuc);
            tb_columns(grid_ls_nx, tb_ls);
            //
            data_tb("danhmuc.txt", tb_danhmuc);
            show_grid(grid_danhmuc, tb_danhmuc);

            data_tb("hientai.txt", tb_hientai);
            show_grid(grid_hientai, tb_hientai);

            data_tb("ls.txt", tb_ls);
            show_grid(grid_ls_nx, tb_ls);

            data_tb("tb_cauhinh.txt", tb_cauhinh);
            //
           
          
            data_tb("ls.txt", tb_ls);
            show_grid(grid_ls_nx, tb_ls);

            // load cau hinh: tam
            //editGridview(grid_hientai);
            //editGridview(grid_danhmuc);
            //editGridview(grid_ls_nx); 


        }

        // SHOW DATAGRIDVIEW 

        public void show_grid(DataGridView dg, DataTable tb)
        {
            //dg.Rows.Clear();
            //for (int i = 0; i < tb.Rows.Count; i++)
            //{
            //    DataGridViewRow dr = (DataGridViewRow)dg.Rows[0].Clone();
            //    for (int j = 0; j < dg.Columns.Count; j++)
            //    {
            //        dr.Cells[j].Value = tb.Rows[i][j];
            //    }
            //    dg.Rows.Add(dr);
            //}
     
            dg.Rows.Clear();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dg.Rows.Add("");
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    dg.Rows[i].Cells[j].Value = tb.Rows[i][j];
                }
            }
            editGridview(dg);
        }
        // sửa datagrid view
        public void editGridview(DataGridView a)
        {
            if (a.Columns.Count > 0)
            {
                for (int i = 0; i < a.Columns.Count - 1; i++)
                {
                    a.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                a.Columns[a.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        // add các combobox 
        void addItem(List<string> l, ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("Tất cả");
            for (int i = 0; i < l.Count; i++)
            {
                cb.Items.Add(l[i]);
            }
        }



        // 
        #endregion
        //
        //----------------------------GIAO DIỆN HOME------------------------------------
        //

        #region: GIAO DIỆN HOME + CẤU HÌNH 



        // chiếu đứng 
        private void btBack_Click(object sender, EventArgs e)
        {
            //tb_trangchu.Rows.Clear(); 
            timer_timkiem.Stop();
            pnBang1.Visible = true;
            pnDung.Visible = false;
            //thong_tin(tb_trangchu, lb_soluong, lb_theloai);
            //xóa bảng  tìm kiếm 


            foreach (Button b in l_bao)
            {
                b.Visible = false;
            }
            foreach (Panel p in l_tu)
            {
                p.Enabled = true;
            }

        }

        // chiếu bằng
        private void panelke_Click(object sender, EventArgs e)
        {
            //timer_timkiem.Stop();
            Panel p = (Panel)sender;
            pnBang1.Visible = false;
            pnDung.Visible = true;
            lb_tentu.Text = "Tủ sách số: " + p.Name;

           
            // tạo ô chứa sách

            foreach (Panel panel in l_o)
            {
                string a = panel.Name.Split('_')[2].Substring(0, 1);
                if (a == "L")
                {
                    L.Controls.Remove(panel);
                }
                else
                {
                    R.Controls.Remove(panel);
                }
            }

            // show ô 
            l_o.Clear();
            void pa(int x, int y, Panel tu)
            {

                int dem = 0;
                for (int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < 2; i++)
                    {

                        dem++;
                        Panel p1 = new Panel();
                        p1.Size = new Size(150, 96);
                        p1.Location = new Point(x + 169 * i, y + 125 * j);
                        p1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                        p1.Name = lb_tentu.Text.Split(':')[1].Trim() + "_" + tu.Name + (dem).ToString();
                        p1.Click += new System.EventHandler(panelo_Click);

                        Label l = new Label();
                        l.Enabled = false;
                        l.Text = tu.Name + (dem).ToString();
                        l.Location = new Point(0, 0);
                        l.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        p1.Controls.Add(l);
                        l_o.Add(p1);
                        tu.Controls.Add(p1);
                        

                    }
                }
            }
            // mặt bên trái 
            pa(41, 38, R);
            // mặt bên phải 
            pa(20, 38, L);
            show_tb_trangchu(p.Name);
            lb_bang.Text = "Thông tin chi chiết của tủ:" + p.Name;
            thong_tin(tb_trangchu, lb_soluong, lb_theloai);

            // sửa lại 
            dung_luong_tu.Value = (dung_luong / 20);
            string adl = dung_luong_tu.Value.ToString();
            label3.Text = adl; 

        }

        // các ô chi tiết click 
        private void panelo_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            show_tb_trangchu(p.Name);
            lb_bang.Text = "Thông tin chi tiết của ngăn: " + p.Name; 
        }

        // Tim kiem trang chu ==> trả về giá trị vị trí , sử dụng bộ tìm kiếm chi tết 
        string flag_timkiem = "";
        private void btSearch_Click(object sender, EventArgs e)
        {
            // tim_sach.Clear();


            flag_timkiem = "Tìm kiếm trang chủ";

            pn_trangchu.Controls.Add(pnSearchDanhm);
            pn_trangchu.Controls.Add(pnSeach);
            pnSearchDanhm.Location = new Point(555, -1);
            pnSeach.Location = new Point(1680, -1);

            // thông báo sách
            for (int i = 0; i < l_cbb_chitiet.Count; i++)
            {
                timer_timkiem.Stop();
                l_cbb_chitiet[i].Text = "Tất cả";
                tb_loc.Rows.Clear();
                tim_sach.Clear();
                foreach (Button b in l_bao)
                {
                    b.Visible = false;
                }
            }

            show_grid(grid_hientai, tb_hientai);
            addItem(lit_loc(tb_hientai, 0), cbb_msp_0);
            addItem(lit_loc(tb_hientai, 7), cbb_nxb_7);
            addItem(lit_loc(tb_hientai, 3), cbb_tl_3);
            addItem(lit_loc(tb_hientai, 1), cbb_ts_1);
            addItem(lit_loc(tb_hientai, 2), cbb_tg_2);
            addItem(lit_loc(tb_hientai, 6), cbb_vt_6);
        }

        // show data thông tin chi tiết 
        public static DataTable tb_trangchu = new DataTable();
        public void show_tb_trangchu(string vi_tri_click)
        {
            tb_trangchu.Columns.Clear();
            tb_trangchu.Rows.Clear();
            //
            for (int i = 0; i < tb_hientai.Columns.Count; i++)
            {
                tb_trangchu.Columns.Add(tb_hientai.Columns[i].Caption);
            }
            //
            for (int i = 0; i < tb_hientai.Rows.Count; i++)
            {
                DataRow dr = tb_hientai.Rows[i];
                string[] a = tb_hientai.Rows[i][6].ToString().Split('_');
                // tong thong tin
                if (vi_tri_click.Trim() == a[0] + "_" + a[1])
                {
                    tb_trangchu.Rows.Add(dr.ItemArray);
                   
                }

                // chi tiết trên từng ô 
                if (vi_tri_click.Trim() == tb_hientai.Rows[i][6].ToString().Trim())
                {
                    tb_trangchu.Rows.Add(dr.ItemArray);
                }

            }

            // thông tin 
            
            grid_trangchu.DataSource = null;
            grid_trangchu.DataSource = tb_trangchu;
            editGridview(grid_trangchu);
        }
        // thông tin số lượng sách và đầu sách toàn bộ thư viện 

        int dung_luong = 0; 
        public void thong_tin(DataTable tb, Label sl, Label tl)
        {
            //List<ProgressBar> l = new List<ProgressBar> { progressBar1, bunifuCircleProgressbar1 }; 
            List<string> theloai = new List<string>();
            int soluong = 0;
            List<string> vitri = new List<string>(); 
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                theloai.Add(tb.Rows[i][3].ToString());
                soluong += Convert.ToInt32(tb.Rows[i][5].ToString());
                vitri.Add(tb.Rows[i][6].ToString());

            }
            theloai = theloai.Distinct().ToList();
            vitri = vitri.Distinct().ToList();
            sl.Text = soluong.ToString();
            tl.Text = theloai.Count.ToString();
            dung_luong = vitri.Count;
        }
        // thông tin dung lượng thư viện
    
        

        #endregion



        //
        //----------------------------Tap Danh mục ------------------------------------
        //


        #region : danh mục 

        private void btTatca_Click(object sender, EventArgs e)
        {
            pndanhm.Visible = true;
            pnThem.Visible = false;
            //
            show_grid(grid_danhmuc, tb_danhmuc);
            //

            radio(pnTheloai, l_theloai);
            radio(pnTacgia, l_tacgia);
            radio(pnNXB, l_nxb);
        }

        private void btThemM_Click(object sender, EventArgs e)
        {
            pndanhm.Visible = false;
            pnThem.Visible = true;
        }



        private void btLoc_Click(object sender, EventArgs e)
        {
            pn_danhmuc_chucnag.Controls.Add(pnSearchDanhm);
            pn_danhmuc_chucnag.Controls.Add(pnSeach);
            pnSearchDanhm.Location = new Point(322, 1);
            pnSeach.Location = new Point(1586, 1);

            if (lbName.Text == "BẢNG DANH MỤC SÁCH")
            {
                for (int i = 0; i < l_cbb_danhmuc.Count; i++)
                {
                    l_cbb_danhmuc[i].Text = "Tất cả";
                }

                show_grid(grid_danhmuc, tb_danhmuc);


                addItem(lit_loc(tb_danhmuc, 0), cbb_ms_0);
                addItem(lit_loc(tb_danhmuc, 1), cbb_ts1_1);
                addItem(lit_loc(tb_danhmuc, 2), cbb_tg1_2);
                addItem(lit_loc(tb_danhmuc, 7), cbb_nxb1_7);
                addItem(lit_loc(tb_danhmuc, 3), cbb_tl1_3);
                pnSearchDanhm.Visible = false;
                spnSearchHt.Visible = true;
            }
            else
            {
                for (int i = 0; i < l_cbb_chitiet.Count; i++)
                {
                    l_cbb_chitiet[i].Text = "Tất cả";
                }

                show_grid(grid_hientai, tb_hientai);
                addItem(lit_loc(tb_hientai, 0), cbb_msp_0);
                addItem(lit_loc(tb_hientai, 7), cbb_nxb_7);
                addItem(lit_loc(tb_hientai, 3), cbb_tl_3);
                addItem(lit_loc(tb_hientai, 1), cbb_ts_1);
                addItem(lit_loc(tb_hientai, 2), cbb_tg_2);
                addItem(lit_loc(tb_hientai, 6), cbb_vt_6);
                pnSearchDanhm.Visible = true;
                spnSearchHt.Visible = false;
            }

        }

        // chi tiết 
        private void button5_Click(object sender, EventArgs e)
        {
            if (lbName.Text == "BẢNG DANH MỤC SÁCH")
            {
                lbName.Text = "";
                lbName.Text = "BẢNG DANH SÁCH CHI TIẾT TRÊN GIÁ";
                show_grid(grid_hientai, tb_hientai);
                grid_hientai.Visible = true;
                grid_danhmuc.Visible = false;

            }
            else
            {
                lbName.Text = "";
                lbName.Text = "BẢNG DANH MỤC SÁCH";
                grid_hientai.Visible = false;
                grid_danhmuc.Visible = true;
                show_grid(grid_danhmuc, tb_danhmuc);

            }

            lbName.Location = new Point(pnshow.Width / 2 - lbName.Width / 2, 38);


            pnSearchDanhm.Visible = false;
            spnSearchHt.Visible = false;
        }

        private void tbSearchDM_Click(object sender, EventArgs e)
        {
            tbSearchHt.Clear();
        }


        // thêm button tim kiem 


        public static List<string> l_theloai = new List<string>();
        public static List<string> l_tacgia = new List<string>();
        public static List<string> l_nxb = new List<string>();

        // liệt kê các thông tin chi tiết 
        public void danhmucthongtin()
        {
            List<string> l(DataTable tb, int c)
            {
                List<string> ldm = new List<string>();
                List<string> lds = new List<string>();
                // danh muc
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    lds.Add(tb.Rows[i][c].ToString());
                }
                lds = lds.Distinct().ToList();

                DataTable tb1 = new DataTable();
                tb1.Columns.Add("a");
                tb1.Columns.Add("sl");
                for (int i = 0; i < lds.Count; i++)
                {
                    int sl = 0;
                    tb1.Rows.Add(lds[i]);
                    for (int j = 0; j < tb.Rows.Count; j++)
                    {
                        if (string.Compare(lds[i], tb.Rows[j][c].ToString(), true) == 0)
                        {
                            sl += Convert.ToInt32(tb.Rows[j][5].ToString());
                            tb1.Rows[i][1] = sl;
                        }
                    }
                }
                for (int i = 0; i < tb1.Rows.Count; i++)
                {
                    ldm.Add(tb1.Rows[i][0].ToString() + "(" + tb1.Rows[i][1].ToString() + ")");
                }


                return ldm;
            }

            l_theloai = l(tb_danhmuc, 3);
            l_tacgia = l(tb_danhmuc, 2);
            l_nxb = l(tb_danhmuc, 7);
        }

        public void radio(Panel p, List<string> lt)
        {
            for (int i = 0; i < lt.Count; i++)
            {
                Button b = new Button();
                b.Dock = System.Windows.Forms.DockStyle.Top;
                b.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Name = "bt" + i.ToString();
                b.Margin = new System.Windows.Forms.Padding(3);
                b.TabIndex = i;
                b.Text = lt[i];
                b.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                b.UseVisualStyleBackColor = true;
                b.AutoSize = true;
                //b.Size = new Size(404, 28);
                b.BackColor = System.Drawing.Color.Transparent;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.AutoSize = true;
                b.Click += new System.EventHandler(bttk_Click);
                p.Controls.Add(b);
            }

        }


        //
        //----------------------------tìm kiếm  ------------------------------------
        //
        private void bttk_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string[] t = b.Text.Split('(');
            grid_danhmuc.Rows.Clear();
            for (int i = 0; i < tb_danhmuc.Rows.Count; i++)
            {
                if (string.Compare(t[0], tb_danhmuc.Rows[i][2].ToString(), true) == 0
                    || string.Compare(t[0], tb_danhmuc.Rows[i][3].ToString(), true) == 0
                    || string.Compare(t[0], tb_danhmuc.Rows[i][7].ToString(), true) == 0)
                {
                    DataGridViewRow dr = (DataGridViewRow)grid_danhmuc.Rows[0].Clone();
                    for (int j = 0; j < grid_danhmuc.Columns.Count; j++)
                    {
                        dr.Cells[j].Value = tb_danhmuc.Rows[i][j];
                    }
                    grid_danhmuc.Rows.Add(dr);
                }
            }
        }

        // tim kiem tong the 

        public void timkiem(DataTable tb, DataGridView dg)
        {
            dg.Rows.Clear();
            int dem = 0;

            string t = convertToUnSign(tbSearchHt.Text);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    string a = convertToUnSign(tb.Rows[i][j].ToString());
                    if (string.Compare(t.Trim(), a.Trim(), true) == 0)
                    {

                        DataGridViewRow dr = (DataGridViewRow)dg.Rows[0].Clone();
                        for (int k = 0; k < dg.Columns.Count; k++)
                        {
                            dr.Cells[k].Value = tb.Rows[i][k];
                        }
                        dem++;
                        dg.Rows.Add(dr);

                    }
                }
            }

            if (dem == 0)
            {
                MessageBox.Show("Không có thông tin!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbSearchHt.Text = "Tìm kiếm";
                show_grid(grid_hientai, tb_hientai);
                show_grid(grid_danhmuc, tb_danhmuc);
            }
        }

        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        // vị trí tìm kiếm 
        List<string> tim_sach = new List<string>();
        private void bt_timkiemdm_Click(object sender, EventArgs e)
        {
            if (lbName.Text == "BẢNG DANH MỤC SÁCH")
            {
                timkiem(tb_danhmuc, grid_danhmuc);
            }
            // 
            if (lbName.Text != "BẢNG DANH MỤC SÁCH")
            {
                timkiem(tb_hientai, grid_hientai);
            }

            //
            if (flag_timkiem == "Tìm kiếm trang chủ")
            {
                tim_sach.Clear();
                string t = convertToUnSign(tbSearchHt.Text);
                for (int i = 0; i < tb_hientai.Rows.Count; i++)
                {
                    for (int j = 0; j < tb_hientai.Columns.Count; j++)
                    {
                        string a = convertToUnSign(tb_hientai.Rows[i][j].ToString());
                        if (string.Compare(t.Trim(), a.Trim(), true) == 0)
                        {
                            tim_sach.Add(tb_hientai.Rows[i][6].ToString());
                        }
                    }
                }
                //
                if (tim_sach.Count == 0)
                {
                    MessageBox.Show("Không có thông tin cần tìm!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    timer_timkiem.Start();
                }

            }
        }

        // tối ưu hóa hàm tìm kiếm, bỏ dấu tiếng việt

       


        // danh sách các item trong combobox 
        public List<string> lit_loc(DataTable tb, int c)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                l.Add(tb.Rows[i][c].ToString());
            }
            l = l.Distinct().ToList();
            return l;
        }

        // thêm item cho ô 


        //
        //---------------------------Lọc danh mục ------------------------------------
        //

        DataTable tb_loc = new DataTable();

        List<ComboBox> l_cbb_chitiet = new List<ComboBox>();
        List<ComboBox> l_cbb_danhmuc = new List<ComboBox>();


        public void loc_danhmuc(DataTable tb)
        {
            tb_loc.Rows.Clear();
            tb_loc.Columns.Clear();

            for (int i = 0; i < tb.Columns.Count; i++)
            {
                tb_loc.Columns.Add(tb.Columns[i].Caption);
            }

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                DataRow dr1 = tb.Rows[i];
                tb_loc.Rows.Add(dr1.ItemArray);
            }

            #region: xay dung lai
            ////List<ComboBox> l_cb = new List<ComboBox> { cbb_ms_0, cbb_ts1_1, cbb_tg1_2, cbb_tl1_3, cbb_nxb1_6 };

            ////DataTable tb1_tk(ComboBox c)
            ////{
            ////    DataTable tb1 = new DataTable();

            ////    for (int i = 0; i < tb_loc.Columns.Count; i++)
            ////    {
            ////        tb1.Columns.Add(tb_loc.Columns[i].Caption);
            ////    }

            ////    for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            ////    {
            ////        string a = tb_loc.Rows[i][Convert.ToInt16(c.Name.Substring(c.Name.Length - 1))].ToString().Trim();

            ////        if (c.Text != "" && c.Text != "Tất cả")
            ////        {
            ////            if (string.Compare(c.Text, a, true) == 0)
            ////            {

            ////                DataRow dr = tb_loc.Rows[i];
            ////                tb1.Rows.Add(dr.ItemArray);
            ////                MessageBox.Show(dr.ItemArray[1].ToString());
            ////            }
            ////        }
            ////    }
            ////    return tb1;
            ////}



            ////show_grid(grid_danhmuc, tb1_tk(l_cb[0]));
            ////show_grid(grid_danhmuc, tb1_tk(l_cb[1]));
            ////show_grid(grid_danhmuc, tb1_tk(l_cb[2]));
            ////show_grid(grid_danhmuc, tb1_tk(l_cb[3]));
            ////show_grid(grid_danhmuc, tb1_tk(l_cb[4]));
            #endregion



            //loc danh muc
            List<ComboBox> l_cb1 = new List<ComboBox> { cbb_ms_0, cbb_ts1_1, cbb_tg1_2, cbb_tl1_3, cbb_nxb1_7 };
            l_cbb_danhmuc = l_cb1;
            // -1 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[0].Name.Substring(l_cb1[0].Name.Length - 1))].ToString().Trim();

                if (l_cb1[0].Text != "Tất cả" && l_cb1[0].Text != "")
                {
                    if (string.Compare(l_cb1[0].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -2 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[1].Name.Substring(l_cb1[1].Name.Length - 1))].ToString().Trim();

                if (l_cb1[1].Text != "Tất cả" && l_cb1[1].Text != "")
                {
                    if (string.Compare(l_cb1[1].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -3 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[2].Name.Substring(l_cb1[2].Name.Length - 1))].ToString().Trim();

                if (l_cb1[2].Text != "Tất cả" && l_cb1[2].Text != "")
                {
                    if (string.Compare(l_cb1[2].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -4 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[3].Name.Substring(l_cb1[3].Name.Length - 1))].ToString().Trim();

                if (l_cb1[3].Text != "Tất cả" && l_cb1[3].Text != "")
                {
                    if (string.Compare(l_cb1[3].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -5 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[4].Name.Substring(l_cb1[4].Name.Length - 1))].ToString().Trim();

                if (l_cb1[4].Text != "Tất cả" && l_cb1[4].Text != "")
                {
                    if (string.Compare(l_cb1[4].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }

        }
        public void loc_chi_tiet(DataTable tb)
        {
            tb_loc.Rows.Clear();
            tb_loc.Columns.Clear();

            for (int i = 0; i < tb.Columns.Count; i++)
            {
                tb_loc.Columns.Add(tb.Columns[i].Caption);
            }

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                DataRow dr1 = tb.Rows[i];
                tb_loc.Rows.Add(dr1.ItemArray);
            }


            List<ComboBox> l_cb1 = new List<ComboBox> { cbb_msp_0, cbb_ts_1, cbb_tg_2, cbb_tl_3, cbb_vt_6, cbb_nxb_7 };
            l_cbb_chitiet = l_cb1;
            //-1
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[0].Name.Substring(l_cb1[0].Name.Length - 1))].ToString().Trim();

                if (l_cb1[0].Text != "Tất cả" && l_cb1[0].Text != "")
                {
                    if (string.Compare(l_cb1[0].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -2 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[1].Name.Substring(l_cb1[1].Name.Length - 1))].ToString().Trim();

                if (l_cb1[1].Text != "Tất cả" && l_cb1[1].Text != "")
                {
                    if (string.Compare(l_cb1[1].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -3 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[2].Name.Substring(l_cb1[2].Name.Length - 1))].ToString().Trim();

                if (l_cb1[2].Text != "Tất cả" && l_cb1[2].Text != "")
                {
                    if (string.Compare(l_cb1[2].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -4 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[3].Name.Substring(l_cb1[3].Name.Length - 1))].ToString().Trim();

                if (l_cb1[3].Text != "Tất cả" && l_cb1[3].Text != "")
                {
                    if (string.Compare(l_cb1[3].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // -5 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[4].Name.Substring(l_cb1[4].Name.Length - 1))].ToString().Trim();

                if (l_cb1[4].Text != "Tất cả" && l_cb1[4].Text != "")
                {
                    if (string.Compare(l_cb1[4].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }
            // - 6 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][Convert.ToInt32(l_cb1[5].Name.Substring(l_cb1[5].Name.Length - 1))].ToString().Trim();

                if (l_cb1[5].Text != "Tất cả" && l_cb1[5].Text != "")
                {
                    if (string.Compare(l_cb1[5].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
            }

        }
        private void cbb_nxb1_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            int t = Convert.ToInt32(c.Name.Substring(c.Name.Length - 1));

            loc_danhmuc(tb_danhmuc);
            if (tb_loc.Rows.Count > 0)
            {
                show_grid(grid_danhmuc, tb_loc);

                //thu ngắn cbb
                for (int i = 0; i < l_cbb_danhmuc.Count; i++)
                {
                    if (c.Name != l_cbb_danhmuc[i].Name)
                    {
                        addItem(lit_loc(tb_loc, Convert.ToInt32(l_cbb_danhmuc[i].Name.Substring(l_cbb_danhmuc[i].Name.Length - 1))), l_cbb_danhmuc[i]);

                    }
                }

            }
            else
            {
                show_grid(grid_danhmuc, tb_danhmuc);
                for (int i = 0; i < l_cbb_danhmuc.Count; i++)
                {
                    l_cbb_danhmuc[i].Text = "Tất cả";

                }
                MessageBox.Show("Không có thông tin!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void cbb_nxb_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            loc_chi_tiet(tb_hientai);
            if (tb_loc.Rows.Count > 0)
            {
                show_grid(grid_hientai, tb_loc);
                //thu ngắn cbb
                for (int i = 0; i < l_cbb_chitiet.Count; i++)
                {
                    if (c.Name != l_cbb_chitiet[i].Name)
                    {
                        addItem(lit_loc(tb_loc, Convert.ToInt32(l_cbb_chitiet[i].Name.Substring(l_cbb_chitiet[i].Name.Length - 1))), l_cbb_chitiet[i]);

                    }
                }
            }
            else
            {

                show_grid(grid_hientai, tb_hientai);
                for (int i = 0; i < l_cbb_chitiet.Count; i++)
                {
                    l_cbb_chitiet[i].Text = "Tất cả";
                }
                MessageBox.Show("Không có thông tin!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //
            if (flag_timkiem == "Tìm kiếm trang chủ")
            {
                tim_sach.Clear();
                for (int i = 0; i < tb_loc.Rows.Count; i++)
                {
                    tim_sach.Add(tb_loc.Rows[i][6].ToString());

                }

                if (tim_sach.Count == 0)
                {
                    timer_timkiem.Stop();
                }
                else
                {
                    timer_timkiem.Start();
                }



            }
        }

        // thông báo có hàng 
        int tick = 0;
        private void timer_timkiem_Tick(object sender, EventArgs e)
        {

            tick++;


            for (int j = 0; j < tim_sach.Count; j++)
            {
                string vt = tim_sach[j].Split('_')[0] + "_" + tim_sach[j].Split('_')[1];

                for (int i = 0; i < l_tu.Count; i++)
                {
                    string vtt = l_tu[i].Name;
                    if (vt == vtt)
                    {
                        l_tu[i].Enabled = true;
                        if (tick % 2 == 0)
                        {
                            l_bao[i].Visible = true;
                        }
                        else
                        {
                            l_bao[i].Visible = false;
                        }

                    }
                    else
                    {
                        l_tu[i].Enabled = false;
                    }
                }
                // ô 
                string o = tim_sach[j].Trim();
                for (int i = 0; i < l_o.Count; i++)
                {
                    if (l_o[i].Name == o)
                    {
                        if (tick % 2 == 0)
                        {
                            l_o[i].BackColor = Color.Red;
                        }
                        else
                        {
                            l_o[i].BackColor = SystemColors.GradientInactiveCaption;
                        }

                    }
                }

            }




        }



        #endregion


        // nhập sách 
        private void btNhap_Click(object sender, EventArgs e)
        {
            F_NhapXuat f = new F_NhapXuat();
            f.Show();
        }

        // xuất sách 
        private void btXuat_Click(object sender, EventArgs e)
        {

        }

        // mượn sách 
        private void btMuon_Click(object sender, EventArgs e)
        {

        }

        // trả sách
        private void btTra_Click(object sender, EventArgs e)
        {

        }


        // 
        public static string tille_nx;
        private void btNhap_Click_1(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            tille_nx = b.Text;
            F_NXMT f = new F_NXMT();
            f.Show();
            f.Text = b.Text;
            // 
            // danh sách các ngăn còn trống 
            for (int i = 0; i < tb_hientai.Rows.Count; i++)
            {
                for (int j = 0; j < l_vitri.Count; j++)
                {
                    if (l_vitri[j] == tb_hientai.Rows[i][6].ToString().Trim())
                    {
                        l_vitri.RemoveAt(j); 
                    }
                }
            }
          

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
