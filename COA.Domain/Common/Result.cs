using System.Collections.Generic;
using System.Threading.Tasks;

namespace COA.Domain.Common
{
    #region AboutResult
    /// <summary>
    /// La clase Result se utiliza para dar mensajes al usuario, junto con los ActionResult
    /// <br></br>
    /// Todo endpoint que no regrese informacion debera regresar un Result
    /// </summary> 
    #endregion
    public class Result
    {
        public bool HasErrors { get; set; }
        public IList<string> Messages { get; set; }

        public Result()
        {
            HasErrors = false;
            Messages = new List<string>();
        }
        public async Task Success(string message)
        {
            HasErrors = false;
            Messages = new List<string>() { message };
        }
        public async Task Fail(string message)
        {
            HasErrors = true;
            Messages = new List<string>() { message };
        }
        public async Task NotFound()
        {
            HasErrors = true;
            Messages = new List<string>()
            { "No se ha podido encontrar un registro con los datos proporcionados" };
        }

    }
    public class Result<T> : Result where T : class
    {
        public T Value { get; set; }

        public Result()
        {
            HasErrors = false;
            Messages = new List<string>();
        }

        public async Task Success(T result)
        {
            HasErrors = false;
            Value = result;
        }

    }
}
