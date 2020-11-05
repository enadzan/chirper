using System;
using Microsoft.Data.SqlClient;

namespace Chirper.Server
{
    public static class ExceptionExtensions
    {
        public static bool IsPrimaryOrUniqueKeyViolation(this Exception e)
        {
            if (e is SqlException sqlException)
            {
                return sqlException.Number == 2627 || sqlException.Number == 2601;
            }

            return false;
        }
    }
}
