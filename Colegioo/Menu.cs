using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colegio;

namespace Colegioo
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            this.Hide();
            usuarios.ShowDialog();
            this.Show();
        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alumnos alumnos = new Alumnos();
            this.Hide();
            alumnos.ShowDialog();
            this.Show();
        }

        private void calificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calificaciones calificaciones = new Calificaciones();
            this.Hide();
            calificaciones.ShowDialog();
            this.Show();
        }

        private void cursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Curso curso = new Curso();
            this.Hide();
            curso.ShowDialog();
            this.Show();
        }

        private void asignaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Asignatura asignatura = new Asignatura();
            this.Hide();
            asignatura.ShowDialog();
            this.Show();
        }

        private void profesoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profesores profesores = new Profesores();
            this.Hide();
            profesores.ShowDialog();
            this.Show();
        }
    }
}
