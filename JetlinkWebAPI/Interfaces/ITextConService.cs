using JetlinkWebAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetlinkWebAPI.Application.Interfaces
{
    public interface ITextConService
    {
        UserTextModel ConvertWordToNumber(UserTextModel input);
    }
}
