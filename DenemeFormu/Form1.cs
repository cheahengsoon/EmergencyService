using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenemeFormu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=EMRECEYLAN-PC;Initial Catalog=Emergency Service;Integrated Security=True");
            var cmd = new SqlCommand("Hastane", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();

            try
            {
                cmd.Parameters.Add("@ıd", SqlDbType.Int).Value = 1;
                var returnValue = new SqlParameter("@hastane", SqlDbType.VarChar) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(returnValue);
                cmd.ExecuteNonQuery();

                label1.Text =(String)returnValue.Value;

            }

            finally
            {
                conn.Close();
            }

        }
        
    }
}
