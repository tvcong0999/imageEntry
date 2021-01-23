
using DetectText_GoogleVision.DL.Models;
using System.Collections.Generic;

namespace DetectText_GoogleVision.DL.IService
{
    public interface ICategoryObjectService<T>
    {
        List<T> GetTextAnnotation(string HtmlResult);
        List<T> GetItemWithTaxCode(string HtmlResult);
        List<T> GetItemWithTicketNumber(string HtmlResult);
        List<T> GetItemWithPrice(string HtmlResult);
        //List<DistanceOfItems> DistancePriceAndTaxCode(string HtmlResult);
        //List<DistanceOfItems> DistanceTicketNumberAndTaxCode(string HtmlResult);
        List<ListItemsModel> GetAll(string HtmlResult);
        //List<ListItems> MergeTwoObject(string HtmlResult);
    }
}
