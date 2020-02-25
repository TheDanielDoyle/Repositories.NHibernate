using System.Data;
using NHibernate;

namespace Repositories.NHibernate
{
    public interface IDataContext
    {
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
