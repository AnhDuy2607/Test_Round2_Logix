using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Utilities
{
    public class ConstantValue
    {
        public const string PASSWORD_SALT = "P@5SW0RD#S@LT";
        public const string DEFAULT_SCHEMA_AUTHENTICATION = "Basic";
        public const string BEARER_TYPE = "bearer";
        public const string AUTHORIZATION_KEY = "Authorization";
        public const string UNAUTHORIZED = "Unauthorized";
        public const string CHARACTER_EMPTY = " ";
        public const string ACTIVE_STATUS = "Hoạt động";
        public const string INACTIVE_STATUS = "Đã khóa";
        public const string REGEX_EMAIL = "^[a-z][a-z0-9_\\.]{3,32}@[a-z0-9]{2,}(\\.[a-z0-9]{2,4}){1,2}$";
        public const int PAGE_SIZE = 10;
    }
}
