﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class NhanKhauThuongTruGUI : Form
    {
        NhanKhauBUS nk;
        NhanKhauThuongTruBUS nktt;
        TieuSuBUS tieuSu;
        public TieuSuDTO tieusudto;
        TienAnTienSuBUS tienAn;
        public TienAnTienSuDTO tienanDTO;
        public NhanKhauThuongTruDTO nkttDTO;
        SoHoKhauBUS shk;
        TinhThanhPhoBUS ttp;
        public NhanKhauThuongTruGUI()
        {
            InitializeComponent();
            nktt = new NhanKhauThuongTruBUS();
            tieuSu = new TieuSuBUS();
            tienAn = new TienAnTienSuBUS();
            shk = new SoHoKhauBUS();
            tbMaNKTT.Text = TrinhTaoMa.TangMa9kytu(TrinhTaoMa.getLastID_MaNhanKhauThuongTru());
            ttp = new TinhThanhPhoBUS();

            //dGVTieuSu.DataSource = null;
            //dGVTieuSu.Rows.Clear();
            //dGVTieuSu.DataSource = nktt.GetAll().Tables["nhankhauthuongtru"];
            LoadtieuSu();
            //dGVTienAnTienSu.DataSource = tienAn.GetAll().Tables[0];
            Loadtienantiensu();
            //themMaDinhDanhBang(); hàm này để chạy 2 cái datafridview bị lỗi.... ô sửa lại đi

            cbbNoiCap.DisplayMember = "ten";
            cbbNoiCap.ValueMember = "matp";
            cbbNoiCap.DataSource = ttp.GetAll().Tables[0];
            cbbNoiSinh.DisplayMember = "ten";
            cbbNoiSinh.ValueMember = "matp";
            cbbNoiSinh.DataSource = ttp.GetAll().Tables[0];
            cbbNoiCap.SelectedValue = cbbNoiSinh.SelectedValue = "74";
        }

        #region Các hàm phụ hỗ trợ
        private void themMaDinhDanhBang()
        {
            for (int i = 0; i < dGVTieuSu.RowCount; i++)
                dGVTieuSu.Rows[i].Cells[1].Value = tbmadinhdanh.Text;
            for (int i = 0; i < dGVTienAnTienSu.RowCount; i++)
            {
                dGVTienAnTienSu.Rows[i].Cells[1].Value = tbmadinhdanh.Text;

            }
        }
        private void LoadtieuSu()
        {
            try
            {
                dGVTieuSu.DataSource = null;
                dGVTieuSu.Rows.Clear();
                dGVTieuSu.DataSource = tieuSu.TimKiem("madinhdanh='" + tbmadinhdanh.Text + "'").Tables["tieusu"];
                for (int i = 0; i < dGVTieuSu.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dGVTieuSu[dGVTieuSu.ColumnCount - 1, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Loadtienantiensu()
        {
            try
            {
                dGVTienAnTienSu.DataSource = null;
                dGVTienAnTienSu.Rows.Clear();
                dGVTienAnTienSu.DataSource = tienAn.TimKiem("madinhdanh='" + tbmadinhdanh.Text + "'").Tables["tienantiensu"];
                for (int i = 0; i < dGVTienAnTienSu.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dGVTienAnTienSu[dGVTienAnTienSu.ColumnCount - 1, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void fillData()
        {
            tbhoten.Text = nkttDTO.HoTen;
            tbTenKhac.Text = nkttDTO.TenKhac;
            rdNam.Checked = (nkttDTO.GioiTinh == "nam"); rdNu.Checked = (nkttDTO.GioiTinh == "nu");
            dtpNgaySinh.Value = nkttDTO.NgaySinh;
            tbdantoc.Text = nkttDTO.DanToc;
            tbNgheNghiep.Text = nkttDTO.NgheNghiep;
            tbmadinhdanh.Text = nkttDTO.MaDinhDanh;
            tbhochieu.Text = nkttDTO.HoChieu;
            dtpNgayCap.Value = DateTime.Now;
            cbbNoiCap.SelectedValue = "74";
            tbnguyenquan.Text = nkttDTO.NguyenQuan;
            cbbNoiSinh.Text = nkttDTO.NoiSinh;
            tbquoctich.Text = nkttDTO.QuocTich;
            tbtongiao.Text = nkttDTO.TonGiao;
            tbsodienthoai.Text = nkttDTO.SDT;

            tbMaNKTT.Text = nkttDTO.MaNhanKhauThuongTru;
            tbSoSHK.Text = nkttDTO.SoSoHoKhau;
            tbDCThuongTru.Text = nkttDTO.DiaChiThuongTru;
            tbDCHienTai.Text = nkttDTO.DiaChiHienNay;
            tbTrinhDoHocVan.Text = nkttDTO.TrinhDoHocVan;
            tbTrinhDoCM.Text = nkttDTO.TrinhDoChuyenMon;
            tbBietTiengDanToc.Text = nkttDTO.BietTiengDanToc;
            tbTrinhDoNN.Text = nkttDTO.TrinhDoNgoaiNgu;
            tbNoiLamViec.Text = "Tỉnh Bình Dương";
            tbQHVoiCH.Text = nkttDTO.QuanHeVoiChuHo;
            LoadtieuSu();
            Loadtienantiensu();
        }

        #endregion

        public NhanKhauThuongTruGUI(string sosohokhau)
        {
            InitializeComponent();
            nktt = new NhanKhauThuongTruBUS();
            tieuSu = new TieuSuBUS();
            tienAn = new TienAnTienSuBUS();
            shk = new SoHoKhauBUS();
            ttp = new TinhThanhPhoBUS();

            tbSoSHK.Text = sosohokhau;
            tbSoSHK.Enabled = false;
            tbMaNKTT.Text = TrinhTaoMa.TangMa9kytu(TrinhTaoMa.getLastID_MaNhanKhauThuongTru());
            LoadtieuSu();
            Loadtienantiensu();

            cbbNoiCap.DisplayMember = "ten";
            cbbNoiCap.ValueMember = "matp";
            cbbNoiCap.DataSource = ttp.GetAll().Tables[0];
            cbbNoiSinh.DisplayMember = "ten";
            cbbNoiSinh.ValueMember = "matp";
            cbbNoiSinh.DataSource = ttp.GetAll().Tables[0];
        }
        public NhanKhauThuongTruGUI(string sosohokhau, string diachithuongtru)
        {
            InitializeComponent();
            nktt = new NhanKhauThuongTruBUS();
            tieuSu = new TieuSuBUS();
            tienAn = new TienAnTienSuBUS();
            shk = new SoHoKhauBUS();
            ttp = new TinhThanhPhoBUS();

            tbSoSHK.Text = sosohokhau;
            tbSoSHK.Enabled = false;
            tbDCThuongTru.Text = diachithuongtru;
            tbDCThuongTru.Enabled = false;

            tbMaNKTT.Text = TrinhTaoMa.TangMa9kytu(TrinhTaoMa.getLastID_MaNhanKhauThuongTru());
            LoadtieuSu();
            Loadtienantiensu();

            cbbNoiCap.DisplayMember = "ten";
            cbbNoiCap.ValueMember = "matp";
            cbbNoiCap.DataSource = ttp.GetAll().Tables[0];
            cbbNoiSinh.DisplayMember = "ten";
            cbbNoiSinh.ValueMember = "matp";
            cbbNoiSinh.DataSource = ttp.GetAll().Tables[0];
        }

        public NhanKhauThuongTruGUI(string madinhdanh, int i)
        {
            InitializeComponent();
            nktt = new NhanKhauThuongTruBUS();
            tieuSu = new TieuSuBUS();
            tienAn = new TienAnTienSuBUS();
            shk = new SoHoKhauBUS();
            ttp = new TinhThanhPhoBUS();

            tbmadinhdanh.Text = madinhdanh;
            tbSoSHK.Enabled = false;

            tbMaNKTT.Text = TrinhTaoMa.TangMa9kytu(TrinhTaoMa.getLastID_MaNhanKhauThuongTru());
            LoadtieuSu();
            Loadtienantiensu();

            cbbNoiCap.DisplayMember = "ten";
            cbbNoiCap.ValueMember = "matp";
            cbbNoiCap.DataSource = ttp.GetAll().Tables[0];
            cbbNoiSinh.DisplayMember = "ten";
            cbbNoiSinh.ValueMember = "matp";
            cbbNoiSinh.DataSource = ttp.GetAll().Tables[0];
        }

        private void NhanKhauThuongTruGUI_Load(object sender, EventArgs e)
        {


        }

        private void button_them_Click(object sender, EventArgs e)
        {
            //string gioiTinh = rdNam.Checked ? "nam" : "nu";


            nkttDTO = new NhanKhauThuongTruDTO(tbMaNKTT.Text,tbDCThuongTru.Text, tbQHVoiCH.Text, tbSoSHK.Text, tbmadinhdanh.Text, tbhoten.Text, tbTenKhac.Text, dtpNgaySinh.Value,
                rdNam.Checked?"nam":"nu",cbbNoiSinh.Text, tbnguyenquan.Text, tbdantoc.Text, tbtongiao.Text, tbquoctich.Text, tbhochieu.Text, tbDCThuongTru.Text,
                tbDCHienTai.Text, tbsodienthoai.Text,tbTrinhDoHocVan.Text,tbTrinhDoCM.Text,tbBietTiengDanToc.Text, tbTrinhDoNN.Text,tbNgheNghiep.Text);

            if (nktt.Add(nkttDTO))
            {
                MessageBox.Show(this, "Thành công!");

            }
            else
            {
                MessageBox.Show(this, "Lỗi!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_sua_Click(object sender, EventArgs e)
        {
            nkttDTO = new NhanKhauThuongTruDTO(tbMaNKTT.Text, tbDCThuongTru.Text, tbQHVoiCH.Text, tbSoSHK.Text, tbmadinhdanh.Text, tbhoten.Text, tbTenKhac.Text, dtpNgaySinh.Value,
                rdNam.Checked ? "nam" : "nu", cbbNoiSinh.Text, tbnguyenquan.Text, tbdantoc.Text, tbtongiao.Text, tbquoctich.Text, tbhochieu.Text, tbDCThuongTru.Text,
                tbDCHienTai.Text, tbsodienthoai.Text, tbTrinhDoHocVan.Text, tbTrinhDoCM.Text, tbBietTiengDanToc.Text, tbTrinhDoNN.Text, tbNgheNghiep.Text);

            if (nktt.Update(nkttDTO, -1))
            {
                MessageBox.Show(this, "Thành công!");
            }
            else
            {
                MessageBox.Show(this, "Lỗi!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_xoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbMaNKTT.Text))
            {
                MessageBox.Show(this, "Thiếu!", "Vui Lòng nhập mã nhân khẩu hoặc mã thường trú", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nktt.XoaNKTT(tbMaNKTT.Text))
            {
                MessageBox.Show(this, "Thành công!");
            }
            else
            {
                MessageBox.Show(this, "Lỗi!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void tbDCThuongTru_Enter(object sender, EventArgs e)
        {

            using (ChonDonViHanhChinhGUI a = new ChonDonViHanhChinhGUI())
            {
                a.ShowDialog(this);
                if (a.diaChi != "")
                    tbDCThuongTru.Text = a.diaChi;
            }
        }

        private void tbDCHienTai_Enter(object sender, EventArgs e)
        {
            using (ChonDonViHanhChinhGUI a = new ChonDonViHanhChinhGUI())
            {
                a.ShowDialog(this);
                if (a.diaChi != "")
                    tbDCHienTai.Text = a.diaChi;
            }
        }

        private void tbnguyenquan_Enter(object sender, EventArgs e)
        {
            using (ChonDonViHanhChinhGUI a = new ChonDonViHanhChinhGUI())
            {
                a.ShowDialog(this);
                if (a.diaChi != "")
                    tbnguyenquan.Text = a.diaChi;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            nktt.XoaNKTT(nkttDTO.MaNhanKhauThuongTru);
            nkttDTO = null;
            this.Close();
        }
       
        private void useradd(DataGridView data)
        {
            try
            {
                int lastRow = data.Rows.Count - 2;
                DataGridViewRow nRow = data.Rows[lastRow];
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                data[data.ColumnCount - 1, lastRow] = linkCell;
                nRow.Cells["Change"].Value = "Insert";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void doubleclick(DataGridView data, int lastRow)
        {
            try
            {
                DataGridViewRow nRow = data.Rows[lastRow];
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                data[data.ColumnCount - 1, lastRow] = linkCell;
                nRow.Cells["Change"].Value = "Update";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dGVTieuSu_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            useradd(dGVTieuSu);
        }

        private void dGVTieuSu_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            doubleclick(dGVTieuSu, e.RowIndex);
        }

        private void dGVTienAnTienSu_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            useradd(dGVTienAnTienSu);
        }

        private void dGVTienAnTienSu_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            doubleclick(dGVTienAnTienSu, e.RowIndex);
        }

        private void dGVTieuSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dGVTieuSu.ColumnCount - 1)
                {
                    string Task = dGVTieuSu.Rows[e.RowIndex].Cells[dGVTieuSu.ColumnCount - 1].Value.ToString();
                    if (Task == "Delete")
                    {
                        if (MessageBox.Show("Bạn có chắc chắm muốn xóa không?", "Đang xóa...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            tieuSu.Delete(rowIndex);
                        }
                    }
                    else if (Task == "Insert")
                    {
                        int row = dGVTieuSu.Rows.Count - 2;
                        string matieusu = dGVTieuSu.Rows[row].Cells["matieusu"].Value.ToString();
                        string madinhdanh = dGVTieuSu.Rows[row].Cells["madinhdanh"].Value.ToString();
                        string thoigianbatdau = dGVTieuSu.Rows[row].Cells["thoigianbatdau"].Value.ToString();
                        DateTime date_tgbd = DateTime.Parse(thoigianbatdau);
                        string thoigianketthuc = dGVTieuSu.Rows[row].Cells["thoigianketthuc"].Value.ToString();
                        DateTime date_tgkt = DateTime.Parse(thoigianketthuc);
                        string choo = dGVTieuSu.Rows[row].Cells["choo"].Value.ToString();
                        string manghenghiep = dGVTieuSu.Rows[row].Cells["nghenghiep"].Value.ToString();
                        string noilamviec = dGVTieuSu.Rows[row].Cells["noilamviec"].Value.ToString();
                        tieusudto = new TieuSuDTO(matieusu, madinhdanh, date_tgbd, date_tgkt, choo, manghenghiep, noilamviec);
                        tieuSu.Add_Table(tieusudto);
                        dGVTieuSu.Rows.RemoveAt(dGVTieuSu.Rows.Count - 2);
                        dGVTieuSu.Rows[e.RowIndex].Cells[dGVTieuSu.ColumnCount - 1].Value = "Delete";


                    }
                    else if (Task == "Update")
                    {
                        int row = e.RowIndex;
                        string matieusu = dGVTieuSu.Rows[row].Cells["matieusu"].Value.ToString();
                        string madinhdanh = dGVTieuSu.Rows[row].Cells["madinhdanh"].Value.ToString();
                        string thoigianbatdau = dGVTieuSu.Rows[row].Cells["thoigianbatdau"].Value.ToString();
                        DateTime date_tgbd = DateTime.Parse(thoigianbatdau);
                        string thoigianketthuc = dGVTieuSu.Rows[row].Cells["thoigianketthuc"].Value.ToString();
                        DateTime date_tgkt = DateTime.Parse(thoigianketthuc);
                        string choo = dGVTieuSu.Rows[row].Cells["choo"].Value.ToString();
                        string manghenghiep = dGVTieuSu.Rows[row].Cells["nghenghiep"].Value.ToString();
                        string noilamviec = dGVTieuSu.Rows[row].Cells["noilamviec"].Value.ToString();
                        tieusudto = new TieuSuDTO(matieusu, madinhdanh, date_tgbd, date_tgkt, choo, manghenghiep, noilamviec);
                        tieuSu.Update(tieusudto, row);
                        LoadtieuSu();
                    }
                }
            }
            catch(Exception ex)
                    {
                MessageBox.Show(ex.Message);
            }
        }

        private void dGVTienAnTienSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dGVTienAnTienSu.ColumnCount - 1)
                {
                    string Task = dGVTienAnTienSu.Rows[e.RowIndex].Cells[dGVTienAnTienSu.ColumnCount - 1].Value.ToString();
                    if (Task == "Delete")
                    {
                        if (MessageBox.Show("Bạn có chắc chắm muốn xóa không?", "Đang xóa...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            tienAn.Delete(rowIndex);
                        }
                    }
                    else if (Task == "Insert")
                    {
                        int row = dGVTienAnTienSu.Rows.Count - 2;
                        string matienantiensu = dGVTienAnTienSu.Rows[row].Cells["matienantiensu"].Value.ToString();
                        string madinhdanh = dGVTienAnTienSu.Rows[row].Cells["madinhdanh"].Value.ToString();
                        string banan = dGVTienAnTienSu.Rows[row].Cells["banan"].Value.ToString();
                        string toidanh = dGVTienAnTienSu.Rows[row].Cells["toidanh"].Value.ToString();
                        string hinhphat = dGVTienAnTienSu.Rows[row].Cells["hinhphat"].Value.ToString();
                        string ngayphat = dGVTienAnTienSu.Rows[row].Cells["ngayphat"].Value.ToString();
                        DateTime date_ngayphat = DateTime.Parse(ngayphat);
                        //string ghichu = dGVTienAnTienSu.Rows[row].Cells["ghichu"].Value.ToString();
                        tienanDTO = new TienAnTienSuDTO(matienantiensu, madinhdanh, banan, toidanh, hinhphat, date_ngayphat);
                        tienAn.Add_Table(tienanDTO);
                        dGVTienAnTienSu.Rows.RemoveAt(dGVTienAnTienSu.Rows.Count - 2);
                        dGVTienAnTienSu.Rows[e.RowIndex].Cells[dGVTienAnTienSu.ColumnCount - 1].Value = "Delete";
                        

                    }
                    else if (Task == "Update")
                    {
                        int row = e.RowIndex;
                        string matienantiensu = dGVTienAnTienSu.Rows[row].Cells["matienantiensu"].Value.ToString();
                        string madinhdanh = dGVTienAnTienSu.Rows[row].Cells["madinhdanh"].Value.ToString();
                        string banan = dGVTienAnTienSu.Rows[row].Cells["banan"].Value.ToString();
                        string toidanh = dGVTienAnTienSu.Rows[row].Cells["toidanh"].Value.ToString();
                        string hinhphat = dGVTienAnTienSu.Rows[row].Cells["hinhphat"].Value.ToString();
                        string ngayphat = dGVTienAnTienSu.Rows[row].Cells["ngayphat"].Value.ToString();
                        DateTime date_ngayphat = DateTime.Parse(ngayphat);
                        //string ghichu = dGVTienAnTienSu.Rows[row].Cells["ghichu"].Value.ToString();
                        tienanDTO = new TienAnTienSuDTO(matienantiensu, madinhdanh, banan, toidanh, hinhphat, date_ngayphat);
                        tienAn.Update(tienanDTO, row);
                        Loadtienantiensu(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbmadinhdanh_Enter(object sender, EventArgs e)
        {
            string gioiTinh = rdNam.Checked ? "nam" : "nu";
            try
            {
                tbmadinhdanh.Text = string.IsNullOrEmpty(tbmadinhdanh.Text) ?TrinhTaoMa.TangMa12Kytu(gioiTinh, dtpNgaySinh.Value.Year.ToString()): tbmadinhdanh.Text;
                tbmadinhdanh.SelectAll();
                //tbmadinhdanh.SelectionStart = 0;
                //tbmadinhdanh.SelectionLength = tbmadinhdanh.Text.Length;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tbmadinhdanh_TextChanged(object sender, EventArgs e)
        {
            LoadtieuSu();
            Loadtienantiensu();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DataTable kq = nktt.TimKiemJoinNhanKhau("nhankhau.madinhdanh='" + tbmadinhdanh.Text + "'").Tables[0];
            if (kq.Rows.Count > 0)
            {
                DataRow dt = kq.Rows[0];
                nkttDTO = new NhanKhauThuongTruDTO(dt);

                fillData();
            }
            else
            {
                MessageBox.Show(this, "Nhân khẩu này không tồn tại!", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void btnTimKiemTT_Click(object sender, EventArgs e)
        {
            DataTable kq = nktt.TimKiemJoinNhanKhau("manhankhauthuongtru='" + tbMaNKTT.Text + "'").Tables[0];
            if (kq.Rows.Count > 0)
            {
                DataRow dt = kq.Rows[0];
                nkttDTO = new NhanKhauThuongTruDTO(dt);

                fillData();
            }
            else
            {
                MessageBox.Show(this, "Nhân khẩu này không tồn tại!", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
