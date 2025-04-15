using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_editeur_graphique_winforms_Nick_Suebang
{
    public abstract class Forme
    {
        protected Color Couleur;
        protected int Epaisseur;
        protected Point Debut;
        protected Point Fin;

        public Forme(Point debut, Point fin, Color couleur, int epaisseur)
        {
            Debut = debut;
            Fin = fin;
            Couleur = couleur;
            Epaisseur = epaisseur;
        }

        public void RedimensionnerPourInclure(Point fin)
        {
            this.Fin = fin;
        }

        public abstract void Dessiner(Graphics graphe);
        public abstract string Sauvegarder();

    }
}
