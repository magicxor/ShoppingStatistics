using System;
using System.Collections.Generic;

namespace ShoppingStatistics.Core.Models.Domain
{
    public class Receipt
    {
        public int ecashTotalSum { get; set; }
        public string user { get; set; }
        public int fiscalDocumentNumber { get; set; }
        public string rawData { get; set; }
        public int operationType { get; set; }
        public List<object> stornoItems { get; set; }
        public int shiftNumber { get; set; }
        public int cashTotalSum { get; set; }
        public int totalSum { get; set; }
        public List<Item> items { get; set; }
        public object fiscalSign { get; set; }
        public int taxationType { get; set; }
        public List<object> modifiers { get; set; }
        public string kktRegId { get; set; }
        public string userInn { get; set; }
        public string @operator { get; set; }
        public int requestNumber { get; set; }
        public string fiscalDriveNumber { get; set; }
        public int ndsNo { get; set; }
        public int receiptCode { get; set; }
        public DateTime dateTime { get; set; }
        public int? nds0 { get; set; }
        public string retailPlaceAddress { get; set; }
        public int? ndsCalculated18 { get; set; }
        public int? ndsCalculated10 { get; set; }
        public int? nds18 { get; set; }
        public int? nds10 { get; set; }
        public List<object> properties { get; set; }
        public string reqDocDate { get; set; }
        public int? rqId { get; set; }
        public string buyerAddress { get; set; }
        public string addressToCheckFiscalSign { get; set; }
        public string senderAddress { get; set; }
        public long? messageFiscalSign { get; set; }
        public List<object> message { get; set; }
        public UserProperty userProperty { get; set; }
        public List<object> operatorPhoneToTransfer { get; set; }
        public int? code { get; set; }
        public string operatorTransferInn { get; set; }
        public string sellerAddress { get; set; }
        public string paymentAgentOperation { get; set; }
        public List<object> providerPhone { get; set; }
        public string operatorTransferName { get; set; }
        public List<object> operatorToReceivePhone { get; set; }
        public int? creditSum { get; set; }
        public string operatorTransferAddress { get; set; }
        public string operatorInn { get; set; }
        public int? provisionSum { get; set; }
        public int? paymentAgentType { get; set; }
        public string buyerPhoneOrAddress { get; set; }
        public int? prepaidSum { get; set; }
        public List<object> paymentAgentPhone { get; set; }
        public string authorityUri { get; set; }
        public int? counterSubmissionSum { get; set; }
        public int? prepaymentSum { get; set; }
        public int? postpaymentSum { get; set; }
    }
}