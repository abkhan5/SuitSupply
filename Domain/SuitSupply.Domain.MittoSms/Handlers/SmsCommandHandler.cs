#region Namespace

using System;
using SuitSupply.Core.DataAccess;
using SuitSupply.Core.Messaging;
using SuitSupply.Domain.MittoSms.Command;

#endregion
namespace SuitSupply.Domain.MittoSms.Handlers
{
    public class SmsCommandHandler :
        ICommandHandler<AddNumberCommand>,
        ICommandHandler<AddSmsCommand>
    {
        #region Private members

        private Lazy<IUnitOfWork> _dataAccess;
        private readonly Func<IUnitOfWork> _dataAccessInstanceMethod;
        private readonly Lazy<IUnitOfWork> _eventContextDal;

        #endregion

        public SmsCommandHandler(Func<IUnitOfWork> dataAccessInstanceMethod, Func<IUnitOfWork> eventContextDal)
        {
            _dataAccessInstanceMethod = dataAccessInstanceMethod;
            _eventContextDal = new Lazy<IUnitOfWork>(eventContextDal);
            _dataAccess = new Lazy<IUnitOfWork>(_dataAccessInstanceMethod);
        }


       
        public void Handle(ICommand command)
        {
            _dataAccess = new Lazy<IUnitOfWork>(_dataAccessInstanceMethod);

            var commandResult = new Event
            {
                CommandId = command.ID.ToString(),
                Payload = command.GetType().Name,
                WasCommandSuccessfull = true
            };
            try
            {


                if (command.GetType() == typeof(AddNumberCommand))
                {
                    Handle(command as AddNumberCommand);
                }
                else if (command.GetType() == typeof(AddSmsCommand))
                {
                    Handle(command as AddSmsCommand);
                }
            }
            catch (Exception ex)
            {
                commandResult.WasCommandSuccessfull = false;
                throw;
            }
            finally
            {
                _dataAccess = new Lazy<IUnitOfWork>(_dataAccessInstanceMethod);
                _eventContextDal.Value.AddEntity(commandResult);
                _eventContextDal.Value.Save();
            }
        }

        public void Handle(AddSmsCommand command)
        {
            var message = command.Message;
            _dataAccess.Value.AddEntity(message);
            _dataAccess.Value.Save();
        }

        public void Handle(AddNumberCommand command)
        {

        }

        //private bool CheckAndHandle<TExpected, TActual>(TExpected expected, TActual actual)
        //    where TExpected : ICommand
        //    where TActual : ICommand
        //{
        //    if (expected.GetType()== actual.GetType())
        //    {
        //        Handle(actual);
        //    }
        //}
    }
}
