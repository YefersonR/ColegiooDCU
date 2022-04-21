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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ClassData.sqlDatareader
        (ClassData.SqlCommand(
            "select * from Usuario where Nombre = '"
            + User.Text.ToString()
            + "'and Telefono ='" + Pass.Text.ToString() + "'", CommandType.Text
        )
    ).HasRows == false
)
            {

                MessageBox.Show("Acceso No Válido. Revise Sus Credenciales", "Atención", MessageBoxButtons.OK);
                Pass.Text = "";

            }
            else
            {

                Program.boolUserAuthenticated = true;


            }


            ClassData.SQLConnectionDB().Close();
            ClassData.SQLConnectionDB().Dispose();

            if (Program.boolUserAuthenticated)
            {
                Program.boolUserAuthenticated = true;
                this.Close();
            }
        }
    }
}
