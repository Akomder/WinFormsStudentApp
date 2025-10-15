using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class CoursesForm : Form
    {
        // Change connection string as needed
        private string connectionString = "Data Source=.;Initial Catalog=UniversityDB;Integrated Security=True";

        public CoursesForm()
        {
            InitializeComponent();
        }

        private void CoursesForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // READ
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblCourses", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCourses.DataSource = dt;
            }
        }

        // CREATE
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblCourses (CourseName, Credits) VALUES (@name, @credits)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", "New Course");
                cmd.Parameters.AddWithValue("@credits", 3);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            LoadData();
        }

        // UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvCourses.CurrentRow.Cells["CourseId"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblCourses SET CourseName=@name, Credits=@credits WHERE CourseId=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", "Updated Course");
                    cmd.Parameters.AddWithValue("@credits", 4);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                LoadData();
            }
        }

        // DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvCourses.CurrentRow.Cells["CourseId"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblCourses WHERE CourseId=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                LoadData();
            }
        }

        // REFRESH
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
