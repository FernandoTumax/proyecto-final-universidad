using CrystalCollector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalCollector
{
    public partial class FrmEstadoPersonaje : Form
    {
        Avatar personaje;
        int contGemas;
        int ubicacionPersonajeY;
        int ubicacionPersonajeX;
        public FrmEstadoPersonaje(Avatar avatar, int gemas, int y, int x)
        {
            personaje = avatar;
            contGemas = gemas;
            ubicacionPersonajeY = y;
            ubicacionPersonajeX = x;
            InitializeComponent();
        }

        private void FrmEstadoPersonaje_Load(object sender, EventArgs e)
        {
            if (personaje.genero == "Masculino")
            {
                Image image = Image.FromFile("../../../assets/images/personaje_masculino_2.png");
                pbPersonaje.Image = image;
            }
            else
            {
                Image image = Image.FromFile("../../../assets/images/avatar_femenino.png");
                pbPersonaje.Image = image;
            }
            this.lblNombre.Text = personaje.nombre;
            this.lblGenero.Text = personaje.genero;
            this.lblVidas.Text = $"{personaje.vida} Vidas";
            this.lblGemas.Text = $"{contGemas} Gemas";
            this.lblPuntos.Text = $"{personaje.puntos} Puntos";
            this.lblUbicacion.Text = $"Y: {ubicacionPersonajeY}, X: {ubicacionPersonajeX}";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
