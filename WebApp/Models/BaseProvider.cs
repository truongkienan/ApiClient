namespace WebApp.Models
{
    public abstract class BaseProvider : IDisposable
    {
        CallCenterContext context;
        protected CallCenterContext Context
        {
            get 
            {
                if (context is null)
                {
                    context = new CallCenterContext();
                }
                return context; 
            }
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
