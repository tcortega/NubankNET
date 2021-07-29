using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tcortega.NubankClient.Enums
{
    public enum Category 
    { 
        BillFlowClosed, 
        BillFlowPaid, 
        CardActivated, 
        Payment, 
        RewardsFee, 
        RewardsSignup, 
        Transaction, 
        TransactionReversed, 
        Tutorial, 
        Welcome 
    };

    public enum Status 
    { 
        Canceled, 
        Reversed, 
        Settled, 
        Unsettled 
    };

    public enum Subcategory 
    { 
        CardNotPresent, 
        CardPresent, 
        Unknown 
    };

    public enum Source 
    { 
        InstallmentsMerchant,
        UpfrontForeign,
        UpfrontNational 
    };
}
