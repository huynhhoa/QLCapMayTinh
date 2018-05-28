using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLCapMayTinh.Class;

namespace QLCapMayTinh
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        KetNoi kn = new KetNoi();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        //QLMAY

        public void LoadDataGridView()
        {
            try
            {
                kn.Ket_Noi(); // mở kết nối
                //hien thi DGVDSMAY
                ds = kn.FillDataSet("Select * From May");
                //  dt = ds.Tables[0];  có thể đổ ds vào datatble , sau đó load vào dgv
                dgvDSMay.DataSource = ds.Tables[0];
                //Hien thi dgvDSMayChuaSD
                DataSet dschuaSD = new DataSet();
                dschuaSD = kn.FillDataSet("Select * From May Where TrangThai=0");
                dgvDSChuaSD.DataSource = dschuaSD.Tables[0];

                //hien thi dgvGiao
                DataSet dsGiao = new DataSet();
                dsGiao = kn.FillDataSet("Select * From Giao");
                dgvGiao.DataSource = dsGiao.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chú ý");
            }
        }
        #region tab1
        private void frmMain_Load(object sender, EventArgs e)
        {

            LoadDataGridView();
        }
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtSerial.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerial.Focus();
                return;
            }
            if (txtTenMay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMay.Focus();
                return;
            }
            sql = "Select IdMay From May where IdMay=N'" + txtSerial.Text.Trim() + "'";

            if (Class.KetNoi.CheckKey(sql))
            {
                MessageBox.Show("Serial máy đã tồn tại, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerial.Focus();
                return;
            }
            sql = "insert into May(IdMay,TenMay,GiaTri,Loai,MauSac,KichThuoc,TrongLuong,CPU,RAM,ManHinh,Ocung,HeDieuHanh,DoHoa,NgayNhapMay,TinhTrangMay,TrangThai) values('" + txtSerial.Text + "',N'" + txtTenMay.Text + "',N'" +
                txtGiaTri.Text + "',N'" + txtLoai.Text + "',N'" + txtMauSac.Text + "',N'" + txtKichThuoc.Text +
                "',N'" + txtTrongLuong.Text + "',N'" + txtCPU.Text + "',N'"
                    + txtRAM.Text + "',N'" + txtManHinh.Text + "',N'" + txtOCung.Text + "',N'" + txtHDH.Text +
                    "',N'" + txtDoHoa.Text + "',N'" + dtpNgayNhanMay.Value + "',N'" + txtTinhTrang.Text + "',N'" +
                    0 + "')";
            kn.RunSQL(sql);
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataGridView();
            lamsach();

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            KetNoi kn = new KetNoi();

            string sql;
            if (MessageBox.Show("Bạn có muốn xóa máy [" + txtTenMay.Text + "] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE May WHERE IdMay=N'" + txtSerial.Text + "'";
                kn.RunSQL(sql);
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
                lamsach();
            }
        }
        private void lamsach()
        {
            txtSerial.Clear(); txtTenMay.Clear();
            txtGiaTri.Clear(); txtLoai.Clear();
            txtMauSac.Clear(); txtKichThuoc.Clear();
            txtTrongLuong.Clear(); txtCPU.Clear();
            txtRAM.Clear(); txtManHinh.Clear();
            txtOCung.Clear(); txtHDH.Clear();
            txtDoHoa.Clear();
            txtTrangThai.Clear(); txtTinhTrang.Clear();
            btnThemMoi.Enabled = true;
            txtSerial.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;

        }
        private void btnLamSach_Click(object sender, EventArgs e)
        {
            lamsach();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtTenMay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMay.Focus();
                return;
            }
            sql = "UPDATE May Set TenMay=N'" + txtTenMay.Text.Trim().ToString() + "',GiaTri=N'" + txtGiaTri.Text +
                "',Loai=N'" + txtLoai.Text + "',MauSac=N'" + txtMauSac.Text + "',KichThuoc=N'" + txtKichThuoc.Text +
                "',TrongLuong=N'" + txtTrongLuong.Text + "',CPU=N'" + txtCPU.Text + "',RAM=N'" + txtRAM.Text +
                "',ManHinh=N'" + txtManHinh.Text + "',Ocung=N'" + txtOCung.Text + "',HeDieuHanh=N'" + txtHDH.Text +
                "',DoHoa=N'" + txtDoHoa.Text + "',NgayNhapMay=N'" + (dtpNgayNhanMay.Value) +
                "',TinhTrangMay=N'" + txtTinhTrang.Text +
                "'WHERE IdMay=N'" + txtSerial.Text + "'";
            kn.RunSQL(sql);
            MessageBox.Show("Chỉnh sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDataGridView();
        }
        private void txtSerial_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnTimKiemMay_Click(object sender, EventArgs e)
        {

        }
        private void dgvDSMay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThemMoi.Enabled = false;
            txtSerial.Enabled = false;

            if (dgvDSMay.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtSerial.Text = dgvDSMay.CurrentRow.Cells["IdMay"].Value.ToString();
            txtTenMay.Text = dgvDSMay.CurrentRow.Cells["TenMay"].Value.ToString();
            txtGiaTri.Text = dgvDSMay.CurrentRow.Cells["GiaTri"].Value.ToString();
            txtLoai.Text = dgvDSMay.CurrentRow.Cells["Loai"].Value.ToString();
            txtMauSac.Text = dgvDSMay.CurrentRow.Cells["MauSac"].Value.ToString();
            txtKichThuoc.Text = dgvDSMay.CurrentRow.Cells["KichThuoc"].Value.ToString();
            txtTrongLuong.Text = dgvDSMay.CurrentRow.Cells["TrongLuong"].Value.ToString();
            txtCPU.Text = dgvDSMay.CurrentRow.Cells["CPU"].Value.ToString();
            txtRAM.Text = dgvDSMay.CurrentRow.Cells["RAM"].Value.ToString();
            txtManHinh.Text = dgvDSMay.CurrentRow.Cells["ManHinh"].Value.ToString();
            txtOCung.Text = dgvDSMay.CurrentRow.Cells["Ocung"].Value.ToString();
            txtHDH.Text = dgvDSMay.CurrentRow.Cells["HeDieuHanh"].Value.ToString();
            txtDoHoa.Text = dgvDSMay.CurrentRow.Cells["DoHoa"].Value.ToString();
            dtpNgayNhanMay.Value = DateTime.Parse(dgvDSMay.CurrentRow.Cells["NgayNhapMay"].Value.ToString());
            //    DateTime dt = new DateTime();
            ////HAVE A PROBLEM

            //dt = Convert.ToDateTime(dgvDSMay.CurrentRow.Cells["NgayNhapMay"].Value.ToString()); //chuyển date từ datagridview sang định dạng date
            //    mskNgayNhanMay.Text = dt.ToString("MM/dd/yyyy");

            txtTinhTrang.Text = dgvDSMay.CurrentRow.Cells["TinhTrangMay"].Value.ToString();

            if (bool.Parse(dgvDSMay.CurrentRow.Cells["TrangThai"].Value.ToString()) == false)   //HAVE A PROBLEM
                txtTrangThai.Text = "Chưa sử dụng";
            else if (bool.Parse(dgvDSMay.CurrentRow.Cells["TrangThai"].Value.ToString()) == true)
                txtTrangThai.Text = "Đang được sử dụng";

            btnLuu.Enabled = true;
            btnXoa.Enabled = true;
        }
        #endregion
        #region tab2
        private void btnLamMoiGiao_Click(object sender, EventArgs e)
        {
            txtCMND.Clear();

            txtHoTen.Clear();
            txtThuongTru.Clear();
            txtDienThoai.Clear();
            txtQuocTich.Clear();
            txtEmail.Clear();
            txtTamTru.Clear();
            txtNoiLamViec.Clear();
            txtBoPhan.Clear();
            txtChucVu.Clear();
            txtSerialGiao.Clear();
            txtTenMayGiao.Clear();
            txtNguoiGiao.Clear();
            txtLyDoGiao.Clear();
            txtMaGiao.Clear();
            btnLuuGiao.Enabled = false;
            btnXoaGiao.Enabled = false;
            btnXuatGiao.Enabled = false;
            btnXacNhanGiao.Enabled = true;
        }
        private void dgvDSChuaSD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXuatGiao.Enabled = false;
            btnLuuGiao.Enabled = false;
            btnXoaGiao.Enabled = false;
            if (dgvDSChuaSD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtSerialGiao.Text = dgvDSChuaSD.CurrentRow.Cells["IdMay"].Value.ToString();
            txtTenMayGiao.Text = dgvDSChuaSD.CurrentRow.Cells["TenMay"].Value.ToString();
        }
        private void btnXacNhanGiao_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCMND.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập CMND", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCMND.Focus();
                    return;
                }
                if (txtHoTen.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập họ tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }
                if (cboGioiTinh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboGioiTinh.Focus();
                    return;
                }
                if (txtSerialGiao.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSerialGiao.Focus();
                    return;
                }
                string sql;
                sql = "Insert into Giao(IdMay,CMND, NgayGiao,NguoiGiao,LydoGiao) values ('" + txtSerialGiao.Text + "',N'" +
                   txtCMND.Text + "',N'" + dpkNgayGiao.Value + "',N'" + txtNguoiGiao.Text + "',N'" + txtLyDoGiao.Text + "')";
                kn.RunSQL(sql);
                sql = "Update May Set TrangThai=1 Where IdMay=N'" + txtSerialGiao.Text + "'";
                kn.RunSQL(sql);
                sql = "Insert into NhanVien(CMND, HoTen,GioiTinh,NgaySinh,QuocTich,SoDienThoai,Email,ThuongTru,BoPhan,ChucDanh,TamTru,NoiLamViec,NgayVaoCTY) Values ('" + txtCMND.Text + "',N'" +
                    txtHoTen.Text + "',N'" + cboGioiTinh.SelectedValue + "',N'" + dpkNgaySinh.Value + "',N'" + txtQuocTich.Text + "',N'" + txtDienThoai.Text + "',N'" +
                    txtEmail.Text + "',N'" + txtThuongTru.Text + "',N'" + txtBoPhan.Text + "',N'" + txtChucVu.Text + "',N'" +
                    txtTamTru.Text + "',N'" + txtNoiLamViec.Text + "',N'" + dpkNgayVaoLam.Value + "')";
                kn.RunSQL(sql);
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
                btnXacNhanGiao.Enabled = false;
                btnLuu.Enabled = true;
                btnXoa.Enabled = true;
                btnXuatGiao.Enabled = true;
            }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message, "Chú ý");
            }
        }
        //Kiểm tra nếu nhân viên đã được cấp máy trước đây
      
        private void txtCMND_TextChanged(object sender, EventArgs e)
        {
            string sql;
            sql = "Select CMND From NhanVien where CMND=N'" + txtCMND.Text.Trim() + "'";
            if (Class.KetNoi.CheckKey(sql))
            {
                DialogResult da = MessageBox.Show("Nhân viên đã được cấp máy tính trước đó. Tiếp tục cấp máy khác? ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (da == DialogResult.OK)
                {

                }
                else txtCMND.Text = null;
            }
        }

        SqlDataReader dr;

        private void dgvGiao_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btnXacNhanGiao.Enabled = false;
            txtMaGiao.Text = dgvGiao.CurrentRow.Cells["MaGiao"].Value.ToString();
            txtLyDoGiao.Text = dgvGiao.CurrentRow.Cells["LydoGiao"].Value.ToString();
            txtNguoiGiao.Text = dgvGiao.CurrentRow.Cells["NguoiGiao"].Value.ToString();
            dpkNgayGiao.Value = DateTime.Parse(dgvGiao.CurrentRow.Cells["NgayGiao"].Value.ToString());
            txtSerialGiao.Text = dgvGiao.CurrentRow.Cells["IdMay"].Value.ToString();
            string sql;
              sql  = "Select TenMay From May Where IdMay = N'" + txtSerialGiao.Text  +"'";
            dr = kn.ThucHienReader(sql);
            if (dr.Read())
            {
                txtTenMayGiao.Text = dr[0].ToString();
                
            }

            string manv = dgvGiao.CurrentRow.Cells["CMND"].Value.ToString();
            string sql2 = "Select * From NhanVien where CMND = N'" + manv + "'";
            dr = kn.ThucHienReader(sql2);
            if(dr.Read())
            {
                txtCMND.Text = dr[0].ToString();
                txtHoTen.Text = dr[1].ToString();

            }
            kn.Dong_KetNoi();

        }
        #endregion
    }
}
