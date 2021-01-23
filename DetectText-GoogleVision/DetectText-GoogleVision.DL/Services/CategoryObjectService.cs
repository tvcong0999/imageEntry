using DetectText_GoogleVision.DL.IService;
using DetectText_GoogleVision.DL.Models;
using DetectText_GoogleVision.Service.Models.ModelForGoogleAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DetectText_GoogleVision.DL.Services
{
    public class CategoryObjectService : ICategoryObjectService<ResponseGoogleVision.TextAnnotation>
    {
        private readonly IGetValueService _getValueService;
        public CategoryObjectService(IGetValueService getValueService)
        {
            _getValueService = getValueService;
        }

        public List<ResponseGoogleVision.TextAnnotation> GetTextAnnotation(string HtmlResult)
        {
            var textAnnotationObject = JsonConvert.DeserializeObject<ResponseGoogleVision.Root>(HtmlResult);
            List<ResponseGoogleVision.TextAnnotation> content = textAnnotationObject.responses[0].textAnnotations;
            return content;
        }


        public List<ResponseGoogleVision.TextAnnotation> GetItemWithTaxCode(string HtmlResult)
        {
            var result = GetTextAnnotation(HtmlResult);
            List<string> valueResult = _getValueService.ListTaxCode(HtmlResult);
            List<ResponseGoogleVision.TextAnnotation> listTextAnnotations = new List<ResponseGoogleVision.TextAnnotation>();
            foreach (var item in result)
            {
                if (item != null)
                {
                    foreach (var value in valueResult)
                    {
                        if (item.description.Equals(value))
                        {
                            listTextAnnotations.Add(item);
                        }
                    }
                }
            }
            return listTextAnnotations.Distinct().ToList();
        }

        public List<ResponseGoogleVision.TextAnnotation> GetItemWithTicketNumber(string HtmlResult)
        {
            var result = GetTextAnnotation(HtmlResult);
            List<string> valueResult = _getValueService.ListTicketNumber(HtmlResult);
            List<ResponseGoogleVision.TextAnnotation> listTextAnnotations = new List<ResponseGoogleVision.TextAnnotation>();
            foreach (var item in result)
            {
                if (item != null)
                {
                    foreach (var value in valueResult)
                    {
                        if (item.description.Equals(value))
                        {
                            listTextAnnotations.Add(item);
                        }
                    }
                }
            }
            return listTextAnnotations.Distinct().ToList();
        }

        public List<ResponseGoogleVision.TextAnnotation> GetItemWithPrice(string HtmlResult)
        {
            var result = GetTextAnnotation(HtmlResult);
            List<string> valueResult = _getValueService.ListPrice(HtmlResult);
            List<ResponseGoogleVision.TextAnnotation> listTextAnnotations = new List<ResponseGoogleVision.TextAnnotation>();
            foreach (var item in result)
            {
                foreach (var value in valueResult)
                {
                    if (item.description.Equals(value))
                    {
                        listTextAnnotations.Add(item);
                    }
                }
            }
            return listTextAnnotations.Distinct().ToList();
        }


        //public List<DistanceOfItems> DistancePriceAndTaxCode(string HtmlResult)
        //{
        //    List<ResponseGoogleVision.TextAnnotation> listTaxCode = GetItemWithTaxCode(HtmlResult);
        //    List<ResponseGoogleVision.TextAnnotation> listPrice = GetItemWithPrice(HtmlResult);
        //    List<DistanceOfItems> listDistance = new List<DistanceOfItems>();
        //    foreach (var taxCode in listTaxCode)
        //    {
        //        int xTaxCode = taxCode.boundingPoly.vertices[0].x;
        //        int yTaxCode = taxCode.boundingPoly.vertices[0].y;
        //        foreach (var price in listPrice)
        //        {
        //            int xPrice = price.boundingPoly.vertices[0].x;
        //            int yPrice = price.boundingPoly.vertices[0].y;
        //            double distance = Math.Pow(xPrice - xTaxCode, 2) + Math.Pow(yPrice - yTaxCode, 2);
        //            DistanceOfItems item = new DistanceOfItems();
        //            item.TaxCode = taxCode.description;
        //            item.Price = price.description;
        //            item.Distance = distance;
        //            listDistance.Add(item);
        //        }
        //    }
        //    return listDistance.Distinct().GroupBy(p => p.TaxCode,
        //                   (key, g) => new DistanceOfItems { TaxCode = key, Price = g.FirstOrDefault(x => x.Distance == g.Min(x => x.Distance))?.Price, Distance = g.Min(x => x.Distance) }).ToList();
        //}

        public List<ListItemsModel> GetAll(string HtmlResult)
        {
            List<ResponseGoogleVision.TextAnnotation> listTaxCode = GetItemWithTaxCode(HtmlResult);
            List<ResponseGoogleVision.TextAnnotation> listTicketNumber = GetItemWithTicketNumber(HtmlResult);
            List<ResponseGoogleVision.TextAnnotation> listPrice = GetItemWithPrice(HtmlResult);
            List<ListItemsModel> listItems = new List<ListItemsModel>();
            ListItemsModel item = new ListItemsModel();
            if (listTaxCode.Count == 0)
                item.TaxCode = "";
            else
                item.TaxCode = listTaxCode[0].description;
            if (listTicketNumber.Count == 0)
                item.TicketNumber = "";
            else
                item.TicketNumber = listTicketNumber[0].description;
            if (listPrice.Count == 0)
                item.Price = "";
            else
                item.Price = listPrice[listPrice.Count-1].description;
            listItems.Add(item);

            return listItems;
        }
    }
    //    public List<DistanceOfItems> DistanceTicketNumberAndTaxCode(string HtmlResult)
    //    {
    //        List<ResponseGoogleVision.TextAnnotation> listTaxCode = GetItemWithTaxCode(HtmlResult);
    //        List<ResponseGoogleVision.TextAnnotation> listTicketNumber = GetItemWithTicketNumber(HtmlResult);
    //        List<DistanceOfItems> listDistance = new List<DistanceOfItems>();
    //        foreach (var taxCode in listTaxCode)
    //        {
    //            int xTaxCode = taxCode.boundingPoly.vertices[0].x;
    //            int yTaxCode = taxCode.boundingPoly.vertices[0].y;
    //            foreach (var ticketNumber in listTicketNumber)
    //            {
    //                int xTicketNumber = ticketNumber.boundingPoly.vertices[0].x;
    //                int yTicketNumber = ticketNumber.boundingPoly.vertices[0].y;
    //                double distance = Math.Pow(xTicketNumber - xTaxCode, 2) + Math.Pow(yTicketNumber - yTaxCode, 2);
    //                DistanceOfItems item = new DistanceOfItems();
    //                item.TaxCode = taxCode.description;
    //                item.TicketNumber = ticketNumber.description;
    //                item.Distance = distance;
    //                listDistance.Add(item);
    //            }
    //        }
    //        return listDistance.Distinct().GroupBy(p => p.TaxCode,
    //                      (key, g) => new DistanceOfItems { TaxCode = key, TicketNumber = g.FirstOrDefault(x => x.Distance == g.Min(x => x.Distance))?.TicketNumber, Distance = g.Min(x => x.Distance) }).ToList();
    //    }

    //    public List<ListItems> MergeTwoObject(string HtmlResult)
    //    {
    //        List<DistanceOfItems> distancePriceAndTaxCode = DistancePriceAndTaxCode(HtmlResult);
    //        List<DistanceOfItems> distanceTicketNumberAndTaxCode = DistanceTicketNumberAndTaxCode(HtmlResult);
    //        List<ListItems> listItems = new List<ListItems>();         
    //        var query = from pt in distancePriceAndTaxCode
    //                    join tt in distanceTicketNumberAndTaxCode on pt.TaxCode equals tt.TaxCode
    //                    select new ListItems
    //                    {
    //                        TaxCode = pt.TaxCode,
    //                        Price = pt.Price,
    //                        TicketNumber = tt.TicketNumber,
    //                    };
    //        foreach (var q in query)
    //        {
    //            ListItems l = new ListItems();
    //            l = q;
    //            listItems.Add(l);
    //        }
    //        return listItems;
    //    }
    //}
}
