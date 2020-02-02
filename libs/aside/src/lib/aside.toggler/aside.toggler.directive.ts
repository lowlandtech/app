import { Directive, HostListener, ElementRef, HostBinding, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

/**
* Allows the aside to be toggled via click.
*/
@Directive({
  selector: '[scxAsideToggler]'
})
export class AsideTogglerDirective {
  @HostBinding('class.d-md-down-none') class1 = true;

  constructor(
    private elementRef:ElementRef,
    @Inject(DOCUMENT) private document: Document){
    this.elementRef.nativeElement.innerHTML ='<span class="navbar-toggler-icon"></span>';
  }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    this.document.querySelector('body').classList.toggle('aside-menu-hidden');
  }
}

