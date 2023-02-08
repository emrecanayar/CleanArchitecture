using System.ComponentModel.DataAnnotations;

namespace Core.Domain.ComplexTypes
{
    public enum RecordStatu
    {
        None = 0,
        Active = 1,
        Passive = 2,
    }

    public enum FileType
    {
        None,
        Xls,
        Xlsx,
        Doc,
        Pps,
        Pdf,
        Img,
        Mp4
    }

    public enum NationalityType
    {
        [Display(Name = "Turkish National")]
        Turkish = 1,
        [Display(Name = "Foreign National")]
        Foreign = 2,
    }
    public enum InvoiceType
    {
        [Display(Name = "Individual Invoice")]
        Individual = 1,
        [Display(Name = "Corporate Invoice")]
        Corporate = 2,
    }
    public enum TaxPayerType
    {
        None = 0,
        [Display(Name = "Individual Invoice")]
        EInvoice = 1,
        [Display(Name = "Corporate Invoice")]
        EArchive = 2,
    }
    public enum CartStatus
    {
        None = 0,
        Cart = 1,
        Campaign = 2,
        Invoice = 3,
        Payment = 4,
        Completed = 5
    }
    public enum OrderStatus
    {
        None = 0,
    }

    public enum TransactionType
    {
        Output = -1,
        Input = 1
    }

    public enum OperationType
    {
        WarehouseTransaction = 1,
        Sale = 2,
        Wastage = 3
    }

    public enum NotificationType
    {
        Tablet = 1,
        HeadOffice = 2
    }

    public enum NotificationActionType
    {
        ShoppingStarted = 1,
        ShoppingCompleted = 2,
        CartActive = 3,
        Charging = 4,
        Help = 5,
        Warning = 6,
        Report = 7,
        NewStock = 8,
        CriticalStock = 9,
        ExitDoor = 10

    }

    public enum ProductStatu
    {
        None = 0,
        Active = 1,
        Delist = 2,
    }

    public enum ConfirmStatus
    {
        Waiting = 0,
        Confirmed = 1,
        Rejected = 2,
    }

    public enum ConfirmationTypes
    {
        None = 0,
        StockDown = 1,
    }

}
