import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: 'img[appImgFallback]'
})
export class ImgFallbackDirective {

  @Input() appImgFallback:string="";

  constructor(private eRef:ElementRef) { 

  }

  @HostListener('error')
  LoadFallbackOnError()
  {
    const element:HTMLImageElement = <HTMLImageElement> this.eRef.nativeElement;
    element.src = this.appImgFallback || '/assets/images/NoImage.png';
  }

}
