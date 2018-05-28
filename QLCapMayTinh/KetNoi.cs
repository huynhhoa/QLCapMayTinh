using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLCapMayTinh.Class
{
    class KetNoi
    {
        //public static
        public static SqlConnection cn;
        //public static SqlCommand cmd;
        //Download source code mien phi tai Sharecode.vn
        public void Ket_Noi()
        {
            //Download source code mien phi tai Sharecode.vn
            cn = new SqlConnection();
            string str = @"Data Source=HUYNHTHOA-PRO44;Initial Catalog=QLMay;Integrated Security=True";
            // string str = @"server="+ frmChonMayTinh.tenmay.Trim() +"\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            cn.ConnectionString = str;
            cn.Open();
        }
        public void Dong_KetNoi()
        {
            cn.Close();
        }
        public DataTable GetTable(string sql)
        {
            SqlDataAdapter ad = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataSet FillDataSet(string sql)
        {

            DataSet dataset = new DataSet();
            SqlDataAdapter DataAdap = new SqlDataAdapter(sql, cn);
            DataAdap.Fill(dataset);
            DataAdap.Dispose();
            return dataset;
        }
        public void RunSQL(string sql) //Them or Luu OR Xoa
        {
            SqlCommand sqlcom = new SqlCommand(); // Đối tượng thuộc lớp SqlCommand
            sqlcom.CommandText = sql; //Gán lenh sql
            sqlcom.Connection = cn; //gan ket noi
            sqlcom.ExecuteNonQuery(); //thuc hien cau lenh
        }
       
        public SqlDataReader ThucHienReader(string sql) 
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.Connection = cn;
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            return reader;
        }
        //kiểm tra Serial
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter MyData = new SqlDataAdapter(sql, cn); //gán lệnh sql
            DataTable table = new DataTable(); //tạo một bảng mới
            MyData.Fill(table); // đưa output vào bảng
            if (table.Rows.Count > 0) //nếu bảng lớn hơn =
                return true; //thì trả về true 
            else return false;
        }
        
        //Chuyển đổi ngày
        public static string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }
        public static bool IsDate(string date)
        {
            string[] elements = date.Split('/');
            if ((Convert.ToInt32(elements[0]) >= 1) && (Convert.ToInt32(elements[0]) <= 31) && (Convert.ToInt32(elements[1]) >= 1) && (Convert.ToInt32(elements[1]) <= 12) && (Convert.ToInt32(elements[2]) >= 1900))
                return true;
            else return false;
        }
    }
}
