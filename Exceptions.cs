using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_BoardGames_Framework
{

    internal class ColumnFullException : Exception
    {
        public ColumnFullException() : base("Selected Column is full, please select a different column") { }
    }
    internal class SaveAndExit : Exception
    {
        public SaveAndExit() : base("Your game will save and you'll exit the game") { }
    }
}
