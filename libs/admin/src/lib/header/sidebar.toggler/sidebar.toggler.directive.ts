import { Directive, HostListener, HostBinding, ElementRef, OnInit, Inject } from '@angular/core';
import { AdminStateFacade } from '../../+state';
import { DOCUMENT } from '@angular/common';
import { hasClass } from '../../tools';

/**
* Allows the sidebar to be toggled via click.
*/
@Directive({
  selector: '[scxSidebarToggler]'
})
export class SidebarTogglerDirective implements OnInit {
  @HostBinding('class.d-md-down-none.mr-auto') class1 = true;
  @HostBinding('class.mr-auto') class2 = true;
  private className = 'sidebar-hidden';
  private element: HTMLBodyElement;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private facade: AdminStateFacade,
    private elementRef:ElementRef ) {
    this.elementRef.nativeElement.innerHTML ='<span class="navbar-toggler-icon"></span>';
    this.element = this.document.querySelector('body');
  }

  public ngOnInit() {
    this.facade.sidebar$.subscribe(sidebar => {
      if(sidebar.state === 'HIDDEN') {
        this.element.classList.add(this.className);
      } else {
        this.element.classList.remove(this.className);
      }
    });
  }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    this.facade.hide(!hasClass(this.element, this.className));
   }
}
