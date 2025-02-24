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
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=Fayaz\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEmployees();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmployees.Rows[e.RowIndex];
                txtname.Text = row.Cells["Name"].Value.ToString();
                txtposition.Text = row.Cells["Position"].Value.ToString();
                txtsalary.Text = row.Cells["Salary"].Value.ToString();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Employee deleted successfully!");
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Employees SET Name=@Name, Position=@Position, Salary=@Salary WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", txtname.Text);
                    cmd.Parameters.AddWithValue("@Position", txtposition.Text);
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtsalary.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee updated successfully!");
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to update.");
            }

        }

        private void Create_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Employees (Name, Position, Salary) VALUES (@Name, @Position, @Salary)", conn);
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Position", txtposition.Text);
                cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtsalary.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee added successfully!");
                LoadEmployees();
            }

        }
        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employees", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvEmployees.DataSource = dt;
            }
        }

    }
}
