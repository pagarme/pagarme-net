using PagarMe.Base;
using PagarMe.Filter;
using PagarMe.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Model
{
    public class BalanceOperation : Base.Model
    {
        protected override string Endpoint { get { return "/operations"; } }

        public BalanceOperation() : this(null) { }

        public BalanceOperation(PagarMeService service) : base(service) { }

        public BalanceOperation(PagarMeService service, string endpointPrefix = "") :base(service, endpointPrefix) { }

        public BalanceOperationStatus Status
        {
            get { return GetAttribute<BalanceOperationStatus>("status"); }
        }

        public int BalanceAmount
        {
            get { return GetAttribute<int>("balance_amount"); }
        }

        public int BalanceOldAmount
        {
            get { return GetAttribute<int>("balance_old_amount"); }
        }

        public BalanceOperationType MovementType
        {
            get { return GetAttribute<BalanceOperationType>("type"); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
        }

        public Payable MovementPayable
        {
            get { return (Payable)ChooseBalanceOperationType(BalanceOperationType.Payable); }
        }

        public BulkAnticipation MovementBulkAnticipation
        {
            get { return (BulkAnticipation)ChooseBalanceOperationType(BalanceOperationType.Anticipation); }
        }

        public Transfer MovementTransfer
        {
            get { return (Transfer)ChooseBalanceOperationType(BalanceOperationType.Transfer); }
        }

        private Base.Model ChooseBalanceOperationType(BalanceOperationType type)
        {
            switch (type)
            {
                case BalanceOperationType.Anticipation :
                    return ChooseMovementObject(type);
                case BalanceOperationType.Payable :
                    return ChooseMovementObject(type);
                case BalanceOperationType.Transfer :
                    return ChooseMovementObject(type);
                default :
                    return null;
            }
        }

        private Base.Model ChooseMovementObject(BalanceOperationType type)
        {
            switch (GetAttribute<BalanceOperationType>("type"))
            {
                case BalanceOperationType.Anticipation:

                    if (type == BalanceOperationType.Anticipation)
                        return GetAttribute<BulkAnticipation>("movement_object");
                    else
                        return null;
                case BalanceOperationType.Payable:

                    if (type == BalanceOperationType.Payable)
                        return GetAttribute<Payable>("movement_object");
                    else
                        return null;
                case BalanceOperationType.Transfer:

                    if (type == BalanceOperationType.Transfer)
                        return GetAttribute<Transfer>("movement_object");
                    else
                        return null;
                default:
                    return null;
            }
        }
        
        public PagarMe.Base.QueriableModelCollection<BalanceOperation, BalanceOperationQueriableParameters> History(string endpoint)
        {
            return new QueriableModelCollection<BalanceOperation, BalanceOperationQueriableParameters>(Service, endpoint + Endpoint, endpointPrefix ); 
        }
    }
}
