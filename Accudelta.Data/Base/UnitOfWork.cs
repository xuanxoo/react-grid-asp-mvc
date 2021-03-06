﻿using System;
using System.Web;
using System.Collections;
using System.Threading;
using Accudelta.Data.Interface;

namespace Accudelta.Data.Base
{
    public class UnitOfWork 
    {
        private const string HTTPCONTEXTKEY = "Accudelta.Repository.Base.HttpContext.Key";
        
        private static readonly Hashtable _threads = new Hashtable();
        

        public static void Commit()
        {
            IUnitOfWork unitOfWork = GetUnitOfWork();

            if (unitOfWork != null)
            {
                unitOfWork.Commit();
            }
        }

        public static IUnitOfWork Current 
        {
            get
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();

                if (unitOfWork == null)
                {
                    unitOfWork = EFUnitOfWorkFactory.Create();
                    SaveUnitOfWork(unitOfWork);
                }

                return unitOfWork;
            }
        }

        private static IUnitOfWork GetUnitOfWork()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HTTPCONTEXTKEY))
                {
                    return (IUnitOfWork)HttpContext.Current.Items[HTTPCONTEXTKEY];
                }

                return null;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();
                    return null;
                }
                else
                {
                    lock (_threads.SyncRoot)
                    {
                        return (IUnitOfWork)_threads[Thread.CurrentThread.Name];
                    }
                }
            }
        }

        private static void SaveUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[HTTPCONTEXTKEY] = unitOfWork;
            }
            else
            {
                lock(_threads.SyncRoot)
                {
                    _threads[Thread.CurrentThread.Name] = unitOfWork;
                }
            }
        }
    }
}
