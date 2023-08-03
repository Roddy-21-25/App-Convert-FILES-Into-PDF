namespace PresentationLayer___PizzaDay.ControllerOrder
{
    public class SwaggerControllerOrderAttribute : Attribute
    {
        public uint Order { get; private set; }
        public SwaggerControllerOrderAttribute(uint order)
        {
            Order = order;
        }
    }
}
