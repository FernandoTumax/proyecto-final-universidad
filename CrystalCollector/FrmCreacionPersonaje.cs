using CrystalCollector.Models;

namespace CrystalCollector
{
    public partial class FrmCreacionPersonaje : Form
    {
        Form1 formPrincipal = new Form1();
        
        public FrmCreacionPersonaje(Form1 form1)
        {
            InitializeComponent();
            formPrincipal = form1;
        }

        private void FrmCreacionPersonaje_Load(object sender, EventArgs e)
        {

        }

        private void cmbGenero_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbGenero.Text == "Masculino")
            {
                Image image = Image.FromFile("../../../assets/images/personaje_masculino_2.png");
                pbPersonaje.Image = image;
            } else
            {
                Image image = Image.FromFile("../../../assets/images/avatar_femenino.png");
                pbPersonaje.Image = image;
            }
        }

        private void btnEmpezar_Click_1(object sender, EventArgs e)
        {
            if (this.txtNombre.Text == "" || this.cmbGenero.SelectedItem == null)
            {
                MessageBox.Show("Por favor ingrese el nombre de su avatar y genero.");
            }
            else
            {
                Avatar avatar = new Avatar();
                avatar.nombre = this.txtNombre.Text;
                avatar.genero = this.cmbGenero.Text;
                FrmTablero frmTablero = new FrmTablero(avatar, formPrincipal);
                frmTablero.Show();
                this.Close();

            }
        }

        private void FrmCreacionPersonaje_Load_1(object sender, EventArgs e)
        {

        }
    }
}
