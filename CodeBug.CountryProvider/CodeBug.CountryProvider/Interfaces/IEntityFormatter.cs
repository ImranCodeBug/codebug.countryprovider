using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Interfaces
{
    public interface IEntityFormatter<T>
    {
        Entity ConvertToEntity(T resultItem);
    }
}
