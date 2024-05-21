using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Exceptions
{
    public class DonorException : Exception
    {
        public DonorException(string message)
            : base(message)
        {
        }
    }
}
