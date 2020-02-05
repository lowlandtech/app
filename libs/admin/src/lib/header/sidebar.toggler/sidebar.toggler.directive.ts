import { Directive, HostListener, HostBinding, ElementRef } from '@angular/core';

/**
* Allows the sidebar to be toggled via click.
*/
@Directive({
  selector: '[scxSidebarToggler]'
})
export class SidebarTogglerDirective {
  @HostBinding('class.d-md-down-none.mr-auto') class1 = true;
  @HostBinding('class.mr-auto') class2 = true;

  constructor(private elementRef:ElementRef){
    this.elementRef.nativeElement.innerHTML ='<span class="navbar-toggler-icon"></span>';
  }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('sidebar-hidden');
  }
}
