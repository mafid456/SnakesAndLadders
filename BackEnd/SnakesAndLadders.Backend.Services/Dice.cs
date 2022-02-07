using SnakesAndLadders.BackEnd.Contracts.Services;
using System;

namespace SnakesAndLadders.BackEnd.Services
{
    public class Dice : IDice
    {
        #region Constants
        private const int MIN_VALUE = 1;
        private const int MAX_VALUE = 6;
        #endregion

        #region Internal vars
        private readonly Random _randomGenerator;
        #endregion

        #region Constructors
        public Dice() => this._randomGenerator = new Random();
        #endregion

        #region Methods & Functions
        public int Roll() => this._randomGenerator.Next(Dice.MIN_VALUE, Dice.MAX_VALUE + 1);
        #endregion
    }
}
