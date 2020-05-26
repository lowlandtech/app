import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[scxAsideListGroup]'
})
export class AsideListGroupDirective {

  constructor(public viewContainerRef: ViewContainerRef) { }

}
