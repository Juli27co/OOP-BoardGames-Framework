namespace OOP_BoardGames_Framework
{
    internal class ColumnFullException : Exception
    {
        public ColumnFullException() : base("Selected Column is full, please select a different column") { }
    }
}
