using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLCapMayTinh
{
    class KetNoi
    {
        //public static
        SqlConnection cn = new SqlConnection();
        //public static SqlCommand cmd;
        //Download source code mien phi tai Sharecode.vn
        public void Ket_Noi()
        {
            //Download source code mien phi tai Sharecode.vn
            cn = new SqlConnection();
            string str = @"Data Source=huynhthoa-pro44;Initial Catalog=QLMay;Integrated Security=True";
            // string str = @"server="+ frmChonMayTinh.tenmay.Trim() +"\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            cn.ConnectionString = str;
            cn.Open();
        }
        public void Dong_KetNoi()
        {
            cn.Close();
        }
        public DataSet FillDataSet(string sql)
        {

            DataSet dataset = new DataSet();
            SqlDataAdapter DataAdap = new SqlDataAdapter(sql, cn);
            DataAdap.Fill(dataset);
            DataAdap.Dispose();
            return dataset;
        }
        public void Luu(string sql)
        {
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.CommandText = sql;
            sqlcom.Connection = cn;
            sqlcom.ExecuteNonQuery();
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
    
    }
}
