import { Directive, HostListener, HostBinding, ElementRef } from '@angular/core';

@Directive({
  selector: '[scxMobileSidebarToggler]'
})
export class MobileSidebarTogglerDirective {
  @HostBinding('class.d-lg-none') class1 = true;

  constructor(private elementRef:ElementRef){
    this.elementRef.nativeElement.innerHTML ='<span class="navbar-toggler-icon"></span>';
  }

  // Check if element has class
  private hasClass(target: any, elementClassName: string) {
    return new RegExp('(\\s|^)' + elementClassName + '(\\s|$)').test(target.className);
  }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('sidebar-mobile-show');
  }
}
