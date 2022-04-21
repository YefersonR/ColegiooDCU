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
    public partial class Calificaciones : Form
    {
        public Calificaciones()
        {
            InitializeComponent();
        }

        private void Calificaciones_Load(object sender, EventArgs e)
        {
            mostrar();
            SqlDataAdapter comand = new SqlDataAdapter(ClassData.SqlCommand("select IDasignatura from Asignaturas", CommandType.Text));
            DataTable dt = new DataTable();
            comand.Fill(dt);
            idasignatura.DataSource = dt;
            idasignatura.ValueMember = "IDasignatura";

            SqlDataAdapter comand2 = new SqlDataAdapter(ClassData.SqlCommand("select Matricula from Alumnos", CommandType.Text));
            DataTable dt2 = new DataTable();
            comand2.Fill(dt2);
            idasignatura.DataSource = dt2;
            idasignatura.ValueMember = "Matricula";

            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
        }
        public void mostrar()
        {
            List<DCalificaciones> list = new List<DCalificaciones>();
            var reader = ClassData.SqlCommand("Select * from Calificaciones", CommandType.Text).ExecuteReader();

            while (reader.Read())
            {
                DCalificaciones calificaciones = new DCalificaciones();
                calificaciones.IDCalificacion = reader.GetInt32(0);
                calificaciones.IDAsignatura = reader.GetInt32(1);
                calificaciones.Matricula = reader.GetInt32(2);
                calificaciones.Calificacion = reader.GetInt32(3);
             

                list.Add(calificaciones);
            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            dataGridView1.DataSource = list;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand(" insert into Calificaciones(IDasignatura,Matricula,Calificacion)values(@idasignatura,@matricula,@calificacion)", CommandType.Text);
            comand.Parameters.AddWithValue("@idasignatura", idasignatura.Text);
            comand.Parameters.AddWithValue("@matricula", matricula.Text);
            comand.Parameters.AddWithValue("@calificacion", calificacion.Text);

            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("update Calificaciones set IDasignatura=idasignatura,Matricula=@matricula,Calificacion=@calificacion where IDcalificacion=@idcalificacion", CommandType.Text);
            comand.Parameters.AddWithValue("@idcalificacion", idcalificacion.Text);
            comand.Parameters.AddWithValue("@idasignatura", idasignatura.Text);
            comand.Parameters.AddWithValue("@matricula", matricula.Text);
            comand.Parameters.AddWithValue("@calificacion", calificacion.Text);

            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("delete from Calificaciones where IDcalificacion=@idcalificacion", CommandType.Text);
            comand.Parameters.AddWithValue("@idcalificacion", idcalificacion.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }
        public void limpiar()
        {
            idcalificacion.Text = "";
            idasignatura.Text = "";
            matricula.Text = "";
            calificacion.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idcalificacion.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            idasignatura.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            matricula.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            calificacion.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();

        }
    }
}
