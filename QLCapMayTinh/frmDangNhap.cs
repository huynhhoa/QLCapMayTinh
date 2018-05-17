using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLCapMayTinh
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        //goi lop ket noi
        KetNoi conn = new KetNoi();
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            try
            {
                //goi phuong thuc Ket_Noi() cua class KetNoi
                conn.Ket_Noi();
            }
            catch
            {
                MessageBox.Show("Lỗi Kết Nối");
            }
        }
        public static string username;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            //truyền câu lệnh SQL vào hàm ThucHienReader để load dữ liệu
            tb.Load(conn.ThucHienReader("SELECT * FROM Admin WHERE TenDangNhap='" + txtUN.Text.Trim() + "'"));
            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Không tồn tại tên đăng nhập này. Bạn hãy liên hệ với admin để tạo tài khoản.", "Thông Báo");
            }
            else
                if (tb.Rows[0]["MatKhau"].ToString().Trim() == txtPass.Text.Trim())
            {
                this.Hide();
                frmMain frm = new frmMain();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Mật khẩu của bạn không đúng", "Thông Báo");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }
    }
}
