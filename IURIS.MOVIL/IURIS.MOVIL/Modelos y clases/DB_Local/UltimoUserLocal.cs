using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases.DB_Local.COMMON
{
    public class UltimoUserLocal
    {

        public bool Save(string IdUser)
        {
            try
            {
                UltimoUser ultimoUser = new UltimoUser() { IdUser = IdUser };

                App.ultimoUser.SaveUltimoUserAsync(ultimoUser);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task< bool> Update(UltimoUser ultimoUser)
        {
            try
            {
               await App.ultimoUser.UpdateUltimoUserAsync(ultimoUser);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
