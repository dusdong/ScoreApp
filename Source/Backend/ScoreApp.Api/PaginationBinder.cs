using ScoreApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace ScoreApp.Api
{
    public class PaginationBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Pagination))
                return false;

            var page = bindingContext.ValueProvider.GetValue("Page");
            var itemsPerPage = bindingContext.ValueProvider.GetValue("ItemsPerPage");
            if (page == null || itemsPerPage == null)
                bindingContext.Model = Pagination.Default;
            else
                bindingContext.Model = Pagination.Create(Convert.ToInt32(page.RawValue), Convert.ToInt32(itemsPerPage.RawValue));

            return true;
        }
    }
}