using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinFirebase.Helper;

namespace Cinemaddict.ViewModels
{
    class CommandViewModel
    {
        public async Task ResetDB()
        {
           await new FirebaseHelper().DeleteAllUser();
        }
    }
}
