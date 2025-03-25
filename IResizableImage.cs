using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Projet1
{
    /// <summary>
    /// Interface pour définir les méthodes
    /// </summary>
    internal interface IResizableImage
    {
        public void DoubleSize();
        public void Save(List<string> newPixels);
    }
}
