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
    public partial class SoHoKhauGUI : Form
    {
        SoHoKhauBUS shk;
        NhanKhauThuongTruBUS nktt;
        SoHoKhauDTO shkDTO;
        public SoHoKhauGUI()
        {
            shk = new SoHoKhauBUS();
            nktt = new NhanKhauThuongTruBUS();
            shkDTO = new SoHoKhauDTO();
            InitializeComponent();

            var bindingList = new BindingList<NhanKhauThuongTruDTO>(shkDTO.NhanKhau);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            tbSoSoHoKhau.Text = TrinhTaoMa.TangMa9kytu(TrinhTaoMa.getLastID_SoSoHoKhau());

        }

        private void SoHoKhauGUI_Load(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSoSoHoKhau.Text)|| string.IsNullOrEmpty(tbDiaChi.Text))
            {
                MessageBox.Show(this, "Vui lòng tạo mã hộ khẩu, và thông tin thường trú!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (NhanKhauThuongTruGUI a = new NhanKhauThuongTruGUI(tbSoSoHoKhau.Text, tbDiaChi.Text))
            {
                a.ShowDialog(this);
                if (a.nkttDTO != null && !String.IsNullOrEmpty(a.nkttDTO.MaNhanKhauThuongTru))
                {
                    shkDTO.NhanKhau.Add(a.nkttDTO);

                    var bindingList = new BindingList<NhanKhauThuongTruDTO>(shkDTO.NhanKhau);
                    var source = new BindingSource(bindingList, null);
                    dataGridView1.DataSource = source;

                    cbbChuHo.DisplayMember = "hoTen";
                    cbbChuHo.ValueMember = "maDinhDanh";
                    cbbChuHo.Items.Add(a.nkttDTO);
                }


            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSoSoHoKhau.Text) ||cbbChuHo.SelectedValue==null || shkDTO.NhanKhau.Count==0
                || string.IsNullOrEmpty(tbDiaChi.Text)|| string.IsNullOrEmpty(tbSoDangKy.Text))
            {
                MessageBox.Show(this, "Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            shk.Add(new SoHoKhauDTO(tbSoSoHoKhau.Text, cbbChuHo.SelectedValue.ToString(), tbDiaChi.Text, dtpNgayCap.Value, tbSoDangKy.Text));
            nktt.DoiChuHo(shkDTO.NhanKhau, cbbChuHo.SelectedValue.ToString());
        }

        private void tbDiaChi_Enter(object sender, EventArgs e)
        {
            using (ChonDonViHanhChinhGUI a = new ChonDonViHanhChinhGUI())
            {
                a.ShowDialog(this);
                if (a.diaChi != "")
                    tbDiaChi.Text = a.diaChi;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }
    }
}
