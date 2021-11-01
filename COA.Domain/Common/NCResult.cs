using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA.Domain.Common
{
    public class NCResult
    {
        public bool HasErrors { get; set; }
        public IList<string> Messages { get; set; }

        public NCResult()
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
}
