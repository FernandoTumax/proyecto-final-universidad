
using CrystalCollector.Models;

namespace CrystalCollector
{
    public partial class FrmTablero : Form
    {
        Random rnd = new Random();
        Avatar personaje = new Avatar();
        Form1 formPrincipal = new Form1();
        PictureBox[,] tablero;
        List<int> listIndexControl = new List<int>();
        List<Enemigo> enemigos = new List<Enemigo>();
        int primeraCoordenaAvatar = 0, segundaCoordenaAvatar = 0, filaMatriz = 0, columnaMatriz = 0, nivel = 1, contGemas = 0, coordenadaPrimeraPortalDes = 0, coordenadaSegundaPortalDes = 0, coordenadaPrimeraPortal = 0, coordenadaSegundaPortal = 0;

        public FrmTablero(Avatar avatar, Form1 form1, int nivelTablero = 1)
        {
            InitializeComponent();
            personaje = avatar;
            formPrincipal = form1;
            nivel = nivelTablero;
            enemigos.Add(new Enemigo()
            {
                pregunta = "¿Cuál es el mejor equipo actualmente?",
                respuestaValida = "Barcelona",
                respuestaInValida1 = "Manchester City",
                respuestaInValida2 = "Inter",
                respuestaInValida3 = "Milan"
            });
            enemigos.Add(new Enemigo()
            {
                pregunta = "¿Cuál es el mejor juego de 2023?",
                respuestaValida = "The legend of zelda Tears of the Kingdom",
                respuestaInValida1 = "Resident Evil 4 Remake",
                respuestaInValida2 = "Star wars Jedi Survivor",
                respuestaInValida3 = "Diablo IV"
            });
            enemigos.Add(new Enemigo()
            {
                pregunta = "¿Cuál de estos es un sabor primario?",
                respuestaValida = "Umami",
                respuestaInValida1 = "Agrio",
                respuestaInValida2 = "Picante",
                respuestaInValida3 = "Refrescante"
            });
        }

        private void FrmTablero_Load_1(object sender, EventArgs e)
        {
            switch (nivel)
            {
                case 1:
                    inicializar(3, 3);
                    break;
                case 2:
                    inicializar(4, 4);
                    break;
                case 3:
                    inicializar(5, 5);
                    break;
                case 4:
                    inicializar(6, 6);
                    break;
                case 5:
                    inicializar(10, 10);
                    break;
                default:
                    break;
            }
        }

        private void tsbEstado_Click(object sender, EventArgs e)
        {
            FrmEstadoPersonaje frmEstado = new FrmEstadoPersonaje(personaje, int.Parse(this.tslContGemas.Text), segundaCoordenaAvatar, primeraCoordenaAvatar);
            frmEstado.Show();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            formPrincipal.Show();
        }

        public void inicializar(int fila, int columna)
        {
            this.tslVidas.Text = $"{personaje.vida}";
            this.tslNivel.Text = $"{nivel}";
            this.tslPuntos.Text = $"{personaje.puntos}";
            tablero = new PictureBox[fila, columna];
            if (listIndexControl.Count >= 2)
            {
                foreach (int item in listIndexControl)
                {
                    Controls.RemoveAt(1);
                }
                listIndexControl = new List<int>();
            }
            filaMatriz = fila - 1;
            columnaMatriz = columna - 1;
            Image resizedImageFondo = getImagen("../../../assets/images/fondo.jpg");
            this.tslContGemas.Text = $"{contGemas}";
            int cont = 1;
            for (int i = 0; i < fila; i++)
            {
                for (int j = 0; j < columna; j++)
                {
                    if (nivel == 5)
                    {
                        tablero[i, j] = new PictureBox();
                        Controls.Add(tablero[i, j]);
                        tablero[i, j].Width = 176;
                        tablero[i, j].Height = 100;
                        tablero[i, j].Top = 100 * j;
                        tablero[i, j].Left = 176 * i;
                        tablero[i, j].BackgroundImage = resizedImageFondo;
                        Point point = tablero[i, j].Location;
                        tablero[i, j].Location = new Point(point.X, point.Y + 28);
                        listIndexControl.Add(cont);
                        cont++;
                    } else
                    {
                        tablero[i, j] = new PictureBox();
                        Controls.Add(tablero[i, j]);
                        tablero[i, j].Width = 176;
                        tablero[i, j].Height = 128;
                        tablero[i, j].Top = 128 * j;
                        tablero[i, j].Left = 176 * i;
                        tablero[i, j].BackgroundImage = resizedImageFondo;
                        Point point = tablero[i, j].Location;
                        tablero[i, j].Location = new Point(point.X, point.Y + 28);
                        listIndexControl.Add(cont);
                        cont++;
                    } 
                }
            }
            switch (nivel)
            {
                case 1:
                    cargarPersonaje(0, 0);
                    cargarPortal(nivel, 2, 2);
                    cargarGemas(3);
                    break;
                case 2:
                    this.Width = 720;
                    this.Height = 579;
                    cargarPersonaje(rnd.Next(0, 4), rnd.Next(0, 4));
                    cargarPortal(2);
                    cargarGemas(4);
                    cargarEnemigo(2);
                    break;
                case 3:
                    this.Width = 895;
                    this.Height = 705;
                    cargarPersonaje(rnd.Next(0, 5), rnd.Next(0, 5));
                    cargarPortal(3);
                    cargarGemas(5);
                    cargarEnemigo(3);
                    break;
                case 4:
                    this.Width = 1072;
                    this.Height = 835;
                    cargarPersonaje(rnd.Next(0, 6), rnd.Next(0, 6));
                    cargarPortal(4);
                    cargarGemas(6);
                    cargarEnemigo(4);
                    break;
                case 5:
                    this.Width = 1570;
                    this.Height = 900;
                    cargarPersonaje(rnd.Next(0, 10), rnd.Next(0, 10));
                    cargarPortal(5);
                    cargarGemas(10);
                    cargarEnemigo(5);
                    break;
                default:
                    break;
            }
        }

        public Image getImagen(string fileName)
        {
            Image image = Image.FromFile(fileName);
            Bitmap imgbitmap = new Bitmap(image);
            Image resizedImage = resizeImage(imgbitmap, new Size(80, 80));
            return resizedImage;
        }

        public void cargarPortal(int nivel = 1, int filaPortal = -1, int columnaPortal = -1, string filePath = "portal_des.png", string nameImage = "Portal_des")
        {
            Image resizedImagePortal = getImagen($"../../../assets/images/{filePath}");
            int fila = 0;
            int columna = 0;
            if (filaPortal == -1 && filaPortal == -1)
            {
                switch (nivel)
                {
                    case 1:
                        fila = rnd.Next(0, 3);
                        columna = rnd.Next(1, 3);
                        while (tablero[fila, columna].Name != "")
                        {
                            fila = rnd.Next(0, 3);
                            columna = rnd.Next(1, 3);
                        }
                        break;
                    case 2:
                        fila = rnd.Next(0, 4);
                        columna = rnd.Next(0, 4);
                        while (tablero[fila, columna].Name != "")
                        {
                            fila = rnd.Next(0, 4);
                        columna = rnd.Next(0, 4);
                        }
                        break;
                    case 3:
                        fila = rnd.Next(0, 5);
                        columna = rnd.Next(0, 5);
                        while (tablero[fila, columna].Name != "")
                        {
                            fila = rnd.Next(0, 5);
                            columna = rnd.Next(0, 5);
                        }
                        break;
                    case 4:
                        fila = rnd.Next(0, 6);
                        columna = rnd.Next(0, 6);
                        while (tablero[fila, columna].Name != "")
                        {
                            fila = rnd.Next(0, 6);
                            columna = rnd.Next(0, 6);
                        }
                        break;
                    case 5:
                        fila = rnd.Next(0, 10);
                        columna = rnd.Next(0, 10);
                        while (tablero[fila, columna].Name != "")
                        {
                            fila = rnd.Next(0, 10);
                            columna = rnd.Next(0, 10);
                        }
                        break;
                    default:
                        break;
                }
                tablero[fila, columna].Image = resizedImagePortal;
                tablero[fila, columna].SizeMode = PictureBoxSizeMode.CenterImage;
                tablero[fila, columna].Name = nameImage;
                coordenadaPrimeraPortal = fila;
                coordenadaSegundaPortal = columna;
            }
            else
            {
                tablero[filaPortal, columnaPortal].Image = resizedImagePortal;
                tablero[filaPortal, columnaPortal].SizeMode = PictureBoxSizeMode.CenterImage;
                tablero[filaPortal, columnaPortal].Name = nameImage;
                coordenadaPrimeraPortal = filaPortal;
                coordenadaSegundaPortal = columnaPortal;
            }
        }

        public void cargarUnEnemigo(int nivel)
        {
            int fila;
            int columna;
            switch (nivel)
            {
                case 2:
                    fila = rnd.Next(0, 4);
                    columna = rnd.Next(0, 4);
                    for (int i = 0; i < 1;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        }
                        else
                        {
                            fila = rnd.Next(0, 4);
                            columna = rnd.Next(0, 4);
                        }
                    }
                    break;
                case 3:
                    fila = rnd.Next(0, 5);
                    columna = rnd.Next(0, 5);
                    for (int i = 0; i < 1;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        }
                        else
                        {
                            fila = rnd.Next(0, 5);
                            columna = rnd.Next(0, 5);
                        }
                    }
                    break;
                case 4:
                    fila = rnd.Next(0, 6);
                    columna = rnd.Next(0, 6);
                    for (int i = 0; i < 1;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        }
                        else
                        {
                            fila = rnd.Next(0, 6);
                            columna = rnd.Next(0, 6);
                        }
                    }
                    break;
                case 5:
                    fila = rnd.Next(0, 10);
                    columna = rnd.Next(0, 10);
                    for (int i = 0; i < 1;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        }
                        else
                        {
                            fila = rnd.Next(0, 10);
                            columna = rnd.Next(0, 10);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void cargarEnemigo(int nivel)
        {
            int fila;
            int columna;
            switch (nivel)
            {
                case 2:
                    fila = rnd.Next(0, 4);
                    columna = rnd.Next(0, 4);
                    for (int i = 0; i < 1;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        } else
                        {
                            fila = rnd.Next(0, 4);
                            columna = rnd.Next(0, 4);
                        }
                    }
                    break;
                case 3:
                    fila = rnd.Next(0, 5);
                    columna = rnd.Next(0, 5);
                    for (int i = 0; i < 4;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        } else
                        {
                            fila = rnd.Next(0, 5);
                            columna = rnd.Next(0, 5);
                        }
                    }
                    break;
                case 4:
                    fila = rnd.Next(0, 6);
                    columna = rnd.Next(0, 6);
                    for (int i = 0; i < 7;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        } else
                        {
                            fila = rnd.Next(0, 6);
                            columna = rnd.Next(0, 6);
                        }
                    }
                    break;
                case 5:
                    fila = rnd.Next(0, 10);
                    columna = rnd.Next(0, 10);
                    for (int i = 0; i < 12;)
                    {
                        if (tablero[fila, columna].Name == "")
                        {
                            tablero[fila, columna].Name = "Enemigo";
                            i++;
                        } else
                        {
                            fila = rnd.Next(0, 10);
                            columna = rnd.Next(0, 10);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void cargarGemas(int columnaGema)
        {
            Image resizedImageArbol = getImagen("../../../assets/images/gema.png");
            for (int i = 0; i < 3;)
            {
                int fila = rnd.Next(0, columnaGema);
                int columna = rnd.Next(0, columnaGema);
                if (tablero[fila, columna].Image == null && (tablero[fila, columna].Name == null || tablero[fila, columna].Name == ""))
                {
                    tablero[fila, columna].Image = resizedImageArbol;
                    tablero[fila, columna].SizeMode = PictureBoxSizeMode.CenterImage;
                    tablero[fila, columna].Name = "Gema";
                    i++;
                }
            }
        }

        public void cargarPersonaje(int fila, int columna)
        {
            primeraCoordenaAvatar = fila;
            segundaCoordenaAvatar = columna;
            Image resizedImage;
            if (personaje.genero == "Masculino")
            {
                resizedImage = getImagen("../../../assets/images/personaje_masculino_2.png");
            } else
            {
                resizedImage = getImagen("../../../assets/images/avatar_femenino.png");
            }
            tablero[fila, columna].Image = resizedImage;
            tablero[fila, columna].Name = "Avatar";
            tablero[fila, columna].SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void FrmTablero_Load(object sender, EventArgs e)
        {
            inicializar(3, 3);
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void FrmTablero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (segundaCoordenaAvatar > 0)
                {
                    foreach (PictureBox item in tablero)
                    {
                        if (item.Name == "Avatar")
                        {
                            item.Image = null;
                            item.Name = null;
                            break;
                        }
                    }
                    int coordenadaAnterior = segundaCoordenaAvatar;
                    segundaCoordenaAvatar -= 1;
                    if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Gema")
                    {
                        contGemas += 1;
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Image = null;
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        this.tslContGemas.Text = $"{contGemas}";
                        if (contGemas == 3)
                        {
                            MessageBox.Show("Portal Activado!!!");
                            cargarPortal(nivel, coordenadaPrimeraPortal, coordenadaSegundaPortal, "portal.png", "Portal");
                            contGemas = 0;
                        }
                        personaje.puntos += 15;
                        this.tslPuntos.Text = $"{personaje.puntos}";
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal_des")
                    {
                        MessageBox.Show("Portal desactivado");
                        personaje.vida = personaje.vida - 1;
                        personaje.puntos -= 5;
                        this.tslVidas.Text = $"{personaje.vida}";
                        this.tslPuntos.Text = $"{personaje.puntos}";
                        if (personaje.vida > 0)
                        {
                            if (nivel > 1)
                            {
                                coordenadaPrimeraPortalDes = primeraCoordenaAvatar;
                                coordenadaSegundaPortalDes = segundaCoordenaAvatar;
                                tablero[primeraCoordenaAvatar, coordenadaAnterior].Image = null;
                                tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                                cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                                cargarPortal(nivel);
                            } else
                            {
                                cargarPersonaje(primeraCoordenaAvatar, coordenadaAnterior);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Acabas de perder todas tus vidas");
                            this.Close();
                            formPrincipal.Show();
                        }
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Enemigo")
                    {
                        FrmBatallaEnemigo frmBatallaEnemigo = new FrmBatallaEnemigo(enemigos, personaje, this, this.tslVidas, nivel, formPrincipal, this.tslPuntos);
                        frmBatallaEnemigo.Show();
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        cargarUnEnemigo(nivel);
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal")
                    {
                        nivel += 1;
                        switch (nivel)
                        {
                            case 2:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(4, 4);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 3:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(5, 5);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 4:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(6, 6);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 5:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(10, 10);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            default:
                                personaje.puntos += 1000;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                MessageBox.Show($"Felicidades, acabas de terminar el juego, tu punteo final es: {personaje.puntos}, buen trabajo :D");
                                this.Close();
                                formPrincipal.Show();
                                break;
                        }
                    }
                    else
                    {
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Image = null;
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                    }
                }
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (segundaCoordenaAvatar < columnaMatriz)
                {
                    foreach (PictureBox item in tablero)
                    {
                        if (item.Name == "Avatar")
                        {
                            item.Image = null;
                            item.Name = null;
                            break;
                        }
                    }
                    int coordenadaAnterior = segundaCoordenaAvatar;
                    segundaCoordenaAvatar += 1;
                    if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Gema")
                    {
                        contGemas += 1;
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Image = null;
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        this.tslContGemas.Text = $"{contGemas}";
                        if (contGemas == 3)
                        {
                            MessageBox.Show("Portal Activado!!!");
                            cargarPortal(nivel, coordenadaPrimeraPortal, coordenadaSegundaPortal, "portal.png", "Portal");
                            contGemas = 0;
                        }
                        personaje.puntos += 15;
                        this.tslPuntos.Text = $"{personaje.puntos}";
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal_des")
                    {
                        MessageBox.Show("Portal desactivado");
                        personaje.vida = personaje.vida - 1;
                        personaje.puntos -= 5;
                        this.tslVidas.Text = $"{personaje.vida}";
                        this.tslPuntos.Text = $"{personaje.puntos}";
                        if (personaje.vida > 0)
                        {
                            if (nivel > 1)
                            {
                                coordenadaPrimeraPortalDes = primeraCoordenaAvatar;
                                coordenadaSegundaPortalDes = segundaCoordenaAvatar;
                                tablero[primeraCoordenaAvatar, coordenadaAnterior].Image = null;
                                tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                                cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                                cargarPortal(nivel);
                            } else
                            {
                                cargarPersonaje(primeraCoordenaAvatar, coordenadaAnterior);
                            }
                           
                        }
                        else
                        {
                            MessageBox.Show("Acabas de perder todas tus vidas");
                            this.Close();
                            formPrincipal.Show();
                        }
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Enemigo")
                    {
                        FrmBatallaEnemigo frmBatallaEnemigo = new FrmBatallaEnemigo(enemigos, personaje, this, this.tslVidas, nivel, formPrincipal, this.tslPuntos);
                        frmBatallaEnemigo.Show();
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        cargarUnEnemigo(nivel);
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal")
                    {
                        nivel += 1;
                        switch (nivel)
                        {
                            case 2:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(4, 4);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 3:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(5, 5);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 4:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(6, 6);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 5:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(10, 10);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            default:
                                personaje.puntos += 1000;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                MessageBox.Show($"Felicidades, acabas de terminar el juego, tu punteo final es: {personaje.puntos}, buen trabajo :D");
                                this.Close();
                                formPrincipal.Show();
                                break;
                        }
                    }
                    else
                    {
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Image = null;
                        tablero[primeraCoordenaAvatar, coordenadaAnterior].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                    }
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (primeraCoordenaAvatar > 0)
                {
                    foreach (PictureBox item in tablero)
                    {
                        if (item.Name == "Avatar")
                        {
                            item.Image = null;
                            item.Name = null;
                            break;
                        }
                    }
                    int coordenadaAnterior = primeraCoordenaAvatar;
                    primeraCoordenaAvatar -= 1;
                    if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Gema")
                    {
                        contGemas += 1;
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Image = null;
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        this.tslContGemas.Text = $"{contGemas}";
                        if (contGemas == 3)
                        {
                            MessageBox.Show("Portal Activado!!!");
                            cargarPortal(nivel, coordenadaPrimeraPortal, coordenadaSegundaPortal, "portal.png", "Portal");
                            contGemas = 0;
                        }
                        personaje.puntos += 15;
                        this.tslPuntos.Text = $"{personaje.puntos}";
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal_des")
                    {
                        MessageBox.Show("Portal desactivado");
                        personaje.vida = personaje.vida - 1;
                        personaje.puntos -= 5;
                        this.tslVidas.Text = $"{personaje.vida}";
                        this.tslPuntos.Text = $"{personaje.puntos}";
                        if (personaje.vida > 0)
                        {
                            if (nivel > 1)
                            {
                                coordenadaPrimeraPortalDes = primeraCoordenaAvatar;
                                coordenadaSegundaPortalDes = segundaCoordenaAvatar;
                                tablero[coordenadaAnterior, segundaCoordenaAvatar].Image = null;
                                tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                                cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                                cargarPortal(nivel);
                            } else
                            {
                                cargarPersonaje(coordenadaAnterior, segundaCoordenaAvatar);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Acabas de perder todas tus vidas");
                            this.Close();
                            formPrincipal.Show();
                        }
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Enemigo")
                    {
                        FrmBatallaEnemigo frmBatallaEnemigo = new FrmBatallaEnemigo(enemigos, personaje, this, this.tslVidas, nivel, formPrincipal, this.tslPuntos);
                        frmBatallaEnemigo.Show();
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        cargarUnEnemigo(nivel);
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal")
                    {
                        nivel += 1;
                        switch (nivel)
                        {
                            case 2:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(4, 4);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 3:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(5, 5);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 4:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(6, 6);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 5:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(10, 10);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            default:
                                personaje.puntos += 1000;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                MessageBox.Show($"Felicidades, acabas de terminar el juego, tu punteo final es: {personaje.puntos}, buen trabajo :D");
                                this.Close();
                                formPrincipal.Show();
                                break;
                        }
                    }
                    else
                    {
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Image = null;
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                    }
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (primeraCoordenaAvatar < columnaMatriz)
                {
                    foreach (PictureBox item in tablero)
                    {
                        if (item.Name == "Avatar")
                        {
                            item.Image = null;
                            item.Name = null;
                            break;
                        }
                    }
                    int coordenadaAnterior = primeraCoordenaAvatar;
                    primeraCoordenaAvatar += 1;
                    if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Gema")
                    {
                        contGemas += 1;
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Image = null;
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        this.tslContGemas.Text = $"{contGemas}";
                        if (contGemas == 3)
                        {
                            MessageBox.Show("Portal Activado!!!");
                            cargarPortal(nivel, coordenadaPrimeraPortal, coordenadaSegundaPortal, "portal.png", "Portal");
                            contGemas = 0;
                        }
                        personaje.puntos += 15;
                        this.tslPuntos.Text = $"{personaje.puntos}";
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal_des")
                    {
                        MessageBox.Show("Portal desactivado");
                        personaje.vida = personaje.vida - 1;
                        personaje.puntos -= 5;
                        this.tslVidas.Text = $"{personaje.vida}";
                        this.tslPuntos.Text = $"{personaje.puntos}";
                        if (personaje.vida > 0)
                        {
                            if (nivel > 1)
                            {
                                coordenadaPrimeraPortalDes = primeraCoordenaAvatar;
                                coordenadaSegundaPortalDes = segundaCoordenaAvatar;
                                tablero[coordenadaAnterior, segundaCoordenaAvatar].Image = null;
                                tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                                cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                                cargarPortal(nivel);
                            }
                            else
                            {
                                cargarPersonaje(coordenadaAnterior, segundaCoordenaAvatar);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Acabas de perder todas tus vidas");
                            this.Close();
                            formPrincipal.Show();
                        }
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Enemigo")
                    {
                        FrmBatallaEnemigo frmBatallaEnemigo = new FrmBatallaEnemigo(enemigos, personaje, this, this.tslVidas, nivel, formPrincipal, this.tslPuntos);
                        frmBatallaEnemigo.Show();
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                        cargarUnEnemigo(nivel);
                    }
                    else if (tablero[primeraCoordenaAvatar, segundaCoordenaAvatar].Name == "Portal")
                    {
                        nivel += 1;
                        switch (nivel)
                        {
                            case 2:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(4, 4);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 3:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(5, 5);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 4:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(6, 6);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            case 5:
                                personaje.puntos += 50;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                inicializar(10, 10);
                                MessageBox.Show($"Terminaste el nivel {nivel - 1} Felicitades!!! :D");
                                break;
                            default:
                                personaje.puntos += 1000;
                                this.tslPuntos.Text = $"{personaje.puntos}";
                                MessageBox.Show($"Felicidades, acabas de terminar el juego, tu punteo final es: {personaje.puntos}, buen trabajo :D");
                                this.Close();
                                formPrincipal.Show();
                                break;
                        }
                    }
                    else
                    {
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Image = null;
                        tablero[coordenadaAnterior, segundaCoordenaAvatar].Name = null;
                        cargarPersonaje(primeraCoordenaAvatar, segundaCoordenaAvatar);
                    }
                }
            }
        }
    }
}
