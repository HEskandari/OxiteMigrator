using System.Data;
using System.Dynamic;

namespace OxiteMigrator.DataAccess
{
    public class OxiteRecord : DynamicObject
    {
        private readonly IDataRecord _dataRecord;

        public OxiteRecord(IDataRecord dataRecord)
        {
            _dataRecord = dataRecord;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _dataRecord[binder.Name];
            return result != null;
        }
    }
}