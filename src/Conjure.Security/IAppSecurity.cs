using System.Collections.Generic;

namespace Conjure.Security
{
    public interface IAppSecurity
    {
        AppUser GetUser();

        AppRole GetRole();

        IEnumerable<AppPermission> GetPermissions();
    }
}
