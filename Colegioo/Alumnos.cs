using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colegioo
{
    public partial class Alumnos : Form
    {
        public Alumnos()
        {
            InitializeComponent();

        }

        private void Alumnos_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        public void mostrar()
        {
            List<DAlumnos> list = new List<DAlumnos>();
            var reader = ClassData.SqlCommand("Select * from Alumnos", CommandType.Text).ExecuteReader();

            while (reader.Read())
            {
                DAlumnos alumnos = new DAlumnos();
                alumnos.Matricula = reader.GetInt32(0);
                alumnos.Nombre = reader.GetString(1);
                alumnos.Sexo = reader.GetString(2);
                alumnos.Fechanacimiento = reader.GetDateTime(3).ToString();
                alumnos.Direccion = reader.GetString(4);
                alumnos.Email = reader.GetString(5);
                alumnos.Telefono = reader.GetString(6);
                alumnos.IDCurso = reader.GetInt32(7);


                list.Add(alumnos);
            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            dataGridView1.DataSource = list;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand(" insert into Alumnos(Nombre,Sexo,Fecha_Nacimiento,Telefono,Email,Direccion,IDcurso)values(@nombre,@sexo,@fecha,@telefono,@email,@direccion,@idcurso)", CommandType.Text);
            comand.Parameters.AddWithValue("@nombre", tbNombre.Text);
            comand.Parameters.AddWithValue("@sexo", cbSexo.Text);
            comand.Parameters.AddWithValue("@fecha", Fecha.Text);
            comand.Parameters.AddWithValue("@telefono", tbTelefono.Text);
            comand.Parameters.AddWithValue("@email", tbEmail.Text);
            comand.Parameters.AddWithValue("@direccion", tbDireccion.Text);
            comand.Parameters.AddWithValue("@idcurso", tbCurso.Text);




            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("delete from Alumnos where Matricula=@matricula", CommandType.Text);
            comand.Parameters.AddWithValue("@matricula", tbMatricula.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }
        public void limpiar()
        {
            tbMatricula.Text = "";
            tbNombre.Text = "";
            cbSexo.Text = "";
            tbTelefono.Text = "";
            tbEmail.Text = "";
            tbDireccion.Text = "";
            tbCurso.Text = "";
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("update Usuario set Nombre=@nombre,Sexo=@sexo,Fecha_Nacimiento=@fecha,Email=@email,Direccion=@direccion,IDcurso=@idcurso where Matricula=matricula", CommandType.Text);
            comand.Parameters.AddWithValue("@matricula", tbMatricula.Text);
            comand.Parameters.AddWithValue("@nombre", tbNombre.Text);
            comand.Parameters.AddWithValue("@sexo", cbSexo.Text);
            comand.Parameters.AddWithValue("@fecha", Fecha.Text);
            comand.Parameters.AddWithValue("@telefono", tbTelefono.Text);
            comand.Parameters.AddWithValue("@email", tbEmail.Text);
            comand.Parameters.AddWithValue("@direccion", tbDireccion.Text);
            comand.Parameters.AddWithValue("@idcurso", tbCurso.Text);

            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbMatricula.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            tbNombre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            cbSexo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            tbTelefono.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            tbEmail.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
            tbDireccion.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
            tbCurso.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
