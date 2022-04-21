using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Colegioo
{
    public partial class Asignatura : Form
    {
        
        public Asignatura()
        {
            InitializeComponent();
        }

        private void Asignatura_Load(object sender, EventArgs e)
        {
            mostrar();
            SqlDataAdapter comand = new SqlDataAdapter(ClassData.SqlCommand("select Codigo from Profesores", CommandType.Text));
            DataTable dt = new DataTable();
            comand.Fill(dt);
            idprofesor.DataSource = dt;
            idprofesor.ValueMember = "Codigo";

            SqlDataAdapter comand2 = new SqlDataAdapter(ClassData.SqlCommand("select IDcurso from Curso", CommandType.Text));
            DataTable dt2 = new DataTable();
            comand2.Fill(dt2);
            Idcurso.DataSource = dt2;
            Idcurso.ValueMember = "IDcurso";

            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
        }
        public void mostrar()
        {
            List<DAsignatura> list = new List<DAsignatura>();
            var reader = ClassData.SqlCommand("Select * from Asignaturas", CommandType.Text).ExecuteReader();

            while (reader.Read())
            {
                DAsignatura asignatura = new DAsignatura();
                asignatura.IDasignatura = reader.GetInt32(0);
                asignatura.Nombre = reader.GetString(1);
                asignatura.IDcurso = reader.GetInt32(2);
                asignatura.IDProfesor = reader.GetInt32(3);


                list.Add(asignatura);
            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            dataGridView1.DataSource = list;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand(" insert into Asignaturas (IDasignatura,Nombre,IDprofesor,IDcurso)values(@Idasignatura,@nombre,@idprofesor,@Idcurso)", CommandType.Text);
            comand.Parameters.AddWithValue("@Idasignatura", Idasignatura.Text);
            comand.Parameters.AddWithValue("@nombre", nombre.Text);
            comand.Parameters.AddWithValue("@idprofesor", idprofesor.Text);
            comand.Parameters.AddWithValue("@Idcurso", Idcurso.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("update Asignatura set IDasignatura=@Idasignatura,Nombre=@nombre,IDprofesor=@idprofesor,IDcurso=@Idcurso", CommandType.Text);
            comand.Parameters.AddWithValue("@Idasignatura", Idasignatura.Text);
            comand.Parameters.AddWithValue("@nombre", nombre.Text);
            comand.Parameters.AddWithValue("@idprofesor", idprofesor.Text);
            comand.Parameters.AddWithValue("@Idcurso", Idcurso.Text);

            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("delete from Asignatura where IDasignatura=@Idasignatura", CommandType.Text);
            comand.Parameters.AddWithValue("@Idasignatura", Idasignatura.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }
        public void limpiar()
        {
            Idasignatura.Text = "";
            idprofesor.Text = "";
            nombre.Text = "";
            Idcurso.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Idasignatura.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            nombre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            Idcurso.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            idprofesor.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();

        }
    }
}
