import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ListSurchageService } from 'src/app/shared/service/list-surchage.service';
import { SurchargeDetailService } from 'src/app/shared/service/surcharge-detail.service';
@Component({
  selector: 'app-surcharge-detail-moredetail',
  templateUrl: './surcharge-detail-moredetail.component.html',
  styleUrls: ['./surcharge-detail-moredetail.component.css'],
})
export class SurchargeDetailMoredetailComponent implements OnInit, AfterViewChecked {
  public srcimg = "";
  public currentSurcharge;
  constructor(private elementRef: ElementRef, public listSurchageService: ListSurchageService, public surchargeDetailService: SurchargeDetailService) { }
  myThumbnail="https://wittlock.github.io/ngx-image-zoom/assets/fullres.jpg";
  myFullresImage="https://wittlock.github.io/ngx-image-zoom/assets/fullres.jpg";
  ngOnInit(): void {
    this.getIdImage();
  }
  ngAfterViewChecked():void{
    $('.ngxImageZoomContainer').width($('.ngxImageZoomContainer img').width());
    $('.ngxImageZoomContainer').attr('style', 'width: '+$('.ngxImageZoomContainer img').width()+ 'px !important');
  }
  async getIdImage(){
    for( let surchargeInfor of this.surchargeDetailService.surCharge)
    {
      if(surchargeInfor.id === parseInt(this.listSurchageService.idImage.substring(3)))
      {
        this.currentSurcharge = await surchargeInfor;
        this.srcimg = await URL.createObjectURL(surchargeInfor.image);
        await $('.content .nameimg').html(this.currentSurcharge.image.name)

        
        await $('.imgdetail img').attr("src",this.srcimg);
        await this.getInforSurcharge(surchargeInfor);
        break;
      }
    }
    
  }
  getInforSurcharge(sur){
    $('.col-infor4').html(sur.name);
    $('.col-infor5').html(sur.inforSurcharge.ticketNumber);
    $('.col-infor6').html(sur.inforSurcharge.taxCode);
    $('.col-infor7').html(sur.inforSurcharge.price);
    $('.col-infor8').html(sur.currency);
    $('.col-infor9').html(sur.includeVAT);
    $('.col-infor10').html(sur.VATrate);
  }
  async prevImage(){
    let prev = await this.surchargeDetailService.surCharge[this.surchargeDetailService.surCharge.indexOf(this.currentSurcharge)-1];
    if(prev!=undefined){
    this.srcimg = await URL.createObjectURL(prev.image);
    await $('.content .nameimg').html(prev.image.name)
    await $('.imgdetail img').attr("src",this.srcimg); 
    this.currentSurcharge = prev;
    this.getInforSurcharge(prev);
   }
  }
  async nextImage(){
    let next = await this.surchargeDetailService.surCharge[this.surchargeDetailService.surCharge.indexOf(this.currentSurcharge)+1];
    if(next != undefined){
    this.srcimg = await URL.createObjectURL(next.image);
    await $('.content .nameimg').html(next.image.name)
    await $('.imgdetail img').attr("src",this.srcimg); 
    this.currentSurcharge = next;
    this.getInforSurcharge(next);
  }
}
}
