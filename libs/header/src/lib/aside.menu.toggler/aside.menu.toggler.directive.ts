import { Directive, HostListener, ElementRef, HostBinding } from '@angular/core';

/**
* Allows the aside to be toggled via click.
*/
@Directive({
  selector: '[scxAsideMenuToggler]'
})
export class AsideMenuTogglerDirective {
  @HostBinding('class.d-md-down-none') class1 = true;

  constructor(private elementRef:ElementRef){
    this.elementRef.nativeElement.innerHTML ='<span class="navbar-toggler-icon"></span>';
  }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('aside-menu-hidden');
  }
}
