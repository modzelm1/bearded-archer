using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.CallbackInterface
{
    public interface ICallbacks
    {
        //[OperationContract(IsOneWay = true)]
        void MyCallbackFunction(string callbackValue);
    }
}
