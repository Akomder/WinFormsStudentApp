using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class DepartmentsForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=SchoolDB;Integrated Security=True";

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblDepartments", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDepartments.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblDepartments (DeptName) VALUES ('New Department')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["DepartmentId"].Value);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblDepartments SET DeptName='Updated Department' WHERE DepartmentId=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["DepartmentId"].Value);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblDepartments WHERE DepartmentId=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
