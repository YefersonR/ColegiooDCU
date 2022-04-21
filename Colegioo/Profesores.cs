using Colegioo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colegio
{
    public partial class Profesores : Form
    {
        public Profesores()
        {
            InitializeComponent();
        }

        private void Profesores_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        public void mostrar()
        {
            List<DProfesores> list = new List<DProfesores>();
            var reader = ClassData.SqlCommand("Select * from Profesores", CommandType.Text).ExecuteReader();

            while (reader.Read())
            {
                DProfesores profesores = new DProfesores();
                profesores.Matricual = reader.GetInt32(0);
                profesores.Nombre = reader.GetString(1);
                profesores.Sexo = reader.GetString(2);
                profesores.Telefono = reader.GetString(3);
                profesores.Direccion = reader.GetString(4);
                profesores.Email = reader.GetString(5);
                

                list.Add(profesores);
            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            dataGridView1.DataSource = list;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand(" insert into Profesores(Nombre,Sexo,Telefono,Direccion,Email)values(@nombre,@sexo,@telefono,@direccion,@email)", CommandType.Text);
            comand.Parameters.AddWithValue("@nombre", nombre.Text);
            comand.Parameters.AddWithValue("@sexo", sexo.Text);
            comand.Parameters.AddWithValue("@telefono", telefono.Text);
            comand.Parameters.AddWithValue("@direccion", direccion.Text);
            comand.Parameters.AddWithValue("@email", email.Text);


            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }
        public void limpiar()
        {
            codigo.Text = "";
            nombre.Text = "";
            sexo.Text = "";
            telefono.Text = "";
            direccion.Text = "";
            email.Text = "";

        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("delete from Profesores where Codigo=@codigo", CommandType.Text);
            comand.Parameters.AddWithValue("@codigo", codigo.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("update Profesores set Nombre=@nombre,Sexo=@sexo,Telefono=@telefono,Direccion=@direccion,Email=@email where Codigo=@Codigo", CommandType.Text);
            comand.Parameters.AddWithValue("@Codigo", codigo.Text);
            comand.Parameters.AddWithValue("@nombre", nombre.Text);
            comand.Parameters.AddWithValue("@sexo", sexo.Text);
            comand.Parameters.AddWithValue("@telefono", telefono.Text);
            comand.Parameters.AddWithValue("@direccion", direccion.Text);
            comand.Parameters.AddWithValue("@email", email.Text);

            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            codigo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            nombre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            sexo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            telefono.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            direccion.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
            email.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();

        }
    }
}
