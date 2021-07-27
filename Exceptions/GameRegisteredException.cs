using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Exceptions
{
    public class GameRegisteredException : Exception
    {
        public GameRegisteredException()
            : base("Game was already resgistered")
        {
        }
    }
}
