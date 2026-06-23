using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Options
{
    public class AuthorizationOptions
    {
        public static readonly string issuer = "shop-server";
        public static readonly string audience = "shop-client";
        public static readonly int lifeTime = 15;
    }
}
