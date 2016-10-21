using System;

namespace SuitSupply.Core.DataAccess
{
    public interface IEventDal
    {
        bool WaitUntilAvailable(Guid commandID);
    }
}
