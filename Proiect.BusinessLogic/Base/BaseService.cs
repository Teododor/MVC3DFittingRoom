﻿using AutoMapper;
using Proiect.Common.DTOs;
using Proiect.DataAccess;
using System.Transactions;

namespace Proiect.BusinessLogic.Base
{
    public class BaseService
    {
        protected readonly IMapper Mapper;
        protected readonly UnitOfWork UnitOfWork;


       protected readonly CurrentUserDto CurrentUser;
        

        public BaseService(ServiceDependencies serviceDependencies)
        {
            Mapper = serviceDependencies.Mapper;
            UnitOfWork = serviceDependencies.UnitOfWork;
            CurrentUser = serviceDependencies.CurrentUser;
        }

        protected TResult ExecuteInTransaction<TResult>(Func<UnitOfWork, TResult> func)
        {
            if(func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            using (var transactionScope = new TransactionScope())
            {
                var result = func(UnitOfWork);
                transactionScope.Complete();

                return result;
            }
        }

        public void ExecuteInTransaction(Action<UnitOfWork> action)
        {
            if(action == null)
            {
                throw new ArgumentNullException(nameof(action));

                using (var transactionScope = new TransactionScope())
                {
                    action(UnitOfWork);
                    transactionScope.Complete();
                }
            }
        }

    }
}
