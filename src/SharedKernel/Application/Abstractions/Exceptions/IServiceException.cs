using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Application.Abstractions.Exceptions;
public interface IServiceException
{

    HttpStatusCode StatusCode { get; }
    string ErrorMessage { get; }
}
