using TNO.API.Models;

namespace TNO.API.Areas.Admin.Models.User;

/// <summary>
/// UserProductModel class, provides a model that represents an user product.
/// </summary>
public class UserProductModel : AuditColumnsModel
{
    #region Properties
    /// <summary>
    /// get/set - Primary key to user.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// get/set - Primary key to user.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// get/set - The product.
    /// </summary>
    public ProductModel? Product { get; set; }

    /// <summary>
    /// get/set - The status of a user's product request.
    /// </summary>
    public Entities.ProductRequestStatus Status { get; set; }


    /// <summary>
    /// get/set - Unique username to identify user.
    /// </summary>
    public string Username { get; set; } = "";

    /// <summary>
    /// get/set - User's email address.
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// get/set - The user's preferred email address.
    /// </summary>
    public string PreferredEmail { get; set; } = "";

    /// <summary>
    /// get/set - Display name of user.
    /// </summary>
    public string DisplayName { get; set; } = "";

    /// <summary>
    /// get/set - First name of user.
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// get/set - Last name of user.
    /// </summary>
    public string LastName { get; set; } = "";


    /// <summary>
    /// get/set - Whether the user is subscribed to the report.
    /// </summary>
    public bool IsSubscribed { get; set; }

    /// <summary>
    /// get/set - Which distribution format the user wants to receive.
    /// </summary>
    public Entities.ReportDistributionFormat? Format { get; set; }

    /// <summary>
    /// get/set - How the email will be sent to the subscriber.
    /// </summary>
    public Entities.EmailSentTo? SendTo { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of an UserProductModel.
    /// </summary>
    public UserProductModel() { }

    /// <summary>
    /// Creates a new instance of an UserProductModel, initializes with specified parameter.
    /// </summary>
    /// <param name="product"></param>
    public UserProductModel(Entities.UserProduct entity) : base(entity)
    {
        if (entity.Product == null) throw new ArgumentNullException(nameof(entity));
        if (entity.User == null) throw new ArgumentNullException(nameof(entity));

        this.UserId = entity.UserId;
        this.ProductId = entity.ProductId;
        this.Product = new ProductModel(entity.Product);
        this.Status = entity.Status;

        this.Username = entity.User.Username;
        this.Email = entity.User.Email;
        this.PreferredEmail = entity.User.PreferredEmail;
        this.DisplayName = entity.User.DisplayName;
        this.FirstName = entity.User.FirstName;
        this.LastName = entity.User.LastName;

        if (entity.Product.ProductType == Entities.ProductType.Report)
        {
            var subscription = entity.User.ReportSubscriptionsManyToMany
                .FirstOrDefault(s => s.UserId == entity.UserId &&
                    s.ReportId == entity.Product!.TargetProductId);
            this.IsSubscribed = subscription?.IsSubscribed ?? false;
            this.Format = subscription?.Format;
            this.SendTo = subscription?.SendTo;
        }
        else if (entity.Product.ProductType == Entities.ProductType.Notification)
        {
            var subscription = entity.User.NotificationSubscriptionsManyToMany
                .FirstOrDefault(s => s.UserId == entity.UserId &&
                    s.NotificationId == entity.Product!.TargetProductId);
            this.IsSubscribed = subscription?.IsSubscribed ?? false;
        }
        else if (entity.Product.ProductType == Entities.ProductType.EveningOverview)
        {
            var subscription = entity.User.AVOverviewSubscriptionsManyToMany
                .FirstOrDefault(s => s.UserId == entity.UserId &&
                    (int)s.TemplateType == entity.Product!.TargetProductId);
            this.IsSubscribed = subscription?.IsSubscribed ?? false;
            this.SendTo = subscription?.SendTo;
        }
    }
    #endregion
}