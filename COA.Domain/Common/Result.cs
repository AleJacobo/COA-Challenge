using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Result Success(string message)
            => new Result() { HasErrors = false, Messages = new List<string>() { message } };
        public Result Fail(string message)
            => new Result() { HasErrors = true, Messages = new List<string>() { message } };
        public Result NotFound()
            => new Result()
            {
                HasErrors = true,
                Messages = new List<string>()
            { "No se ha podido encontrar un registro con los datos proporcionados" }
            };

    }
}
