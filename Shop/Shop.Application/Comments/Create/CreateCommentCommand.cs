using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.Create
{
    public record CreateCommentCommand(long UserId , long ProductId , string Text) : IBaseCommand;

   
}
