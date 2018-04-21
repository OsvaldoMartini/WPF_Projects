using MVC_WPF_Search.Model;

namespace MVC_WPF_Search.Controllers
{
    /// <summary>
    /// Controller for the Product details
    /// </summary>
    public class ProductDetailsController : BaseController
    {

        #region Properties
        Product currentProduct;

        /// <summary>
        /// Gets or sets the current product
        /// </summary>
        public Product CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                currentProduct = value;
                OnPropertyChanged("CurrentProduct");
            }
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductDetailsController()
        {
            //register to the mediator for the SelectProduct message
            Mediator.Register(this, new[]
            {
                Messages.SelectProduct
            });
        }

        /// <summary>
        /// Notification from the Mediator
        /// </summary>
        /// <param name="message">The message type</param>
        /// <param name="args">Arguments for the message</param>
        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                //change the CurrentProduct to be the newly selected product
                case Messages.SelectProduct:
                    CurrentProduct = (Product)args;
                    break;
            }
        }
    }
}
