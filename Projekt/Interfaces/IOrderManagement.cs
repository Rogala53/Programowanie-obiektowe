using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Interfaces
{
    public interface IOrderManagement
    {
        void AddOrder();
        void SetOrder();
        void ChangeOrderStatus();
    }
}
