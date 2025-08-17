using Common.Application;
using FluentValidation;
using Shop.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Comments.ChangeStatus
{
    public record ChangeCommentStatusCommand(long CommentId,CommentStatus status):IBaseCommand
    
}
