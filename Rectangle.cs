using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_editeur_graphique_winforms_Nick_Suebang
{
    public class Rectangle : Forme
    {
        public Rectangle(Point debut, Point fin, Color couleur, int epaisseur) : base(debut, fin, couleur, epaisseur)
        {
        }

        public override void Dessiner(Graphics graphe)
        {
            using (Pen pen = new Pen(Couleur, Epaisseur))
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Math.Min(Debut.X, Fin.X), Math.Min(Debut.Y, Fin.Y), Math.Abs(Fin.X - Debut.X), Math.Abs(Fin.Y - Debut.Y));
                graphe.DrawRectangle(pen, rect);
            }
        }

        public override string Sauvegarder()
        {
            return $"Rectangle;{Debut.X};{Debut.Y};{Fin.X};{Fin.Y};{Couleur.R};{Couleur.G};{Couleur.B};{Epaisseur}";
        }
    }
}
