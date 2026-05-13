using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_BoardGames_Framework
{
    internal class NotEnoughMagneticDiscsException : Exception
    {
        public NotEnoughMagneticDiscsException() : base("You don't have any more magnetic discs available") { }
    }

    internal class NotEnoughExplodingDiscsException : Exception
    {
        public NotEnoughExplodingDiscsException() : base("You don't have any more exploding discs available") { }
    }

    internal class ColumnFullException : Exception
    {
        public ColumnFullException() : base("Selected Column is full, please select a different column") { }
    }
    internal class InvalidTestModeString : Exception
    {
        public InvalidTestModeString() : base("The testing string you typed is not in the right format.") { }
    }
    internal class SaveAndExit : Exception
    {
        public SaveAndExit() : base("Your game will save and you'll exit the game") { }
    }
}
