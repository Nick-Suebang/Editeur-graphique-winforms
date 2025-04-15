using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_editeur_graphique_winforms_Nick_Suebang
{
    public class Ligne : Forme
    {
        public Ligne(Point debut, Point fin, Color couleur, int epaisseur ) : base(debut, fin, couleur, epaisseur)
        {
        }

        public override void Dessiner(Graphics graphe)
        {
            using (Pen pen = new Pen(Couleur, Epaisseur))
            {
                graphe.DrawLine(pen, Debut, Fin);
            }
        }

        public override string Sauvegarder()
        {
            return $"Ligne;{Debut.X};{Debut.Y};{Fin.X};{Fin.Y};{Couleur.R};{Couleur.G};{Couleur.B};{Epaisseur}";
        }
    }
}
