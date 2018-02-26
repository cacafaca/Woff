using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCode.WoffUtility
{
    public class WoffBaseException : Exception
    {
        #region Constructors

        public WoffBaseException(string message)
            : base (message)
        {
                
        }

        public WoffBaseException()
        {

        }

        #endregion
    }
}
