using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IURIS.MOVIL.Modelos_y_clases.Utils
{
    public class BackGroundService : IPeriodicTask
    {
        public TimeSpan Interval { get; set; }

        public BackGroundService(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }


        public async Task<bool> StartJob()
        {
            // YOUR CODE HERE
            // THIS CODE WILL BE EXECUTE EVERY INTERVAL
            Console.WriteLine(DateTime.Now.ToString());
            return true; //return false when you want to stop or trigger only once
        }
    }
}