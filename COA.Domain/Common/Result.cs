using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Domain.Common
{
    #region About: Result
    /// <summary>
    /// La clase Result se utiliza para dar mensajes al usuario
    /// </summary> 
    #endregion
    public class Result
    {
        #region About: HasErrors
        /// <summary>
        /// Si la peticion tuvo exito, fallo o no se encontro, cambia a true o false con su correspondiente Task
        /// </summary> 
        #endregion
        public bool HasErrors { get; set; }

        #region About: Messages
        /// <summary>
        /// Lista de mesajes al usuario
        /// </summary> 
        #endregion
        public IList<string> Messages { get; set; }

        public Result()
        {
            HasErrors = false;
            Messages = new List<string>();
        }

        #region About: SuccessOverload
        /// <summary>
        /// Overload Task para Success. No se manda mensaje al usuario
        /// </summary>
        /// <returns>HasErrors=false</returns> 
        #endregion
        public async Task Success()
        {
            HasErrors = false;
        }

        #region About: Success
        /// <summary>
        /// Si la operacion tuvo resultado positivo
        /// </summary>
        /// <param name="message">Mensaje para mostar al usuario</param>
        /// <returns>HasErrors=false<br></br>Messages=message</returns> 
        #endregion
        public async Task Success(string message)
        {
            HasErrors = false;
            Messages = new List<string>() { message };
        }

        #region About: Fail
        /// <summary>
        /// La operacion no se pudo completar
        /// </summary>
        /// <param name="message">Mensaje para mostar al usuario</param>
        /// <returns>HasErrors=true<br></br>Messages=message</returns> 
        #endregion
        public async Task Fail(string message)
        {
            HasErrors = true;
            Messages = new List<string>() { message };
        }

        #region About: NotFound
        /// <summary>
        /// La operacion se completo, pero no se encontro registro en DB
        /// </summary>
        /// <param name="message">Mensaje para mostar al usuario</param>
        /// <returns>HasErrors=true<br></br>Messages=message</returns> 
        #endregion
        public async Task NotFound()
        {
            HasErrors = true;
            Messages = new List<string>()
            { "No se ha podido encontrar un registro con los datos proporcionados" };
        }
    }

    #region About: Result<T>
    /// <summary>
    /// Result para desplegar informacion, almacenada en Value
    /// </summary>
    /// <typeparam name="T">Tipo que se regresa en Result. T : class </typeparam> 
    #endregion
    public class Result<T> : Result where T : class
    {
        #region About: Value
        /// <summary>
        /// T Value: Almacena la informacion de objetos que se quieran presentar en endpoint
        /// </summary> 
        #endregion
        public T Value { get; set; }

        public Result()
        {
            HasErrors = false;
            Messages = new List<string>();
        }

        #region About: SuccessTask
        /// <summary>
        /// Success de Result, regresa informacion almacenada en Value
        /// </summary>
        /// <param name="result">T : class</param>
        /// <returns>Regresa Value= T.  </returns> 
        #endregion
        public async Task Success(T result)
        {
            HasErrors = false;
            Value = result;
        }

    }
}
