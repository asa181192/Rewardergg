using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewardergg.Application.GraphQlQueries
{
    public class UserQueries
    {
        public static string PlayerAccountData { get; } = @"
            query PlayerData {
              currentUser {
                 id,
                 name,
                 email,
                 discriminator,
                 images{
                  url,
                  height,
                  width
                },
                player{
                  gamerTag,
                  prefix
                }
              }
            }
            @"; 
    }
}
