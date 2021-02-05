using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaSach
{
    public partial class F_NXMT : Form
    {
        public F_NXMT()
        {
            InitializeComponent();
        }

        private void F_NXMT_Load(object sender, EventArgs e)
        {
            show_nhap_xuat();
            lbTile.Text = Form1.tille_nx;
            if (Form1.tille_nx == "Nhập sách" || Form1.tille_nx == "Trả sách")
            {
                cbb_vt_6.Visible = false;
            }
            if (Form1.tille_nx == "Mượn sách")
            {
                button4.Text = "Hạn mượn";
            }

            if (Form1.tille_nx == "Mượn sách" || Form1.tille_nx == "Trả sách")
            {
                pnMuonTra.Visible = true;
                if (Form1.tille_nx == "Trả sách")
                {
                    trasach();

                }
                else
                {
                    muuonsach();
                }
            }





            // 
            tb_ds_dachon.Columns.Add("Mã sách");
            tb_ds_dachon.Columns.Add("Tên sách");
            tb_ds_dachon.Columns.Add("Số lượng");
            tb_ds_dachon.Columns.Add("Vị trí");
            tb_ds_dachon.Columns.Add("Giao dịch");

        }
        // tìm kiếm 

        private void pntk_Click(object sender, EventArgs e)
        {
            tb_search.Clear();
            tb_search.Focus();
        }
        private void tb_search_Click(object sender, EventArgs e)
        {
            tb_search.Clear();
            tb_search.Focus();
        }


        // show grid cho nhập xuất 
        public void show_nhap_xuat()
        {
            // nhập sách 
            if (Form1.tille_nx == "Nhập sách" || Form1.tille_nx == "Trả sách")
            {
                show_grid(Grid_ds, Form1.tb_danhmuc);
                addItem(lit_loc(Form1.tb_danhmuc, 0), cbb_msp_0);
                addItem(lit_loc(Form1.tb_danhmuc, 8), cbb_nxb_7);
                addItem(lit_loc(Form1.tb_danhmuc, 3), cbb_tl_3);
                addItem(lit_loc(Form1.tb_danhmuc, 1), cbb_ts_1);
                addItem(lit_loc(Form1.tb_danhmuc, 2), cbb_tg_2);

                // trả sách 
                if (Form1.tille_nx == "Trả sách")
                {
                    show_grid(Grid_ds, Form1.tb_muontra);
                }

            }

            // xuất sách
            if (Form1.tille_nx == "Xuất sách" || Form1.tille_nx == "Mượn sách")
            {
                show_grid(Grid_ds, Form1.tb_hientai);

                // laod combobox
                addItem(lit_loc(Form1.tb_hientai, 0), cbb_msp_0);
                addItem(lit_loc(Form1.tb_hientai, 7), cbb_nxb_7);
                addItem(lit_loc(Form1.tb_hientai, 3), cbb_tl_3);
                addItem(lit_loc(Form1.tb_hientai, 1), cbb_ts_1);
                addItem(lit_loc(Form1.tb_hientai, 2), cbb_tg_2);
                addItem(lit_loc(Form1.tb_hientai, 6), cbb_vt_6);
            }
        }
        // hàm tất cả click 
        private void button6_Click(object sender, EventArgs e)
        {
            show_nhap_xuat();
            cbb_msp_0.Text = "Mã sách";
            cbb_ts_1.Text = "Tên sách";
            cbb_tg_2.Text = "Tác giả";
            cbb_tl_3.Text = "Thể loại";
            cbb_nxb_7.Text = "NXB";
            cbb_vt_6.Text = "Vị trí";
        }

        private void btsearch_Click(object sender, EventArgs e)
        {
            //Form1.
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                //Form1.timkiem(Form1.tb_hientai, Grid_ds, tb_search,  "Nhập mã, tên, tác giả, thể loại,..");

                timkiem(Form1.tb_danhmuc, Grid_ds);

            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                // Form1.timkiem(Form1.tb_danhmuc, Grid_ds, tb_search, "Nhập mã, tên, tác giả, thể loại,..");
                timkiem(Form1.tb_hientai, Grid_ds);
            }
        }

        // hàm tìm kiếm  
        public void timkiem(DataTable tb, DataGridView dg)
        {
            //dg.Rows.Clear();
            int dem = 0;

            string t = convertToUnSign(tb_search.Text);
            DataTable tbtk = new DataTable();
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                tbtk.Columns.Add(tb.Columns[i].Caption);
            }
            for (int i = tb.Rows.Count - 1; i >= 0; i--)
            {
                for (int j = tb.Columns.Count - 1; j >= 0; j--)
                {
                    string a = convertToUnSign(tb.Rows[i][j].ToString());
                    if (string.Compare(t.Trim(), a.Trim(), true) == 0)
                    {
                        DataRow dr = tb.Rows[i];
                        tbtk.Rows.Add(dr.ItemArray);
                    }
                }
            }
            if (tbtk.Rows.Count != 0)
            {
                show_grid(dg, tbtk);
            }
            else
            {
                MessageBox.Show("Không có thông tin!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_search.Text = "Nhập mã, tên, tác giả, thể loại,..";
                show_grid(dg, tb);
            }
        }

        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        // hàm show datagrid 

        public static void show_grid(DataGridView dg, DataTable tb)
        {
            dg.Columns.Clear();
            dg.Rows.Clear();
            dg.ColumnCount = tb.Columns.Count;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                dg.Columns[i].HeaderText = tb.Columns[i].Caption.Trim();

            }

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dg.Rows.Add("");
                for (int j = 0; j < tb.Columns.Count; j++)
                {
                    dg.Rows[i].Cells[j].Value = tb.Rows[i][j];
                }
            }
            Form1.editGridview(dg);
        }



        // hàm add item cbb 
        void addItem(List<string> l, ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("Tất cả");
            for (int i = 0; i < l.Count; i++)
            {
                cb.Items.Add(l[i]);
            }
        }

        // lit danh sách item 
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


        // tạo bảng danh sách 
        Panel add_ds()
        {
            Panel p = new Panel(); // hàng 

            p.Dock = System.Windows.Forms.DockStyle.Top;
            p.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            p.Size = new System.Drawing.Size(721, 40);
            p.Name = l_ds[0];
            // button Mã sách 
            Button bms = new Button();
            bms.Dock = DockStyle.Left;
            bms.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            bms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bms.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bms.ForeColor = System.Drawing.Color.Black;
            bms.Location = new System.Drawing.Point(664, 0);
            bms.Margin = new System.Windows.Forms.Padding(0);
            bms.Text = "Mã sách";
            bms.TextAlign = ContentAlignment.MiddleLeft;
            bms.Size = new System.Drawing.Size(85, 40);
            bms.BackColor = System.Drawing.Color.White;
            if (l_ds.Count > 0)
            {
                bms.Text = l_ds[0];
            }


            // button tên sách 
            Button bts = new Button();
            bts.Dock = DockStyle.Left;
            bts.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            bts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bts.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bts.ForeColor = System.Drawing.Color.Black;
            bts.BackColor = System.Drawing.Color.White;
            bts.Margin = new System.Windows.Forms.Padding(0);
            bts.Text = "Tên sách";
            bts.TextAlign = ContentAlignment.MiddleLeft;
            bts.Size = new System.Drawing.Size(279, 40);
            bts.Text = l_ds[1];


            // cbb vị trí 
            ComboBox cbbvt = new ComboBox();
            cbbvt.BackColor = System.Drawing.Color.White;
            cbbvt.Dock = System.Windows.Forms.DockStyle.Left;
            cbbvt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cbbvt.FormattingEnabled = true;
            cbbvt.Size = new System.Drawing.Size(91, 37);
            cbbvt.Name = "vt_" + p.Name;
            cbbvt.SelectedValueChanged += new System.EventHandler(cbbvt_SelectedValueChanged);
            // chọn vị trí 
            if (Form1.tille_nx == "Nhập sách" || Form1.tille_nx == "Trả sách")
            {
                // cùng tên 
                for (int j = 0; j < Form1.tb_hientai.Rows.Count; j++)
                {
                    string ma = Form1.tb_hientai.Rows[j][0].ToString().Trim(); // mã 

                    // cùng tên 
                    if (ma == cbbvt.Name.Split('_')[1])
                    {
                        //MessageBox.Show(ma + "\n" + cbbvt.Name.Split('_')[1]);
                        cbbvt.Items.Clear();
                        cbbvt.Items.Add(Form1.tb_hientai.Rows[j][6].ToString().Trim());
                        cbbvt.Text = (Form1.tb_hientai.Rows[j][6].ToString().Trim());
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < Form1.l_vitri.Count; i++)
                        {
                            cbbvt.Items.Add(Form1.l_vitri[i]);
                        }
                    }

                }

            }
            if (Form1.tille_nx == "Xuất sách" || Form1.tille_nx == "Mượn sách")
            {
                cbbvt.Text = l_ds[3];
            }


            // số lượng 
            NumericUpDown n_sl = new NumericUpDown();
            n_sl.BackColor = System.Drawing.Color.White;
            n_sl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            n_sl.Dock = System.Windows.Forms.DockStyle.Left;
            n_sl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            n_sl.Size = new System.Drawing.Size(80, 38);

            n_sl.ValueChanged += new System.EventHandler(n_sl_ValueChanged);
            n_sl.Name = "sl_" + p.Name;
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                n_sl.Maximum = Convert.ToInt32(Grid_ds.Rows[index].Cells[5].Value.ToString());
            }

            if (lbTile.Text == "Trả sách")
            {
                n_sl.Maximum = Convert.ToInt32(Grid_ds.Rows[index].Cells[9].Value.ToString()) - Convert.ToInt32(Grid_ds.Rows[index].Cells[13].Value.ToString());
            }

            // button xóa
            Button b_xoa = new Button();
            b_xoa.BackColor = System.Drawing.Color.White;
            b_xoa.BackgroundImage = global::GiaSach.Properties.Resources.trash;
            b_xoa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            b_xoa.Dock = System.Windows.Forms.DockStyle.Left;
            b_xoa.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            b_xoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            b_xoa.ForeColor = System.Drawing.Color.Black;
            b_xoa.Margin = new System.Windows.Forms.Padding(0);
            b_xoa.Text = "";
            b_xoa.Size = new System.Drawing.Size(57, 40);
            b_xoa.UseVisualStyleBackColor = false;
            b_xoa.Click += new System.EventHandler(xoa_click);
            b_xoa.Name = "X_" + p.Name;

            //
            p.Controls.Add(b_xoa);
            // p.Controls.Add(cbgd);
            p.Controls.Add(cbbvt);
            p.Controls.Add(n_sl);
            p.Controls.Add(bts);
            p.Controls.Add(bms);
            // list danh sách panel 
            l_pds.Add(p);
            return p;
        }
        public void xoa_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (MessageBox.Show("Bạn có muốn xóa sách này ra khỏi giỏ hàng không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = l_pds.Count - 1; i >= 0; i--)
                {
                    if (l_pds[i].Name == b.Name.Split('_')[1])
                    {
                        pn_Giohang.Controls.RemoveAt(i);
                        l_pds.Remove(l_pds[i]);

                    }

                }

                // xóa bảng da cho n
                for (int i = tb_ds_dachon.Rows.Count - 1; i >= 0; i--)
                {
                    if (b.Name.Split('_')[1].Trim() == tb_ds_dachon.Rows[i][0].ToString().Trim())
                    {
                        sls.Text = (Convert.ToInt32(sls.Text) - Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString())).ToString();
                        tb_ds_dachon.Rows.RemoveAt(i);


                    }
                }
            }
            slds.Text = tb_ds_dachon.Rows.Count.ToString();
        }

        // chọn nhâp xuất 
        int index = -2;
        public static DataTable table_nx = new DataTable();
        public static List<string> l_ds = new List<string>();
        List<Panel> l_pds = new List<Panel>();
        DataTable tb_ds_dachon = new DataTable();
        //  int stt_ds = 0; 
        private void Grid_ds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //stt_ds++; 
            l_ds.Clear();
            int ma = -1, ten = -1, sl = -1, vitri = -1;
            for (int i = 0; i < Grid_ds.Columns.Count; i++)
            {

                if (Grid_ds.Columns[i].HeaderText.Trim() == "Mã sách")
                {
                    ma = i;
                }
                if (Grid_ds.Columns[i].HeaderText.Trim() == "Tên sách")
                {
                    ten = i;
                }
                if (Grid_ds.Columns[i].HeaderText.Trim() == "Số lượng" || Grid_ds.Columns[i].HeaderText.Trim() == "Số lượng mượn")
                {
                    sl = i;
                }
                if (Grid_ds.Columns[i].HeaderText.Trim() == "Vị trí")
                {
                    vitri = i;
                }
            }
            index = e.RowIndex;
            if (index != -1)
            {
                l_ds.Add(Grid_ds.Rows[index].Cells[ma].Value.ToString());      // mã sách 
                l_ds.Add(Grid_ds.Rows[index].Cells[ten].Value.ToString());      // tên sách
                l_ds.Add("0");      // số lượng
                l_ds.Add(Grid_ds.Rows[index].Cells[6].Value.ToString());      // vị trí 
                l_ds.Add(Form1.tille_nx);
            }
            // không cho chọn 1 sản phẩm 2 lần 

            // bảng danh sách đã chọn, 
            if (l_ds.Count > 0)
            {
                tb_ds_dachon.Rows.Add(l_ds[0], l_ds[1], l_ds[2], l_ds[3], l_ds[4]);
            }

            // kiểm tra các sản phẩm đã chọn 

            int temp_trung = 0;
            for (int i = 0; i < tb_ds_dachon.Rows.Count - 1; i++)
            {
                if (l_ds.Count > 0)
                {
                    if (l_ds[0] == tb_ds_dachon.Rows[i][0])
                    {
                        temp_trung++;
                        tb_ds_dachon.Rows.RemoveAt(i);
                    }
                }

            }
            // không cho trùng 
            if (temp_trung == 0 && l_ds.Count > 0)
            {
                pn_Giohang.Controls.Add(add_ds());
            }
            slds.Text = tb_ds_dachon.Rows.Count.ToString();
            dataGridView1.DataSource = tb_ds_dachon;

        }




        // đồng ý nhập xuất 
        public static string flag_hoanthanhtacvu = "";
        private void Bt_Save_ls_Click(object sender, EventArgs e)
        {
            int checkdung = 0;
            int checksvt = 0, checksl2 = 0;
            int indexsai = -1;

            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                if (tb_ds_dachon.Rows[i][2].ToString().Trim() != "0" && tb_ds_dachon.Rows[i][3].ToString() != "")
                {
                    checkdung++;

                }
                else
                {
                    checkdung = 0;
                    if (tb_ds_dachon.Rows[i][3].ToString().Trim() == "")
                    {
                        checksvt++; //  chưa chọn vị trí 
                        indexsai = i;
                    }
                    if (tb_ds_dachon.Rows[i][2].ToString().Trim() == "0")
                    {
                        checksl2++; //  chưa chọn số lượng
                        indexsai = i;
                    }
                    break;
                }
            }

            if (checkdung != 0)
            {
                Form1.tb_ds_home.Columns.Clear();
                Form1.tb_ds_home.Rows.Clear();
                for (int i = 0; i < tb_ds_dachon.Columns.Count; i++)
                {
                    Form1.tb_ds_home.Columns.Add(tb_ds_dachon.Columns[i].Caption);

                }
                for (int j = 0; j < tb_ds_dachon.Rows.Count; j++)
                {
                    DataRow dr = tb_ds_dachon.Rows[j];
                    Form1.tb_ds_home.Rows.Add(dr.ItemArray);
                }
                // Form1.tb_ds_home.DefaultView.Sort = "Vị trí";
                if (lbTile.Text == "Mượn sách")
                {
                    module_muon_sach();
                }

                if (lbTile.Text == "Trả sách")
                {
                    module_tra_sach();
                }
                if (lbTile.Text == "Nhập sách" || lbTile.Text == "Xuất sách")
                {
                    save_ls();
                }
                // cập nhật hiện tạo 
                save_danhmuc();
                save_hientai();
                //tác vụ mượn trả 

                // xóa dữ liệu 
                for (int i = l_pds.Count - 1; i >= 0; i--)
                {
                    pn_Giohang.Controls.RemoveAt(i);
                }
                l_pds.Clear();
                l_ds.Clear();


                slds.Text = "0";
                sls.Text = "0";
                //
                this.Close();
            }
            else
            {
                if (checksvt != 0)
                {
                    MessageBox.Show("Vui lòng chọn vị trí để " + lbTile.Text + " ''" + tb_ds_dachon.Rows[indexsai][1].ToString() + "''.", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (checksl2 != 0)
                {
                    MessageBox.Show("Vui lòng chọn số lượng để " + lbTile.Text + " ''" + tb_ds_dachon.Rows[indexsai][1].ToString() + "''.", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

        }

        // ham luu lich su 
        public void save_ls()
        {
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                {
                    if (tb_ds_dachon.Rows[i][0].ToString().Trim() == Form1.tb_danhmuc.Rows[j][0].ToString().Trim())
                    {
                        Form1.tb_ls.Rows.Add(
                                               DateTime.Now.ToShortDateString(), // ngày  
                                               tb_ds_dachon.Rows[i][4], // giao dịch 
                                               tb_ds_dachon.Rows[i][0], // mã sách 
                                               tb_ds_dachon.Rows[i][1], // tên sách
                                               Form1.tb_danhmuc.Rows[j][2],  // tác giả 
                                               Form1.tb_danhmuc.Rows[j][3],  // thể loại
                                               Form1.tb_danhmuc.Rows[j][4],  // số trang
                                               tb_ds_dachon.Rows[i][2], // số lượng
                                               tb_ds_dachon.Rows[i][3], // vị trí 
                                               Form1.tb_danhmuc.Rows[j][8],  // NXB
                                               lb_Acount.Text, // PIC
                                               ""
                                            );
                    }
                }
            }
        }


        // ham luu vao bang nhap 
        public void save_hientai()
        {
            // đã có hàng trên giá 
            for (int i = tb_ds_dachon.Rows.Count - 1; i >= 0; i--)
            {
                string ma_dachon = tb_ds_dachon.Rows[i][0].ToString().Trim();
                for (int k = Form1.tb_hientai.Rows.Count - 1; k >= 0; k--)
                {
                    string ma_hientai = Form1.tb_hientai.Rows[k][0].ToString().Trim();
                    if (ma_dachon == ma_hientai) //trùng nhau 
                    {
                        if (lbTile.Text.Trim() == "Nhập sách" || lbTile.Text.Trim() == "Trả sách")
                        {
                            int sl = Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString()) + Convert.ToInt32(Form1.tb_hientai.Rows[k][5].ToString());
                            Form1.tb_hientai.Rows[k][5] = sl;

                        }


                        if (lbTile.Text == "Xuất sách" || lbTile.Text.Trim() == "Mượn sách")
                        {
                            int sl = -Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString()) + Convert.ToInt32(Form1.tb_hientai.Rows[k][5].ToString());

                            Form1.tb_hientai.Rows[k][5] = sl;
                            if (sl == 0)
                            {
                                Form1.tb_hientai.Rows.RemoveAt(k);
                            }
                        }
                        tb_ds_dachon.Rows.RemoveAt(i);   // xóa ngăn hết hàng 
                        break;
                    }
                }
            }


            // chưa có hàng trên ngăn 

            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                string ma_dachon = tb_ds_dachon.Rows[i][0].ToString().Trim();
                for (int k = 0; k < Form1.tb_hientai.Rows.Count; k++)
                {
                    string ma_hientai = Form1.tb_hientai.Rows[k][0].ToString().Trim();
                    if (ma_dachon != ma_hientai)
                    {
                        int sl = Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                        string vt = tb_ds_dachon.Rows[i][3].ToString();
                        for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                        {
                            if (tb_ds_dachon.Rows[i][0].ToString().Trim() == Form1.tb_danhmuc.Rows[j][0].ToString().Trim())
                            {
                                if (tb_ds_dachon.Rows[i][3].ToString() != null && tb_ds_dachon.Rows[i][3].ToString() != "")
                                {
                                    Form1.tb_hientai.Rows.Add(
                                                    tb_ds_dachon.Rows[i][0], // mã sách 
                                                    tb_ds_dachon.Rows[i][1], // tên sách
                                                    Form1.tb_danhmuc.Rows[j][2],  // tác giả 
                                                    Form1.tb_danhmuc.Rows[j][3],  // thể loại
                                                    Form1.tb_danhmuc.Rows[j][4],  // số trang
                                                    sl.ToString(), // số lượng
                                                    tb_ds_dachon.Rows[i][3],  // vị trí
                                                    Form1.tb_danhmuc.Rows[j][8], // NXB
                                                   "" // ghi chú 
                                                    );
                                }
                                //
                                else
                                {
                                    MessageBox.Show("Vui lòng chọn vị trí bạn muốn " + lbTile.Text + "!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        break;
                    }

                }
            }


        }

        public void save_danhmuc()
        {
            for (int i = 0; i < Form1.tb_danhmuc.Rows.Count; i++)
            {
                for (int j = 0; j < Form1.tb_danhmuc.Columns.Count - 1; j++)
                {
                    if (Form1.tb_danhmuc.Rows[i][j].ToString() == "")
                    {
                        Form1.tb_danhmuc.Rows[i][j] = 0;
                    }
                }
            }
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                string ma_hientai = tb_ds_dachon.Rows[i][0].ToString().Trim();

                for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                {

                    string ma_danhmuc = Form1.tb_danhmuc.Rows[j][0].ToString().Trim();
                    if (string.Compare(ma_danhmuc, ma_hientai, true) == 0)
                    {
                        // nhập sách thì chỉ cộng thêm số lượng sách còn các giá trị còn lại thì không đổi 
                        if (string.Compare(lbTile.Text, "Nhập sách", true) == 0)
                        {
                            int sl = Convert.ToInt32(Form1.tb_danhmuc.Rows[j][5].ToString()) + Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                            Form1.tb_danhmuc.Rows[j][5] = sl;
                        }

                        // xuất sách chỉ thay đổi tổng số lượng
                        if (string.Compare(lbTile.Text, "Xuất sách", true) == 0)
                        {
                            int sl = Convert.ToInt32(Form1.tb_danhmuc.Rows[j][5].ToString()) - Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                            Form1.tb_danhmuc.Rows[j][5] = sl;
                        }
                        // nhaapk  sách thì vẫn giữ nguyên tổng số lượng 
                        if (string.Compare(lbTile.Text, "Mượn sách", true) == 0)
                        {
                            int slm = Convert.ToInt32(Form1.tb_danhmuc.Rows[j][6].ToString()) + Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                            Form1.tb_danhmuc.Rows[j][6] = slm;
                        }

                        // sẽ thay đổi số lượng của số lượng mượn và số dư 

                        if (string.Compare(lbTile.Text, "Trả sách", true) == 0)
                        {
                            int slm = Convert.ToInt32(Form1.tb_danhmuc.Rows[j][6].ToString()) - Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
                            Form1.tb_danhmuc.Rows[j][6] = slm;
                        }



                    }

                    Form1.tb_danhmuc.Rows[j][7] = Convert.ToInt32(Form1.tb_danhmuc.Rows[j][5].ToString()) - Convert.ToInt32(Form1.tb_danhmuc.Rows[j][6].ToString());
                }
            }
        }




        private void n_sl_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown n = (NumericUpDown)sender;
            int soluon = 0, sodausach = 0;

            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {

                if (tb_ds_dachon.Rows[i][0].ToString().Trim() == n.Name.Split('_')[1].Trim())
                {
                    tb_ds_dachon.Rows[i][2] = n.Value.ToString();
                }
                soluon += Convert.ToInt32(tb_ds_dachon.Rows[i][2].ToString());
            }
            sls.Text = soluon.ToString();
            slds.Text = tb_ds_dachon.Rows.Count.ToString();
        }

        private void cbbvt_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                if (tb_ds_dachon.Rows[i][0].ToString().Trim() == c.Name.Split('_')[1].Trim())
                {
                    tb_ds_dachon.Rows[i][3] = c.Text;
                }
            }
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            if (tb_ds_dachon.Rows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác " + lbTile.Text + "?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tb_ds_dachon.Rows.Clear();
                    for (int i = 0; i < l_pds.Count; i++)
                    {
                        pn_Giohang.Controls.Remove(l_pds[i]);
                    }
                    Form1.tb_ds_home.Rows.Clear();

                    l_pds.Clear();
                }
            }
            show_nhap_xuat();
        }



        #region: Lọc tim kiem 
        DataTable tb_loc = new DataTable();

        List<ComboBox> l_cbb_xuatmuon = new List<ComboBox>();
        List<ComboBox> l_cbb_nhaptra = new List<ComboBox>();


        public void loc_nhaptra(DataTable tb)
        {
            //tb_loc.Rows.Clear();
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

            //MessageBox.Show(tb_loc.Rows.Count.ToString()); 

            //loc danh muc
            List<ComboBox> l_cb1 = new List<ComboBox> { cbb_msp_0, cbb_ts_1, cbb_tg_2, cbb_tl_3, cbb_nxb_7, cbb_vt_6 };
            // l_cbb_nhaptra = l_cb1;
            // mã sản phẩm  
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][0].ToString().Trim();
                if (l_cb1[0].Text != "Tất cả" && l_cb1[0].Text != "" && l_cb1[0].Text != "Mã sách")
                {

                    if (string.Compare(l_cb1[0].Text, b, true) != 0)
                    {
                        tb_loc.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    break;
                }

            }
            //  tên sách  
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][1].ToString().Trim();

                if (l_cb1[1].Text != "Tất cả" && l_cb1[1].Text != "" && l_cb1[1].Text != "Tên sách")
                {
                    if (string.Compare(l_cb1[1].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
                else
                {
                    break;
                }
            }
            // tác giả 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][2].ToString().Trim();

                if (l_cb1[2].Text != "Tất cả" && l_cb1[2].Text != "" && l_cb1[2].Text != "Tác giả")
                {
                    if (string.Compare(l_cb1[2].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
                else
                {
                    break;
                }
            }
            // thể loại 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = tb_loc.Rows[i][3].ToString().Trim();

                if (l_cb1[3].Text != "Tất cả" && l_cb1[3].Text != "" && l_cb1[3].Text != "Thể loại")
                {
                    if (string.Compare(l_cb1[3].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
                else
                {
                    break;
                }
            }
            // nhà xuất bản 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {
                string b = "";
                if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
                {
                    b = tb_loc.Rows[i][8].ToString().Trim();
                }
                if (lbTile.Text == "Mượn sách" || lbTile.Text == "Xuất sách")
                {
                    b = tb_loc.Rows[i][7].ToString().Trim();
                }


                if (l_cb1[4].Text != "Tất cả" && l_cb1[4].Text != "" && l_cb1[4].Text != "NXB")
                {
                    if (string.Compare(l_cb1[4].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
                else
                {
                    break;
                }
            }
            // vị trí 
            for (int i = tb_loc.Rows.Count - 1; i >= 0; i--)
            {

                string b = tb_loc.Rows[i][6].ToString().Trim();


                if (l_cb1[4].Text != "Tất cả" && l_cb1[5].Text != "" && l_cb1[5].Text != "Vị trí")
                {
                    if (string.Compare(l_cb1[5].Text, b, true) != 0)
                    {
                        tb_loc.Rows.Remove(tb_loc.Rows[i]);
                    }
                }
                else
                {
                    break;
                }
            }

        }

        // 

        public void show_loc()
        {
            string dk = "";
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                loc_nhaptra(Form1.tb_danhmuc);
                dk = "nhập trả";
            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                loc_nhaptra(Form1.tb_hientai);
                dk = "xuất trả";
            }

            if (tb_loc.Rows.Count > 0)
            {
                show_grid(Grid_ds, tb_loc);


            }
            else
            {

                MessageBox.Show("Không có thông tin!", "Thông báo:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_search.Text = "Nhập mã, tên, tác giả, thể loại,..";
                if (dk == "nhập trả")
                {
                    show_grid(Grid_ds, Form1.tb_danhmuc);

                }
                if (dk == "xuất trả")
                {
                    show_grid(Grid_ds, Form1.tb_hientai);

                }
            }

        }


        private void cbb_msp_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            show_loc();

            //addItem(lit_loc(tb_loc, 0), cbb_msp_0);
            addItem(lit_loc(tb_loc, 1), cbb_ts_1);
            addItem(lit_loc(tb_loc, 2), cbb_tg_2);
            addItem(lit_loc(tb_loc, 3), cbb_tl_3);
            addItem(lit_loc(tb_loc, 6), cbb_vt_6);
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                addItem(lit_loc(tb_loc, 8), cbb_nxb_7);

            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                addItem(lit_loc(tb_loc, 7), cbb_nxb_7);

            }
        }

        private void cbb_ts_1_SelectedValueChanged(object sender, EventArgs e)
        {
            show_loc();
            addItem(lit_loc(tb_loc, 0), cbb_msp_0);
            //addItem(lit_loc(tb_loc, 1), cbb_ts_1);
            addItem(lit_loc(tb_loc, 2), cbb_tg_2);
            addItem(lit_loc(tb_loc, 3), cbb_tl_3);
            addItem(lit_loc(tb_loc, 6), cbb_vt_6);
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                addItem(lit_loc(tb_loc, 8), cbb_nxb_7);

            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                addItem(lit_loc(tb_loc, 7), cbb_nxb_7);

            }

        }

        private void cbb_tg_2_SelectedValueChanged_1(object sender, EventArgs e)
        {
            show_loc();
            addItem(lit_loc(tb_loc, 0), cbb_msp_0);
            addItem(lit_loc(tb_loc, 1), cbb_ts_1);
            //addItem(lit_loc(tb_loc, 2), cbb_tg_2);
            addItem(lit_loc(tb_loc, 3), cbb_tl_3);
            addItem(lit_loc(tb_loc, 6), cbb_vt_6);
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                addItem(lit_loc(tb_loc, 8), cbb_nxb_7);

            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                addItem(lit_loc(tb_loc, 7), cbb_nxb_7);

            }

        }

        private void cbb_tl_3_SelectedValueChanged(object sender, EventArgs e)
        {
            show_loc();
            addItem(lit_loc(tb_loc, 0), cbb_msp_0);
            addItem(lit_loc(tb_loc, 1), cbb_ts_1);
            addItem(lit_loc(tb_loc, 2), cbb_tg_2);
            //addItem(lit_loc(tb_loc, 3), cbb_tl_3);
            addItem(lit_loc(tb_loc, 6), cbb_vt_6);
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                addItem(lit_loc(tb_loc, 8), cbb_nxb_7);

            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                addItem(lit_loc(tb_loc, 7), cbb_nxb_7);

            }
        }

        private void cbb_nxb_7_SelectedValueChanged(object sender, EventArgs e)
        {
            show_loc();
            addItem(lit_loc(tb_loc, 0), cbb_msp_0);
            addItem(lit_loc(tb_loc, 1), cbb_ts_1);
            addItem(lit_loc(tb_loc, 2), cbb_tg_2);
            addItem(lit_loc(tb_loc, 3), cbb_tl_3);
            addItem(lit_loc(tb_loc, 6), cbb_vt_6);
            //if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            //{
            //    addItem(lit_loc(tb_loc, 8), cbb_nxb_7);

            //}
            //if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            //{
            //    addItem(lit_loc(tb_loc, 7), cbb_nxb_7);

            //}
        }

        private void cbb_vt_6_SelectedValueChanged(object sender, EventArgs e)
        {
            show_loc();
            addItem(lit_loc(tb_loc, 0), cbb_msp_0);
            addItem(lit_loc(tb_loc, 1), cbb_ts_1);
            addItem(lit_loc(tb_loc, 2), cbb_tg_2);
            addItem(lit_loc(tb_loc, 3), cbb_tl_3);
            // addItem(lit_loc(tb_loc, 6), cbb_vt_6);
            if (lbTile.Text == "Nhập sách" || lbTile.Text == "Trả sách")
            {
                addItem(lit_loc(tb_loc, 8), cbb_nxb_7);

            }
            if (lbTile.Text == "Xuất sách" || lbTile.Text == "Mượn sách")
            {
                addItem(lit_loc(tb_loc, 7), cbb_nxb_7);

            }
        }
        #endregion

        // GUI trả sách 
        public void trasach()
        {
            List<string> maphieu = new List<string>();
            List<string> mabandoc = new List<string>();
            List<string> tenbandoc = new List<string>();
            // xây dựng bằng hàm 
            for (int i = 0; i < Form1.tb_muontra.Rows.Count; i++)
            {
                maphieu.Add(Form1.tb_muontra.Rows[i][0].ToString());
                mabandoc.Add(Form1.tb_muontra.Rows[i][1].ToString());
                tenbandoc.Add(Form1.tb_muontra.Rows[i][2].ToString());
            }
            maphieu = maphieu.Distinct().ToList();
            mabandoc = mabandoc.Distinct().ToList();
            tenbandoc = tenbandoc.Distinct().ToList();
            for (int i = 0; i < maphieu.Count; i++)
            {
                cbb_maphieu.Items.Add(maphieu[i]);
            }
            for (int i = 0; i < mabandoc.Count; i++)
            {
                cbb_mdocgia.Items.Add(mabandoc[i]);
            }
            for (int i = 0; i < tenbandoc.Count; i++)
            {
                cbb_tendocgia.Items.Add(tenbandoc[i]);
            }
        }

        // GUI mượn sách 
        public void muuonsach()
        {
            cbb_maphieu.Text = "MP1";
            if (Form1.ls_muontra.Rows.Count > 0)
            {
                int max = (Convert.ToInt32(Form1.ls_muontra.Rows[0][2].ToString().Substring(2)));

                // tìm max min 
                for (int i = 1; i < Form1.ls_muontra.Rows.Count; i++)
                {

                    if (max < Convert.ToInt32(Form1.ls_muontra.Rows[i][2].ToString().Substring(2)))
                    {
                        max = Convert.ToInt32(Form1.ls_muontra.Rows[i][2].ToString().Substring(2));
                    }
                }
                cbb_maphieu.Text = "MP" + (max + 1).ToString();
            }
        }

        private void cbb_maphieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            DataTable tb = new DataTable();
            for (int i = 0; i < Form1.tb_muontra.Columns.Count; i++)
            {
                tb.Columns.Add(Form1.tb_muontra.Columns[i].Caption);
            }

            for (int i = 0; i < Form1.tb_muontra.Rows.Count; i++)
            {
                // mã phiếu
                string ma = Form1.tb_muontra.Rows[i][0].ToString().Trim();
                if (ma == c.Text)
                {
                    DataRow dr = Form1.tb_muontra.Rows[i];
                    tb.Rows.Add(dr.ItemArray);
                    cbb_mdocgia.Text = "";
                    cbb_tendocgia.Text = "";
                    //cbb_maphieu.Text = tb.Rows[0][0].ToString();
                }

                // mã độc giả 
                string madocgia = Form1.tb_muontra.Rows[i][1].ToString().Trim();
                if (madocgia == c.Text)
                {
                    DataRow dr = Form1.tb_muontra.Rows[i];
                    tb.Rows.Add(dr.ItemArray);
                    // cbb_mdocgia.Text = "";
                    cbb_tendocgia.Text = "";
                    cbb_maphieu.Text = "";
                }

                // tên độc giả
                string tendocgia = Form1.tb_muontra.Rows[i][2].ToString().Trim();
                if (tendocgia == c.Text)
                {
                    DataRow dr = Form1.tb_muontra.Rows[i];
                    tb.Rows.Add(dr.ItemArray);
                    cbb_mdocgia.Text = "";
                    // cbb_tendocgia.Text = "";
                    cbb_maphieu.Text = "";
                }
            }
            cbb_mdocgia.Text = tb.Rows[0][1].ToString();
            cbb_tendocgia.Text = tb.Rows[0][2].ToString();
            cbb_maphieu.Text = tb.Rows[0][0].ToString();
            show_grid(Grid_ds, tb);
        }

        // xây dựng module mượn trả  
        // hàm lưu vào bảng mượn trả 
        public void module_muon_sach()
        {
            string ghichu = "";
            // tb mượn 
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                string ma = tb_ds_dachon.Rows[i][0].ToString().Trim();
                for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                {
                    string matt = Form1.tb_danhmuc.Rows[j][0].ToString().Trim();
                    if (string.Compare(ma, matt, true) == 0 && lbTile.Text == "Mượn sách")            // lấy thông tin sách 
                    {
                        Form1.tb_muontra.Rows.Add(
                                     cbb_maphieu.Text,                 // mã phiếu
                                     cbb_mdocgia.Text,                 //Mã độc giả  
                                     cbb_tendocgia.Text,               // tên bạn đọc
                                     lb_Acount.Text,                   // nhân viên
                                     ma,                               //"Mã sách"
                                     tb_ds_dachon.Rows[i][1],          //"Tên sách"
                                     Form1.tb_danhmuc.Rows[j][3],      // "Thể loại",
                                     Form1.tb_danhmuc.Rows[j][8],      //"Nhà xuất bản",
                                     tb_ds_dachon.Rows[i][3],          //  "Vị trí",
                                     tb_ds_dachon.Rows[i][2],          // số lượng mượn 
                                     DateF.Value.ToShortDateString(),
                                     dateT.Value.ToShortDateString(),   //"Hạn mượn",
                                     "",                                //"Số lượng trả",
                                     "0",                                //"Ngày trả",
                                     "Còn " + (dateT.Value - DateTime.Now).Days.ToString() + " ngày");//"Ghi chú");
                    }
                }

            }


            // lưu vào lịch sử mượn 
            for (int i = 0; i < tb_ds_dachon.Rows.Count; i++)
            {
                string ma = tb_ds_dachon.Rows[i][0].ToString().Trim();
                if (lbTile.Text == "Mượn sách")
                {
                    for (int j = 0; j < Form1.tb_danhmuc.Rows.Count; j++)
                    {
                        string matt = Form1.tb_danhmuc.Rows[j][0].ToString().Trim();
                        if (string.Compare(ma, matt, true) == 0)  //lấy thonngo tin sách
                        {
                            Form1.ls_muontra.Rows.Add(
                                     DateTime.Now.ToShortDateString(),  // ngày giao dịch 
                                     lbTile.Text,                       // giao dịch
                                     cbb_maphieu.Text,                 // mã phiếu
                                     cbb_mdocgia.Text,                 //Mã độc giả  
                                     cbb_tendocgia.Text,               // tên bạn đọc
                                     lb_Acount.Text,                   // nhân viên
                                     ma,                               //"Mã sách"
                                     tb_ds_dachon.Rows[i][1],          //"Tên sách"
                                     Form1.tb_danhmuc.Rows[j][3],      // "Thể loại",
                                     Form1.tb_danhmuc.Rows[j][8],      //"Nhà xuất bản",
                                     tb_ds_dachon.Rows[i][3],          //  "Vị trí",
                                     tb_ds_dachon.Rows[i][2],          // số lượng mượn 
                                     DateF.Value.ToShortDateString(),   // ngày mượn
                                     dateT.Value.ToShortDateString(),   //"Hạn mượn",
                                     "",                                //"Ngày trả",
                                     "0",                                //"Số lượng trả",
                                     "Còn " + (dateT.Value - DateTime.Now).Days.ToString() + " ngày");//"Ghi chú");
                        }
                    }
                }
            }

        }

        //Module trả sách 
        public void module_tra_sach()
        {
            for (int i = 0; i < Form1.tb_muontra.Rows.Count; i++)
            {
                if (Form1.tb_muontra.Rows[i]["Số lượng trả"].ToString() == "")
                {
                    Form1.tb_muontra.Rows[i]["Số lượng trả"] = 0;
                }
            }

            // tb_mượn trả
            for (int i = Form1.tb_muontra.Rows.Count - 1; i >= 0; i--)
            {
                string maphieu = Form1.tb_muontra.Rows[i]["Mã phiếu"].ToString().Trim();

                for (int j = tb_ds_dachon.Rows.Count - 1 ; j >= 0 ; j--)
                {
                    string masacht = Form1.tb_muontra.Rows[i]["Mã sách"].ToString().Trim();
                    string masach = tb_ds_dachon.Rows[j][0].ToString();
                    if (masach == masacht && maphieu == cbb_maphieu.Text.Trim())  // xác định sách trả
                    {
                        Form1.tb_muontra.Rows[i][13] = Convert.ToInt32(Form1.tb_muontra.Rows[i][13].ToString()) + Convert.ToInt32(tb_ds_dachon.Rows[j][2].ToString());       // số lượng trả
                        Form1.tb_muontra.Rows[i][12] = DateTime.Now.ToShortDateString();                              // ngày trả 
                        if (DateTime.Now > DateTime.Parse(Form1.tb_muontra.Rows[i]["Hạn mượn"].ToString()))
                        {
                            Form1.tb_muontra.Rows[i]["Ghi chú"] = "Quá hạn " + (DateTime.Now - DateTime.Parse(Form1.tb_muontra.Rows[i]["Hạn mượn"].ToString())).Days.ToString() + " ngày";
                        }

                        //lịch sử mượn trả
                        Form1.ls_muontra.Rows.Add(
                           DateTime.Now.ToShortDateString(),
                           lbTile.Text,
                           Form1.tb_muontra.Rows[j][0],
                           Form1.tb_muontra.Rows[j][1],
                           Form1.tb_muontra.Rows[j][2],
                           Form1.tb_muontra.Rows[j][3],
                           Form1.tb_muontra.Rows[j][4],
                           Form1.tb_muontra.Rows[j][5],
                           Form1.tb_muontra.Rows[j][6],
                           Form1.tb_muontra.Rows[j][7],
                           Form1.tb_muontra.Rows[j][8],
                           Form1.tb_muontra.Rows[j][9],
                           Form1.tb_muontra.Rows[j][10],
                           Form1.tb_muontra.Rows[j][11],
                           DateTime.Now.ToShortDateString(),
                           tb_ds_dachon.Rows[i][2],
                           "");
                    }
                }
            }
            //xóa hàng đã trả xong 
            for (int i = Form1.tb_muontra.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(Form1.tb_muontra.Rows[i][9].ToString()) == Convert.ToInt32(Form1.tb_muontra.Rows[i][13].ToString()))
                {
                    Form1.tb_muontra.Rows.Remove(Form1.tb_muontra.Rows[i]);
                }
            }
            
            // cảnh báo quá hạn 

            for (int i = 0; i < Form1.ls_muontra.Rows.Count; i++)
            {
                if (DateTime.Now > DateTime.Parse(Form1.ls_muontra.Rows[i]["Hạn mượn"].ToString()))
                {
                    Form1.ls_muontra.Rows[i]["Ghi chú"] = "Quá hạn " + (DateTime.Now - DateTime.Parse(Form1.ls_muontra.Rows[i]["Hạn mượn"].ToString())).Days.ToString() + " ngày.";
                }
            }
        }
    }
}
