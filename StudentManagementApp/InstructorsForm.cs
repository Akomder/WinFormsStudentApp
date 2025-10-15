using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class InstructorsForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=SchoolDB;Integrated Security=True";

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblInstructors", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvInstructors.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblInstructors (Name, DepartmentId) VALUES ('New Instructor', 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvInstructors.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvInstructors.CurrentRow.Cells["InstructorId"].Value);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblInstructors SET Name='Updated Instructor', DepartmentId=1 WHERE InstructorId=@id";
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
            if (dgvInstructors.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvInstructors.CurrentRow.Cells["InstructorId"].Value);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblInstructors WHERE InstructorId=@id";
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
