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

namespace Colegioo
{
    public partial class Curso : Form
    {
        public Curso()
        {
            InitializeComponent();
        }

        private void Curso_Load(object sender, EventArgs e)
        {
            mostrar();


        }
        public void mostrar()
        {
            List<DCurso> list = new List<DCurso>();
            var reader = ClassData.SqlCommand("Select * from Curso", CommandType.Text).ExecuteReader();

            while (reader.Read())
            {
                DCurso curso = new DCurso();
                curso.IDCurso = reader.GetInt32(0);
                curso.Nombre = reader.GetInt32(1);
                curso.Seccion = reader.GetString(2);


                list.Add(curso);
            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idcurso.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            nombre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            seccion.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("delete from Curso where IDcurso=@idcurso", CommandType.Text);
            comand.Parameters.AddWithValue("@idcurso", idcurso.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }
        public void limpiar()
        {
            idcurso.Text = "";
            nombre.Text = "";
            seccion.Text = "";

        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("update Curso set Nombre=@nombre,Seccion=@seccion where IDcurso=@idcurso", CommandType.Text);
            comand.Parameters.AddWithValue("@idcurso", idcurso.Text);
            comand.Parameters.AddWithValue("@nombre", nombre.Text);
            comand.Parameters.AddWithValue("@seccion", seccion.Text);


            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand(" insert into Curso(Nombre,Seccion)values(@nombre,@seccion)", CommandType.Text);
            comand.Parameters.AddWithValue("@nombre", nombre.Text);
            comand.Parameters.AddWithValue("@seccion", seccion.Text);


            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
