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
    public partial class FrmBatallaEnemigo : Form
    {
        List<Enemigo> preguntas = new List<Enemigo>();
        Avatar personaje;
        FrmTablero frmTablero;
        Form1 frmPrincipal;
        ToolStripLabel lblVidaPersonaje;
        ToolStripLabel lblPuntosPersonaje;
        int index;
        int nivel;
        public FrmBatallaEnemigo(List<Enemigo> enemigos, Avatar avatar, FrmTablero tablero, ToolStripLabel lblVida, int nivelTablero, Form1 form1, ToolStripLabel lblPuntos)
        {
            personaje = avatar;
            frmTablero = tablero;
            preguntas = enemigos;
            lblVidaPersonaje = lblVida;
            nivel = nivelTablero;
            frmPrincipal = form1;
            lblPuntosPersonaje = lblPuntos;
            InitializeComponent();
        }

        private void FrmBatallaEnemigo_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            index = rand.Next(0, 3);
            Enemigo enemigo = preguntas[index];
            lblPregunta.Text = enemigo.pregunta;
            Control.ControlCollection controls = Controls;
            int indexRespuesta;
            for (int i = 1; i <= 4;)
            {
                indexRespuesta = rand.Next(1, 5);
                Control control = controls[indexRespuesta];
                if (control.Text == "" && i == 1)
                {
                    control.Text = enemigo.respuestaValida;
                    i++;
                } else if (control.Text == "" & i > 1)
                {
                    if (i == 2)
                    {
                        control.Text = enemigo.respuestaInValida1;
                        i++;
                    } else if (i == 3)
                    {
                        control.Text = enemigo.respuestaInValida2;
                        i++;
                    } else if (i == 4)
                    {
                        control.Text = enemigo.respuestaInValida3;
                        i++;
                    }
                }
            }
        }

        private void btnRespuesta_Click(object sender, EventArgs e)
        {
            Control btnControl = (Control)sender;
            if (btnControl.Text == preguntas[index].respuestaValida)
            {
                MessageBox.Show("Increible, acabas de vencer al enemigo!!!");
                personaje.vida += 1;
                personaje.acerto = true;
                personaje.puntos += 10;
                lblVidaPersonaje.Text = $"{personaje.vida}";
                lblPuntosPersonaje.Text = $"{personaje.puntos}";
                this.Close();
                frmTablero.Show();
            } else
            {
                MessageBox.Show("Que mala suerte, acabas de perder con el enemigo!!!");
                personaje.vida -= 1;
                personaje.acerto = false;
                personaje.puntos -= 20;
                lblVidaPersonaje.Text = $"{personaje.vida}";
                lblPuntosPersonaje.Text = $"{personaje.puntos}";
                this.Close();
                frmTablero.Close();
                frmTablero = new FrmTablero(personaje, frmPrincipal, nivel - 1);
                frmTablero.Show();
            }

        }
    }
}
