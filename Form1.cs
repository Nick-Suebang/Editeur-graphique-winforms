using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tp_editeur_graphique_winforms_Nick_Suebang
{
    public partial class Form1: Form
    {
        private List<Forme> formes;
        private Forme formeEnCours;
        private Outil outilCourant;
        private Color couleurCourante;
        private int epaisseurCourante;
        private bool dessine;

        public Form1()
        {
            InitializeComponent();

            formes = new List<Forme>();
            outilCourant = Outil.Ligne;
            couleurCourante = Color.Black;
            epaisseurCourante = 2;
            dessine = false;

            MiseAJourStatus();
        }

        private void MiseAJourStatus()
        {
            toolStripStatusLabel1.Text = $"Outil: {outilCourant} | Couleur: {couleurCourante.Name} | Épaisseur: {epaisseurCourante}px";
        }

        private void outilsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form dialog = new Form())
            {
                dialog.Text = "Modifier l'épaisseur";
                dialog.Width = 250;
                dialog.Height = 150;

                Label label = new Label() { Text = "Nouvelle épaisseur :", Left = 10, Top = 20, Width = 120 };
                TextBox textBox = new TextBox() { Left = 140, Top = 20, Width = 50 };
                Button okButton = new Button() { Text = "OK", Left = 40, Width = 60, Top = 60, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "Annuler", Left = 120, Width = 60, Top = 60, DialogResult = DialogResult.Cancel };

                dialog.Controls.Add(label);
                dialog.Controls.Add(textBox);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);
                dialog.AcceptButton = okButton;
                dialog.CancelButton = cancelButton;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(textBox.Text, out int nouvelleEpaisseur) && nouvelleEpaisseur > 0)
                    {
                        epaisseurCourante = nouvelleEpaisseur;
                        MiseAJourStatus();
                    }
                    else
                    {
                        MessageBox.Show("Veuillez entrer une valeur numérique positive.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Outil_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dessine = true;

                switch (outilCourant)
                {
                    case Outil.Ligne:
                        formeEnCours = new Ligne(e.Location, e.Location, couleurCourante, epaisseurCourante); break;
                    case Outil.Rectangle:
                        formeEnCours = new Rectangle(e.Location, e.Location, couleurCourante, epaisseurCourante); break;
                    case Outil.Ellipse:
                        formeEnCours = new Ellipse(e.Location, e.Location, couleurCourante, epaisseurCourante); break;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dessine && formeEnCours != null)
            {
                formeEnCours.RedimensionnerPourInclure(e.Location);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dessine && formeEnCours != null)
            {
                formeEnCours.RedimensionnerPourInclure(e.Location);
                formes.Add(formeEnCours);
                formeEnCours = null;
                dessine = false;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var forme in formes)
            {
                forme.Dessiner(e.Graphics);
            }
            if (dessine && formeEnCours != null)
            {
                formeEnCours.Dessiner(e.Graphics);
            }
        }

        private void couleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = couleurCourante;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    couleurCourante = colorDialog.Color;
                    MiseAJourStatus();
                }
            }
        }

        private void augmenterTraitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            epaisseurCourante++;
            MiseAJourStatus();
        }

        private void diminuerTraitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (epaisseurCourante > 1)
            {
                epaisseurCourante--;
                MiseAJourStatus();
            }
        }

        private void ouvrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Fichiers texte (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(openFileDialog.FileName);
                        formes.Clear();
                        foreach (var line in lines)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length == 9)
                            {
                                string type = parts[0];
                                int x1 = int.Parse(parts[1]);
                                int y1 = int.Parse(parts[2]);
                                int x2 = int.Parse(parts[3]);
                                int y2 = int.Parse(parts[4]);
                                int r = int.Parse(parts[5]);
                                int g = int.Parse(parts[6]);
                                int b = int.Parse(parts[7]);
                                int ep = int.Parse(parts[8]);
                                Color couleur = Color.FromArgb(r, g, b);
                                Point p1 = new Point(x1, y1);
                                Point p2 = new Point(x2, y2);
                                Forme forme = null;
                                switch (type)
                                {
                                    case "Ligne":
                                        forme = new Ligne(p1, p2, couleur, ep); break;
                                    case "Rectangle":
                                        forme = new Rectangle(p1, p2, couleur, ep); break;
                                    case "Ellipse":
                                        forme = new Ellipse(p1, p2, couleur, ep); break;
                                }
                                if (forme != null)
                                {
                                    formes.Add(forme);
                                }
                            }
                        }
                        pictureBox1.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors du chargement : " + ex.Message);
                    }
                }
            }
        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Fichiers texte (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<string> lines = new List<string>();
                        foreach (var forme in formes)
                        {
                            lines.Add(forme.Sauvegarder());
                        }
                        File.WriteAllLines(saveFileDialog.FileName, lines);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outilCourant = Outil.Rectangle;
            MiseAJourStatus();
        }

        private void ligneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outilCourant = Outil.Ligne;
            MiseAJourStatus();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outilCourant = Outil.Ellipse;
            MiseAJourStatus();
        }
    }
}
