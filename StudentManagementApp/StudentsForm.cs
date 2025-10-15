using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class StudentsForm : Form
    {
        string connectionString = ConfigurationManager
            .ConnectionStrings["SchoolDBConnectionString"].ConnectionString;

        public StudentsForm()
        {
            InitializeComponent();
        }

        // Load data when form opens
        private void StudentsForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblStudents", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvStudents.DataSource = dt;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO tblStudents(Name, Age, DepartmentId) VALUES(@Name, @Age, @Dept)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", "New Student");
                cmd.Parameters.AddWithValue("@Age", 20);
                cmd.Parameters.AddWithValue("@Dept", 1);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadStudents();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvStudents.CurrentRow.Cells["StudentId"].Value);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE tblStudents SET Name=@Name WHERE StudentId=@Id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Name", "Updated Name");
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                LoadStudents();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvStudents.CurrentRow.Cells["StudentId"].Value);
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "DELETE FROM tblStudents WHERE StudentId=@Id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                LoadStudents();
            }
        }
    }
}
