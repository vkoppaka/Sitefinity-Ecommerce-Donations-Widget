using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Web.UI.Fields;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Web.UI.HtmlControls;

namespace Telerik.Sitefinity.Samples.Ecommerce.Donations
{
    [ControlDesigner(typeof(DonationsWidgetDesigner))]
    public class DonationsWidget : SimpleScriptView, IOrdersControl
    {
        protected override string LayoutTemplateName
        {
            get
            {
                return DonationsWidget.layoutTemplateName;
            }
        }

        protected override Type ResourcesAssemblyInfo
        {
            get
            {
                return typeof(DonationsWidget);
            }
        }

        protected OrdersManager OrdersManager
        {
            get
            {
                if (this.ordersManager == null)
                    this.ordersManager = OrdersManager.GetManager();
                return this.ordersManager;
            }
        }

        protected CatalogManager CatalogManager
        {
            get
            {
                if (this.catalogManager == null)
                    this.catalogManager = CatalogManager.GetManager();
                return this.catalogManager;
            }
        }


        public Guid ProductId
        {
            get
            {
                return this.productId;
            }
            set
            {
                this.productId = value;
            }
        }

        public string ProductName
        {
            get { return Product == null ? null : Product.Title; }
            set { /* required for WCF */}
        }

        public Product Product
        {
            get
            {
                if (this.product == null)
                {
                    this.product = this.CatalogManager.GetProduct(this.ProductId);
                }
                return this.product;
            }
        }

        public Guid CheckoutPageId
        {
            get
            {
                return this.checkoutPageId;
            }
            set
            {
                this.checkoutPageId = value;
            }
        }

        protected virtual ChoiceField DonationAmountDropDown
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("donationAmountDropDown", true);
            }
        }


        protected virtual IButtonControl AddToCartButton
        {
            get
            {
                return this.Container.GetControl<IButtonControl>("addToCartButton", true);
            }
        }

        protected virtual TextField OtherAmountControl
        {
            get
            {
                return this.Container.GetControl<TextField>("otherAmount", true);
            }
        }

        protected virtual HtmlGenericControl WidgetStatus
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("widgetStatus", false);
            }
        }

        protected virtual HtmlGenericControl DonationsWidgetWrapper
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("donationsWidgetWrapper", false);
            }
        }


        protected virtual Label WidgetStatusMessage
        {
            get
            {
                return this.Container.GetControl<Label>("widgetStatusMessage", true);
            }
        }

        protected virtual Message AddedToCartMessage
        {
            get
            {
                return this.Container.GetControl<Message>("addedToCartMessage", true);
            }
        }


        protected override void InitializeControls(GenericContainer container)
        {
            if (!this.IsDesignMode())
            {
                if (this.ProductId == Guid.Empty)
                {
                    this.WidgetStatus.Visible = true;
                    this.WidgetStatusMessage.Text = "This widget is not configured, please choose a product by editing the widget.";

                    this.DonationsWidgetWrapper.Visible = false;
                }
                else
                {
                    this.WidgetStatus.Visible = false;
                    this.WidgetStatusMessage.Text = "";

                    this.DonationsWidgetWrapper.Visible = true;

                    AddToCartButton.Command += new CommandEventHandler(AddToCartButton_Command);
                }
            }
        }

        protected void AddToCartButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int quantity = 1;

                IShoppingCartAdder shoppingCartAdder = new ShoppingCartAdder();
                string defaultCurrencyName = Config.Get<EcommerceConfig>().DefaultCurrency;


                OptionsDetails optionsDetails = new OptionsDetails();

                decimal price = 0;
                if (!decimal.TryParse(DonationAmountDropDown.Value.ToString(), out price))
                {
                    price = Convert.ToDecimal(OtherAmountControl.Value);
                }
                this.Product.Price = price;
                shoppingCartAdder.AddItemToShoppingCart(this, this.OrdersManager, this.Product, optionsDetails, quantity, defaultCurrencyName);
                this.AddedToCartMessage.ShowPositiveMessage("Donation added to cart");
            }
            catch (Exception ex)
            {
                this.AddedToCartMessage.ShowNegativeMessage("Failed to add donation to cart, try again");
            }

        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>();
            var descriptor = new ScriptControlDescriptor(typeof(DonationsWidget).FullName, this.ClientID);

            descriptor.AddComponentProperty("donationAmountDropDown", this.DonationAmountDropDown.ClientID);
            descriptor.AddComponentProperty("otherAmount", this.OtherAmountControl.ClientID);

            descriptors.Add(descriptor);

            return new[] { descriptor };
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>()
            {
                new ScriptReference(DonationsWidget.donationsWidgetScript, typeof(DonationsWidget).Assembly.FullName),
            };
            return scripts;
        }

        private static readonly string layoutTemplateName = "Telerik.Sitefinity.Samples.Ecommerce.Donations.Resources.DonationsWidget.ascx";
        private static readonly string donationsWidgetScript = "Telerik.Sitefinity.Samples.Ecommerce.Donations.Resources.DonationsWidget.js";

        private CatalogManager catalogManager;
        private OrdersManager ordersManager;
        private Product product;
        private Guid productId;
        private Guid checkoutPageId;
    }

}
