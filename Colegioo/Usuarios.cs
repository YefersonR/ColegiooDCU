using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colegioo
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            imagen.Image.Save(ms, ImageFormat.Bmp);

            var comand = ClassData.SqlCommand(" insert into Usuario(Nombre,Correo,Telefono,Foto)values(@Nombre,@Correo,@Telefono,@imagen)", CommandType.Text);
            comand.Parameters.AddWithValue("@Nombre", Nombre.Text);
            comand.Parameters.AddWithValue("@Correo", Correo.Text);
            comand.Parameters.AddWithValue("@Telefono", Telefono.Text);
            comand.Parameters.AddWithValue("@imagen", ms.GetBuffer());



            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }
        public void limpiar()
        {
            Id.Text = "";
            Nombre.Text = "";
            Correo.Text = "";
            Telefono.Text = "";
            imagen.Image = null;
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            imagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           
            var comand = ClassData.SqlCommand("update Usuario set Nombre=@Nombre,Correo=@Correo,Telefono=@Telefono,Foto=@imagen where IdUsuario=@IdUsuario", CommandType.Text);
            comand.Parameters.AddWithValue("@IdUsuario", Id.Text);
            comand.Parameters.AddWithValue("@Nombre", Nombre.Text);
            comand.Parameters.AddWithValue("@Correo", Correo.Text);
            comand.Parameters.AddWithValue("@Telefono", Telefono.Text);
            comand.Parameters.AddWithValue("@imagen", ms.GetBuffer());


            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var comand = ClassData.SqlCommand("delete from Usuario where IdUsuario=@IdUsuario", CommandType.Text);
            comand.Parameters.AddWithValue("@IdUsuario", Id.Text);
            comand.ExecuteNonQuery();
            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();
            mostrar();
            limpiar();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            mostrar();
        }
        public void mostrar()
        {
            List<Dusuario> list = new List<Dusuario>();
            var reader = ClassData.SqlCommand("select * from Usuario", CommandType.Text).ExecuteReader();
            while (reader.Read())
            {
                Dusuario usuario = new Dusuario();
                usuario.IDUsuario = reader.GetInt32(0);
                usuario.Nombre = reader.GetString(1);
                usuario.Correo = reader.GetString(2);
                usuario.Telefono = reader.GetString(3);
                usuario.Foto = (byte[])reader.GetValue(4);


                list.Add(usuario);
            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            Nombre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            Correo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            Telefono.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();

            byte[] datos = (byte[])dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value;
            Stream img = new MemoryStream(datos);
            imagen.Image = Image.FromStream(img);


        }

        private void btnimagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fo = new OpenFileDialog();
            DialogResult rs = fo.ShowDialog();
            if (rs == DialogResult.OK)
            {
                imagen.Image = Image.FromFile(fo.FileName);
            }

        }
    }

}
