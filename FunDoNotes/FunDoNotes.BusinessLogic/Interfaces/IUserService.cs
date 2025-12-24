using FunDoNotes.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterUserDto dto);
    }
}
