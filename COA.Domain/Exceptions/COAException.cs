using System;

namespace COA.Domain.Exceptions
{
    #region About: CustomException
    /// <summary>
    /// CustomException a nivel de negocio. Regresa Exceptions de logica de negocion, dejando las de systema con Exception
    /// </summary> 
    #endregion
    public class COAException : Exception
    {
        public COAException(string message) : base(message) { }
    }
}
